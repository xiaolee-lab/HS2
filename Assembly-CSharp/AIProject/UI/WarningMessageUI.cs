using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AIProject.Definitions;
using AIProject.UI.Popup;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000FE3 RID: 4067
	[RequireComponent(typeof(CanvasGroup))]
	public class WarningMessageUI : MonoBehaviour
	{
		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x0600883B RID: 34875 RVA: 0x0038C586 File Offset: 0x0038A986
		// (set) Token: 0x0600883C RID: 34876 RVA: 0x0038C5AE File Offset: 0x0038A9AE
		public float CanvasAlpha
		{
			get
			{
				return (!(this.canvasGroup != null)) ? 0f : this.canvasGroup.alpha;
			}
			set
			{
				if (this.canvasGroup != null)
				{
					this.canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x0600883D RID: 34877 RVA: 0x0038C5D0 File Offset: 0x0038A9D0
		private GameObject Prefab
		{
			get
			{
				if (this.prefab_ != null)
				{
					return this.prefab_;
				}
				DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
				this.prefab_ = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefab, "WarningMessageElement", false, definePack.ABManifests.Default);
				if (this.prefab_ != null)
				{
					this.PrefabAssetBundle = new UnityEx.ValueTuple<string, string>(definePack.ABPaths.MapScenePrefab, definePack.ABManifests.Default);
				}
				return this.prefab_;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x0600883E RID: 34878 RVA: 0x0038C65F File Offset: 0x0038AA5F
		// (set) Token: 0x0600883F RID: 34879 RVA: 0x0038C668 File Offset: 0x0038AA68
		public bool Visibled
		{
			get
			{
				return this.visibled;
			}
			set
			{
				if (this.visibled == value)
				{
					return;
				}
				this.visibled = value;
				float _startAlpha = this.CanvasAlpha;
				float _endAlpha = (!value) ? 0f : 1f;
				if (this.visibleFadeDisposable != null)
				{
					this.visibleFadeDisposable.Dispose();
				}
				this.visibleFadeDisposable = ObservableEasing.EaseOutQuint(0.3f, true).TakeUntilDestroy(base.gameObject).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					this.CanvasAlpha = Mathf.Lerp(_startAlpha, _endAlpha, x.Value);
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06008840 RID: 34880 RVA: 0x0038C725 File Offset: 0x0038AB25
		// (set) Token: 0x06008841 RID: 34881 RVA: 0x0038C745 File Offset: 0x0038AB45
		public bool isFadeInForOutWait
		{
			get
			{
				return !(this.currentElement == null) && this.currentElement.isFadeInForOutWait;
			}
			set
			{
				this.isReserveFadeInForOutWait = value;
				if (this.currentElement != null)
				{
					this.currentElement.isFadeInForOutWait = value;
				}
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06008842 RID: 34882 RVA: 0x0038C76B File Offset: 0x0038AB6B
		// (set) Token: 0x06008843 RID: 34883 RVA: 0x0038C773 File Offset: 0x0038AB73
		private bool isReserveFadeInForOutWait { get; set; }

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06008844 RID: 34884 RVA: 0x0038C77C File Offset: 0x0038AB7C
		// (set) Token: 0x06008845 RID: 34885 RVA: 0x0038C784 File Offset: 0x0038AB84
		private WarningMessageElement currentElement { get; set; }

		// Token: 0x06008846 RID: 34886 RVA: 0x0038C78D File Offset: 0x0038AB8D
		private void Awake()
		{
			this.openElements = ListPool<WarningMessageElement>.Get();
			this.closeElements = ListPool<WarningMessageElement>.Get();
			this.messageStock = ListPool<UnityEx.ValueTuple<string, int, Transform, System.Action>>.Get();
			this.StartNextMessageCheker();
		}

		// Token: 0x06008847 RID: 34887 RVA: 0x0038C7B8 File Offset: 0x0038ABB8
		private void OnDestroy()
		{
			if (this.nextMessageCheckerDisposable != null)
			{
				this.nextMessageCheckerDisposable.Dispose();
			}
			ListPool<WarningMessageElement>.Release(this.openElements);
			ListPool<WarningMessageElement>.Release(this.closeElements);
			ListPool<UnityEx.ValueTuple<string, int, Transform, System.Action>>.Release(this.messageStock);
			this.openElements = null;
			this.closeElements = null;
			this.messageStock = null;
			this.currentElement = null;
		}

		// Token: 0x06008848 RID: 34888 RVA: 0x0038C81A File Offset: 0x0038AC1A
		public void ClearStockMessage()
		{
			if (this.messageStock != null)
			{
				this.messageStock.Clear();
			}
		}

		// Token: 0x06008849 RID: 34889 RVA: 0x0038C834 File Offset: 0x0038AC34
		private void StartNextMessageCheker()
		{
			if (this.nextMessageCheckerDisposable != null)
			{
				return;
			}
			IEnumerator _coroutine = this.NextMessageChecker();
			this.nextMessageCheckerDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>();
		}

		// Token: 0x0600884A RID: 34890 RVA: 0x0038C884 File Offset: 0x0038AC84
		private IEnumerator NextMessageChecker()
		{
			for (;;)
			{
				while (this.messageStock.IsNullOrEmpty<UnityEx.ValueTuple<string, int, Transform, System.Action>>() || !base.isActiveAndEnabled)
				{
					yield return null;
				}
				while (this.existsNotPupup && !this.openElements.IsNullOrEmpty<WarningMessageElement>())
				{
					yield return null;
				}
				if (!this.messageStock.IsNullOrEmpty<UnityEx.ValueTuple<string, int, Transform, System.Action>>())
				{
					this.PopupWarning();
					if (!this.existsNotPupup)
					{
						IObservable<long> _timer = Observable.Timer(TimeSpan.FromSeconds((double)this.nextPopupTime), Scheduler.MainThreadIgnoreTimeScale).TakeUntilDestroy(this);
						yield return _timer.ToYieldInstruction<long>();
					}
				}
			}
			yield break;
		}

		// Token: 0x0600884B RID: 34891 RVA: 0x0038C89F File Offset: 0x0038AC9F
		private Color GetColor(int _id)
		{
			if (_id == 1)
			{
				return this.yellowColor;
			}
			if (_id != 2)
			{
				return this.whiteColor;
			}
			return this.redColor;
		}

		// Token: 0x0600884C RID: 34892 RVA: 0x0038C8C8 File Offset: 0x0038ACC8
		private WarningMessageElement GetElement()
		{
			WarningMessageElement warningMessageElement = (!this.closeElements.IsNullOrEmpty<WarningMessageElement>()) ? this.closeElements.Pop<WarningMessageElement>() : null;
			if (warningMessageElement == null)
			{
				GameObject prefab = this.Prefab;
				if (prefab == null)
				{
					return null;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab, base.transform, false);
				warningMessageElement = ((gameObject != null) ? gameObject.GetComponent<WarningMessageElement>() : null);
				if (warningMessageElement == null)
				{
					return null;
				}
				warningMessageElement.gameObject.name = string.Format("{0}_{1}", this.Prefab.name, this.elmCount++);
				warningMessageElement.Root = this;
			}
			if (warningMessageElement.gameObject.activeSelf)
			{
				warningMessageElement.gameObject.SetActive(false);
			}
			warningMessageElement.EndAction = new Action<WarningMessageElement>(this.EndAction);
			return warningMessageElement;
		}

		// Token: 0x0600884D RID: 34893 RVA: 0x0038C9B6 File Offset: 0x0038ADB6
		private void EndAction(WarningMessageElement _elm)
		{
			_elm.isFadeInForOutWait = false;
			_elm.EndAction = null;
			this.ReturnElement(_elm);
		}

		// Token: 0x0600884E RID: 34894 RVA: 0x0038C9D0 File Offset: 0x0038ADD0
		private void ReturnElement(WarningMessageElement _elm)
		{
			this.currentElement = null;
			if (_elm.gameObject.activeSelf)
			{
				_elm.gameObject.SetActive(false);
			}
			if (this.openElements.Contains(_elm))
			{
				this.openElements.Remove(_elm);
			}
			if (!this.closeElements.Contains(_elm))
			{
				this.closeElements.Add(_elm);
			}
		}

		// Token: 0x0600884F RID: 34895 RVA: 0x0038CA3C File Offset: 0x0038AE3C
		public void ShowMessage(Popup.Warning.Type _type, int _colorID, int _posID = 0, System.Action _onComplete = null)
		{
			ReadOnlyDictionary<int, string[]> warningTable = Singleton<Manager.Resources>.Instance.PopupInfo.WarningTable;
			string[] array;
			if (!warningTable.TryGetValue((int)_type, out array))
			{
			}
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			this.ShowMessage(((array != null) ? array.GetElement(index) : null) ?? string.Empty, _colorID, _posID, _onComplete);
		}

		// Token: 0x06008850 RID: 34896 RVA: 0x0038CAAA File Offset: 0x0038AEAA
		public void ShowMessage(Popup.Warning.Type _type)
		{
			this.ShowMessage(_type, 2, 0, null);
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x0038CAB8 File Offset: 0x0038AEB8
		public void ShowMessage(string _message, int _colorID, int _posID, System.Action _onComplete)
		{
			if (_message.IsNullOrEmpty())
			{
				return;
			}
			foreach (UnityEx.ValueTuple<string, int, Transform, System.Action> valueTuple in this.messageStock)
			{
				if (valueTuple.Item1 == _message)
				{
					return;
				}
			}
			foreach (WarningMessageElement warningMessageElement in this.openElements)
			{
				if (warningMessageElement.Text == _message)
				{
					if (warningMessageElement.PlayingDisplay)
					{
						warningMessageElement.StartDisplay();
					}
					else if (warningMessageElement.PlayingFadeOut && this.openElements.Count == 1)
					{
						warningMessageElement.StartFadeIn();
					}
					return;
				}
			}
			Transform element = this.roots.GetElement(_posID);
			this.AddMessage(_message, _colorID, element, _onComplete);
		}

		// Token: 0x06008852 RID: 34898 RVA: 0x0038CBDC File Offset: 0x0038AFDC
		private void AddMessage(string _message, int _colorID, Transform _root, System.Action onComplete)
		{
			this.messageStock.Add(new UnityEx.ValueTuple<string, int, Transform, System.Action>(_message, _colorID, _root, onComplete));
		}

		// Token: 0x06008853 RID: 34899 RVA: 0x0038CBF4 File Offset: 0x0038AFF4
		private void PopupWarning()
		{
			UnityEx.ValueTuple<string, int, Transform, System.Action> valueTuple = this.messageStock.Pop<UnityEx.ValueTuple<string, int, Transform, System.Action>>();
			WarningMessageElement element = this.GetElement();
			this.currentElement = element;
			if (element == null)
			{
				return;
			}
			element.ClosedAction = valueTuple.Item4;
			element.isFadeInForOutWait = this.isReserveFadeInForOutWait;
			element.SetFadeInfo(this.fadeInInfo, this.displayInfo, this.fadeOutInfo);
			element.Text = valueTuple.Item1;
			element.Color = this.GetColor(valueTuple.Item2);
			if (valueTuple.Item3 != null)
			{
				element.transform.localPosition = valueTuple.Item3.localPosition;
			}
			if (!element.gameObject.activeSelf)
			{
				element.gameObject.SetActive(true);
			}
			element.transform.SetAsLastSibling();
			element.StartFadeIn();
			int item = valueTuple.Item2;
			if (item != 0)
			{
				if (item == 1 || item == 2)
				{
					this.PlaySE();
				}
			}
			foreach (WarningMessageElement warningMessageElement in this.openElements)
			{
				if (!warningMessageElement.PlayingFadeOut)
				{
					warningMessageElement.StartFadeOut();
				}
			}
			this.openElements.Add(element);
		}

		// Token: 0x06008854 RID: 34900 RVA: 0x0038CD68 File Offset: 0x0038B168
		private void PlaySE()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack == null)
			{
				return;
			}
			soundPack.Play(SoundPack.SystemSE.Error);
		}

		// Token: 0x04006E6C RID: 28268
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006E6D RID: 28269
		[SerializeField]
		private bool existsNotPupup = true;

		// Token: 0x04006E6E RID: 28270
		[SerializeField]
		private float nextPopupTime = 2f;

		// Token: 0x04006E6F RID: 28271
		[SerializeField]
		private Transform[] roots = new Transform[0];

		// Token: 0x04006E70 RID: 28272
		[SerializeField]
		private FadeInfo fadeInInfo = new FadeInfo(0.5f, 0.8f, 0.5f);

		// Token: 0x04006E71 RID: 28273
		[SerializeField]
		private FadeInfo displayInfo = new FadeInfo(1f, 1f, 2.3f);

		// Token: 0x04006E72 RID: 28274
		[SerializeField]
		private FadeInfo fadeOutInfo = new FadeInfo(0.5f, 0.8f, 0.5f);

		// Token: 0x04006E73 RID: 28275
		[SerializeField]
		private Color whiteColor = new Color(0.92156863f, 0.88235295f, 0.84313726f, 1f);

		// Token: 0x04006E74 RID: 28276
		[SerializeField]
		private Color yellowColor = new Color(0.8f, 0.77254903f, 0.23137255f, 1f);

		// Token: 0x04006E75 RID: 28277
		[SerializeField]
		private Color redColor = new Color(0.87058824f, 0.27058825f, 0.16078432f, 1f);

		// Token: 0x04006E76 RID: 28278
		private UnityEx.ValueTuple<string, string> PrefabAssetBundle = default(UnityEx.ValueTuple<string, string>);

		// Token: 0x04006E77 RID: 28279
		private GameObject prefab_;

		// Token: 0x04006E78 RID: 28280
		private bool visibled = true;

		// Token: 0x04006E79 RID: 28281
		private IDisposable visibleFadeDisposable;

		// Token: 0x04006E7C RID: 28284
		private List<WarningMessageElement> openElements;

		// Token: 0x04006E7D RID: 28285
		private List<WarningMessageElement> closeElements;

		// Token: 0x04006E7E RID: 28286
		private List<UnityEx.ValueTuple<string, int, Transform, System.Action>> messageStock;

		// Token: 0x04006E7F RID: 28287
		private IDisposable nextMessageCheckerDisposable;

		// Token: 0x04006E80 RID: 28288
		private int elmCount;
	}
}

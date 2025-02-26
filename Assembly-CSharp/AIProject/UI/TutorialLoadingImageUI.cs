using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000FDE RID: 4062
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class TutorialLoadingImageUI : MenuUIBehaviour
	{
		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x0600878A RID: 34698 RVA: 0x00388461 File Offset: 0x00386861
		// (set) Token: 0x0600878B RID: 34699 RVA: 0x00388469 File Offset: 0x00386869
		public int PageIndex { get; set; }

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x0600878C RID: 34700 RVA: 0x00388472 File Offset: 0x00386872
		public MenuUIBehaviour[] MenuUIBehaviours
		{
			get
			{
				if (this._menuUIBehaviours == null)
				{
					this._menuUIBehaviours = new MenuUIBehaviour[]
					{
						this
					};
				}
				return this._menuUIBehaviours;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x0600878D RID: 34701 RVA: 0x00388495 File Offset: 0x00386895
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && Singleton<Manager.Input>.Instance.FocusLevel == this._focusLevel;
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x0600878E RID: 34702 RVA: 0x003884B7 File Offset: 0x003868B7
		// (set) Token: 0x0600878F RID: 34703 RVA: 0x003884C4 File Offset: 0x003868C4
		public override bool IsActiveControl
		{
			get
			{
				return this._isActive.Value;
			}
			set
			{
				if (this._isActive.Value == value)
				{
					return;
				}
				this._isActive.Value = value;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06008790 RID: 34704 RVA: 0x003884E4 File Offset: 0x003868E4
		// (set) Token: 0x06008791 RID: 34705 RVA: 0x0038850C File Offset: 0x0038690C
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			private set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x0038852C File Offset: 0x0038692C
		protected override void Awake()
		{
			base.Awake();
			if (this._canvasGroup == null)
			{
				this._canvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			this.Hide();
		}

		// Token: 0x06008793 RID: 34707 RVA: 0x0038857F File Offset: 0x0038697F
		private void Hide()
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			this.SetBlockRaycasts(false);
			this.SetInteractable(false);
			this.CanvasAlpha = 0f;
			base.EnabledInput = false;
		}

		// Token: 0x06008794 RID: 34708 RVA: 0x003885B4 File Offset: 0x003869B4
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			(from _ in this._closeButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoClose();
			});
			(from _ in this._rightButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.PageMove(1);
			});
			(from _ in this._leftButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.PageMove(-1);
			});
		}

		// Token: 0x06008795 RID: 34709 RVA: 0x003886D4 File Offset: 0x00386AD4
		protected override void OnAfterStart()
		{
			base.OnAfterStart();
			this.SetActive(base.gameObject, false);
			IEnumerator coroutine = this.LoadElementSprites();
			Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x00388724 File Offset: 0x00386B24
		private IEnumerator LoadElementSprites()
		{
			if (this._initialize)
			{
				yield break;
			}
			this._initialize = true;
			while (!Singleton<Game>.IsInstance())
			{
				yield return null;
			}
			while (Singleton<Game>.Instance.LoadingSpriteABList.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				yield return null;
			}
			foreach (KeyValuePair<int, AssetBundleInfo> pair in Singleton<Game>.Instance.LoadingSpriteABList)
			{
				AssetBundleInfo abInfo = pair.Value;
				Sprite sprite = LoadingPanel.LoadSpriteAsset(abInfo.assetbundle, abInfo.asset, abInfo.manifest);
				if (sprite != null)
				{
					this._loadingSpriteList.Add(sprite);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x00388740 File Offset: 0x00386B40
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x06008798 RID: 34712 RVA: 0x003887E5 File Offset: 0x00386BE5
		private void DoClose()
		{
			this.PlaySE(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06008799 RID: 34713 RVA: 0x003887F8 File Offset: 0x00386BF8
		private void PageMove(int move)
		{
			int num = Math.Sign(move);
			int num2 = this.PageIndex + move;
			Sprite sprite = this.GetSprite(ref num2);
			if (this.PageIndex == num2)
			{
				return;
			}
			if (this._currentElement != null)
			{
				this._currentElement.Close(this._pageFadeTime, this._pageMoveX * (float)num);
			}
			this._currentElement = this.GetElement();
			TutorialLoadingImageUIElement currentElement = this._currentElement;
			int num3 = num2;
			this.PageIndex = num3;
			currentElement.Index = num3;
			this._currentElement.ImageSprite = sprite;
			this._currentElement.Open(this._pageFadeTime, this._pageMoveX * (float)num);
			this.PlaySE(SoundPack.SystemSE.Page);
		}

		// Token: 0x0600879A RID: 34714 RVA: 0x003888A4 File Offset: 0x00386CA4
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(base.gameObject, true);
			this.SetBlockRaycasts(true);
			this.SetInteractable(false);
			this.SetupElement(this.PageIndex);
			Manager.Input input = Singleton<Manager.Input>.Instance;
			this._prevFocusLevel = input.FocusLevel;
			input.FocusLevel = this._prevFocusLevel + 1;
			base.SetFocusLevel(input.FocusLevel);
			this._prevMenuUIBehavioures = input.MenuElements;
			input.MenuElements = this.MenuUIBehaviours;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 1f;
			this.SetInteractable(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x0600879B RID: 34715 RVA: 0x003888C0 File Offset: 0x00386CC0
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this.SetInteractable(false);
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 0f;
			this.SetBlockRaycasts(false);
			Manager.Input input = Singleton<Manager.Input>.Instance;
			input.FocusLevel = this._prevFocusLevel;
			input.MenuElements = this._prevMenuUIBehavioures;
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x0600879C RID: 34716 RVA: 0x003888DC File Offset: 0x00386CDC
		private void SetupElement(int index)
		{
			if (this._currentElement != null)
			{
				this.ReturnElement(this._currentElement);
				this._currentElement = null;
			}
			TutorialLoadingImageUIElement tutorialLoadingImageUIElement = this._currentElement = this.GetElement();
			tutorialLoadingImageUIElement.ImageSprite = this.GetSprite(ref index);
			tutorialLoadingImageUIElement.Index = index;
			tutorialLoadingImageUIElement.CanvasAlpha = 1f;
			tutorialLoadingImageUIElement.transform.localPosition = Vector3.zero;
		}

		// Token: 0x0600879D RID: 34717 RVA: 0x00388950 File Offset: 0x00386D50
		private TutorialLoadingImageUIElement GetElement()
		{
			TutorialLoadingImageUIElement tutorialLoadingImageUIElement = this._elementPool.PopFront<TutorialLoadingImageUIElement>();
			if (tutorialLoadingImageUIElement == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._elementPrefab, this._elementRoot, false);
				tutorialLoadingImageUIElement = gameObject.GetComponent<TutorialLoadingImageUIElement>();
			}
			tutorialLoadingImageUIElement.CanvasAlpha = 0f;
			tutorialLoadingImageUIElement.transform.SetAsLastSibling();
			this.SetActive(tutorialLoadingImageUIElement, true);
			tutorialLoadingImageUIElement.OnCloseEvent = delegate(TutorialLoadingImageUIElement x)
			{
				this.ReturnElement(x);
			};
			return tutorialLoadingImageUIElement;
		}

		// Token: 0x0600879E RID: 34718 RVA: 0x003889C0 File Offset: 0x00386DC0
		private void ReturnElement(TutorialLoadingImageUIElement elm)
		{
			if (elm == null)
			{
				return;
			}
			if (!this._elementPool.Contains(elm))
			{
				this.SetActive(elm, false);
				this._elementPool.Add(elm);
			}
		}

		// Token: 0x0600879F RID: 34719 RVA: 0x003889F4 File Offset: 0x00386DF4
		private Sprite GetSprite(ref int index)
		{
			if (this._loadingSpriteList.IsNullOrEmpty<Sprite>())
			{
				index = -1;
				return null;
			}
			if (index < 0)
			{
				index = this._loadingSpriteList.Count - 1;
			}
			else if (this._loadingSpriteList.Count <= index)
			{
				index = 0;
			}
			return this._loadingSpriteList.GetElement(index);
		}

		// Token: 0x060087A0 RID: 34720 RVA: 0x00388A54 File Offset: 0x00386E54
		private void SetInteractable(bool active)
		{
			if (this._canvasGroup.interactable != active)
			{
				this._canvasGroup.interactable = active;
			}
		}

		// Token: 0x060087A1 RID: 34721 RVA: 0x00388A73 File Offset: 0x00386E73
		private void SetBlockRaycasts(bool active)
		{
			if (this._canvasGroup.blocksRaycasts != active)
			{
				this._canvasGroup.blocksRaycasts = active;
			}
		}

		// Token: 0x060087A2 RID: 34722 RVA: 0x00388A92 File Offset: 0x00386E92
		private void SetActive(GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x060087A3 RID: 34723 RVA: 0x00388AB4 File Offset: 0x00386EB4
		private void SetActive(Component com, bool active)
		{
			if (com == null || com.gameObject == null)
			{
				return;
			}
			if (com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}

		// Token: 0x060087A4 RID: 34724 RVA: 0x00388AF4 File Offset: 0x00386EF4
		private void PlaySE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04006E13 RID: 28179
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E14 RID: 28180
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006E15 RID: 28181
		[SerializeField]
		private Button _leftButton;

		// Token: 0x04006E16 RID: 28182
		[SerializeField]
		private Button _rightButton;

		// Token: 0x04006E17 RID: 28183
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006E18 RID: 28184
		[SerializeField]
		private Transform _elementRoot;

		// Token: 0x04006E19 RID: 28185
		[SerializeField]
		private GameObject _elementPrefab;

		// Token: 0x04006E1A RID: 28186
		[SerializeField]
		private float _pageFadeTime = 0.5f;

		// Token: 0x04006E1B RID: 28187
		[SerializeField]
		private float _pageMoveX = 20f;

		// Token: 0x04006E1C RID: 28188
		[SerializeField]
		private TutorialUI _tutorialUI;

		// Token: 0x04006E1D RID: 28189
		[SerializeField]
		private TutorialGroupListUI _listUI;

		// Token: 0x04006E1F RID: 28191
		private List<TutorialLoadingImageUIElement> _elementPool = new List<TutorialLoadingImageUIElement>();

		// Token: 0x04006E20 RID: 28192
		private TutorialLoadingImageUIElement _currentElement;

		// Token: 0x04006E21 RID: 28193
		private List<Sprite> _loadingSpriteList = new List<Sprite>();

		// Token: 0x04006E22 RID: 28194
		private MenuUIBehaviour[] _prevMenuUIBehavioures;

		// Token: 0x04006E23 RID: 28195
		private MenuUIBehaviour[] _menuUIBehaviours;

		// Token: 0x04006E24 RID: 28196
		private bool _initialize;

		// Token: 0x04006E25 RID: 28197
		private IDisposable _fadeDisposable;

		// Token: 0x04006E26 RID: 28198
		private int _prevFocusLevel;
	}
}

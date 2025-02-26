using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.UI.Popup;
using ConfigScene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FDC RID: 4060
	[RequireComponent(typeof(CanvasGroup))]
	public class StorySupportUI : UIBehaviour
	{
		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06008748 RID: 34632 RVA: 0x00386CEA File Offset: 0x003850EA
		// (set) Token: 0x06008749 RID: 34633 RVA: 0x00386CF2 File Offset: 0x003850F2
		public string ReceivedMessageText { get; set; } = string.Empty;

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x0600874A RID: 34634 RVA: 0x00386CFB File Offset: 0x003850FB
		// (set) Token: 0x0600874B RID: 34635 RVA: 0x00386D03 File Offset: 0x00385103
		public int Index { get; private set; } = -1;

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x0600874C RID: 34636 RVA: 0x00386D0C File Offset: 0x0038510C
		private int LangIdx
		{
			get
			{
				return (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			}
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x0600874D RID: 34637 RVA: 0x00386D28 File Offset: 0x00385128
		// (set) Token: 0x0600874E RID: 34638 RVA: 0x00386D30 File Offset: 0x00385130
		public bool IsActiveControl
		{
			get
			{
				return this.isActive;
			}
			set
			{
				if (!this.isActive && !value)
				{
					return;
				}
				if (this.isActive && value && this.messageText.text == this.ReceivedMessageText)
				{
					return;
				}
				if (!value)
				{
					this.StartClose();
				}
				else
				{
					this.StartChange();
				}
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x0600874F RID: 34639 RVA: 0x00386D93 File Offset: 0x00385193
		// (set) Token: 0x06008750 RID: 34640 RVA: 0x00386DBB File Offset: 0x003851BB
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

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06008751 RID: 34641 RVA: 0x00386DDA File Offset: 0x003851DA
		// (set) Token: 0x06008752 RID: 34642 RVA: 0x00386E02 File Offset: 0x00385202
		public float ChildCanvasAlpha
		{
			get
			{
				return (!(this.childCanvasGroup != null)) ? 0f : this.childCanvasGroup.alpha;
			}
			private set
			{
				if (this.childCanvasGroup != null)
				{
					this.childCanvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06008753 RID: 34643 RVA: 0x00386E21 File Offset: 0x00385221
		// (set) Token: 0x06008754 RID: 34644 RVA: 0x00386E2E File Offset: 0x0038522E
		private Vector3 ChildLocalPosition
		{
			get
			{
				return this.childRoot.localPosition;
			}
			set
			{
				this.childRoot.localPosition = value;
			}
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x00386E3C File Offset: 0x0038523C
		private void Dispose()
		{
			if (this.openDisposable != null)
			{
				this.openDisposable.Dispose();
			}
			if (this.closeDisposable != null)
			{
				this.closeDisposable.Dispose();
			}
			if (this.changeDisposable != null)
			{
				this.changeDisposable.Dispose();
			}
			this.openDisposable = null;
			this.closeDisposable = null;
			this.changeDisposable = null;
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x00386EA8 File Offset: 0x003852A8
		protected override void Start()
		{
			base.Start();
			if (this.childObject == null && this.childRoot != null && this.childRoot.gameObject != null)
			{
				this.childObject = this.childRoot.gameObject;
			}
			Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).TakeUntilDestroy(this.childObject).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06008757 RID: 34647 RVA: 0x00386F34 File Offset: 0x00385334
		private void OnUpdate()
		{
			GameConfigSystem gameData = Config.GameData;
			if (gameData != null)
			{
				this.SetActive(this.childObject, gameData.StoryHelp);
			}
		}

		// Token: 0x06008758 RID: 34648 RVA: 0x00386F60 File Offset: 0x00385360
		public void RefreshState()
		{
			GameConfigSystem gameData = Config.GameData;
			if (gameData != null && this.childObject != null)
			{
				this.childObject.SetActive(gameData.StoryHelp);
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06008759 RID: 34649 RVA: 0x00386F9B File Offset: 0x0038539B
		// (set) Token: 0x0600875A RID: 34650 RVA: 0x00386FA3 File Offset: 0x003853A3
		public System.Action OpenedAction { get; set; }

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x0600875B RID: 34651 RVA: 0x00386FAC File Offset: 0x003853AC
		// (set) Token: 0x0600875C RID: 34652 RVA: 0x00386FB4 File Offset: 0x003853B4
		public System.Action ClosedAction { get; set; }

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x0600875D RID: 34653 RVA: 0x00386FBD File Offset: 0x003853BD
		public bool PlayingOpen
		{
			[CompilerGenerated]
			get
			{
				return this.openDisposable != null;
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x0600875E RID: 34654 RVA: 0x00386FCB File Offset: 0x003853CB
		public bool PlayingClose
		{
			[CompilerGenerated]
			get
			{
				return this.closeDisposable != null;
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x0600875F RID: 34655 RVA: 0x00386FD9 File Offset: 0x003853D9
		public bool PlayingChange
		{
			[CompilerGenerated]
			get
			{
				return this.changeDisposable != null;
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06008760 RID: 34656 RVA: 0x00386FE7 File Offset: 0x003853E7
		public bool IsPlaying
		{
			[CompilerGenerated]
			get
			{
				return this.PlayingOpen || this.PlayingClose || this.PlayingChange;
			}
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x00387008 File Offset: 0x00385408
		public void Open(Popup.StorySupport.Type _type)
		{
			ReadOnlyDictionary<int, string[]> storySupportTable = Singleton<Manager.Resources>.Instance.PopupInfo.StorySupportTable;
			this.Index = (int)_type;
			string[] array;
			if (!storySupportTable.TryGetValue(this.Index, out array))
			{
				if (this.IsActiveControl)
				{
					this.IsActiveControl = false;
				}
				return;
			}
			string text = (array != null) ? array.GetElement(this.LangIdx) : null;
			if (text.IsNullOrEmpty())
			{
				if (this.IsActiveControl)
				{
					this.IsActiveControl = false;
				}
				return;
			}
			this.ReceivedMessageText = text;
			this.IsActiveControl = true;
		}

		// Token: 0x06008762 RID: 34658 RVA: 0x00387094 File Offset: 0x00385494
		public void Open()
		{
			this.Open(this.Index);
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x003870A4 File Offset: 0x003854A4
		public void Open(int _id)
		{
			ReadOnlyDictionary<int, string[]> storySupportTable = Singleton<Manager.Resources>.Instance.PopupInfo.StorySupportTable;
			this.Index = _id;
			string[] array;
			if (!storySupportTable.TryGetValue(this.Index, out array))
			{
				if (this.IsActiveControl)
				{
					this.IsActiveControl = false;
				}
				return;
			}
			string text = (array != null) ? array.GetElement(this.LangIdx) : null;
			if (text.IsNullOrEmpty())
			{
				if (this.IsActiveControl)
				{
					this.IsActiveControl = false;
				}
				return;
			}
			this.ReceivedMessageText = text;
			this.IsActiveControl = true;
		}

		// Token: 0x06008764 RID: 34660 RVA: 0x00387130 File Offset: 0x00385530
		public void SetIndexClose(int idx)
		{
			this.Index = idx;
			this.IsActiveControl = false;
		}

		// Token: 0x06008765 RID: 34661 RVA: 0x00387140 File Offset: 0x00385540
		public void Close()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x06008766 RID: 34662 RVA: 0x0038714C File Offset: 0x0038554C
		private void StartOpen()
		{
			this.Dispose();
			this.openDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.OpenCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>().AddTo(this.openDisposable);
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x003871A8 File Offset: 0x003855A8
		private IEnumerator OpenCoroutine()
		{
			this.PlaySE();
			this.isActive = true;
			IConnectableObservable<TimeInterval<float>> _fadeInStream = ObservableEasing.EaseOutQuint(this.openInfo.Time, true).TakeUntilDestroy(this).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			Vector3 _startPosition = this.ChildLocalPosition;
			Vector3 _endPosition = new Vector3(this.popupWidth, 0f, 0f);
			float _startAlpha = this.openInfo.Alpha;
			float _endAlpha = 1f;
			this.ChildCanvasAlpha = _startAlpha;
			_fadeInStream.Connect().AddTo(this.openDisposable);
			_fadeInStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				this.ChildCanvasAlpha = Mathf.Lerp(_startAlpha, _endAlpha, value);
				this.ChildLocalPosition = Vector3.Lerp(_startPosition, _endPosition, value);
			}).AddTo(this.openDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeInStream
			}).TakeUntilDestroy(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.openDisposable);
			this.openDisposable = null;
			System.Action openedAction = this.OpenedAction;
			if (openedAction != null)
			{
				openedAction();
			}
			yield break;
		}

		// Token: 0x06008768 RID: 34664 RVA: 0x003871C4 File Offset: 0x003855C4
		private void StartClose()
		{
			this.Dispose();
			this.closeDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.CloseCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>().AddTo(this.closeDisposable);
		}

		// Token: 0x06008769 RID: 34665 RVA: 0x00387220 File Offset: 0x00385620
		private IEnumerator CloseCoroutine()
		{
			IConnectableObservable<TimeInterval<float>> _fadeOutStream = ObservableEasing.EaseOutQuint(this.closeInfo.Time, true).TakeUntilDestroy(this).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			Vector3 _startPosition = this.ChildLocalPosition;
			Vector3 _endPosition = Vector3.zero;
			float _startAlpha = this.ChildCanvasAlpha;
			float _endAlpha = this.closeInfo.Alpha;
			_fadeOutStream.Connect().AddTo(this.closeDisposable);
			_fadeOutStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				this.ChildCanvasAlpha = Mathf.Lerp(_startAlpha, _endAlpha, value);
				this.ChildLocalPosition = Vector3.Lerp(_startPosition, _endPosition, value);
			}).AddTo(this.closeDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeOutStream
			}).TakeUntilDestroy(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.closeDisposable);
			this.closeDisposable = null;
			this.isActive = false;
			System.Action closedAction = this.ClosedAction;
			if (closedAction != null)
			{
				closedAction();
			}
			yield break;
		}

		// Token: 0x0600876A RID: 34666 RVA: 0x0038723C File Offset: 0x0038563C
		private void StartChange()
		{
			this.Dispose();
			this.changeDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.ChangeCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>().AddTo(this.changeDisposable);
		}

		// Token: 0x0600876B RID: 34667 RVA: 0x00387298 File Offset: 0x00385698
		private IEnumerator ChangeCoroutine()
		{
			if (this.isActive)
			{
				this.closeDisposable = new CompositeDisposable();
				IEnumerator _closeCoroutine = this.CloseCoroutine();
				yield return Observable.FromCoroutine(() => _closeCoroutine, false).TakeUntilDestroy(this).ToYieldInstruction<Unit>().AddTo(this.closeDisposable);
				yield return Observable.Timer(TimeSpan.FromSeconds(0.5), Scheduler.MainThreadIgnoreTimeScale).TakeUntilDestroy(this).ToYieldInstruction<long>().AddTo(this.changeDisposable);
			}
			this.messageText.text = this.ReceivedMessageText;
			this.openDisposable = new CompositeDisposable();
			IEnumerator _openCoroutine = this.OpenCoroutine();
			yield return Observable.FromCoroutine(() => _openCoroutine, false).TakeUntilDestroy(this).ToYieldInstruction<Unit>().AddTo(this.openDisposable);
			this.changeDisposable = null;
			yield break;
		}

		// Token: 0x0600876C RID: 34668 RVA: 0x003872B4 File Offset: 0x003856B4
		private void PlaySE()
		{
			if (Mathf.Approximately(this.CanvasAlpha, 0f))
			{
				return;
			}
			if (this.childRoot != null && this.childRoot.gameObject != null && !this.childRoot.gameObject.activeSelf)
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			soundPack.Play(SoundPack.SystemSE.Popup);
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x00387331 File Offset: 0x00385731
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

		// Token: 0x04006DF1 RID: 28145
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006DF2 RID: 28146
		[SerializeField]
		private RectTransform childRoot;

		// Token: 0x04006DF3 RID: 28147
		[SerializeField]
		private GameObject childObject;

		// Token: 0x04006DF4 RID: 28148
		[SerializeField]
		private CanvasGroup childCanvasGroup;

		// Token: 0x04006DF5 RID: 28149
		[SerializeField]
		private Text messageText;

		// Token: 0x04006DF6 RID: 28150
		[SerializeField]
		private float popupWidth = -380f;

		// Token: 0x04006DF7 RID: 28151
		[SerializeField]
		private FadeInfo openInfo = default(FadeInfo);

		// Token: 0x04006DF8 RID: 28152
		[SerializeField]
		private FadeInfo closeInfo = default(FadeInfo);

		// Token: 0x04006DFB RID: 28155
		private bool isActive;

		// Token: 0x04006DFC RID: 28156
		private CompositeDisposable openDisposable;

		// Token: 0x04006DFD RID: 28157
		private CompositeDisposable closeDisposable;

		// Token: 0x04006DFE RID: 28158
		private CompositeDisposable changeDisposable;
	}
}

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA5 RID: 3749
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class RecyclingItemDeleteRequestUI : MenuUIBehaviour
	{
		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060079DE RID: 31198 RVA: 0x00333F01 File Offset: 0x00332301
		public IObservable<Unit> OnSubmitClick
		{
			[CompilerGenerated]
			get
			{
				return this._submitButton.OnClickAsObservable();
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060079DF RID: 31199 RVA: 0x00333F0E File Offset: 0x0033230E
		public IObservable<Unit> OnCancelClick
		{
			[CompilerGenerated]
			get
			{
				return this._cancelButton.OnClickAsObservable();
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060079E0 RID: 31200 RVA: 0x00333F1B File Offset: 0x0033231B
		// (set) Token: 0x060079E1 RID: 31201 RVA: 0x00333F43 File Offset: 0x00332343
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

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060079E2 RID: 31202 RVA: 0x00333F62 File Offset: 0x00332362
		// (set) Token: 0x060079E3 RID: 31203 RVA: 0x00333F86 File Offset: 0x00332386
		public bool BlockRaycast
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.blocksRaycasts;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.blocksRaycasts != value)
				{
					this._canvasGroup.blocksRaycasts = value;
				}
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060079E4 RID: 31204 RVA: 0x00333FB6 File Offset: 0x003323B6
		// (set) Token: 0x060079E5 RID: 31205 RVA: 0x00333FDA File Offset: 0x003323DA
		public bool Interactable
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.interactable;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.interactable != value)
				{
					this._canvasGroup.interactable = value;
				}
			}
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x0033400A File Offset: 0x0033240A
		protected override void Awake()
		{
			base.Awake();
			if (this._recyclingUI == null)
			{
				this._recyclingUI = base.GetComponentInParent<RecyclingUI>();
			}
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x00334030 File Offset: 0x00332430
		protected override void Start()
		{
			base.Start();
			bool flag = false;
			this.BlockRaycast = flag;
			flag = flag;
			this.Interactable = flag;
			base.EnabledInput = flag;
			this.SetActive(base.gameObject, false);
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x0033406A File Offset: 0x0033246A
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool _)
			{
				this.SetActiveControl(_);
			}).AddTo(this);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x0033408A File Offset: 0x0033248A
		public void DoOpen()
		{
			this.IsActiveControl = true;
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x00334093 File Offset: 0x00332493
		public void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x0033409C File Offset: 0x0033249C
		private void SetActiveControl(bool active)
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x00334108 File Offset: 0x00332508
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(base.gameObject, true);
			this.BlockRaycast = true;
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				this._prevFocusLevel = instance.FocusLevel;
				instance.FocusLevel = this._prevFocusLevel + 1;
				base.SetFocusLevel(instance.FocusLevel);
			}
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this);
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 1f;
			this.BlockRaycast = true;
			flag = true;
			this.Interactable = flag;
			base.EnabledInput = flag;
			yield break;
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x00334124 File Offset: 0x00332524
		private IEnumerator CloseCoroutine()
		{
			this.BlockRaycast = true;
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this);
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				instance.FocusLevel = this._prevFocusLevel;
			}
			this.CanvasAlpha = 0f;
			this.BlockRaycast = false;
			flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x00334140 File Offset: 0x00332540
		public void ForcedClose()
		{
			if (this.IsActiveControl && Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				instance.FocusLevel = this._prevFocusLevel;
			}
			this.CanvasAlpha = 0f;
			base.EnabledInput = false;
			this.Interactable = false;
			this.BlockRaycast = false;
			this.IsActiveControl = false;
			this.SetActive(base.gameObject, false);
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x003341A9 File Offset: 0x003325A9
		private bool SetActive(GameObject obj, bool active)
		{
			if (obj != null && obj.activeSelf != active)
			{
				obj.SetActive(active);
				return true;
			}
			return false;
		}

		// Token: 0x060079F0 RID: 31216 RVA: 0x003341CD File Offset: 0x003325CD
		private bool SetActive(Component com, bool active)
		{
			if (com != null && com.gameObject != null && com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
				return true;
			}
			return false;
		}

		// Token: 0x04006214 RID: 25108
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x04006215 RID: 25109
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006216 RID: 25110
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006217 RID: 25111
		[SerializeField]
		private Text _titleText;

		// Token: 0x04006218 RID: 25112
		[SerializeField]
		private Button _submitButton;

		// Token: 0x04006219 RID: 25113
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x0400621A RID: 25114
		private IDisposable _fadeDisposable;

		// Token: 0x0400621B RID: 25115
		private int _prevFocusLevel = -1;
	}
}

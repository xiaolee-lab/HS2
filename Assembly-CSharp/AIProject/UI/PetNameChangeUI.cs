using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E9D RID: 3741
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class PetNameChangeUI : MenuUIBehaviour
	{
		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x0032F109 File Offset: 0x0032D509
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x0032F111 File Offset: 0x0032D511
		public RectTransform RectTransform
		{
			[CompilerGenerated]
			get
			{
				return this._rectTransform;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x0032F119 File Offset: 0x0032D519
		public Button CloseButton
		{
			[CompilerGenerated]
			get
			{
				return this._closeButton;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x060078DE RID: 30942 RVA: 0x0032F121 File Offset: 0x0032D521
		public InputField NameInputField
		{
			[CompilerGenerated]
			get
			{
				return this._nameInputField;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x060078DF RID: 30943 RVA: 0x0032F129 File Offset: 0x0032D529
		public Button SubmitButton
		{
			[CompilerGenerated]
			get
			{
				return this._submitButton;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x060078E0 RID: 30944 RVA: 0x0032F131 File Offset: 0x0032D531
		// (set) Token: 0x060078E1 RID: 30945 RVA: 0x0032F139 File Offset: 0x0032D539
		public Action<string> SubmitAction { get; set; }

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x060078E2 RID: 30946 RVA: 0x0032F142 File Offset: 0x0032D542
		// (set) Token: 0x060078E3 RID: 30947 RVA: 0x0032F14A File Offset: 0x0032D54A
		public Action<string> CancelAction { get; set; }

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x060078E4 RID: 30948 RVA: 0x0032F153 File Offset: 0x0032D553
		// (set) Token: 0x060078E5 RID: 30949 RVA: 0x0032F17B File Offset: 0x0032D57B
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x0032F19C File Offset: 0x0032D59C
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
			base.EnabledInput = false;
			if (this._canvasGroup == null)
			{
				return;
			}
			this.CanvasAlpha = 0f;
			this.SetBlockRaycast(false);
			this.SetInteractable(false);
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x0032F21C File Offset: 0x0032D61C
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			if (this._closeButton != null)
			{
				this._closeButton.onClick.AddListener(delegate()
				{
					this.DoCancel();
				});
			}
			if (this._submitButton != null)
			{
				this._submitButton.onClick.AddListener(delegate()
				{
					this.DoSubmit();
				});
			}
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x0032F29C File Offset: 0x0032D69C
		public void Dispose()
		{
			if (this._activeChangeDisposable != null)
			{
				this._activeChangeDisposable.Dispose();
				this._activeChangeDisposable = null;
			}
			if (this._openDisposable != null)
			{
				this._openDisposable.Dispose();
				this._openDisposable = null;
			}
			if (this._closeDisposable != null)
			{
				this._closeDisposable.Dispose();
				this._closeDisposable = null;
			}
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x0032F300 File Offset: 0x0032D700
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._activeChangeDisposable != null)
			{
				this._activeChangeDisposable.Dispose();
			}
			this._activeChangeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x0032F365 File Offset: 0x0032D765
		public bool IsOpening
		{
			[CompilerGenerated]
			get
			{
				return this._openDisposable != null;
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x060078EB RID: 30955 RVA: 0x0032F373 File Offset: 0x0032D773
		public bool IsClosing
		{
			[CompilerGenerated]
			get
			{
				return this._closeDisposable != null;
			}
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x0032F384 File Offset: 0x0032D784
		public void Open()
		{
			if (this.IsOpening)
			{
				return;
			}
			if (this._closeDisposable != null)
			{
				this._closeDisposable.Dispose();
			}
			IEnumerator coroutine = this.OpenCoroutine();
			this._openDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x0032F3E4 File Offset: 0x0032D7E4
		private IEnumerator OpenCoroutine()
		{
			this.SetBlockRaycast(true);
			float startAlpha = this.CanvasAlpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.SetInteractable(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x0032F400 File Offset: 0x0032D800
		public void Close()
		{
			if (this.IsClosing)
			{
				return;
			}
			if (this._openDisposable != null)
			{
				this._openDisposable.Dispose();
			}
			IEnumerator coroutine = this.CloseCoroutine();
			this._closeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x0032F460 File Offset: 0x0032D860
		private IEnumerator CloseCoroutine()
		{
			this.SetInteractable(false);
			base.EnabledInput = false;
			float startAlpha = this.CanvasAlpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.SetBlockRaycast(false);
			yield break;
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x0032F47B File Offset: 0x0032D87B
		private void DoSubmit()
		{
			this.PlaySystemSE(SoundPack.SystemSE.OK_L);
			Action<string> submitAction = this.SubmitAction;
			if (submitAction != null)
			{
				submitAction(this._nameInputField.text);
			}
			this._nameInputField.text = string.Empty;
			this.IsActiveControl = false;
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x0032F4BA File Offset: 0x0032D8BA
		private void DoCancel()
		{
			this.PlaySystemSE(SoundPack.SystemSE.Cancel);
			Action<string> cancelAction = this.CancelAction;
			if (cancelAction != null)
			{
				cancelAction(this._nameInputField.text);
			}
			this._nameInputField.text = string.Empty;
			this.IsActiveControl = false;
		}

		// Token: 0x060078F2 RID: 30962 RVA: 0x0032F4F9 File Offset: 0x0032D8F9
		public void QuickOpen()
		{
			this.CanvasAlpha = 1f;
			this.SetBlockRaycast(true);
			this.SetInteractable(true);
			base.EnabledInput = true;
		}

		// Token: 0x060078F3 RID: 30963 RVA: 0x0032F51B File Offset: 0x0032D91B
		public void QuickClose()
		{
			this.IsActiveControl = false;
			this.Dispose();
			base.EnabledInput = false;
			this.SetBlockRaycast(false);
			this.SetInteractable(false);
			this.CanvasAlpha = 0f;
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x0032F54A File Offset: 0x0032D94A
		private void SetBlockRaycast(bool active)
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			if (this._canvasGroup.blocksRaycasts != active)
			{
				this._canvasGroup.blocksRaycasts = active;
			}
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x0032F57B File Offset: 0x0032D97B
		private void SetInteractable(bool active)
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			if (this._canvasGroup.interactable != active)
			{
				this._canvasGroup.interactable = active;
			}
		}

		// Token: 0x060078F6 RID: 30966 RVA: 0x0032F5AC File Offset: 0x0032D9AC
		private void PlaySystemSE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x040061AE RID: 25006
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040061AF RID: 25007
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x040061B0 RID: 25008
		[SerializeField]
		private Button _closeButton;

		// Token: 0x040061B1 RID: 25009
		[SerializeField]
		private InputField _nameInputField;

		// Token: 0x040061B2 RID: 25010
		[SerializeField]
		private Button _submitButton;

		// Token: 0x040061B5 RID: 25013
		private IDisposable _activeChangeDisposable;

		// Token: 0x040061B6 RID: 25014
		private IDisposable _openDisposable;

		// Token: 0x040061B7 RID: 25015
		private IDisposable _closeDisposable;
	}
}

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
	// Token: 0x02000FD3 RID: 4051
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class EventDialogUI : MenuUIBehaviour
	{
		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x060086AC RID: 34476 RVA: 0x00383FB4 File Offset: 0x003823B4
		// (set) Token: 0x060086AD RID: 34477 RVA: 0x00383FBC File Offset: 0x003823BC
		public Action SubmitEvent
		{
			get
			{
				return this._submitEvent;
			}
			set
			{
				this._submitEvent = value;
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x060086AE RID: 34478 RVA: 0x00383FC5 File Offset: 0x003823C5
		// (set) Token: 0x060086AF RID: 34479 RVA: 0x00383FCD File Offset: 0x003823CD
		public Action CancelEvent
		{
			get
			{
				return this._cancelEvent;
			}
			set
			{
				this._cancelEvent = value;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x060086B0 RID: 34480 RVA: 0x00383FD6 File Offset: 0x003823D6
		// (set) Token: 0x060086B1 RID: 34481 RVA: 0x00383FDE File Offset: 0x003823DE
		public Action ClosedEvent
		{
			get
			{
				return this._closedEvent;
			}
			set
			{
				this._closedEvent = value;
			}
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x060086B2 RID: 34482 RVA: 0x00383FE7 File Offset: 0x003823E7
		// (set) Token: 0x060086B3 RID: 34483 RVA: 0x00383FEF File Offset: 0x003823EF
		public float TimeScale { get; set; }

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x060086B4 RID: 34484 RVA: 0x00383FF8 File Offset: 0x003823F8
		// (set) Token: 0x060086B5 RID: 34485 RVA: 0x00384005 File Offset: 0x00382405
		public string MessageText
		{
			get
			{
				return this._messageText.text;
			}
			set
			{
				this._messageText.text = value;
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x060086B6 RID: 34486 RVA: 0x00384013 File Offset: 0x00382413
		// (set) Token: 0x060086B7 RID: 34487 RVA: 0x00384020 File Offset: 0x00382420
		public string SubmitButtonText
		{
			get
			{
				return this._submitText.text;
			}
			set
			{
				this._submitText.text = value;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x060086B8 RID: 34488 RVA: 0x0038402E File Offset: 0x0038242E
		// (set) Token: 0x060086B9 RID: 34489 RVA: 0x0038403B File Offset: 0x0038243B
		public string CancelButtonText
		{
			get
			{
				return this._cancelText.text;
			}
			set
			{
				this._cancelText.text = value;
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x060086BA RID: 34490 RVA: 0x00384049 File Offset: 0x00382449
		// (set) Token: 0x060086BB RID: 34491 RVA: 0x00384071 File Offset: 0x00382471
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			protected set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x060086BC RID: 34492 RVA: 0x00384090 File Offset: 0x00382490
		protected override void Awake()
		{
			base.Awake();
			this.SubmitEvent = null;
			this.CancelEvent = null;
			this.ClosedEvent = null;
			this._submit = false;
			this._cancel = false;
			Graphic messageText = this._messageText;
			Color color = this._defaultColor;
			this._cancelText.color = color;
			color = color;
			this._submitText.color = color;
			messageText.color = color;
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x060086BD RID: 34493 RVA: 0x003840F3 File Offset: 0x003824F3
		// (set) Token: 0x060086BE RID: 34494 RVA: 0x00384100 File Offset: 0x00382500
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

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x060086BF RID: 34495 RVA: 0x00384120 File Offset: 0x00382520
		private MenuUIBehaviour[] MenuUIElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._uiElements) == null)
				{
					result = (this._uiElements = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x060086C0 RID: 34496 RVA: 0x00384150 File Offset: 0x00382550
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this._lerpStream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this);
			this._submitButton.onClick.AddListener(delegate()
			{
				this.DoSubmit();
			});
			this._cancelButton.onClick.AddListener(delegate()
			{
				this.DoCancel();
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoCancel();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoCancel();
			});
			this._keyCommands.Add(keyCodeDownCommand);
		}

		// Token: 0x060086C1 RID: 34497 RVA: 0x00384234 File Offset: 0x00382634
		protected override void OnAfterStart()
		{
			this.SetCanvasGroupParam(false, false);
			this.CanvasAlpha = 0f;
			base.EnabledInput = false;
			this.SetActive(false);
		}

		// Token: 0x060086C2 RID: 34498 RVA: 0x00384258 File Offset: 0x00382658
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

		// Token: 0x060086C3 RID: 34499 RVA: 0x003842FD File Offset: 0x003826FD
		private void DoSubmit()
		{
			this.PlaySystemSE(SoundPack.SystemSE.OK_L);
			this._submit = true;
			this.IsActiveControl = false;
		}

		// Token: 0x060086C4 RID: 34500 RVA: 0x00384314 File Offset: 0x00382714
		private void DoCancel()
		{
			this.PlaySystemSE(SoundPack.SystemSE.Cancel);
			this._cancel = true;
			this.IsActiveControl = false;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x0038432C File Offset: 0x0038272C
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(true);
			this._submit = (this._cancel = false);
			this._prevTimeScale = Time.timeScale;
			Time.timeScale = this.TimeScale;
			this._prevCommandAcception = MapUIContainer.CommandLabel.Acception;
			if (this._prevCommandAcception != CommandLabel.AcceptionState.None)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			}
			Manager.Input input = Singleton<Manager.Input>.Instance;
			this._prevInputValid = input.State;
			if (this._prevInputValid != Manager.Input.ValidType.UI)
			{
				input.ReserveState(Manager.Input.ValidType.UI);
				input.SetupState();
			}
			PlayerActor player = Singleton<Map>.Instance.Player;
			this._prevPlayerScheduleInteraction = player.CurrentInteractionState;
			if (this._prevPlayerScheduleInteraction)
			{
				player.SetScheduledInteractionState(false);
				player.ReleaseInteraction();
			}
			this.SetCanvasGroupParam(true, false);
			input.FocusLevel = (this._focusLevel = input.FocusLevel + 1);
			input.MenuElements = this.MenuUIElements;
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 1f;
			this.SetInteractable(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x00384348 File Offset: 0x00382748
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			Manager.Input input = Singleton<Manager.Input>.Instance;
			input.ClearMenuElements();
			this.SetInteractable(false);
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 0f;
			PlayerActor player = Singleton<Map>.Instance.Player;
			player.SetScheduledInteractionState(this._prevPlayerScheduleInteraction);
			player.ReleaseInteraction();
			input.FocusLevel--;
			input.ReserveState(this._prevInputValid);
			input.SetupState();
			MapUIContainer.SetCommandLabelAcception(this._prevCommandAcception);
			this.SetCanvasGroupParam(false, false);
			Time.timeScale = this._prevTimeScale;
			this.EventInvoke(ref this._closedEvent);
			if (this._submit)
			{
				this.EventInvoke(ref this._submitEvent);
			}
			if (this._cancel)
			{
				this.EventInvoke(ref this._cancelEvent);
			}
			this._submit = (this._cancel = false);
			this.SetActive(false);
			yield break;
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x00384364 File Offset: 0x00382764
		private void SetCanvasGroupParam(bool blockRaycasts, bool interactable)
		{
			CanvasGroup canvasGroup = this._canvasGroup;
			if (canvasGroup == null)
			{
				return;
			}
			if (canvasGroup.blocksRaycasts != blockRaycasts)
			{
				canvasGroup.blocksRaycasts = blockRaycasts;
			}
			if (canvasGroup.interactable != interactable)
			{
				canvasGroup.interactable = interactable;
			}
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x003843AC File Offset: 0x003827AC
		private void SetBlockRaycasts(bool blockRaycasts)
		{
			CanvasGroup canvasGroup = this._canvasGroup;
			if (canvasGroup == null)
			{
				return;
			}
			if (canvasGroup.blocksRaycasts != blockRaycasts)
			{
				canvasGroup.blocksRaycasts = blockRaycasts;
			}
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x003843E0 File Offset: 0x003827E0
		private void SetInteractable(bool interactable)
		{
			CanvasGroup canvasGroup = this._canvasGroup;
			if (canvasGroup == null)
			{
				return;
			}
			if (canvasGroup.interactable != interactable)
			{
				canvasGroup.interactable = interactable;
			}
		}

		// Token: 0x060086CA RID: 34506 RVA: 0x00384414 File Offset: 0x00382814
		public Color GetListColor(int id)
		{
			if (this._colors.IsNullOrEmpty<Color>())
			{
				return this._defaultColor;
			}
			if (0 <= id && id < this._colors.Length)
			{
				return this._colors[id];
			}
			return this._defaultColor;
		}

		// Token: 0x060086CB RID: 34507 RVA: 0x00384465 File Offset: 0x00382865
		public void SetMessageColor(Color color)
		{
			this._messageText.color = color;
		}

		// Token: 0x060086CC RID: 34508 RVA: 0x00384473 File Offset: 0x00382873
		public void SetMessageColor(int id)
		{
			this._messageText.color = this.GetListColor(id);
		}

		// Token: 0x060086CD RID: 34509 RVA: 0x00384488 File Offset: 0x00382888
		private void EventInvoke(ref Action action)
		{
			if (action != null)
			{
				Action action2 = action;
				action = null;
				action2();
			}
		}

		// Token: 0x060086CE RID: 34510 RVA: 0x003844A8 File Offset: 0x003828A8
		private void SetActive(bool active)
		{
			if (base.gameObject.activeSelf != active)
			{
				base.gameObject.SetActive(active);
			}
		}

		// Token: 0x060086CF RID: 34511 RVA: 0x003844C8 File Offset: 0x003828C8
		private void PlaySystemSE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04006D83 RID: 28035
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006D84 RID: 28036
		[SerializeField]
		private Text _messageText;

		// Token: 0x04006D85 RID: 28037
		[SerializeField]
		private Text _submitText;

		// Token: 0x04006D86 RID: 28038
		[SerializeField]
		private Text _cancelText;

		// Token: 0x04006D87 RID: 28039
		[SerializeField]
		private Button _submitButton;

		// Token: 0x04006D88 RID: 28040
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x04006D89 RID: 28041
		[SerializeField]
		private Color _defaultColor = Color.white;

		// Token: 0x04006D8A RID: 28042
		[SerializeField]
		private Color[] _colors = new Color[0];

		// Token: 0x04006D8B RID: 28043
		private Action _submitEvent;

		// Token: 0x04006D8C RID: 28044
		private Action _cancelEvent;

		// Token: 0x04006D8D RID: 28045
		private Action _closedEvent;

		// Token: 0x04006D8F RID: 28047
		private bool _submit;

		// Token: 0x04006D90 RID: 28048
		private bool _cancel;

		// Token: 0x04006D91 RID: 28049
		private IObservable<TimeInterval<float>> _lerpStream;

		// Token: 0x04006D92 RID: 28050
		private MenuUIBehaviour[] _uiElements;

		// Token: 0x04006D93 RID: 28051
		private IDisposable _fadeDisposable;

		// Token: 0x04006D94 RID: 28052
		private CommandLabel.AcceptionState _prevCommandAcception = CommandLabel.AcceptionState.None;

		// Token: 0x04006D95 RID: 28053
		private Manager.Input.ValidType _prevInputValid;

		// Token: 0x04006D96 RID: 28054
		private bool _prevPlayerScheduleInteraction;

		// Token: 0x04006D97 RID: 28055
		private float _prevTimeScale = 1f;
	}
}

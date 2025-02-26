using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E78 RID: 3704
	public class PlantInfoUI : MenuUIBehaviour
	{
		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06007606 RID: 30214 RVA: 0x00320B28 File Offset: 0x0031EF28
		public IObservable<bool> OnComplete
		{
			[CompilerGenerated]
			get
			{
				return (from finish in this._finished
				where finish
				select finish).AsObservable<bool>();
			}
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x00320B57 File Offset: 0x0031EF57
		public void ItemCancelInteractable(bool interactable)
		{
			this._cancelButton.interactable = interactable;
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06007608 RID: 30216 RVA: 0x00320B65 File Offset: 0x0031EF65
		// (set) Token: 0x06007609 RID: 30217 RVA: 0x00320B6D File Offset: 0x0031EF6D
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x0600760A RID: 30218 RVA: 0x00320B76 File Offset: 0x0031EF76
		// (set) Token: 0x0600760B RID: 30219 RVA: 0x00320B7E File Offset: 0x0031EF7E
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x0600760C RID: 30220 RVA: 0x00320B87 File Offset: 0x0031EF87
		public PlantIcon currentIcon
		{
			[CompilerGenerated]
			get
			{
				return this._icon;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x0600760D RID: 30221 RVA: 0x00320B8F File Offset: 0x0031EF8F
		// (set) Token: 0x0600760E RID: 30222 RVA: 0x00320B97 File Offset: 0x0031EF97
		private PlantIcon _icon { get; set; }

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x0600760F RID: 30223 RVA: 0x00320BA0 File Offset: 0x0031EFA0
		private BoolReactiveProperty _finished { get; } = new BoolReactiveProperty(false);

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06007610 RID: 30224 RVA: 0x00320BA8 File Offset: 0x0031EFA8
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x00320BB0 File Offset: 0x0031EFB0
		public virtual void Open(PlantIcon icon)
		{
			this.Refresh(icon);
			this.IsActiveControl = true;
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x00320BC0 File Offset: 0x0031EFC0
		public virtual void Close()
		{
			this._icon = null;
			if (this.isOpen)
			{
				this.IsActiveControl = false;
			}
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x00320BDB File Offset: 0x0031EFDB
		public virtual void Refresh(PlantIcon icon)
		{
			this._icon = icon;
			this._itemName.text = icon.itemName;
			this._itemIcon.sprite = icon.itemIcon;
			this._finished.Value = false;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x00320C14 File Offset: 0x0031F014
		private void OnUpdate()
		{
			AIProject.SaveData.Environment.PlantInfo info = this._icon.info;
			Slider progressBar = this._progressBar;
			float? num = (info != null) ? new float?(info.progress) : null;
			progressBar.value = ((num == null) ? 1f : num.Value);
			bool? flag = (info != null) ? new bool?(info.isEnd) : null;
			bool flag2 = flag == null || flag.Value;
			this._timeText.text = ((!flag2) ? info.ToString() : this._completeStr);
			this._finished.Value = flag2;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x00320CD8 File Offset: 0x0031F0D8
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this._updateDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where this.isOpen
			where this._icon != null
			where !this._finished.Value
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			if (this._cancelButton != null)
			{
				this._cancelButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnInputSubmit();
				});
			}
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.SquareX
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			base.Start();
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x00320E57 File Offset: 0x0031F257
		protected virtual void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
			if (this._updateDisposable != null)
			{
				this._updateDisposable.Dispose();
			}
			this._updateDisposable = null;
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x00320E98 File Offset: 0x0031F298
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.DoClose() : this.DoOpen();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x00320F00 File Offset: 0x0031F300
		private IEnumerator DoOpen()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x00320F1C File Offset: 0x0031F31C
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x00320F37 File Offset: 0x0031F337
		private void OnInputSubmit()
		{
			if (!this.isOpen)
			{
				return;
			}
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x00320F59 File Offset: 0x0031F359
		private void OnInputCancel()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.Close();
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x0400601D RID: 24605
		[Header("Infomation Setting")]
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400601E RID: 24606
		[SerializeField]
		private Image _itemIcon;

		// Token: 0x0400601F RID: 24607
		[SerializeField]
		private Text _itemName;

		// Token: 0x04006020 RID: 24608
		[SerializeField]
		private Text _timeText;

		// Token: 0x04006021 RID: 24609
		[SerializeField]
		private Slider _progressBar;

		// Token: 0x04006022 RID: 24610
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x04006023 RID: 24611
		[SerializeField]
		private string _completeStr = string.Empty;

		// Token: 0x04006026 RID: 24614
		private IDisposable _fadeDisposable;

		// Token: 0x04006027 RID: 24615
		private IDisposable _updateDisposable;
	}
}

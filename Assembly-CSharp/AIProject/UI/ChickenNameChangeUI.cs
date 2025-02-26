using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E4A RID: 3658
	public class ChickenNameChangeUI : MenuUIBehaviour
	{
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x0600731A RID: 29466 RVA: 0x00311174 File Offset: 0x0030F574
		// (remove) Token: 0x0600731B RID: 29467 RVA: 0x003111AC File Offset: 0x0030F5AC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnSubmit;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x0600731C RID: 29468 RVA: 0x003111E4 File Offset: 0x0030F5E4
		// (remove) Token: 0x0600731D RID: 29469 RVA: 0x0031121C File Offset: 0x0030F61C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnCancel;

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x0600731E RID: 29470 RVA: 0x00311252 File Offset: 0x0030F652
		// (set) Token: 0x0600731F RID: 29471 RVA: 0x0031125A File Offset: 0x0030F65A
		private AIProject.SaveData.Environment.ChickenInfo _info { get; set; }

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06007320 RID: 29472 RVA: 0x00311263 File Offset: 0x0030F663
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x0031126B File Offset: 0x0030F66B
		public virtual void Open(AIProject.SaveData.Environment.ChickenInfo info)
		{
			this.IsActiveControl = true;
			this._info = info;
			this._nameInputField.text = info.name;
		}

		// Token: 0x06007322 RID: 29474 RVA: 0x0031128C File Offset: 0x0030F68C
		public virtual void Close()
		{
			if (this.isOpen)
			{
				this.IsActiveControl = false;
			}
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x003112A0 File Offset: 0x0030F6A0
		public virtual void Refresh()
		{
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x003112A4 File Offset: 0x0030F6A4
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			if (this._enterButton != null)
			{
				this._enterButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnInputSubmit();
				});
			}
			if (this._closeButton != null)
			{
				this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnInputCancel();
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

		// Token: 0x06007325 RID: 29477 RVA: 0x003113F7 File Offset: 0x0030F7F7
		protected virtual void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x00311418 File Offset: 0x0030F818
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.DoClose() : this.DoOpen();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x00311480 File Offset: 0x0030F880
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

		// Token: 0x06007328 RID: 29480 RVA: 0x0031149C File Offset: 0x0030F89C
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

		// Token: 0x06007329 RID: 29481 RVA: 0x003114B8 File Offset: 0x0030F8B8
		private void OnInputSubmit()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (this._info != null)
			{
				this._info.name = this._nameInputField.text;
			}
			this._info = null;
			this._nameInputField.text = string.Empty;
			if (this.OnSubmit != null)
			{
				this.OnSubmit();
			}
			this.Close();
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x00311527 File Offset: 0x0030F927
		private void OnInputCancel()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.Close();
			if (this.OnCancel != null)
			{
				this.OnCancel();
			}
		}

		// Token: 0x04005E30 RID: 24112
		[Header("Infomation Setting")]
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005E31 RID: 24113
		[SerializeField]
		private Button _enterButton;

		// Token: 0x04005E32 RID: 24114
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005E33 RID: 24115
		[SerializeField]
		private InputField _nameInputField;

		// Token: 0x04005E35 RID: 24117
		private IDisposable _fadeDisposable;
	}
}

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E5E RID: 3678
	public class CountSettingUI : MenuUIBehaviour
	{
		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06007457 RID: 29783 RVA: 0x00317AA9 File Offset: 0x00315EA9
		// (set) Token: 0x06007458 RID: 29784 RVA: 0x00317AB6 File Offset: 0x00315EB6
		public Button.ButtonClickedEvent OnSubmit
		{
			get
			{
				return this._submitButton.onClick;
			}
			set
			{
				this._submitButton.onClick = value;
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06007459 RID: 29785 RVA: 0x00317AC4 File Offset: 0x00315EC4
		// (set) Token: 0x0600745A RID: 29786 RVA: 0x00317AD1 File Offset: 0x00315ED1
		public Button.ButtonClickedEvent OnCancel
		{
			get
			{
				return this._cancelButton.onClick;
			}
			set
			{
				this._cancelButton.onClick = value;
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x0600745B RID: 29787 RVA: 0x00317ADF File Offset: 0x00315EDF
		// (set) Token: 0x0600745C RID: 29788 RVA: 0x00317AE7 File Offset: 0x00315EE7
		public Action OnClosed { get; set; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x0600745D RID: 29789 RVA: 0x00317AF0 File Offset: 0x00315EF0
		public Image SelectedIcon
		{
			[CompilerGenerated]
			get
			{
				return this._selectedIcon;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x0600745E RID: 29790 RVA: 0x00317AF8 File Offset: 0x00315EF8
		// (set) Token: 0x0600745F RID: 29791 RVA: 0x00317B00 File Offset: 0x00315F00
		public StuffItem Item { get; set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06007460 RID: 29792 RVA: 0x00317B09 File Offset: 0x00315F09
		// (set) Token: 0x06007461 RID: 29793 RVA: 0x00317B11 File Offset: 0x00315F11
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
				if (this._count < 1)
				{
					this._count = 1;
				}
				this.UpdateInteraction();
			}
		}

		// Token: 0x06007462 RID: 29794 RVA: 0x00317B33 File Offset: 0x00315F33
		private void UpdateInteraction()
		{
			this._addButton.interactable = (this._count < this.Item.Count);
			this._subButton.interactable = (this._count > 1);
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x00317B68 File Offset: 0x00315F68
		protected override void Start()
		{
			this._addButton.onClick.AddListener(delegate()
			{
				this._dropdown.Value++;
			});
			this._subButton.onClick.AddListener(delegate()
			{
				this._dropdown.Value--;
			});
			this._dropdown.OnValueChanged.AddListener(delegate(int x)
			{
				this.Count = x + 1;
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			base.Start();
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x00317C38 File Offset: 0x00316038
		public void Open()
		{
			this._dropdown.Options = (from x in Enumerable.Range(1, this.Item.Count)
			select new OptimizedDropdown.OptionData(x.ToString())).ToList<OptimizedDropdown.OptionData>();
			this._canvasGroup.blocksRaycasts = true;
			if (this._subscriber != null)
			{
				this._subscriber.Dispose();
			}
			this._subscriber = ObservableEasing.EaseOutQuint(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = x.Value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this._canvasGroup.interactable = true;
			});
			this._count = 1;
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x00317D04 File Offset: 0x00316104
		public void Close()
		{
			this._canvasGroup.interactable = false;
			if (this._subscriber != null)
			{
				this._subscriber.Dispose();
			}
			this._subscriber = ObservableEasing.EaseInQuint(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = 1f - x.Value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this._canvasGroup.blocksRaycasts = false;
				Action onClosed = this.OnClosed;
				if (onClosed != null)
				{
					onClosed();
				}
			});
			this._count = 0;
		}

		// Token: 0x06007466 RID: 29798 RVA: 0x00317D90 File Offset: 0x00316190
		public override void OnInputMoveDirection(MoveDirection moveDir)
		{
			if (moveDir != MoveDirection.Left)
			{
				if (moveDir == MoveDirection.Right)
				{
					if (this._addButton != null)
					{
						if (this._addButton.IsActive() || this._addButton.IsInteractable())
						{
							this._addButton.onClick.Invoke();
						}
					}
				}
			}
			else if (this._subButton != null)
			{
				if (this._subButton.IsActive() || this._subButton.IsInteractable())
				{
					this._subButton.onClick.Invoke();
				}
			}
		}

		// Token: 0x06007467 RID: 29799 RVA: 0x00317E48 File Offset: 0x00316248
		private void OnInputSubmit()
		{
			if (this._submitButton != null)
			{
				if (this._submitButton.IsActive() && this._submitButton.IsInteractable())
				{
					this._submitButton.onClick.Invoke();
				}
			}
		}

		// Token: 0x06007468 RID: 29800 RVA: 0x00317E9C File Offset: 0x0031629C
		private void OnInputCancel()
		{
			if (this._cancelButton != null)
			{
				if (this._cancelButton.IsActive() && this._cancelButton.IsInteractable())
				{
					this._cancelButton.onClick.Invoke();
				}
			}
		}

		// Token: 0x04005F13 RID: 24339
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005F14 RID: 24340
		[SerializeField]
		private Button _addButton;

		// Token: 0x04005F15 RID: 24341
		[SerializeField]
		private Button _subButton;

		// Token: 0x04005F16 RID: 24342
		[SerializeField]
		private Button _submitButton;

		// Token: 0x04005F17 RID: 24343
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x04005F19 RID: 24345
		[SerializeField]
		private Image _selectedIcon;

		// Token: 0x04005F1A RID: 24346
		[SerializeField]
		private OptimizedDropdown _dropdown;

		// Token: 0x04005F1C RID: 24348
		private int _count;

		// Token: 0x04005F1D RID: 24349
		private IDisposable _subscriber;
	}
}

using System;
using AIProject.UI;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AIProject.Scene
{
	// Token: 0x02000A35 RID: 2613
	public class ExitScene : MenuUIBehaviour
	{
		// Token: 0x06004DD5 RID: 19925 RVA: 0x001DD38B File Offset: 0x001DB78B
		private void DisableRaycast()
		{
			this._runButton.DisableRaycast();
			this._cancelButton.DisableRaycast();
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x001DD3A4 File Offset: 0x001DB7A4
		protected override void Awake()
		{
			if (Singleton<Game>.IsInstance())
			{
				if (Singleton<Game>.Instance.Dialog != null)
				{
					Singleton<Game>.Instance.DestroyDialog();
				}
				Singleton<Game>.Instance.ExitScene = this;
			}
			this._timeScale = Time.timeScale;
			Time.timeScale = 0f;
			if (Singleton<Manager.Input>.IsInstance())
			{
				this._validType = Singleton<Manager.Input>.Instance.State;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				Singleton<Manager.Input>.Instance.SetupState();
			}
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x001DD42C File Offset: 0x001DB82C
		protected override void Start()
		{
			this._runButton.AddListener(delegate
			{
				this.Exit();
			});
			this._runButton.AddListener(delegate
			{
				if (Singleton<Manager.Resources>.IsInstance())
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				}
			});
			this._runButton.AddListener(delegate
			{
				this.DisableRaycast();
			});
			this._cancelButton.AddListener(delegate
			{
				this.Cancel();
			});
			this._cancelButton.AddListener(delegate
			{
				if (Singleton<Manager.Resources>.IsInstance())
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				}
			});
			this._cancelButton.AddListener(delegate
			{
				this.DisableRaycast();
			});
			this.Open();
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand();
			actionIDDownCommand.ActionID = ActionID.Submit;
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand();
			actionIDDownCommand2.ActionID = ActionID.Cancel;
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			this._selectedID.Subscribe(delegate(int x)
			{
				this.UpdateSelectedFrame(x);
			});
			this.UpdateSelectedFrame(1);
			this._runButton.OnPointerEnterEvent.AddListener(delegate()
			{
				this._selectedID.Value = 0;
			});
			this._cancelButton.OnPointerEnterEvent.AddListener(delegate()
			{
				this._selectedID.Value = 1;
			});
			base.Start();
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x001DD5AC File Offset: 0x001DB9AC
		private void UpdateSelectedFrame(int x)
		{
			this._runButton.IsActiveSelectedFrame = (x == 0);
			this._cancelButton.IsActiveSelectedFrame = (x == 1);
			if (x == 0)
			{
				this._selectedButton = this._runButton;
			}
			else
			{
				this._selectedButton = this._cancelButton;
			}
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x001DD5FA File Offset: 0x001DB9FA
		protected override void OnDisable()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.ExitScene = null;
			}
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x001DD614 File Offset: 0x001DBA14
		private void Open()
		{
			this._panelCanvasGroup.blocksRaycasts = false;
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				CanvasGroup backgroundCanvasGroup = this._backgroundCanvasGroup;
				float value = x.Value;
				this._panelCanvasGroup.alpha = value;
				backgroundCanvasGroup.alpha = value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this._panelCanvasGroup.blocksRaycasts = true;
			});
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x001DD67C File Offset: 0x001DBA7C
		private void Close(Action onCompleted)
		{
			this._panelCanvasGroup.blocksRaycasts = false;
			Time.timeScale = this._timeScale;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Singleton<Manager.Input>.Instance.ReserveState(this._validType);
				Singleton<Manager.Input>.Instance.SetupState();
			}
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				CanvasGroup backgroundCanvasGroup = this._backgroundCanvasGroup;
				float alpha = 1f - x.Value;
				this._panelCanvasGroup.alpha = alpha;
				backgroundCanvasGroup.alpha = alpha;
			}, delegate(Exception ex)
			{
			}, onCompleted);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x001DD705 File Offset: 0x001DBB05
		private void Exit()
		{
			this.Close(delegate
			{
				Singleton<Scene>.Instance.GameEnd(false);
				Singleton<Scene>.Instance.isSkipGameExit = false;
			});
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x001DD72A File Offset: 0x001DBB2A
		private void Cancel()
		{
			this.Close(delegate
			{
				UnityEngine.Object.Destroy(base.gameObject);
				Singleton<Scene>.Instance.isGameEndCheck = true;
				Singleton<Scene>.Instance.isSkipGameExit = false;
				GC.Collect();
				UnityEngine.Resources.UnloadUnusedAssets();
			});
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x001DD73E File Offset: 0x001DBB3E
		private void OnInputSubmit()
		{
			this._selectedButton.Invoke();
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x001DD74B File Offset: 0x001DBB4B
		private void OnInputCancel()
		{
			this._cancelButton.Invoke();
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x001DD758 File Offset: 0x001DBB58
		public override void OnInputMoveDirection(MoveDirection moveDir)
		{
			if (moveDir != MoveDirection.Left)
			{
				if (moveDir == MoveDirection.Right)
				{
					this._selectedID.Value++;
					if (this._selectedID.Value > 1)
					{
						this._selectedID.Value = 0;
					}
				}
			}
			else
			{
				this._selectedID.Value--;
				if (this._selectedID.Value < 0)
				{
					this._selectedID.Value = 1;
				}
			}
		}

		// Token: 0x0400470A RID: 18186
		[SerializeField]
		private ConfirmationButton _runButton;

		// Token: 0x0400470B RID: 18187
		[SerializeField]
		private ConfirmationButton _cancelButton;

		// Token: 0x0400470C RID: 18188
		[SerializeField]
		private CanvasGroup _backgroundCanvasGroup;

		// Token: 0x0400470D RID: 18189
		[SerializeField]
		private CanvasGroup _panelCanvasGroup;

		// Token: 0x0400470E RID: 18190
		private float _timeScale = 1f;

		// Token: 0x0400470F RID: 18191
		private Manager.Input.ValidType _validType;

		// Token: 0x04004710 RID: 18192
		private IntReactiveProperty _selectedID = new IntReactiveProperty(1);

		// Token: 0x04004711 RID: 18193
		private ConfirmationButton _selectedButton;
	}
}

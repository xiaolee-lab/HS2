using System;
using AIProject.UI;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.Scene
{
	// Token: 0x02000A33 RID: 2611
	public class ConfirmScene : MenuUIBehaviour
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x001DC44C File Offset: 0x001DA84C
		// (set) Token: 0x06004D9A RID: 19866 RVA: 0x001DC454 File Offset: 0x001DA854
		public float TimeScale
		{
			get
			{
				return this._timeScale;
			}
			set
			{
				this._timeScale = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x001DC45D File Offset: 0x001DA85D
		// (set) Token: 0x06004D9C RID: 19868 RVA: 0x001DC464 File Offset: 0x001DA864
		public static Sprite Sprite { get; set; }

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06004D9D RID: 19869 RVA: 0x001DC46C File Offset: 0x001DA86C
		// (set) Token: 0x06004D9E RID: 19870 RVA: 0x001DC473 File Offset: 0x001DA873
		public static string Sentence { get; set; }

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004D9F RID: 19871 RVA: 0x001DC47B File Offset: 0x001DA87B
		// (set) Token: 0x06004DA0 RID: 19872 RVA: 0x001DC482 File Offset: 0x001DA882
		public static Func<string> YesTextFunc { get; set; }

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x001DC48A File Offset: 0x001DA88A
		// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x001DC491 File Offset: 0x001DA891
		public static Func<string> NoTextFunc { get; set; }

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x001DC499 File Offset: 0x001DA899
		// (set) Token: 0x06004DA4 RID: 19876 RVA: 0x001DC4A0 File Offset: 0x001DA8A0
		public static Action OnClickedYes { get; set; }

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x001DC4A8 File Offset: 0x001DA8A8
		// (set) Token: 0x06004DA6 RID: 19878 RVA: 0x001DC4AF File Offset: 0x001DA8AF
		public static Action OnClickedNo { get; set; }

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x001DC4B7 File Offset: 0x001DA8B7
		// (set) Token: 0x06004DA8 RID: 19880 RVA: 0x001DC4BE File Offset: 0x001DA8BE
		public static bool CloseImmediately { get; set; }

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x001DC4C6 File Offset: 0x001DA8C6
		// (set) Token: 0x06004DAA RID: 19882 RVA: 0x001DC4CD File Offset: 0x001DA8CD
		public static float? BackAlpha { get; set; }

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x001DC4D5 File Offset: 0x001DA8D5
		// (set) Token: 0x06004DAC RID: 19884 RVA: 0x001DC4DD File Offset: 0x001DA8DD
		private Color _backColor { get; set; }

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x001DC4E6 File Offset: 0x001DA8E6
		// (set) Token: 0x06004DAE RID: 19886 RVA: 0x001DC4ED File Offset: 0x001DA8ED
		public static Vector2? Offset { get; set; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x001DC4F5 File Offset: 0x001DA8F5
		// (set) Token: 0x06004DB0 RID: 19888 RVA: 0x001DC4FD File Offset: 0x001DA8FD
		private Vector2 _offset { get; set; }

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004DB1 RID: 19889 RVA: 0x001DC506 File Offset: 0x001DA906
		// (set) Token: 0x06004DB2 RID: 19890 RVA: 0x001DC50E File Offset: 0x001DA90E
		private RectTransform _offsetTarget { get; set; }

		// Token: 0x06004DB3 RID: 19891 RVA: 0x001DC517 File Offset: 0x001DA917
		private void DisableRaycast()
		{
			this._runButton.DisableRaycast();
			this._cancelButton.DisableRaycast();
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x001DC530 File Offset: 0x001DA930
		protected override void Awake()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.Dialog = this;
			}
			this._timeScale = Time.timeScale;
			Time.timeScale = 0f;
			this._backColor = this._panel.color;
			this._offsetTarget = this._back.rectTransform;
			this._offset = this._offsetTarget.anchoredPosition;
			if (Singleton<Manager.Input>.IsInstance())
			{
				this._validType = Singleton<Manager.Input>.Instance.State;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				Singleton<Manager.Input>.Instance.SetupState();
			}
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x001DC5CC File Offset: 0x001DA9CC
		protected override void Start()
		{
			this._back.sprite = ConfirmScene.Sprite;
			this._runButton.AddListener(delegate
			{
				Action onClickedYes = ConfirmScene.OnClickedYes;
				if (onClickedYes != null)
				{
					onClickedYes();
				}
			});
			this._runButton.AddListener(delegate
			{
				this.DisableRaycast();
			});
			this._cancelButton.AddListener(delegate
			{
				this.DisableRaycast();
			});
			this._cancelButton.AddListener(delegate
			{
				Action onClickedNo = ConfirmScene.OnClickedNo;
				if (onClickedNo != null)
				{
					onClickedNo();
				}
			});
			this._runButton.AddListener(delegate
			{
				this.Close();
			});
			this._cancelButton.AddListener(delegate
			{
				this.Close();
			});
			this.Open();
			this._sentenceText.text = ConfirmScene.Sentence;
			if (ConfirmScene.YesTextFunc != null)
			{
				this._yesText.text = ConfirmScene.YesTextFunc();
			}
			if (ConfirmScene.NoTextFunc != null)
			{
				this._noText.text = ConfirmScene.NoTextFunc();
			}
			if (ConfirmScene.BackAlpha != null)
			{
				float value = ConfirmScene.BackAlpha.Value;
				Color color = this._panel.color;
				color.a = value;
				this._panel.color = color;
				this._sentenceTextBack.enabled = (value <= 0f);
			}
			else
			{
				this._sentenceTextBack.enabled = false;
			}
			if (ConfirmScene.Offset != null)
			{
				Vector2 vector = this._offsetTarget.anchoredPosition;
				vector += ConfirmScene.Offset.Value;
				this._offsetTarget.anchoredPosition = vector;
			}
			base.Start();
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand();
			actionIDDownCommand.ActionID = ActionID.Submit;
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand();
			actionIDDownCommand2.ActionID = ActionID.Cancel;
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnCancel();
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

		// Token: 0x06004DB6 RID: 19894 RVA: 0x001DC864 File Offset: 0x001DAC64
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

		// Token: 0x06004DB7 RID: 19895 RVA: 0x001DC8B4 File Offset: 0x001DACB4
		private void Open()
		{
			ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
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

		// Token: 0x06004DB8 RID: 19896 RVA: 0x001DC910 File Offset: 0x001DAD10
		private void Close()
		{
			if (ConfirmScene.CloseImmediately)
			{
				this._backgroundCanvasGroup.alpha = 0f;
				this._panelCanvasGroup.blocksRaycasts = false;
				UnityEngine.Object.Destroy(base.gameObject);
				GC.Collect();
				UnityEngine.Resources.UnloadUnusedAssets();
				return;
			}
			this._panelCanvasGroup.blocksRaycasts = false;
			ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				CanvasGroup backgroundCanvasGroup = this._backgroundCanvasGroup;
				float alpha = 1f - x.Value;
				this._panelCanvasGroup.alpha = alpha;
				backgroundCanvasGroup.alpha = alpha;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				UnityEngine.Object.Destroy(base.gameObject);
				GC.Collect();
				UnityEngine.Resources.UnloadUnusedAssets();
			});
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x001DC9B4 File Offset: 0x001DADB4
		protected override void OnDisable()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.Dialog = null;
			}
			Time.timeScale = this._timeScale;
			this._panel.color = this._backColor;
			this._offsetTarget.anchoredPosition = this._offset;
			ConfirmScene.BackAlpha = null;
			ConfirmScene.Offset = null;
			ConfirmScene.YesTextFunc = null;
			ConfirmScene.NoTextFunc = null;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Singleton<Manager.Input>.Instance.ReserveState(this._validType);
				Singleton<Manager.Input>.Instance.SetupState();
			}
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x001DCA4F File Offset: 0x001DAE4F
		protected void OnSubmit()
		{
			this._selectedButton.Invoke();
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x001DCA5C File Offset: 0x001DAE5C
		protected void OnCancel()
		{
			this._cancelButton.Invoke();
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x001DCA6C File Offset: 0x001DAE6C
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

		// Token: 0x040046E2 RID: 18146
		[SerializeField]
		private Image _panel;

		// Token: 0x040046E3 RID: 18147
		[SerializeField]
		private Image _back;

		// Token: 0x040046E4 RID: 18148
		[SerializeField]
		private Image _sentenceTextBack;

		// Token: 0x040046E5 RID: 18149
		[SerializeField]
		private Text _sentenceText;

		// Token: 0x040046E6 RID: 18150
		[SerializeField]
		private Text _yesText;

		// Token: 0x040046E7 RID: 18151
		[SerializeField]
		private Text _noText;

		// Token: 0x040046E8 RID: 18152
		[SerializeField]
		private ConfirmationButton _runButton;

		// Token: 0x040046E9 RID: 18153
		[SerializeField]
		private ConfirmationButton _cancelButton;

		// Token: 0x040046EA RID: 18154
		[SerializeField]
		private CanvasGroup _backgroundCanvasGroup;

		// Token: 0x040046EB RID: 18155
		[SerializeField]
		private CanvasGroup _panelCanvasGroup;

		// Token: 0x040046EC RID: 18156
		private float _timeScale = 1f;

		// Token: 0x040046ED RID: 18157
		private Manager.Input.ValidType _validType;

		// Token: 0x040046EE RID: 18158
		private ConfirmationButton _selectedButton;

		// Token: 0x040046F6 RID: 18166
		private IntReactiveProperty _selectedID = new IntReactiveProperty(1);
	}
}

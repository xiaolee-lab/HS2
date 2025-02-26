using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Scene;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008BB RID: 2235
	[Serializable]
	public class SystemUICtrl : UIDerived
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003A33 RID: 14899 RVA: 0x00155648 File Offset: 0x00153A48
		// (set) Token: 0x06003A34 RID: 14900 RVA: 0x00155655 File Offset: 0x00153A55
		public bool Camera
		{
			get
			{
				return this.cameraReactive.Value;
			}
			set
			{
				this.cameraReactive.Value = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003A35 RID: 14901 RVA: 0x00155663 File Offset: 0x00153A63
		// (set) Token: 0x06003A36 RID: 14902 RVA: 0x00155670 File Offset: 0x00153A70
		public bool Axis
		{
			get
			{
				return this.axisReactive.Value;
			}
			set
			{
				this.axisReactive.Value = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x0015567E File Offset: 0x00153A7E
		// (set) Token: 0x06003A38 RID: 14904 RVA: 0x0015568B File Offset: 0x00153A8B
		public bool Grid
		{
			get
			{
				return this.gridReactive.Value;
			}
			set
			{
				this.gridReactive.Value = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x00155699 File Offset: 0x00153A99
		// (set) Token: 0x06003A3A RID: 14906 RVA: 0x001556A1 File Offset: 0x00153AA1
		public bool IsMessage { get; private set; }

		// Token: 0x06003A3B RID: 14907 RVA: 0x001556AC File Offset: 0x00153AAC
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			this.buttonAdd.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				base.UICtrl.AddUICtrl.Active = !base.UICtrl.AddUICtrl.Active;
			});
			this.buttonUndo.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (Singleton<UndoRedoManager>.IsInstance())
				{
					Singleton<UndoRedoManager>.Instance.Undo();
				}
			});
			this.buttonRedo.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (Singleton<UndoRedoManager>.IsInstance())
				{
					Singleton<UndoRedoManager>.Instance.Redo();
				}
			});
			this.buttonCamera.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Camera = !this.Camera;
			});
			this.buttonAxis.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Axis = !this.Axis;
			});
			this.buttonGrid.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Grid = !this.Grid;
			});
			this.buttonSave.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Save();
			});
			this.buttonLoad.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				base.UICtrl.SaveLoadUICtrl.Open();
			});
			this.buttonReset.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				ConfirmScene.Sentence = "初期化しますか？\n" + "セットされたアイテムは削除されます。".Coloring("#DE4529FF").Size(24);
				ConfirmScene.OnClickedYes = delegate()
				{
					base.UICtrl.ListUICtrl.VirtualizingTreeView.SelectedIndex = -1;
					Singleton<Housing>.Instance.ResetObject();
					base.UICtrl.ListUICtrl.UpdateUI();
					Singleton<UndoRedoManager>.Instance.Clear();
				};
				ConfirmScene.OnClickedNo = delegate()
				{
				};
				Singleton<Game>.Instance.LoadDialog();
			});
			this.buttonEnd.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (Singleton<CraftScene>.Instance.IsShortcutUI)
				{
					return;
				}
				if (Singleton<CraftScene>.Instance.CraftInfo.IsOverlapNow)
				{
					this.IsMessage = true;
					MapUIContainer.PushMessageUI("配置に問題があるものが存在します", 2, 1, delegate()
					{
						this.IsMessage = false;
					});
					return;
				}
				ConfirmScene.Sentence = "ハウジングを終了しますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					Singleton<CraftScene>.Instance.SceneEnd();
					IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true);
					source.Subscribe(delegate(Unit __)
					{
					}, delegate()
					{
						this.EndHousing();
					});
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<CraftScene>.Instance.IsEndCheck = false;
				};
				Singleton<Game>.Instance.LoadDialog();
				Singleton<CraftScene>.Instance.IsEndCheck = true;
			});
			this.cameraReactive.Subscribe(delegate(bool _b)
			{
				this.spritesCamera.SafeProc((!_b) ? 1 : 0, delegate(Sprite _s)
				{
					this.buttonCamera.image.sprite = _s;
				});
			});
			this.axisReactive.Subscribe(delegate(bool _b)
			{
				this.spritesAxis.SafeProc((!_b) ? 1 : 0, delegate(Sprite _s)
				{
					this.buttonAxis.image.sprite = _s;
				});
				GuideObject guideObject = Singleton<GuideManager>.Instance.GuideObject;
				if (guideObject)
				{
					guideObject.visibleOutside = _b;
				}
			});
			this.gridReactive.Subscribe(delegate(bool _b)
			{
				this.spritesGrid.SafeProc((!_b) ? 1 : 0, delegate(Sprite _s)
				{
					this.buttonGrid.image.sprite = _s;
				});
				if (Singleton<GuideManager>.IsInstance())
				{
					Singleton<GuideManager>.Instance.VisibleGrid = _b;
				}
			});
			CraftCamera craftCamera = base.UICtrl.CraftCamera;
			craftCamera.NoCtrlCondition = (VirtualCameraController.NoCtrlFunc)Delegate.Combine(craftCamera.NoCtrlCondition, new VirtualCameraController.NoCtrlFunc(this.NoCameraCtrl));
			if (Singleton<UndoRedoManager>.IsInstance())
			{
				Singleton<UndoRedoManager>.Instance.CanUndoChange += this.CanUndoChange;
				Singleton<UndoRedoManager>.Instance.CanRedoChange += this.CanRedoChange;
			}
			this.buttonUndo.interactable = false;
			this.buttonRedo.interactable = false;
			if (_tutorial)
			{
				this.buttonSave.interactable = false;
				this.buttonLoad.interactable = false;
			}
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x001558E5 File Offset: 0x00153CE5
		public override void UpdateUI()
		{
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x001558E8 File Offset: 0x00153CE8
		[DebuggerStepThrough]
		private void EndHousing()
		{
			SystemUICtrl.<EndHousing>c__async0 <EndHousing>c__async;
			<EndHousing>c__async.$builder = AsyncVoidMethodBuilder.Create();
			<EndHousing>c__async.$builder.Start<SystemUICtrl.<EndHousing>c__async0>(ref <EndHousing>c__async);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0015590F File Offset: 0x00153D0F
		public void Save()
		{
			if (this.IsMessage)
			{
				return;
			}
			Singleton<CraftScene>.Instance.Capture(delegate(byte[] _png)
			{
				int sizeType = Singleton<Housing>.Instance.GetSizeType(Singleton<CraftScene>.Instance.HousingID);
				DateTime now = DateTime.Now;
				string str = string.Format("{0}_{1:00}{2:00}_{3:00}{4:00}_{5:00}_{6:000}.png", new object[]
				{
					now.Year,
					now.Month,
					now.Day,
					now.Hour,
					now.Minute,
					now.Second,
					now.Millisecond
				});
				string path = UserData.Create(string.Format("{0}{1:00}", "housing/", sizeType + 1)) + str;
				Singleton<CraftScene>.Instance.CraftInfo.Save(path, _png);
				this.IsMessage = true;
				MapUIContainer.PushMessageUI("保存しました", 0, 1, delegate()
				{
					this.IsMessage = false;
				});
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Save);
			});
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00155934 File Offset: 0x00153D34
		public bool NoCameraCtrl()
		{
			if (this.Camera)
			{
				return false;
			}
			bool flag = UnityEngine.Input.GetKey(KeyCode.LeftControl) | UnityEngine.Input.GetKey(KeyCode.RightControl);
			return !flag;
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00155968 File Offset: 0x00153D68
		private void CanUndoChange(object _sender, CanhangeArgs _e)
		{
			this.buttonUndo.interactable = _e.Can;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0015597B File Offset: 0x00153D7B
		private void CanRedoChange(object _sender, CanhangeArgs _e)
		{
			this.buttonRedo.interactable = _e.Can;
		}

		// Token: 0x04003991 RID: 14737
		[SerializeField]
		private Button buttonAdd;

		// Token: 0x04003992 RID: 14738
		[SerializeField]
		private Button buttonUndo;

		// Token: 0x04003993 RID: 14739
		[SerializeField]
		private Button buttonRedo;

		// Token: 0x04003994 RID: 14740
		[SerializeField]
		private Button buttonCamera;

		// Token: 0x04003995 RID: 14741
		[SerializeField]
		private Button buttonAxis;

		// Token: 0x04003996 RID: 14742
		[SerializeField]
		private Button buttonGrid;

		// Token: 0x04003997 RID: 14743
		[SerializeField]
		private Button buttonSave;

		// Token: 0x04003998 RID: 14744
		[SerializeField]
		private Button buttonLoad;

		// Token: 0x04003999 RID: 14745
		[SerializeField]
		private Button buttonReset;

		// Token: 0x0400399A RID: 14746
		[SerializeField]
		private Button buttonEnd;

		// Token: 0x0400399B RID: 14747
		[SerializeField]
		[Header("カメラ関係")]
		private Sprite[] spritesCamera;

		// Token: 0x0400399C RID: 14748
		[SerializeField]
		[Header("操作軸関係")]
		private Sprite[] spritesAxis;

		// Token: 0x0400399D RID: 14749
		[SerializeField]
		[Header("グリッド関係")]
		private Sprite[] spritesGrid;

		// Token: 0x0400399E RID: 14750
		private BoolReactiveProperty cameraReactive = new BoolReactiveProperty(true);

		// Token: 0x0400399F RID: 14751
		private BoolReactiveProperty axisReactive = new BoolReactiveProperty(true);

		// Token: 0x040039A0 RID: 14752
		private BoolReactiveProperty gridReactive = new BoolReactiveProperty(true);
	}
}

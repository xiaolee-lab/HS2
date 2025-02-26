using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001238 RID: 4664
	public class ShortcutKeyCtrl : MonoBehaviour
	{
		// Token: 0x06009972 RID: 39282 RVA: 0x003F30AE File Offset: 0x003F14AE
		public ShortcutKeyCtrl()
		{
			KeyCode[] array = new KeyCode[10];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.$field-D6BA6896865F0AC0004C331AF3FEED735F78F322).FieldHandle);
			this.cameraKey = array;
			base..ctor();
		}

		// Token: 0x06009973 RID: 39283 RVA: 0x003F30D0 File Offset: 0x003F14D0
		private void Notification(int _id)
		{
			NotificationScene.spriteMessage = this.sprites[_id];
			NotificationScene.waitTime = 2f;
			NotificationScene.width = 416f;
			NotificationScene.height = 160f;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioNotification",
				isAdd = true
			}, false);
		}

		// Token: 0x06009974 RID: 39284 RVA: 0x003F312C File Offset: 0x003F152C
		private void Update()
		{
			if (!Singleton<Studio>.IsInstance())
			{
				return;
			}
			if (Singleton<Studio>.Instance.isInputNow)
			{
				return;
			}
			if (!Singleton<Scene>.IsInstance())
			{
				return;
			}
			if (Singleton<Scene>.Instance.AddSceneName != string.Empty)
			{
				return;
			}
			bool flag = UnityEngine.Input.GetKey(KeyCode.LeftControl) | UnityEngine.Input.GetKey(KeyCode.RightControl);
			bool flag2 = UnityEngine.Input.GetKey(KeyCode.LeftShift) | UnityEngine.Input.GetKey(KeyCode.RightShift);
			if (flag2 & UnityEngine.Input.GetKeyDown(KeyCode.Z))
			{
				if (Singleton<UndoRedoManager>.Instance.CanRedo)
				{
					Singleton<UndoRedoManager>.Instance.Redo();
				}
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
			{
				if (Singleton<UndoRedoManager>.Instance.CanUndo)
				{
					Singleton<UndoRedoManager>.Instance.Undo();
				}
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.F))
			{
				TreeNodeObject selectNode = this.treeNodeCtrl.selectNode;
				if (!(selectNode == null))
				{
					ObjectCtrlInfo objectCtrlInfo = null;
					if (Singleton<Studio>.Instance.dicInfo.TryGetValue(selectNode, out objectCtrlInfo))
					{
						this.cameraControl.targetPos = objectCtrlInfo.guideObject.transform.position;
					}
				}
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.C))
			{
				GuideObject[] selectObjects = Singleton<GuideObjectManager>.Instance.selectObjects;
				if (!selectObjects.IsNullOrEmpty<GuideObject>())
				{
					List<GuideCommand.EqualsInfo> list = new List<GuideCommand.EqualsInfo>();
					foreach (GuideObject guideObject in selectObjects)
					{
						if (guideObject.enablePos)
						{
							list.Add(guideObject.SetWorldPos(this.cameraControl.targetPos));
						}
					}
					if (!list.IsNullOrEmpty<GuideCommand.EqualsInfo>())
					{
						Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.MoveEqualsCommand(list.ToArray()));
					}
				}
			}
			else if (flag && UnityEngine.Input.GetKeyDown(KeyCode.S))
			{
				this.systemButtonCtrl.OnClickSave();
			}
			else if (flag && UnityEngine.Input.GetKeyDown(KeyCode.D))
			{
				Singleton<Studio>.Instance.Duplicate();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
			{
				this.workspaceCtrl.OnClickDelete();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.W))
			{
				Singleton<GuideObjectManager>.Instance.mode = 0;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
			{
				Singleton<GuideObjectManager>.Instance.mode = 1;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.R))
			{
				Singleton<GuideObjectManager>.Instance.mode = 2;
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				this.studioScene.OnClickAxis();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.J))
			{
				this.studioScene.OnClickAxisTrans();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.K))
			{
				this.studioScene.OnClickAxisCenter();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.F11))
			{
				this.gameScreenShot.Capture(string.Empty);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
			{
				if (Singleton<GameCursor>.IsInstance())
				{
					Singleton<GameCursor>.Instance.SetCursorLock(false);
				}
				Singleton<Scene>.Instance.LoadReserve(new Scene.Data
				{
					levelName = "StudioShortcutMenu",
					isAdd = true
				}, false);
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				if (Singleton<GameCursor>.IsInstance())
				{
					Singleton<GameCursor>.Instance.SetCursorLock(false);
				}
				Singleton<Scene>.Instance.GameEnd(true);
			}
			else
			{
				bool flag3 = false;
				for (int j = 0; j < 10; j++)
				{
					if (UnityEngine.Input.GetKeyDown(this.cameraKey[j]))
					{
						this.studioScene.OnClickLoadCamera(j);
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					if (UnityEngine.Input.GetKeyDown(KeyCode.H))
					{
						Singleton<Studio>.Instance.cameraSelector.NextCamera();
					}
				}
			}
		}

		// Token: 0x04007ACD RID: 31437
		[SerializeField]
		private StudioScene studioScene;

		// Token: 0x04007ACE RID: 31438
		[SerializeField]
		private SystemButtonCtrl systemButtonCtrl;

		// Token: 0x04007ACF RID: 31439
		[SerializeField]
		private WorkspaceCtrl workspaceCtrl;

		// Token: 0x04007AD0 RID: 31440
		[SerializeField]
		private CameraControl cameraControl;

		// Token: 0x04007AD1 RID: 31441
		[SerializeField]
		private TreeNodeCtrl treeNodeCtrl;

		// Token: 0x04007AD2 RID: 31442
		[SerializeField]
		private GameScreenShot gameScreenShot;

		// Token: 0x04007AD3 RID: 31443
		[SerializeField]
		private Sprite[] sprites;

		// Token: 0x04007AD4 RID: 31444
		private readonly KeyCode[] cameraKey;
	}
}

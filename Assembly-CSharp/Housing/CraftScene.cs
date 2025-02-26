using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008D7 RID: 2263
	public class CraftScene : Singleton<CraftScene>
	{
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x0015C09D File Offset: 0x0015A49D
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x0015C0A5 File Offset: 0x0015A4A5
		public bool IsInit { get; private set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x0015C0AE File Offset: 0x0015A4AE
		public bool IsInputNow
		{
			[CompilerGenerated]
			get
			{
				return this.inputFieldNow && this.inputFieldNow.isFocused;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x0015C0D1 File Offset: 0x0015A4D1
		public UICtrl UICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.uiCtrl;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x0015C0D9 File Offset: 0x0015A4D9
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0015C0E1 File Offset: 0x0015A4E1
		public WorkingUICtrl WorkingUICtrl { get; private set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x0015C0EA File Offset: 0x0015A4EA
		public int HousingID
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Map>.IsInstance()) ? 0 : Singleton<Map>.Instance.HousingID;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x0015C106 File Offset: 0x0015A506
		public CraftInfo CraftInfo
		{
			[CompilerGenerated]
			get
			{
				return this.craftInfo;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0015C10E File Offset: 0x0015A50E
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x0015C116 File Offset: 0x0015A516
		public bool DisplayTutorial { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0015C11F File Offset: 0x0015A51F
		public BoxCollider[] TestColliders
		{
			[CompilerGenerated]
			get
			{
				return this.testColliders;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06003B74 RID: 15220 RVA: 0x0015C127 File Offset: 0x0015A527
		// (set) Token: 0x06003B75 RID: 15221 RVA: 0x0015C12F File Offset: 0x0015A52F
		public bool IsEnd { get; private set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x0015C138 File Offset: 0x0015A538
		// (set) Token: 0x06003B77 RID: 15223 RVA: 0x0015C140 File Offset: 0x0015A540
		public bool IsEndCheck { get; set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x0015C149 File Offset: 0x0015A549
		public bool IsDialog
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Game>.IsInstance() && (Singleton<Game>.Instance.Dialog || Singleton<Game>.Instance.ExitScene);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x0015C17E File Offset: 0x0015A57E
		public bool IsGuide
		{
			[CompilerGenerated]
			get
			{
				return Singleton<GuideManager>.IsInstance() && Singleton<GuideManager>.Instance.IsGuide;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x0015C197 File Offset: 0x0015A597
		public bool IsShortcutUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Game>.IsInstance() && Singleton<Game>.Instance.MapShortcutUI;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x0015C1B5 File Offset: 0x0015A5B5
		public bool IsWorkingUI
		{
			[CompilerGenerated]
			get
			{
				return this.WorkingUICtrl != null && this.WorkingUICtrl.Visible;
			}
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x0015C1D9 File Offset: 0x0015A5D9
		public void SceneEnd()
		{
			this.IsEnd = true;
			this.uiCtrl.ListUICtrl.Select(null);
			this.imageEnd.enabled = true;
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x0015C1FF File Offset: 0x0015A5FF
		public void SelectInputField(InputField _input)
		{
			this.inputFieldNow = _input;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x0015C208 File Offset: 0x0015A608
		public void DeselectInputField(InputField _input)
		{
			if (this.inputFieldNow == _input)
			{
				this.inputFieldNow = null;
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x0015C224 File Offset: 0x0015A624
		public void Capture(Action<byte[]> _onCompleted)
		{
			if (this.cameras.IsNullOrEmpty<Camera>())
			{
				Action<byte[]> onCompleted = _onCompleted;
				if (onCompleted != null)
				{
					onCompleted(null);
				}
				return;
			}
			Observable.FromCoroutine(new Func<IEnumerator>(this.CaptureFunc), false).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Action<byte[]> onCompleted2 = _onCompleted;
				if (onCompleted2 != null)
				{
					onCompleted2(this.bytesCapture);
				}
			}).AddTo(this);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x0015C2B4 File Offset: 0x0015A6B4
		private IEnumerator CaptureFunc()
		{
			yield return new WaitForEndOfFrame();
			Texture2D screenShot = new Texture2D(this.width, this.height, TextureFormat.RGB24, false);
			int anti = (QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1;
			RenderTexture rt = RenderTexture.GetTemporary(screenShot.width, screenShot.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, anti);
			bool grid = Singleton<GuideManager>.Instance.VisibleGrid;
			Singleton<GuideManager>.Instance.VisibleGrid = false;
			Graphics.SetRenderTarget(rt);
			GL.Clear(true, true, Color.black);
			Graphics.SetRenderTarget(null);
			bool sRgbWrite = GL.sRGBWrite;
			GL.sRGBWrite = true;
			foreach (Camera camera in this.cameras)
			{
				if (!(null == camera))
				{
					bool enabled = camera.enabled;
					RenderTexture targetTexture = camera.targetTexture;
					Rect rect = camera.rect;
					camera.enabled = true;
					camera.targetTexture = rt;
					camera.Render();
					camera.targetTexture = targetTexture;
					camera.rect = rect;
					camera.enabled = enabled;
				}
			}
			GL.sRGBWrite = sRgbWrite;
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0f, 0f, (float)screenShot.width, (float)screenShot.height), 0, 0);
			screenShot.Apply();
			RenderTexture.active = null;
			this.bytesCapture = screenShot.EncodeToPNG();
			RenderTexture.ReleaseTemporary(rt);
			UnityEngine.Object.Destroy(screenShot);
			screenShot = null;
			Singleton<GuideManager>.Instance.VisibleGrid = grid;
			yield return null;
			yield break;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x0015C2D0 File Offset: 0x0015A6D0
		private bool CheckAddScene()
		{
			if (!Singleton<Scene>.IsInstance())
			{
				return false;
			}
			bool flag = false;
			foreach (string value in Singleton<Scene>.Instance.NowSceneNames)
			{
				flag |= this.addSceneNames.Contains(value);
			}
			return flag;
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x0015C348 File Offset: 0x0015A748
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			this.addSceneNames = new string[]
			{
				"Config",
				"Exit"
			};
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x0015C374 File Offset: 0x0015A774
		private IEnumerator Start()
		{
			this.craftInfo = null;
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.WorldData != null && Singleton<Map>.IsInstance())
			{
				Singleton<Game>.Instance.WorldData.HousingData.CraftInfos.TryGetValue(Singleton<Map>.Instance.HousingID, out this.craftInfo);
			}
			Transform tRoot = this.craftInfo.ObjRoot.transform;
			base.transform.SetPositionAndRotation(tRoot.position, tRoot.rotation);
			Singleton<Housing>.Instance.SetCraftInfo(this.craftInfo, false);
			this.craftInfo.SetOverlapColliders(true);
			bool tutorial = Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Environment.TutorialProgress <= 13;
			yield return this.uiCtrl.Init(this.craftCamera, tutorial);
			CraftCamera craftCamera = this.craftCamera;
			craftCamera.NoCtrlCondition = (VirtualCameraController.NoCtrlFunc)Delegate.Combine(craftCamera.NoCtrlCondition, new VirtualCameraController.NoCtrlFunc(Singleton<GuideManager>.Instance.NoCameraCtrl));
			CraftCamera craftCamera2 = this.craftCamera;
			craftCamera2.KeyCondition = (VirtualCameraController.NoCtrlFunc)Delegate.Combine(craftCamera2.KeyCondition, new VirtualCameraController.NoCtrlFunc(() => !this.IsEnd && !this.IsInputNow && !this.IsDialog && !this.IsShortcutUI && !this.CheckAddScene() && !this.IsWorkingUI));
			if (this.DisplayTutorial)
			{
				CraftCamera craftCamera3 = this.craftCamera;
				craftCamera3.NoCtrlCondition = (VirtualCameraController.NoCtrlFunc)Delegate.Combine(craftCamera3.NoCtrlCondition, new VirtualCameraController.NoCtrlFunc(this.get_DisplayTutorial));
				CraftCamera craftCamera4 = this.craftCamera;
				craftCamera4.KeyCondition = (VirtualCameraController.NoCtrlFunc)Delegate.Combine(craftCamera4.KeyCondition, new VirtualCameraController.NoCtrlFunc(() => !this.DisplayTutorial));
			}
			Singleton<GuideManager>.Instance.Init(this.craftInfo.LimitSize);
			Singleton<GuideManager>.Instance.TransformRoot = tRoot;
			this.imageEnd.enabled = false;
			this.uiCtrl.ListUICtrl.UpdateUI();
			Vector3 limit = this.craftInfo.LimitSize;
			Vector3 limithalf = limit * 0.5f;
			this.craftCamera.limitAreaMin = new Vector3(-limithalf.x, 2.5f, -limithalf.z);
			this.craftCamera.limitAreaMax = new Vector3(limithalf.x, limit.y, limithalf.z);
			GameObject gameObject = CommonLib.LoadAsset<GameObject>("housing/base/08.unity3d", "Canvas Working", true, string.Empty);
			gameObject.transform.SetParent(base.transform);
			this.WorkingUICtrl = gameObject.GetComponent<WorkingUICtrl>();
			this.IsInit = true;
			yield break;
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x0015C390 File Offset: 0x0015A790
		private void Update()
		{
			if (this.DisplayTutorial)
			{
				return;
			}
			bool flag = UnityEngine.Input.GetKey(KeyCode.LeftControl) | UnityEngine.Input.GetKey(KeyCode.RightControl);
			if (this.uiCtrl.SaveLoadUICtrl.Visible)
			{
				return;
			}
			if (Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			if (this.IsEndCheck || this.IsEnd || this.IsInputNow || this.IsGuide || this.IsDialog || this.IsShortcutUI || this.CheckAddScene())
			{
				return;
			}
			if (this.IsWorkingUI)
			{
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
			{
				Singleton<Game>.Instance.LoadShortcut(4, null);
				return;
			}
			if (!flag)
			{
				return;
			}
			bool flag2 = UnityEngine.Input.GetKey(KeyCode.LeftShift) | UnityEngine.Input.GetKey(KeyCode.RightShift);
			if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
			{
				if (flag2)
				{
					if (Singleton<UndoRedoManager>.IsInstance())
					{
						Singleton<UndoRedoManager>.Instance.Redo();
					}
				}
				else if (Singleton<UndoRedoManager>.IsInstance())
				{
					Singleton<UndoRedoManager>.Instance.Undo();
				}
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				this.uiCtrl.SystemUICtrl.Axis = !this.uiCtrl.SystemUICtrl.Axis;
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				this.uiCtrl.SystemUICtrl.Grid = !this.uiCtrl.SystemUICtrl.Grid;
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.S))
			{
				this.uiCtrl.SystemUICtrl.Save();
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.D))
			{
				this.uiCtrl.ListUICtrl.Duplicate();
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.F))
			{
				ObjectCtrl[] selectObjects = Singleton<Selection>.Instance.SelectObjects;
				ObjectCtrl objectCtrl;
				if (!selectObjects.IsNullOrEmpty<ObjectCtrl>())
				{
					objectCtrl = (from v in selectObjects
					where v.GameObject != null
					select v).FirstOrDefault((ObjectCtrl v) => v.Kind == 0);
				}
				else
				{
					objectCtrl = null;
				}
				ObjectCtrl objectCtrl2 = objectCtrl;
				if (objectCtrl2 != null)
				{
					this.craftCamera.TargetPos = this.craftCamera.LookAt.parent.InverseTransformPoint(objectCtrl2.Position);
				}
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.G))
			{
				this.overlapObjectCtrl = this.craftInfo.FindOverlapObject(this.overlapObjectCtrl);
				if (this.overlapObjectCtrl != null)
				{
					this.uiCtrl.ListUICtrl.Select(this.overlapObjectCtrl);
				}
				return;
			}
		}

		// Token: 0x04003A45 RID: 14917
		[SerializeField]
		private UICtrl uiCtrl;

		// Token: 0x04003A46 RID: 14918
		[SerializeField]
		private CraftCamera craftCamera;

		// Token: 0x04003A47 RID: 14919
		[SerializeField]
		private BoxCollider[] testColliders;

		// Token: 0x04003A48 RID: 14920
		[SerializeField]
		[Header("画像作成関係")]
		private Camera[] cameras;

		// Token: 0x04003A49 RID: 14921
		[SerializeField]
		private int width = 320;

		// Token: 0x04003A4A RID: 14922
		[SerializeField]
		private int height = 180;

		// Token: 0x04003A4B RID: 14923
		[SerializeField]
		[Header("終了時処理関係")]
		private Image imageEnd;

		// Token: 0x04003A51 RID: 14929
		private CraftInfo craftInfo;

		// Token: 0x04003A52 RID: 14930
		[ReadOnly]
		private InputField inputFieldNow;

		// Token: 0x04003A53 RID: 14931
		private byte[] bytesCapture;

		// Token: 0x04003A54 RID: 14932
		private ObjectCtrl overlapObjectCtrl;

		// Token: 0x04003A55 RID: 14933
		private string[] addSceneNames;
	}
}

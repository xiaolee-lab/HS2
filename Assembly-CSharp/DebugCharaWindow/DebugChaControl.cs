using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using UnityEngine;

namespace DebugCharaWindow
{
	// Token: 0x020007E2 RID: 2018
	public class DebugChaControl : Singleton<DebugChaControl>
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x0012EA5B File Offset: 0x0012CE5B
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x0012EA63 File Offset: 0x0012CE63
		public GameScreenShot screenShot { get; private set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x0012EA6C File Offset: 0x0012CE6C
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x0012EA74 File Offset: 0x0012CE74
		public GameScreenShotSerial screenShotSerial { get; private set; }

		// Token: 0x060031CE RID: 12750 RVA: 0x0012EA7D File Offset: 0x0012CE7D
		private void Start()
		{
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x0012EA80 File Offset: 0x0012CE80
		private void Update()
		{
			if (null != Camera.main)
			{
				this.camMain = Camera.main;
			}
			if (this.enableShape)
			{
				foreach (KeyValuePair<ChaControl, DebugChaControl.DebugChaValue> keyValuePair in this.dictChaValue)
				{
					if (keyValuePair.Key.loadEnd)
					{
						for (int i = 0; i < keyValuePair.Value.shapeFace.Length; i++)
						{
							keyValuePair.Key.SetShapeFaceValue(i, keyValuePair.Value.shapeFace[i]);
						}
						for (int j = 0; j < keyValuePair.Value.shapeBody.Length; j++)
						{
							keyValuePair.Key.SetShapeBodyValue(j, keyValuePair.Value.shapeBody[j]);
						}
						keyValuePair.Key.ChangeBustSoftness(keyValuePair.Value.bustEtc[0]);
						keyValuePair.Key.ChangeBustGravity(keyValuePair.Value.bustEtc[1]);
						if (keyValuePair.Value.bustEtc[2] != keyValuePair.Key.chaFile.custom.body.areolaSize)
						{
							keyValuePair.Key.chaFile.custom.body.areolaSize = keyValuePair.Value.bustEtc[2];
							keyValuePair.Key.ChangeNipScale();
						}
						keyValuePair.Key.DisableShapeMouth(keyValuePair.Value.disableMaskFace);
						for (int k = 0; k < 2; k++)
						{
							for (int l = 0; l < ChaFileDefine.cf_BustShapeMaskID.Length; l++)
							{
								keyValuePair.Key.DisableShapeBodyID(k, l, keyValuePair.Value.disableMaskBody[k, l]);
							}
						}
						keyValuePair.Key.updateBustSize = true;
						if (keyValuePair.Key.sex == 1 && null != keyValuePair.Key.animBody)
						{
							foreach (AnimatorControllerParameter animatorControllerParameter in keyValuePair.Key.animBody.parameters)
							{
								string text = animatorControllerParameter.name.ToLower();
								if (text != null)
								{
									if (!(text == "height"))
									{
										if (text == "breast")
										{
											if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
											{
												keyValuePair.Key.animBody.SetFloat(animatorControllerParameter.name, keyValuePair.Key.GetShapeBodyValue(1));
											}
										}
									}
									else if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
									{
										keyValuePair.Key.animBody.SetFloat(animatorControllerParameter.name, keyValuePair.Key.GetShapeBodyValue(0));
									}
								}
							}
						}
					}
				}
			}
			if (null != this.camView[0] && this.camView[0].enabled && null != this.camMain)
			{
				this.camView[0].transform.localPosition = this.camMain.transform.position;
				this.camView[0].transform.localRotation = this.camMain.transform.rotation;
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x0012EE28 File Offset: 0x0012D228
		private void OnGUI()
		{
			if (this.drawOnGUI)
			{
				string[] array = this.dictGuiStr.Values.ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					GUI.TextField(new Rect(0f, (float)(20 * i), 1000f, 20f), array[i]);
				}
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0012EE88 File Offset: 0x0012D288
		protected new void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			this.camMain = Camera.main;
			this.screenShot = base.gameObject.AddComponent<GameScreenShot>();
			this.screenShotSerial = base.gameObject.AddComponent<GameScreenShotSerial>();
			this.CreateCamera();
			this.UpdateCameraSetting();
			this.objAudio = new GameObject("objAudio");
			if (null != this.objAudio)
			{
				this.objAudio.AddComponent<AudioSource>();
			}
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0012EF08 File Offset: 0x0012D308
		public void UpdateCameraSetting()
		{
			foreach (Camera camera in this.camView)
			{
				camera.targetDisplay = ((!this.useAnotherDisplay) ? 0 : 1);
			}
			if (null != this.camMain)
			{
				this.camMain.enabled = this.useAnotherDisplay;
			}
			if (MathfEx.RangeEqualOn<int>(0, this.viewType, 3))
			{
				foreach (Camera camera2 in this.camView)
				{
					camera2.enabled = false;
				}
				if (this.viewType == 0)
				{
					this.camView[0].enabled = true;
					this.camView[0].rect = new Rect(0f, 0f, 1f, 1f);
				}
				else if (this.viewType == 1)
				{
					this.camView[1].enabled = true;
					this.camView[1].rect = new Rect(0f, 0f, 1f, 1f);
				}
				else if (this.viewType == 2)
				{
					this.camView[2].enabled = true;
					this.camView[2].rect = new Rect(0f, 0f, 1f, 1f);
				}
				else if (this.viewType == 3)
				{
					this.camView[3].enabled = true;
					this.camView[3].rect = new Rect(0f, 0f, 1f, 1f);
				}
			}
			else if (this.viewType == 4)
			{
				foreach (Camera camera3 in this.camView)
				{
					camera3.enabled = true;
				}
				this.camView[0].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
				this.camView[1].rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
				this.camView[2].rect = new Rect(0f, 0f, 0.5f, 0.5f);
				this.camView[3].rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
			}
			else
			{
				foreach (Camera camera4 in this.camView)
				{
					camera4.enabled = false;
				}
				if (null != this.camMain)
				{
					this.camMain.enabled = true;
				}
			}
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x0012F1F4 File Offset: 0x0012D5F4
		public Vector2 GetCameraPosition(int no)
		{
			Vector2 zero = Vector2.zero;
			switch (no)
			{
			case 1:
				zero.x = -this.camView[no].transform.position.x;
				zero.y = -this.camView[no].transform.position.z;
				break;
			case 2:
				zero.x = this.camView[no].transform.position.x;
				zero.y = -this.camView[no].transform.position.y;
				break;
			case 3:
				zero.x = this.camView[no].transform.position.z;
				zero.y = -this.camView[no].transform.position.y;
				break;
			}
			return zero;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x0012F30C File Offset: 0x0012D70C
		public void SetCameraPosition(int no, Vector2 v)
		{
			switch (no)
			{
			case 1:
				this.camView[no].transform.position = new Vector3(-v.x, this.camView[no].transform.position.y, -v.y);
				break;
			case 2:
				this.camView[no].transform.position = new Vector3(v.x, -v.y, this.camView[no].transform.position.z);
				break;
			case 3:
				this.camView[no].transform.position = new Vector3(this.camView[no].transform.position.x, -v.y, v.x);
				break;
			}
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0012F40E File Offset: 0x0012D80E
		public float GetCameraSize(int no)
		{
			if (no == 4)
			{
				return 1f;
			}
			return 1f - this.camView[no].orthographicSize;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0012F430 File Offset: 0x0012D830
		public void SetCameraSize(int no, float size)
		{
			if (no == 4)
			{
				return;
			}
			this.camView[no].orthographicSize = 1f - size;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0012F450 File Offset: 0x0012D850
		public bool GetWireframeRender(int no)
		{
			if (no == 4)
			{
				return false;
			}
			WireFrameRender component = this.camView[no].GetComponent<WireFrameRender>();
			return component.wireFrameDraw;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0012F47C File Offset: 0x0012D87C
		public void SetWireframeRender(int no, bool wire)
		{
			if (no == 4)
			{
				return;
			}
			WireFrameRender component = this.camView[no].GetComponent<WireFrameRender>();
			component.wireFrameDraw = wire;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0012F4A8 File Offset: 0x0012D8A8
		private void CreateCamera()
		{
			string[] array = new string[]
			{
				"Persp",
				"Top",
				"Front",
				"Side"
			};
			int cullingMask = LayerMask.NameToLayer("Chara") | LayerMask.NameToLayer("Map");
			if (null != this.camMain)
			{
				cullingMask = this.camMain.cullingMask;
			}
			GameObject gameObject = new GameObject(array[0]);
			gameObject.transform.SetParent(base.transform);
			this.camView[0] = gameObject.AddComponent<Camera>();
			if (null != this.camMain)
			{
				this.camView[0].CopyFrom(this.camMain);
			}
			this.camView[0].clearFlags = CameraClearFlags.Color;
			this.camView[0].backgroundColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			gameObject.AddComponent<WireFrameRender>();
			Color[] array2 = new Color[]
			{
				new Color(0.6f, 0.6f, 0.6f, 1f),
				new Color(0.65f, 0.65f, 0.65f, 1f),
				new Color(0.7f, 0.7f, 0.7f, 1f),
				new Color(0.75f, 0.75f, 0.75f, 1f)
			};
			Vector3[] array3 = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(0f, 0f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, 1f, 0f)
			};
			Vector3[] array4 = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(90f, 0f, 0f),
				new Vector3(0f, 180f, 0f),
				new Vector3(0f, 90f, 0f)
			};
			for (int i = 1; i < 4; i++)
			{
				GameObject gameObject2 = new GameObject(string.Format(array[i], i));
				gameObject2.transform.SetParent(base.transform);
				gameObject2.transform.localPosition = array3[i];
				gameObject2.transform.localEulerAngles = array4[i];
				this.camView[i] = gameObject2.AddComponent<Camera>();
				this.camView[i].cullingMask = cullingMask;
				this.camView[i].clearFlags = CameraClearFlags.Color;
				this.camView[i].backgroundColor = array2[i];
				this.camView[i].orthographic = true;
				this.camView[i].orthographicSize = 0.5f;
				this.camView[i].nearClipPlane = -10f;
				this.camView[i].farClipPlane = 10f;
				this.camView[i].depth = 1000f;
				this.camView[i].useOcclusionCulling = true;
				this.camView[i].allowHDR = false;
				this.camView[i].allowMSAA = true;
				gameObject2.AddComponent<WireFrameRender>();
			}
		}

		// Token: 0x040031F2 RID: 12786
		[HideInInspector]
		public bool drawOnGUI;

		// Token: 0x040031F3 RID: 12787
		[HideInInspector]
		public bool enableShape;

		// Token: 0x040031F4 RID: 12788
		[HideInInspector]
		public int[] selectChara = new int[2];

		// Token: 0x040031F5 RID: 12789
		[HideInInspector]
		public int selectSex = 1;

		// Token: 0x040031F6 RID: 12790
		[HideInInspector]
		public Camera[] camView = new Camera[4];

		// Token: 0x040031F7 RID: 12791
		private Camera camMain;

		// Token: 0x040031F8 RID: 12792
		[HideInInspector]
		public int viewType = 5;

		// Token: 0x040031F9 RID: 12793
		[HideInInspector]
		public bool useAnotherDisplay;

		// Token: 0x040031FA RID: 12794
		public GameObject objAudio;

		// Token: 0x040031FB RID: 12795
		public Dictionary<string, string> dictGuiStr = new Dictionary<string, string>();

		// Token: 0x040031FC RID: 12796
		public Dictionary<ChaControl, DebugChaControl.DebugChaValue> dictChaValue = new Dictionary<ChaControl, DebugChaControl.DebugChaValue>();

		// Token: 0x020007E3 RID: 2019
		public class DebugChaValue
		{
			// Token: 0x060031DB RID: 12763 RVA: 0x0012F8E8 File Offset: 0x0012DCE8
			public void Initialize(ChaControl _chaCtrl)
			{
				this.UpdateParam(_chaCtrl);
				this.disableMaskFace = _chaCtrl.chaFile.status.disableMouthShapeMask;
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length; j++)
					{
						this.disableMaskBody[i, j] = _chaCtrl.chaFile.status.disableBustShapeMask[i, j];
					}
				}
			}

			// Token: 0x060031DC RID: 12764 RVA: 0x0012F960 File Offset: 0x0012DD60
			public void UpdateParam(ChaControl _chaCtrl)
			{
				for (int i = 0; i < this.shapeFace.Length; i++)
				{
					this.shapeFace[i] = _chaCtrl.chaFile.custom.face.shapeValueFace[i];
				}
				for (int j = 0; j < this.shapeBody.Length; j++)
				{
					this.shapeBody[j] = _chaCtrl.chaFile.custom.body.shapeValueBody[j];
				}
				this.bustEtc[0] = _chaCtrl.chaFile.custom.body.bustSoftness;
				this.bustEtc[1] = _chaCtrl.chaFile.custom.body.bustWeight;
				this.bustEtc[2] = _chaCtrl.chaFile.custom.body.areolaSize;
			}

			// Token: 0x040031FD RID: 12797
			public float[] shapeFace = new float[ChaFileDefine.cf_headshapename.Length];

			// Token: 0x040031FE RID: 12798
			public float[] shapeBody = new float[ChaFileDefine.cf_bodyshapename.Length];

			// Token: 0x040031FF RID: 12799
			public float[] bustEtc = new float[3];

			// Token: 0x04003200 RID: 12800
			public bool disableMaskFace;

			// Token: 0x04003201 RID: 12801
			public bool[,] disableMaskBody = new bool[2, ChaFileDefine.cf_BustShapeMaskID.Length];
		}
	}
}

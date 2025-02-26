using System;
using AIProject;
using AIProject.CaptionScript;
using Cinemachine;
using IllusionUtility.GetUtility;
using UniRx;
using UnityEngine;
using UnityEx;

namespace Manager
{
	// Token: 0x020008DA RID: 2266
	public class ADV : Singleton<ADV>
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x0015CF7D File Offset: 0x0015B37D
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x0015CF85 File Offset: 0x0015B385
		public AgentActor TargetCharacter { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x0015CF8E File Offset: 0x0015B38E
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x0015CF96 File Offset: 0x0015B396
		public MerchantActor TargetMerchant { get; set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x0015CF9F File Offset: 0x0015B39F
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x0015CFA7 File Offset: 0x0015B3A7
		public Captions Captions { get; set; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x0015CFB0 File Offset: 0x0015B3B0
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x0015CFB8 File Offset: 0x0015B3B8
		public AssetBundleInfo AssetBundleInfo { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x0015CFC1 File Offset: 0x0015B3C1
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x0015CFC9 File Offset: 0x0015B3C9
		public bool UsedBGM { get; set; }

		// Token: 0x06003B97 RID: 15255 RVA: 0x0015CFD2 File Offset: 0x0015B3D2
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0015CFEC File Offset: 0x0015B3EC
		public static void ChangeADVCamera(Actor actor)
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
			Vector3 position = player.Position;
			Vector3 pB = actor.Position;
			position.y = (pB.y = 0f);
			Vector3 forward = Vector3.Normalize(pB - position);
			if (Mathf.Approximately(forward.magnitude, 0f))
			{
				forward = Vector3.back;
			}
			Quaternion rotation = Quaternion.LookRotation(forward);
			GameObject gameObject = actor.transform.FindLoop("cf_J_Mune00");
			Transform target = gameObject.transform;
			Transform cameraTarget = player.CameraTarget.transform;
			Vector3 offset = Singleton<Resources>.Instance.LocomotionProfile.CommunicationOffset;
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				cameraTarget.position = new Vector3(pB.x, target.position.y, pB.z);
				if (player.CameraControl.VanishControl != null)
				{
					player.CameraControl.VanishControl.LookAtPosition = cameraTarget.position;
				}
				player.CameraControl.ADVCamera.LookAt = cameraTarget;
				player.CameraControl.ADVCamera.transform.position = cameraTarget.position + rotation * offset;
				player.CameraControl.Mode = CameraMode.ADV;
			});
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x0015D0E4 File Offset: 0x0015B4E4
		public static void ChangeADVCameraDiagonal(Actor actor)
		{
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				string communicationDiagonalTargetName = Singleton<Resources>.Instance.LocomotionProfile.CommunicationDiagonalTargetName;
				GameObject gameObject = player.transform.FindLoop(communicationDiagonalTargetName);
				Transform transform = gameObject.transform;
				GameObject gameObject2 = actor.transform.FindLoop("cf_J_Mune00");
				Transform transform2 = gameObject2.transform;
				Vector3 a = Vector3.Normalize(transform.position - transform2.position);
				if (Mathf.Approximately(a.magnitude, 0f))
				{
					a = Vector3.back;
				}
				Transform transform3 = player.CameraTarget.transform;
				transform3.position = transform2.position;
				if (player.CameraControl.VanishControl != null)
				{
					player.CameraControl.VanishControl.LookAtPosition = transform3.position;
				}
				player.CameraControl.ADVCamera.LookAt = transform3;
				player.CameraControl.ADVCamera.transform.position = transform2.position + a * 8f;
				player.CameraControl.Mode = CameraMode.ADV;
			});
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x0015D11C File Offset: 0x0015B51C
		public static void ChangeADVFixedAngleCamera(Actor actor, int attitudeID)
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
			Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
			{
				GameObject gameObject = actor.ChaControl.animBody.transform.FindLoop("cf_J_Mune00");
				if (gameObject != null)
				{
					Transform advnotStandRoot = player.CameraControl.ADVNotStandRoot;
					advnotStandRoot.position = gameObject.transform.position;
					advnotStandRoot.rotation = actor.Rotation;
					if (player.CameraControl.VanishControl != null)
					{
						player.CameraControl.VanishControl.LookAtPosition = gameObject.transform.position;
					}
				}
				ABInfoData.Param param;
				if (Singleton<Resources>.Instance.Action.ComCameraList.TryGetValue(attitudeID, out param))
				{
					TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(param.AssetBundle, param.AssetFile, false, string.Empty);
					string text = (textAsset != null) ? textAsset.text : null;
					if (text.IsNullOrEmpty())
					{
						GlobalMethod.DebugLog("cameraファイルが読み込めません", 1);
						return;
					}
					string[][] array;
					GlobalMethod.GetListString(text, out array);
					Vector3 localPosition;
					float num;
					localPosition.x = ((!float.TryParse(array[0][0], out num)) ? 0f : num);
					localPosition.y = ((!float.TryParse(array[1][0], out num)) ? 0f : num);
					localPosition.z = ((!float.TryParse(array[2][0], out num)) ? 0f : num);
					Quaternion localRotation;
					localRotation.x = ((!float.TryParse(array[3][0], out num)) ? 0f : num);
					localRotation.y = ((!float.TryParse(array[4][0], out num)) ? 0f : num);
					localRotation.z = ((!float.TryParse(array[5][0], out num)) ? 0f : num);
					localRotation.w = ((!float.TryParse(array[6][0], out num)) ? 0f : num);
					CinemachineVirtualCameraBase advnotStandCamera = player.CameraControl.ADVNotStandCamera;
					advnotStandCamera.transform.localPosition = localPosition;
					advnotStandCamera.transform.localRotation = localRotation;
					CinemachineVirtualCamera cinemachineVirtualCamera = advnotStandCamera as CinemachineVirtualCamera;
					float fieldOfView;
					if (cinemachineVirtualCamera != null && float.TryParse(array[7][0], out fieldOfView))
					{
						cinemachineVirtualCamera.m_Lens.FieldOfView = fieldOfView;
					}
					player.CameraControl.Mode = CameraMode.ADVExceptStand;
				}
			}, delegate(Exception ex)
			{
			}, delegate()
			{
			});
		}
	}
}

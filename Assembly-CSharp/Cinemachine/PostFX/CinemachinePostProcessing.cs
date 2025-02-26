using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

namespace Cinemachine.PostFX
{
	// Token: 0x020002E3 RID: 739
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[ExecuteInEditMode]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachinePostProcessing : CinemachineExtension
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00031F6A File Offset: 0x0003036A
		public PostProcessProfile Profile
		{
			get
			{
				return (!(this.mProfileCopy != null)) ? this.m_Profile : this.mProfileCopy;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00031F8E File Offset: 0x0003038E
		public bool IsValid
		{
			get
			{
				return this.m_Profile != null && this.m_Profile.settings.Count > 0;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00031FB7 File Offset: 0x000303B7
		public void InvalidateCachedProfile()
		{
			this.mCachedProfileIsInvalid = true;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00031FC0 File Offset: 0x000303C0
		private void CreateProfileCopy()
		{
			this.DestroyProfileCopy();
			PostProcessProfile postProcessProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
			if (this.m_Profile != null)
			{
				foreach (PostProcessEffectSettings original in this.m_Profile.settings)
				{
					PostProcessEffectSettings item = UnityEngine.Object.Instantiate<PostProcessEffectSettings>(original);
					postProcessProfile.settings.Add(item);
				}
			}
			this.mProfileCopy = postProcessProfile;
			this.mCachedProfileIsInvalid = false;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00032058 File Offset: 0x00030458
		private void DestroyProfileCopy()
		{
			if (this.mProfileCopy != null)
			{
				RuntimeUtility.DestroyObject(this.mProfileCopy);
			}
			this.mProfileCopy = null;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003207D File Offset: 0x0003047D
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.DestroyProfileCopy();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003208C File Offset: 0x0003048C
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Aim)
			{
				if (!this.IsValid)
				{
					this.DestroyProfileCopy();
				}
				else
				{
					if (!this.m_FocusTracksTarget)
					{
						this.DestroyProfileCopy();
					}
					else
					{
						if (this.mProfileCopy == null || this.mCachedProfileIsInvalid)
						{
							this.CreateProfileCopy();
						}
						DepthOfField depthOfField;
						if (this.mProfileCopy.TryGetSettings<DepthOfField>(out depthOfField))
						{
							float num = this.m_FocusOffset;
							if (state.HasLookAt)
							{
								num += (state.FinalPosition - state.ReferenceLookAt).magnitude;
							}
							depthOfField.focusDistance.value = Mathf.Max(0f, num);
						}
					}
					state.AddCustomBlendable(new CameraState.CustomBlendable(this, 1f));
				}
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00032158 File Offset: 0x00030558
		private static void OnCameraCut(CinemachineBrain brain)
		{
			PostProcessLayer pplayer = CinemachinePostProcessing.GetPPLayer(brain);
			if (pplayer != null)
			{
				pplayer.ResetHistory();
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00032180 File Offset: 0x00030580
		private static void ApplyPostFX(CinemachineBrain brain)
		{
			PostProcessLayer pplayer = CinemachinePostProcessing.GetPPLayer(brain);
			if (pplayer == null || !pplayer.enabled || pplayer.volumeLayer == 0)
			{
				return;
			}
			CameraState currentCameraState = brain.CurrentCameraState;
			int numCustomBlendables = currentCameraState.NumCustomBlendables;
			List<PostProcessVolume> dynamicBrainVolumes = CinemachinePostProcessing.GetDynamicBrainVolumes(brain, pplayer, numCustomBlendables);
			for (int i = 0; i < dynamicBrainVolumes.Count; i++)
			{
				dynamicBrainVolumes[i].weight = 0f;
				dynamicBrainVolumes[i].sharedProfile = null;
				dynamicBrainVolumes[i].profile = null;
			}
			PostProcessVolume postProcessVolume = null;
			int num = 0;
			for (int j = 0; j < numCustomBlendables; j++)
			{
				CameraState.CustomBlendable customBlendable = currentCameraState.GetCustomBlendable(j);
				CinemachinePostProcessing cinemachinePostProcessing = customBlendable.m_Custom as CinemachinePostProcessing;
				if (!(cinemachinePostProcessing == null))
				{
					PostProcessVolume postProcessVolume2 = dynamicBrainVolumes[j];
					if (postProcessVolume == null)
					{
						postProcessVolume = postProcessVolume2;
					}
					postProcessVolume2.sharedProfile = cinemachinePostProcessing.Profile;
					postProcessVolume2.isGlobal = true;
					postProcessVolume2.priority = float.MaxValue - (float)(numCustomBlendables - j) - 1f;
					postProcessVolume2.weight = customBlendable.m_Weight;
					num++;
				}
				if (num > 1)
				{
					postProcessVolume.weight = 1f;
				}
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000322D0 File Offset: 0x000306D0
		private static List<PostProcessVolume> GetDynamicBrainVolumes(CinemachineBrain brain, PostProcessLayer ppLayer, int minVolumes)
		{
			GameObject gameObject = null;
			Transform transform = brain.transform;
			int childCount = transform.childCount;
			CinemachinePostProcessing.sVolumes.Clear();
			int num = 0;
			while (gameObject == null && num < childCount)
			{
				GameObject gameObject2 = transform.GetChild(num).gameObject;
				if (gameObject2.hideFlags == HideFlags.HideAndDontSave)
				{
					gameObject2.GetComponents<PostProcessVolume>(CinemachinePostProcessing.sVolumes);
					if (CinemachinePostProcessing.sVolumes.Count > 0)
					{
						gameObject = gameObject2;
					}
				}
				num++;
			}
			if (minVolumes > 0)
			{
				if (gameObject == null)
				{
					gameObject = new GameObject(CinemachinePostProcessing.sVolumeOwnerName);
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					gameObject.transform.parent = transform;
				}
				int value = ppLayer.volumeLayer.value;
				for (int i = 0; i < 32; i++)
				{
					if ((value & 1 << i) != 0)
					{
						gameObject.layer = i;
						break;
					}
				}
				while (CinemachinePostProcessing.sVolumes.Count < minVolumes)
				{
					CinemachinePostProcessing.sVolumes.Add(gameObject.gameObject.AddComponent<PostProcessVolume>());
				}
			}
			return CinemachinePostProcessing.sVolumes;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000323F4 File Offset: 0x000307F4
		private static PostProcessLayer GetPPLayer(CinemachineBrain brain)
		{
			PostProcessLayer postProcessLayer = null;
			if (CinemachinePostProcessing.mBrainToLayer.TryGetValue(brain, out postProcessLayer))
			{
				return postProcessLayer;
			}
			postProcessLayer = brain.GetComponent<PostProcessLayer>();
			CinemachinePostProcessing.mBrainToLayer[brain] = postProcessLayer;
			if (postProcessLayer != null)
			{
				UnityEvent<CinemachineBrain> cameraCutEvent = brain.m_CameraCutEvent;
				if (CinemachinePostProcessing.<>f__mg$cache0 == null)
				{
					CinemachinePostProcessing.<>f__mg$cache0 = new UnityAction<CinemachineBrain>(CinemachinePostProcessing.OnCameraCut);
				}
				cameraCutEvent.AddListener(CinemachinePostProcessing.<>f__mg$cache0);
			}
			else
			{
				UnityEvent<CinemachineBrain> cameraCutEvent2 = brain.m_CameraCutEvent;
				if (CinemachinePostProcessing.<>f__mg$cache1 == null)
				{
					CinemachinePostProcessing.<>f__mg$cache1 = new UnityAction<CinemachineBrain>(CinemachinePostProcessing.OnCameraCut);
				}
				cameraCutEvent2.RemoveListener(CinemachinePostProcessing.<>f__mg$cache1);
			}
			return postProcessLayer;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003248C File Offset: 0x0003088C
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeModule()
		{
			UnityEvent<CinemachineBrain> cameraUpdatedEvent = CinemachineCore.CameraUpdatedEvent;
			if (CinemachinePostProcessing.<>f__mg$cache2 == null)
			{
				CinemachinePostProcessing.<>f__mg$cache2 = new UnityAction<CinemachineBrain>(CinemachinePostProcessing.ApplyPostFX);
			}
			cameraUpdatedEvent.RemoveListener(CinemachinePostProcessing.<>f__mg$cache2);
			UnityEvent<CinemachineBrain> cameraUpdatedEvent2 = CinemachineCore.CameraUpdatedEvent;
			if (CinemachinePostProcessing.<>f__mg$cache3 == null)
			{
				CinemachinePostProcessing.<>f__mg$cache3 = new UnityAction<CinemachineBrain>(CinemachinePostProcessing.ApplyPostFX);
			}
			cameraUpdatedEvent2.AddListener(CinemachinePostProcessing.<>f__mg$cache3);
		}

		// Token: 0x04000B62 RID: 2914
		[Tooltip("If checked, then the Focus Distance will be set to the distance between the camera and the LookAt target.  Requires DepthOfField effect in the Profile")]
		public bool m_FocusTracksTarget;

		// Token: 0x04000B63 RID: 2915
		[Tooltip("Offset from target distance, to be used with Focus Tracks Target.  Offsets the sharpest point away from the LookAt target.")]
		public float m_FocusOffset;

		// Token: 0x04000B64 RID: 2916
		[Tooltip("This Post-Processing profile will be applied whenever this virtual camera is live")]
		public PostProcessProfile m_Profile;

		// Token: 0x04000B65 RID: 2917
		private bool mCachedProfileIsInvalid = true;

		// Token: 0x04000B66 RID: 2918
		private PostProcessProfile mProfileCopy;

		// Token: 0x04000B67 RID: 2919
		private static string sVolumeOwnerName = "__CMVolumes";

		// Token: 0x04000B68 RID: 2920
		private static List<PostProcessVolume> sVolumes = new List<PostProcessVolume>();

		// Token: 0x04000B69 RID: 2921
		private static Dictionary<CinemachineBrain, PostProcessLayer> mBrainToLayer = new Dictionary<CinemachineBrain, PostProcessLayer>();

		// Token: 0x04000B6A RID: 2922
		[CompilerGenerated]
		private static UnityAction<CinemachineBrain> <>f__mg$cache0;

		// Token: 0x04000B6B RID: 2923
		[CompilerGenerated]
		private static UnityAction<CinemachineBrain> <>f__mg$cache1;

		// Token: 0x04000B6C RID: 2924
		[CompilerGenerated]
		private static UnityAction<CinemachineBrain> <>f__mg$cache2;

		// Token: 0x04000B6D RID: 2925
		[CompilerGenerated]
		private static UnityAction<CinemachineBrain> <>f__mg$cache3;
	}
}

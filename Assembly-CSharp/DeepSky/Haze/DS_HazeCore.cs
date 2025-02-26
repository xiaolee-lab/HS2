using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSky.Haze
{
	// Token: 0x020002EC RID: 748
	[ExecuteInEditMode]
	[AddComponentMenu("DeepSky Haze/Controller", 51)]
	public class DS_HazeCore : MonoBehaviour
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00033E26 File Offset: 0x00032226
		public static DS_HazeCore Instance
		{
			get
			{
				if (DS_HazeCore.instance == null)
				{
					DS_HazeCore.instance = UnityEngine.Object.FindObjectOfType<DS_HazeCore>();
				}
				return DS_HazeCore.instance;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00033E47 File Offset: 0x00032247
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00033E4F File Offset: 0x0003224F
		public float Time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00033E5D File Offset: 0x0003225D
		public Texture3D NoiseLUT
		{
			get
			{
				return this.m_NoiseLUT;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00033E65 File Offset: 0x00032265
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00033E6D File Offset: 0x0003226D
		public DS_HazeCore.HeightFalloffType HeightFalloff
		{
			get
			{
				return this.m_HeightFalloff;
			}
			set
			{
				this.m_HeightFalloff = value;
				this.SetGlobalHeightFalloff();
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00033E7C File Offset: 0x0003227C
		private void SetGlobalHeightFalloff()
		{
			DS_HazeCore.HeightFalloffType heightFalloff = this.m_HeightFalloff;
			if (heightFalloff != DS_HazeCore.HeightFalloffType.Exponential)
			{
				if (heightFalloff == DS_HazeCore.HeightFalloffType.None)
				{
					Shader.EnableKeyword("DS_HAZE_HEIGHT_FALLOFF_NONE");
				}
			}
			else
			{
				Shader.DisableKeyword("DS_HAZE_HEIGHT_FALLOFF_NONE");
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00033EC8 File Offset: 0x000322C8
		private void OnTransformChildrenChanged()
		{
			this.m_Zones.Clear();
			DS_HazeZone[] componentsInChildren = base.GetComponentsInChildren<DS_HazeZone>(true);
			this.m_Zones.AddRange(componentsInChildren);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00033EF4 File Offset: 0x000322F4
		private void Awake()
		{
			if (DS_HazeCore.instance == null)
			{
				DS_HazeCore.instance = this;
			}
			else if (DS_HazeCore.instance != this)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033F28 File Offset: 0x00032328
		private void OnEnable()
		{
			this.SetGlobalHeightFalloff();
			Shader.SetGlobalTexture("_SamplingOffsets", this.m_NoiseLUT);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00033F40 File Offset: 0x00032340
		private void Reset()
		{
			this.OnTransformChildrenChanged();
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00033F48 File Offset: 0x00032348
		public void SetGlobalNoiseLUT()
		{
			Shader.SetGlobalTexture("_SamplingOffsets", this.m_NoiseLUT);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00033F5A File Offset: 0x0003235A
		public void AddLightVolume(DS_HazeLightVolume lightVolume)
		{
			this.RemoveLightVolume(lightVolume);
			this.m_LightVolumes.Add(lightVolume);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00033F70 File Offset: 0x00032370
		public void RemoveLightVolume(DS_HazeLightVolume lightVolume)
		{
			this.m_LightVolumes.Remove(lightVolume);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00033F80 File Offset: 0x00032380
		public void GetRenderLightVolumes(Vector3 cameraPosition, List<DS_HazeLightVolume> lightVolumes, List<DS_HazeLightVolume> shadowVolumes)
		{
			foreach (DS_HazeLightVolume ds_HazeLightVolume in this.m_LightVolumes)
			{
				if (ds_HazeLightVolume.WillRender(cameraPosition))
				{
					if (ds_HazeLightVolume.CastShadows)
					{
						shadowVolumes.Add(ds_HazeLightVolume);
					}
					else
					{
						lightVolumes.Add(ds_HazeLightVolume);
					}
				}
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034000 File Offset: 0x00032400
		public DS_HazeContextItem GetRenderContextAtPosition(Vector3 position)
		{
			List<DS_HazeZone> list = new List<DS_HazeZone>();
			for (int i = 0; i < this.m_Zones.Count; i++)
			{
				if (this.m_Zones[i].Contains(position) && this.m_Zones[i].enabled)
				{
					list.Add(this.m_Zones[i]);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0].Context.GetContextItemBlended(this.m_Time);
			}
			list.Sort(delegate(DS_HazeZone z1, DS_HazeZone z2)
			{
				if (z1 < z2)
				{
					return -1;
				}
				return 1;
			});
			DS_HazeContextItem contextItemBlended = list[0].Context.GetContextItemBlended(this.m_Time);
			for (int j = 1; j < list.Count; j++)
			{
				float blendWeight = list[j].GetBlendWeight(position);
				contextItemBlended.Lerp(list[j].Context.GetContextItemBlended(this.m_Time), blendWeight);
			}
			return contextItemBlended;
		}

		// Token: 0x04000BC6 RID: 3014
		public static string kVersionStr = "DeepSky Haze v1.4.0";

		// Token: 0x04000BC7 RID: 3015
		private static int kGUIHeight = 180;

		// Token: 0x04000BC8 RID: 3016
		private static DS_HazeCore instance;

		// Token: 0x04000BC9 RID: 3017
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("The time at which Zones will evaluate their settings. Animate this or set in code to create time-of-day transitions.")]
		private float m_Time;

		// Token: 0x04000BCA RID: 3018
		[SerializeField]
		[Tooltip("The height falloff method to use globally (default Exponential).")]
		private DS_HazeCore.HeightFalloffType m_HeightFalloff;

		// Token: 0x04000BCB RID: 3019
		[SerializeField]
		private List<DS_HazeZone> m_Zones = new List<DS_HazeZone>();

		// Token: 0x04000BCC RID: 3020
		[SerializeField]
		private DS_HazeCore.DebugGUIPosition m_DebugGUIPosition;

		// Token: 0x04000BCD RID: 3021
		private HashSet<DS_HazeLightVolume> m_LightVolumes = new HashSet<DS_HazeLightVolume>();

		// Token: 0x04000BCE RID: 3022
		[SerializeField]
		private Texture3D m_NoiseLUT;

		// Token: 0x04000BCF RID: 3023
		[SerializeField]
		private bool m_ShowDebugGUI;

		// Token: 0x04000BD0 RID: 3024
		private Vector2 m_GUIScrollPosition;

		// Token: 0x04000BD1 RID: 3025
		private int m_GUISelectedView = -1;

		// Token: 0x04000BD2 RID: 3026
		private bool m_GUISelectionPopup;

		// Token: 0x04000BD3 RID: 3027
		private DS_HazeView m_GUIDisplayedView;

		// Token: 0x020002ED RID: 749
		public enum HeightFalloffType
		{
			// Token: 0x04000BD6 RID: 3030
			Exponential,
			// Token: 0x04000BD7 RID: 3031
			None
		}

		// Token: 0x020002EE RID: 750
		public enum NoiseTextureSize
		{
			// Token: 0x04000BD9 RID: 3033
			x8 = 8,
			// Token: 0x04000BDA RID: 3034
			x16 = 16,
			// Token: 0x04000BDB RID: 3035
			x32 = 32
		}

		// Token: 0x020002EF RID: 751
		public enum DebugGUIPosition
		{
			// Token: 0x04000BDD RID: 3037
			TopLeft,
			// Token: 0x04000BDE RID: 3038
			TopCenter,
			// Token: 0x04000BDF RID: 3039
			TopRight,
			// Token: 0x04000BE0 RID: 3040
			CenterLeft,
			// Token: 0x04000BE1 RID: 3041
			Center,
			// Token: 0x04000BE2 RID: 3042
			CenterRight,
			// Token: 0x04000BE3 RID: 3043
			BottomLeft,
			// Token: 0x04000BE4 RID: 3044
			BottomCenter,
			// Token: 0x04000BE5 RID: 3045
			BottomRight
		}
	}
}

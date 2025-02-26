using System;
using System.Collections.Generic;
using AIProject;
using Manager;
using PlaceholderSoftware.WetStuff;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ConfigScene
{
	// Token: 0x02000851 RID: 2129
	[DisallowMultipleComponent]
	public class ConfigEffector : MonoBehaviour
	{
		// Token: 0x0600364E RID: 13902 RVA: 0x0014030C File Offset: 0x0013E70C
		private void Refresh()
		{
			GraphicSystem graphicData = Config.GraphicData;
			foreach (Bloom bloom in this._bloom)
			{
				if (bloom.active != graphicData.Bloom)
				{
					bloom.active = graphicData.Bloom;
				}
			}
			foreach (AmbientOcclusion ambientOcclusion in this._ao)
			{
				if (ambientOcclusion.active != graphicData.SSAO)
				{
					ambientOcclusion.active = graphicData.SSAO;
				}
			}
			foreach (ScreenSpaceReflections screenSpaceReflections in this._ssr)
			{
				if (screenSpaceReflections.active != graphicData.SSR)
				{
					screenSpaceReflections.active = graphicData.SSR;
				}
			}
			foreach (DepthOfField depthOfField in this._dof)
			{
				if (depthOfField.active != graphicData.DepthOfField)
				{
					depthOfField.active = graphicData.DepthOfField;
				}
			}
			foreach (Vignette vignette in this._vignette)
			{
				if (vignette.active != graphicData.Vignette)
				{
					vignette.active = graphicData.Vignette;
				}
			}
			if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Simulator != null && Singleton<Map>.Instance.Simulator.EnviroSky != null)
			{
				EnviroSky enviroSky = Singleton<Map>.Instance.Simulator.EnviroSky;
				enviroSky.fogSettings.distanceFog = graphicData.Atmospheric;
				enviroSky.fogSettings.heightFog = graphicData.Atmospheric;
				enviroSky.volumeLighting = graphicData.Atmospheric;
				enviroSky.LightShafts.sunLightShafts = graphicData.Atmospheric;
				enviroSky.LightShafts.moonLightShafts = graphicData.Atmospheric;
			}
			if (this.Wetstuff != null && this.Wetstuff.enabled != graphicData.Rain)
			{
				this.Wetstuff.enabled = graphicData.Rain;
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x001405EC File Offset: 0x0013E9EC
		private void Reset()
		{
			this.PostProcessLayer = base.GetComponent<PostProcessLayer>();
			this.Wetstuff = base.GetComponent<WetStuff>();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00140606 File Offset: 0x0013EA06
		private void Awake()
		{
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x00140608 File Offset: 0x0013EA08
		private void Start()
		{
			List<PostProcessVolume> list = ListPool<PostProcessVolume>.Get();
			PostProcessManager.instance.GetActiveVolumes(this.PostProcessLayer, list, true, true);
			foreach (PostProcessVolume postProcessVolume in list)
			{
				Bloom setting = postProcessVolume.profile.GetSetting<Bloom>();
				if (setting)
				{
					this._bloom.Add(setting);
				}
				AmbientOcclusion setting2 = postProcessVolume.profile.GetSetting<AmbientOcclusion>();
				if (setting2 != null && setting2.active)
				{
					this._ao.Add(setting2);
				}
				ScreenSpaceReflections setting3 = postProcessVolume.profile.GetSetting<ScreenSpaceReflections>();
				if (setting3 != null && setting3.active)
				{
					this._ssr.Add(setting3);
				}
				DepthOfField setting4 = postProcessVolume.profile.GetSetting<DepthOfField>();
				if (setting4 != null && setting4.active)
				{
					this._dof.Add(setting4);
				}
				Vignette setting5 = postProcessVolume.profile.GetSetting<Vignette>();
				if (setting5 != null && setting5.active)
				{
					this._vignette.Add(setting5);
				}
			}
			this.Refresh();
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x00140774 File Offset: 0x0013EB74
		private void Update()
		{
			this.Refresh();
		}

		// Token: 0x0400369A RID: 13978
		public WetStuff Wetstuff;

		// Token: 0x0400369B RID: 13979
		public PostProcessLayer PostProcessLayer;

		// Token: 0x0400369C RID: 13980
		private List<Bloom> _bloom = new List<Bloom>();

		// Token: 0x0400369D RID: 13981
		private List<AmbientOcclusion> _ao = new List<AmbientOcclusion>();

		// Token: 0x0400369E RID: 13982
		private List<ScreenSpaceReflections> _ssr = new List<ScreenSpaceReflections>();

		// Token: 0x0400369F RID: 13983
		private List<DepthOfField> _dof = new List<DepthOfField>();

		// Token: 0x040036A0 RID: 13984
		private List<Vignette> _vignette = new List<Vignette>();

		// Token: 0x040036A1 RID: 13985
		public bool isInput = true;
	}
}

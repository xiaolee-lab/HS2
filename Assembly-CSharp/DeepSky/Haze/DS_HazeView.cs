using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace DeepSky.Haze
{
	// Token: 0x020002F3 RID: 755
	[ExecuteInEditMode]
	[AddComponentMenu("DeepSky Haze/View", 1)]
	public class DS_HazeView : MonoBehaviour
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000353D9 File Offset: 0x000337D9
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x000353E1 File Offset: 0x000337E1
		public bool OverrideTime
		{
			get
			{
				return this.m_OverrideTime;
			}
			set
			{
				this.m_OverrideTime = value;
				if (value && this.m_OverrideContextVariant)
				{
					this.m_OverrideContextVariant = false;
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00035402 File Offset: 0x00033802
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0003540A File Offset: 0x0003380A
		public float Time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00035413 File Offset: 0x00033813
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0003541B File Offset: 0x0003381B
		public bool OverrideContextAsset
		{
			get
			{
				return this.m_OverrideContextAsset;
			}
			set
			{
				this.m_OverrideContextAsset = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00035424 File Offset: 0x00033824
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0003542C File Offset: 0x0003382C
		public DS_HazeContextAsset ContextAsset
		{
			get
			{
				return this.m_Context;
			}
			set
			{
				this.m_Context = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00035435 File Offset: 0x00033835
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0003543D File Offset: 0x0003383D
		public bool OverrideContextVariant
		{
			get
			{
				return this.m_OverrideContextVariant;
			}
			set
			{
				this.m_OverrideContextVariant = value;
				if (value && this.m_OverrideTime)
				{
					this.m_OverrideTime = false;
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003545E File Offset: 0x0003385E
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00035466 File Offset: 0x00033866
		public int ContextItemIndex
		{
			get
			{
				return this.m_ContextItemIndex;
			}
			set
			{
				this.m_ContextItemIndex = ((value <= 0) ? 0 : value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003547C File Offset: 0x0003387C
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00035484 File Offset: 0x00033884
		public Light DirectLight
		{
			get
			{
				return this.m_DirectLight;
			}
			set
			{
				this.m_DirectLight = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0003548D File Offset: 0x0003388D
		public Vector2 RadianceTargetSize
		{
			get
			{
				return new Vector2((float)this.m_X, (float)this.m_Y);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000354A4 File Offset: 0x000338A4
		public int SampleCount
		{
			get
			{
				switch (this.m_VolumeSamples)
				{
				case DS_HazeView.VolumeSamples.x16:
					return 16;
				case DS_HazeView.VolumeSamples.x24:
					return 24;
				case DS_HazeView.VolumeSamples.x32:
					return 32;
				default:
					return 16;
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000354DA File Offset: 0x000338DA
		public int DownSampleFactor
		{
			get
			{
				return (this.m_DownsampleFactor != DS_HazeView.SizeFactor.Half) ? 4 : 2;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000354EF File Offset: 0x000338EF
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x000354F7 File Offset: 0x000338F7
		public bool RenderAtmosphereVolumetrics
		{
			get
			{
				return this.m_RenderAtmosphereVolumetrics;
			}
			set
			{
				this.m_RenderAtmosphereVolumetrics = value;
				this.SetTemporalKeywords();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00035506 File Offset: 0x00033906
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0003550E File Offset: 0x0003390E
		public bool RenderLocalVolumetrics
		{
			get
			{
				return this.m_RenderLocalVolumetrics;
			}
			set
			{
				this.m_RenderLocalVolumetrics = value;
				this.SetTemporalKeywords();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0003551D File Offset: 0x0003391D
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00035525 File Offset: 0x00033925
		public bool TemporalReprojection
		{
			get
			{
				return this.m_TemporalReprojection;
			}
			set
			{
				this.m_TemporalReprojection = value;
				this.SetTemporalKeywords();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00035534 File Offset: 0x00033934
		public bool WillRenderWithTemporalReprojection
		{
			get
			{
				return this.m_TemporalReprojection & (this.m_RenderAtmosphereVolumetrics | this.m_RenderLocalVolumetrics);
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003554C File Offset: 0x0003394C
		public int AntiAliasingLevel()
		{
			int result = 1;
			if (this.m_Camera.actualRenderingPath == RenderingPath.Forward && this.m_Camera.allowMSAA && QualitySettings.antiAliasing > 0)
			{
				result = QualitySettings.antiAliasing;
			}
			return result;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00035590 File Offset: 0x00033990
		private bool CheckHasSystemSupport()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return false;
			}
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				base.enabled = false;
				return false;
			}
			if (this.m_Camera.allowHDR && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
			{
				base.enabled = false;
				return false;
			}
			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RFloat))
			{
				base.enabled = false;
				return false;
			}
			return true;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00035600 File Offset: 0x00033A00
		private void SetMaterialFromContext(DS_HazeContextItem ctx)
		{
			if (this.WillRenderWithTemporalReprojection)
			{
				this.m_InterleavedOffsetIndex += 0.0625f;
				if (Mathf.Approximately(this.m_InterleavedOffsetIndex, 1f))
				{
					this.m_InterleavedOffsetIndex = 0f;
				}
			}
			float x = 1f;
			float y = 1f;
			float z = 1f;
			DS_HazeCore.HeightFalloffType heightFalloff = DS_HazeCore.Instance.HeightFalloff;
			if (heightFalloff != DS_HazeCore.HeightFalloffType.None)
			{
				if (heightFalloff == DS_HazeCore.HeightFalloffType.Exponential)
				{
					float num = Mathf.Abs(base.transform.position.y);
					x = Mathf.Exp(-ctx.m_AirDensityHeightFalloff * num);
					y = Mathf.Exp(-ctx.m_HazeDensityHeightFalloff * num);
					z = Mathf.Exp(-ctx.m_FogDensityHeightFalloff * (num - ctx.m_FogStartHeight));
				}
			}
			else
			{
				x = 1f;
				y = 1f;
				z = 1f;
			}
			Vector3 v = ctx.m_AirScatteringScale * new Vector3(0.00116f, 0.0027f, 0.00662f);
			float x2 = ctx.m_HazeScatteringScale * 0.0021f;
			float fogScatteringScale = ctx.m_FogScatteringScale;
			float w = ctx.m_FogExtinctionScale * 0.01f;
			Vector4 value = new Vector4(ctx.m_AirDensityHeightFalloff, ctx.m_HazeDensityHeightFalloff, 0f, ctx.m_HazeScatteringDirection);
			Vector4 value2 = new Vector4(x2, (!this.m_RenderAtmosphereVolumetrics) ? 0f : ctx.m_HazeSecondaryScatteringRatio, fogScatteringScale, w);
			Vector4 value3 = new Vector4(x, y, z, 0f);
			Vector4 value4 = new Vector4(ctx.m_FogStartDistance, ctx.m_FogDensityHeightFalloff, ctx.m_FogOpacity, ctx.m_FogScatteringDirection);
			Vector4 value5 = new Vector4((float)this.m_GaussianDepthFalloff, this.m_UpsampleDepthThreshold * 0.01f, this.m_TemporalRejectionScale, this.m_TemporalBlendFactor);
			this.m_Material.SetVector("_SamplingParams", value5);
			this.m_Material.SetVector("_InterleavedOffset", new Vector4(this.m_InterleavedOffsetIndex, 0f, 0f, 0f));
			this.m_Material.SetMatrix("_PreviousViewProjMatrix", this.m_PreviousViewProjMatrix);
			this.m_Material.SetMatrix("_PreviousInvViewProjMatrix", this.m_PreviousInvViewProjMatrix);
			Shader.SetGlobalVector("_DS_BetaParams", value2);
			Shader.SetGlobalVector("_DS_RBetaS", v);
			Shader.SetGlobalVector("_DS_AirHazeParams", value);
			Shader.SetGlobalVector("_DS_FogParams", value4);
			Shader.SetGlobalVector("_DS_InitialDensityParams", value3);
			Vector3 v2;
			Color c;
			if (this.m_DirectLight)
			{
				v2 = -this.m_DirectLight.transform.forward;
				c = this.m_DirectLight.color.linear * this.m_DirectLight.intensity;
				Shader.SetGlobalColor("_DS_FogAmbientLight", ctx.m_FogAmbientColour.linear * this.m_DirectLight.intensity);
				Shader.SetGlobalColor("_DS_FogDirectLight", ctx.m_FogLightColour.linear * this.m_DirectLight.intensity);
			}
			else
			{
				v2 = Vector3.up;
				c = Color.white;
				Shader.SetGlobalColor("_DS_FogAmbientLight", ctx.m_FogAmbientColour.linear);
				Shader.SetGlobalColor("_DS_FogDirectLight", ctx.m_FogLightColour.linear);
			}
			Shader.SetGlobalVector("_DS_LightDirection", v2);
			Shader.SetGlobalVector("_DS_LightColour", c);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003596A File Offset: 0x00033D6A
		private void SetGlobalParamsToNull()
		{
			Shader.SetGlobalVector("_DS_BetaParams", Vector4.zero);
			Shader.SetGlobalVector("_DS_RBetaS", Vector4.zero);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003598C File Offset: 0x00033D8C
		public void SetDebugKeywords()
		{
			if (this.m_ShowTemporalRejection)
			{
				this.m_Material.EnableKeyword("SHOW_TEMPORAL_REJECTION");
			}
			else
			{
				this.m_Material.DisableKeyword("SHOW_TEMPORAL_REJECTION");
			}
			if (this.m_ShowUpsampleThreshold)
			{
				this.m_Material.EnableKeyword("SHOW_UPSAMPLE_THRESHOLD");
			}
			else
			{
				this.m_Material.DisableKeyword("SHOW_UPSAMPLE_THRESHOLD");
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000359FC File Offset: 0x00033DFC
		public void SetSkyboxKeywords()
		{
			if (this.m_ApplyAirToSkybox)
			{
				this.m_Material.EnableKeyword("DS_HAZE_APPLY_RAYLEIGH");
			}
			else
			{
				this.m_Material.DisableKeyword("DS_HAZE_APPLY_RAYLEIGH");
			}
			if (this.m_ApplyHazeToSkybox)
			{
				this.m_Material.EnableKeyword("DS_HAZE_APPLY_MIE");
			}
			else
			{
				this.m_Material.DisableKeyword("DS_HAZE_APPLY_MIE");
			}
			if (this.m_ApplyFogExtinctionToSkybox)
			{
				this.m_Material.EnableKeyword("DS_HAZE_APPLY_FOG_EXTINCTION");
			}
			else
			{
				this.m_Material.DisableKeyword("DS_HAZE_APPLY_FOG_EXTINCTION");
			}
			if (this.m_ApplyFogLightingToSkybox)
			{
				this.m_Material.EnableKeyword("DS_HAZE_APPLY_FOG_RADIANCE");
			}
			else
			{
				this.m_Material.DisableKeyword("DS_HAZE_APPLY_FOG_RADIANCE");
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00035ACC File Offset: 0x00033ECC
		public void SetTemporalKeywords()
		{
			if (this.WillRenderWithTemporalReprojection)
			{
				this.m_Material.EnableKeyword("DS_HAZE_TEMPORAL");
			}
			else
			{
				this.m_Material.DisableKeyword("DS_HAZE_TEMPORAL");
				if (this.m_ShowTemporalRejection)
				{
					this.m_ShowTemporalRejection = false;
					this.m_Material.DisableKeyword("SHOW_TEMPORAL_REJECTION");
				}
				if (this.m_RadianceTarget_01)
				{
					this.m_RadianceTarget_01.Release();
					UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_01);
					this.m_RadianceTarget_01 = null;
				}
				if (this.m_RadianceTarget_02)
				{
					this.m_RadianceTarget_02.Release();
					UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_02);
					this.m_RadianceTarget_02 = null;
				}
				if (this.m_PreviousDepthTarget)
				{
					this.m_PreviousDepthTarget.Release();
					UnityEngine.Object.DestroyImmediate(this.m_PreviousDepthTarget);
					this.m_PreviousDepthTarget = null;
				}
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00035BB4 File Offset: 0x00033FB4
		private void SetShaderKeyWords()
		{
			if (this.m_ShadowProjectionType == ShadowProjection.CloseFit)
			{
				this.m_Material.EnableKeyword("SHADOW_PROJ_CLOSE");
			}
			else if (this.m_ShadowProjectionType == ShadowProjection.StableFit)
			{
				this.m_Material.DisableKeyword("SHADOW_PROJ_CLOSE");
			}
			if (DS_HazeCore.Instance != null)
			{
				DS_HazeCore.HeightFalloffType heightFalloff = DS_HazeCore.Instance.HeightFalloff;
				if (heightFalloff != DS_HazeCore.HeightFalloffType.None)
				{
					if (heightFalloff != DS_HazeCore.HeightFalloffType.Exponential)
					{
						this.m_Material.EnableKeyword("DS_HAZE_HEIGHT_FALLOFF_NONE");
					}
					else
					{
						this.m_Material.DisableKeyword("DS_HAZE_HEIGHT_FALLOFF_NONE");
					}
				}
				else
				{
					this.m_Material.EnableKeyword("DS_HAZE_HEIGHT_FALLOFF_NONE");
				}
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00035C6C File Offset: 0x0003406C
		private void OnEnable()
		{
			this.SetGlobalParamsToNull();
			this.m_Camera = base.GetComponent<Camera>();
			if (!this.m_Camera)
			{
				base.enabled = false;
				return;
			}
			if (!this.CheckHasSystemSupport())
			{
				base.enabled = false;
				return;
			}
			if (DS_HazeView.kShader == null)
			{
				DS_HazeView.kShader = Resources.Load<Shader>("DS_Haze");
			}
			if (this.m_Material == null)
			{
				this.m_Material = new Material(DS_HazeView.kShader);
				this.m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			if (this.m_Camera.actualRenderingPath == RenderingPath.Forward && (this.m_Camera.depthTextureMode & DepthTextureMode.Depth) != DepthTextureMode.Depth)
			{
				this.m_Camera.depthTextureMode = (this.m_Camera.depthTextureMode | DepthTextureMode.Depth);
			}
			if (this.m_RenderNonShadowVolumes == null)
			{
				CommandBuffer[] commandBuffers = this.m_Camera.GetCommandBuffers(CameraEvent.BeforeImageEffectsOpaque);
				bool flag = false;
				foreach (CommandBuffer commandBuffer in commandBuffers)
				{
					if (commandBuffer.name == DS_HazeView.kRenderLightVolumeCmdBufferName)
					{
						this.m_RenderNonShadowVolumes = commandBuffer;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_RenderNonShadowVolumes = new CommandBuffer();
					this.m_RenderNonShadowVolumes.name = DS_HazeView.kRenderLightVolumeCmdBufferName;
					this.m_Camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_RenderNonShadowVolumes);
				}
			}
			this.m_CurrentRadianceTarget = this.m_RadianceTarget_01;
			this.m_PreviousRadianceTarget = this.m_RadianceTarget_02;
			this.SetSkyboxKeywords();
			this.SetDebugKeywords();
			this.m_ColourSpace = QualitySettings.activeColorSpace;
			this.m_PreviousRenderPath = this.m_Camera.actualRenderingPath;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00035E10 File Offset: 0x00034210
		private void CreateRadianceTarget(string name, out RenderTexture radianceTarget)
		{
			if (this.m_Camera.allowHDR)
			{
				radianceTarget = new RenderTexture(this.m_Camera.pixelWidth, this.m_Camera.pixelHeight, 0, RenderTextureFormat.ARGBHalf);
			}
			else
			{
				radianceTarget = new RenderTexture(this.m_Camera.pixelWidth, this.m_Camera.pixelHeight, 0, RenderTextureFormat.ARGB32);
			}
			radianceTarget.name = name;
			radianceTarget.antiAliasing = this.AntiAliasingLevel();
			radianceTarget.useMipMap = false;
			radianceTarget.hideFlags = HideFlags.HideAndDontSave;
			radianceTarget.filterMode = FilterMode.Point;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00035EA0 File Offset: 0x000342A0
		private void CreateDepthTarget(string name, out RenderTexture depthTarget, bool downsample = false)
		{
			depthTarget = new RenderTexture((!downsample) ? this.m_Camera.pixelWidth : this.m_X, (!downsample) ? this.m_Camera.pixelHeight : this.m_Y, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear);
			depthTarget.name = name;
			depthTarget.antiAliasing = 1;
			depthTarget.useMipMap = false;
			depthTarget.hideFlags = HideFlags.HideAndDontSave;
			depthTarget.filterMode = FilterMode.Point;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00035F1C File Offset: 0x0003431C
		private bool CameraHasClearRadianceCmdBuffer(out CommandBuffer foundCmd)
		{
			CommandBuffer[] commandBuffers;
			if (this.m_Camera.actualRenderingPath == RenderingPath.DeferredShading)
			{
				commandBuffers = this.m_Camera.GetCommandBuffers(CameraEvent.BeforeGBuffer);
			}
			else
			{
				CameraEvent evt = ((this.m_Camera.depthTextureMode & DepthTextureMode.DepthNormals) != DepthTextureMode.DepthNormals) ? CameraEvent.BeforeDepthTexture : CameraEvent.BeforeDepthNormalsTexture;
				commandBuffers = this.m_Camera.GetCommandBuffers(evt);
			}
			foreach (CommandBuffer commandBuffer in commandBuffers)
			{
				if (commandBuffer.name == DS_HazeView.kClearRadianceCmdBufferName)
				{
					foundCmd = commandBuffer;
					return true;
				}
			}
			foundCmd = null;
			return false;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00035FB4 File Offset: 0x000343B4
		private CommandBuffer LightHasCascadesCopyCmdBuffer()
		{
			CommandBuffer[] commandBuffers = this.m_DirectLight.GetCommandBuffers(LightEvent.AfterShadowMap);
			foreach (CommandBuffer commandBuffer in commandBuffers)
			{
				if (commandBuffer.name == DS_HazeView.kShadowCascadesCmdBufferName)
				{
					return commandBuffer;
				}
			}
			return null;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00036000 File Offset: 0x00034400
		private CommandBuffer LightHasRenderCmdBuffer()
		{
			CommandBuffer[] commandBuffers = this.m_DirectLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask);
			foreach (CommandBuffer commandBuffer in commandBuffers)
			{
				if (commandBuffer.name == DS_HazeView.kDirectionalLightCmdBufferName)
				{
					return commandBuffer;
				}
			}
			return null;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003604C File Offset: 0x0003444C
		public void RemoveCommandBufferFromLight(Light light)
		{
			CommandBuffer[] commandBuffers = light.GetCommandBuffers(LightEvent.AfterShadowMap);
			for (int i = 0; i < commandBuffers.Length; i++)
			{
				if (commandBuffers[i].name == DS_HazeView.kShadowCascadesCmdBufferName)
				{
					light.RemoveCommandBuffer(LightEvent.AfterShadowMap, commandBuffers[i]);
					break;
				}
			}
			commandBuffers = light.GetCommandBuffers(LightEvent.AfterScreenspaceMask);
			for (int j = 0; j < commandBuffers.Length; j++)
			{
				if (commandBuffers[j].name == DS_HazeView.kDirectionalLightCmdBufferName)
				{
					light.RemoveCommandBuffer(LightEvent.AfterScreenspaceMask, commandBuffers[j]);
					break;
				}
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000360E0 File Offset: 0x000344E0
		private void RenderPathChanged()
		{
			if (this.m_Camera.actualRenderingPath == RenderingPath.Forward && (this.m_Camera.depthTextureMode & DepthTextureMode.Depth) != DepthTextureMode.Depth)
			{
				this.m_Camera.depthTextureMode = (this.m_Camera.depthTextureMode | DepthTextureMode.Depth);
			}
			if (this.m_ClearRadianceCmdBuffer != null)
			{
				CameraEvent evt;
				if (this.m_PreviousRenderPath == RenderingPath.DeferredShading)
				{
					evt = CameraEvent.BeforeGBuffer;
				}
				else
				{
					evt = (((this.m_Camera.depthTextureMode & DepthTextureMode.DepthNormals) != DepthTextureMode.DepthNormals) ? CameraEvent.BeforeDepthTexture : CameraEvent.BeforeDepthNormalsTexture);
				}
				CommandBuffer[] commandBuffers = this.m_Camera.GetCommandBuffers(evt);
				foreach (CommandBuffer commandBuffer in commandBuffers)
				{
					if (commandBuffer.name == DS_HazeView.kClearRadianceCmdBufferName)
					{
						this.m_Camera.RemoveCommandBuffer(evt, commandBuffer);
						break;
					}
				}
			}
			this.m_PreviousRenderPath = this.m_Camera.actualRenderingPath;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000361C8 File Offset: 0x000345C8
		private void UpdateResources()
		{
			this.m_X = this.m_Camera.pixelWidth / (int)this.m_DownsampleFactor;
			this.m_Y = this.m_Camera.pixelHeight / (int)this.m_DownsampleFactor;
			if (this.m_Camera.actualRenderingPath != this.m_PreviousRenderPath)
			{
				this.RenderPathChanged();
			}
			RenderTextureFormat renderTextureFormat = (!this.m_Camera.allowHDR) ? RenderTextureFormat.ARGB32 : RenderTextureFormat.ARGBHalf;
			bool flag = this.m_ColourSpace != QualitySettings.activeColorSpace;
			this.m_ColourSpace = QualitySettings.activeColorSpace;
			if (this.WillRenderWithTemporalReprojection)
			{
				if (this.m_RadianceTarget_01 == null)
				{
					this.CreateRadianceTarget(DS_HazeView.kRadianceTarget01Name, out this.m_RadianceTarget_01);
					this.m_CurrentRadianceTarget = this.m_RadianceTarget_01;
				}
				else if (flag || this.m_RadianceTarget_01.width != this.m_Camera.pixelWidth || this.m_RadianceTarget_01.height != this.m_Camera.pixelHeight || this.m_RadianceTarget_01.format != renderTextureFormat)
				{
					UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_01);
					this.CreateRadianceTarget(DS_HazeView.kRadianceTarget01Name, out this.m_RadianceTarget_01);
					this.m_CurrentRadianceTarget = this.m_RadianceTarget_01;
				}
				if (this.m_RadianceTarget_02 == null)
				{
					this.CreateRadianceTarget(DS_HazeView.kRadianceTarget02Name, out this.m_RadianceTarget_02);
					this.m_PreviousRadianceTarget = this.m_RadianceTarget_02;
				}
				else if (flag || this.m_RadianceTarget_02.width != this.m_Camera.pixelWidth || this.m_RadianceTarget_02.height != this.m_Camera.pixelHeight || this.m_RadianceTarget_02.format != renderTextureFormat)
				{
					UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_02);
					this.CreateRadianceTarget(DS_HazeView.kRadianceTarget02Name, out this.m_RadianceTarget_02);
					this.m_PreviousRadianceTarget = this.m_RadianceTarget_02;
				}
				if (this.m_PreviousDepthTarget == null)
				{
					this.CreateDepthTarget(DS_HazeView.kPreviousDepthTargetName, out this.m_PreviousDepthTarget, false);
				}
				else if (this.m_PreviousDepthTarget.width != this.m_Camera.pixelWidth || this.m_PreviousDepthTarget.height != this.m_Camera.pixelHeight)
				{
					UnityEngine.Object.DestroyImmediate(this.m_PreviousDepthTarget);
					this.CreateDepthTarget(DS_HazeView.kPreviousDepthTargetName, out this.m_PreviousDepthTarget, false);
				}
			}
			if (this.m_ClearRadianceCmdBuffer == null)
			{
				this.m_ClearRadianceCmdBuffer = new CommandBuffer();
				this.m_ClearRadianceCmdBuffer.name = DS_HazeView.kClearRadianceCmdBufferName;
			}
			CameraEvent evt;
			if (this.m_Camera.actualRenderingPath == RenderingPath.DeferredShading)
			{
				evt = CameraEvent.BeforeGBuffer;
			}
			else
			{
				evt = (((this.m_Camera.depthTextureMode & DepthTextureMode.DepthNormals) != DepthTextureMode.DepthNormals) ? CameraEvent.BeforeDepthTexture : CameraEvent.BeforeDepthNormalsTexture);
			}
			CommandBuffer commandBuffer;
			if (!this.CameraHasClearRadianceCmdBuffer(out commandBuffer))
			{
				this.m_Camera.AddCommandBuffer(evt, this.m_ClearRadianceCmdBuffer);
			}
			else if (commandBuffer != this.m_ClearRadianceCmdBuffer)
			{
				this.m_Camera.RemoveCommandBuffer(evt, commandBuffer);
				commandBuffer.Dispose();
				this.m_Camera.AddCommandBuffer(evt, this.m_ClearRadianceCmdBuffer);
			}
			if (this.m_DirectLight)
			{
				this.m_ShadowCascadesCmdBuffer = this.LightHasCascadesCopyCmdBuffer();
				if (this.m_ShadowCascadesCmdBuffer == null)
				{
					this.m_ShadowCascadesCmdBuffer = new CommandBuffer();
					this.m_ShadowCascadesCmdBuffer.name = DS_HazeView.kShadowCascadesCmdBufferName;
					this.m_ShadowCascadesCmdBuffer.SetGlobalTexture("_ShadowCascades", new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive));
					this.m_DirectLight.AddCommandBuffer(LightEvent.AfterShadowMap, this.m_ShadowCascadesCmdBuffer);
				}
				this.m_DirectionalLightCmdBuffer = this.LightHasRenderCmdBuffer();
				if (this.m_DirectionalLightCmdBuffer == null)
				{
					this.m_DirectionalLightCmdBuffer = new CommandBuffer();
					this.m_DirectionalLightCmdBuffer.name = DS_HazeView.kDirectionalLightCmdBufferName;
					this.m_DirectLight.AddCommandBuffer(LightEvent.AfterScreenspaceMask, this.m_DirectionalLightCmdBuffer);
				}
				if (this.m_ShadowProjectionType != QualitySettings.shadowProjection)
				{
					this.m_ShadowProjectionType = QualitySettings.shadowProjection;
				}
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000365A4 File Offset: 0x000349A4
		private void OnDisable()
		{
			this.SetGlobalParamsToNull();
			CommandBuffer[] commandBuffers = this.m_Camera.GetCommandBuffers(CameraEvent.AfterSkybox);
			CameraEvent evt;
			if (this.m_Camera.actualRenderingPath == RenderingPath.DeferredShading)
			{
				evt = CameraEvent.BeforeGBuffer;
			}
			else
			{
				evt = (((this.m_Camera.depthTextureMode & DepthTextureMode.DepthNormals) != DepthTextureMode.DepthNormals) ? CameraEvent.BeforeDepthTexture : CameraEvent.BeforeDepthNormalsTexture);
			}
			commandBuffers = this.m_Camera.GetCommandBuffers(evt);
			foreach (CommandBuffer commandBuffer in commandBuffers)
			{
				if (commandBuffer.name == DS_HazeView.kClearRadianceCmdBufferName)
				{
					this.m_Camera.RemoveCommandBuffer(evt, commandBuffer);
					break;
				}
			}
			if (this.m_DirectLight)
			{
				commandBuffers = this.m_DirectLight.GetCommandBuffers(LightEvent.AfterShadowMap);
				foreach (CommandBuffer commandBuffer2 in commandBuffers)
				{
					if (commandBuffer2.name == DS_HazeView.kShadowCascadesCmdBufferName)
					{
						this.m_DirectLight.RemoveCommandBuffer(LightEvent.AfterShadowMap, commandBuffer2);
						break;
					}
				}
				commandBuffers = this.m_DirectLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask);
				foreach (CommandBuffer commandBuffer3 in commandBuffers)
				{
					if (commandBuffer3.name == DS_HazeView.kDirectionalLightCmdBufferName)
					{
						this.m_DirectLight.RemoveCommandBuffer(LightEvent.AfterScreenspaceMask, commandBuffer3);
						break;
					}
				}
			}
			if (this.m_LightVolumeCmdBuffers.Count > 0)
			{
				foreach (KeyValuePair<Light, CommandBuffer> keyValuePair in this.m_LightVolumeCmdBuffers)
				{
					keyValuePair.Key.RemoveCommandBuffer(LightEvent.AfterShadowMap, keyValuePair.Value);
					keyValuePair.Value.Dispose();
				}
				this.m_LightVolumeCmdBuffers.Clear();
			}
			if (this.m_RenderNonShadowVolumes != null)
			{
				this.m_RenderNonShadowVolumes.Clear();
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000367AC File Offset: 0x00034BAC
		private void OnDestroy()
		{
			if (this.m_RadianceTarget_01)
			{
				this.m_RadianceTarget_01.Release();
				UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_01);
				this.m_RadianceTarget_01 = null;
			}
			if (this.m_RadianceTarget_02)
			{
				this.m_RadianceTarget_02.Release();
				UnityEngine.Object.DestroyImmediate(this.m_RadianceTarget_02);
				this.m_RadianceTarget_02 = null;
			}
			if (this.m_PreviousDepthTarget)
			{
				this.m_PreviousDepthTarget.Release();
				UnityEngine.Object.DestroyImmediate(this.m_PreviousDepthTarget);
				this.m_PreviousDepthTarget = null;
			}
			if (this.m_ClearRadianceCmdBuffer != null)
			{
				if (this.m_Camera.actualRenderingPath == RenderingPath.DeferredShading)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeGBuffer, this.m_ClearRadianceCmdBuffer);
				}
				else
				{
					CameraEvent evt = ((this.m_Camera.depthTextureMode & DepthTextureMode.DepthNormals) != DepthTextureMode.DepthNormals) ? CameraEvent.BeforeDepthTexture : CameraEvent.BeforeDepthNormalsTexture;
					this.m_Camera.RemoveCommandBuffer(evt, this.m_ClearRadianceCmdBuffer);
				}
				this.m_ClearRadianceCmdBuffer.Dispose();
				this.m_ClearRadianceCmdBuffer = null;
			}
			if (this.m_ShadowCascadesCmdBuffer != null)
			{
				if (this.m_DirectLight != null)
				{
					this.m_DirectLight.RemoveCommandBuffer(LightEvent.AfterShadowMap, this.m_ShadowCascadesCmdBuffer);
				}
				this.m_ShadowCascadesCmdBuffer.Dispose();
				this.m_ShadowCascadesCmdBuffer = null;
			}
			if (this.m_DirectionalLightCmdBuffer != null)
			{
				if (this.m_DirectLight != null)
				{
					this.m_DirectLight.RemoveCommandBuffer(LightEvent.AfterScreenspaceMask, this.m_DirectionalLightCmdBuffer);
				}
				this.m_DirectionalLightCmdBuffer.Dispose();
				this.m_DirectionalLightCmdBuffer = null;
			}
			if (this.m_LightVolumeCmdBuffers.Count > 0)
			{
				foreach (KeyValuePair<Light, CommandBuffer> keyValuePair in this.m_LightVolumeCmdBuffers)
				{
					keyValuePair.Key.RemoveCommandBuffer(LightEvent.AfterShadowMap, keyValuePair.Value);
					keyValuePair.Value.Dispose();
				}
				this.m_LightVolumeCmdBuffers.Clear();
			}
			if (this.m_RenderNonShadowVolumes != null)
			{
				this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_RenderNonShadowVolumes);
				this.m_RenderNonShadowVolumes.Dispose();
				this.m_RenderNonShadowVolumes = null;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000369E8 File Offset: 0x00034DE8
		private void OnPreRender()
		{
			if (!this.CheckHasSystemSupport())
			{
				base.enabled = false;
			}
			this.UpdateResources();
			this.SetShaderKeyWords();
			RenderTextureFormat format = (!this.m_Camera.allowHDR) ? RenderTextureFormat.ARGB32 : RenderTextureFormat.ARGBHalf;
			this.m_PerFrameRadianceTarget = RenderTexture.GetTemporary(this.m_X, this.m_Y, 0, format, RenderTextureReadWrite.Linear, this.AntiAliasingLevel());
			this.m_PerFrameRadianceTarget.name = "_DS_Haze_PerFrameRadiance";
			this.m_PerFrameRadianceTarget.filterMode = FilterMode.Point;
			this.m_ClearRadianceCmdBuffer.Clear();
			this.m_ClearRadianceCmdBuffer.SetRenderTarget(this.m_PerFrameRadianceTarget);
			this.m_ClearRadianceCmdBuffer.ClearRenderTarget(false, true, Color.clear);
			DS_HazeCore instance = DS_HazeCore.Instance;
			DS_HazeContextItem ds_HazeContextItem;
			if (this.m_OverrideContextAsset && this.m_Context != null)
			{
				if (this.m_OverrideContextVariant)
				{
					ds_HazeContextItem = this.m_Context.Context.GetItemAtIndex(this.m_ContextItemIndex);
				}
				else
				{
					ds_HazeContextItem = this.m_Context.Context.GetContextItemBlended(this.m_Time);
				}
			}
			else
			{
				if (instance == null)
				{
					this.SetGlobalParamsToNull();
					return;
				}
				ds_HazeContextItem = instance.GetRenderContextAtPosition(base.transform.position);
			}
			if (ds_HazeContextItem == null)
			{
				this.SetGlobalParamsToNull();
			}
			else
			{
				this.SetMaterialFromContext(ds_HazeContextItem);
				float farClipPlane = this.m_Camera.farClipPlane;
				float num = this.m_Camera.fieldOfView * 0.5f;
				float num2 = Mathf.Tan(num * 0.017453292f);
				float d = num2 * this.m_Camera.aspect;
				Vector3 a = base.transform.forward * farClipPlane;
				Vector3 vector = base.transform.right * d * farClipPlane;
				Vector3 vector2 = base.transform.up * num2 * farClipPlane;
				this.m_Material.SetVector("_ViewportCorner", a - vector - vector2);
				this.m_Material.SetVector("_ViewportRight", vector * 2f);
				this.m_Material.SetVector("_ViewportUp", vector2 * 2f);
				if (this.m_DirectLight && this.m_RenderAtmosphereVolumetrics)
				{
					this.m_DirectionalLightCmdBuffer.Blit(BuiltinRenderTextureType.None, this.m_PerFrameRadianceTarget, this.m_Material, (int)(this.m_VolumeSamples + ((this.m_DownsampleFactor != DS_HazeView.SizeFactor.Half) ? 3 : 0)));
				}
			}
			if (!this.m_RenderLocalVolumetrics)
			{
				return;
			}
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.m_Camera.projectionMatrix, true);
			Matrix4x4 viewProjMtx = gpuprojectionMatrix * this.m_Camera.worldToCameraMatrix;
			instance.GetRenderLightVolumes(base.transform.position, this.m_PerFrameLightVolumes, this.m_PerFrameShadowLightVolumes);
			if (this.m_PerFrameLightVolumes.Count > 0)
			{
				this.m_RenderNonShadowVolumes.SetRenderTarget(this.m_PerFrameRadianceTarget);
			}
			foreach (DS_HazeLightVolume ds_HazeLightVolume in this.m_PerFrameLightVolumes)
			{
				ds_HazeLightVolume.SetupMaterialPerFrame(viewProjMtx, this.m_Camera.worldToCameraMatrix, base.transform, (!this.WillRenderWithTemporalReprojection) ? 0f : this.m_InterleavedOffsetIndex);
				ds_HazeLightVolume.AddLightRenderCommand(base.transform, this.m_RenderNonShadowVolumes, (int)this.m_DownsampleFactor);
			}
			foreach (DS_HazeLightVolume ds_HazeLightVolume2 in this.m_PerFrameShadowLightVolumes)
			{
				ds_HazeLightVolume2.SetupMaterialPerFrame(viewProjMtx, this.m_Camera.worldToCameraMatrix, base.transform, (!this.WillRenderWithTemporalReprojection) ? 0f : this.m_InterleavedOffsetIndex);
				ds_HazeLightVolume2.FillLightCommandBuffer(this.m_PerFrameRadianceTarget, base.transform, (int)this.m_DownsampleFactor);
				this.m_LightVolumeCmdBuffers.Add(ds_HazeLightVolume2.LightSource, ds_HazeLightVolume2.RenderCommandBuffer);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00036E40 File Offset: 0x00035240
		private void BlitToMRT(RenderTexture source, RenderTexture[] destination, Material mat, int pass)
		{
			RenderBuffer[] array = new RenderBuffer[destination.Length];
			for (int i = 0; i < destination.Length; i++)
			{
				array[i] = destination[i].colorBuffer;
			}
			Graphics.SetRenderTarget(array, destination[0].depthBuffer);
			mat.SetTexture("_MainTex", source);
			mat.SetPass(pass);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00036F4C File Offset: 0x0003534C
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			RenderTexture renderTexture = null;
			RenderTexture renderTexture2 = null;
			if (this.m_RenderAtmosphereVolumetrics || this.m_RenderLocalVolumetrics)
			{
				renderTexture = RenderTexture.GetTemporary(this.m_X, this.m_Y, 0, (!this.m_Camera.allowHDR) ? RenderTextureFormat.ARGB32 : RenderTextureFormat.ARGBHalf);
				renderTexture2 = RenderTexture.GetTemporary(this.m_X, this.m_Y, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear, 1);
				Graphics.Blit(null, renderTexture2, this.m_Material, (this.m_DownsampleFactor != DS_HazeView.SizeFactor.Half) ? 11 : 10);
				this.m_Material.SetTexture("_HalfResDepth", renderTexture2);
				Graphics.Blit(this.m_PerFrameRadianceTarget, renderTexture, this.m_Material, 6);
				Graphics.Blit(renderTexture, this.m_PerFrameRadianceTarget, this.m_Material, 7);
				if (this.m_TemporalReprojection)
				{
					this.m_Material.SetTexture("_PrevAccumBuffer", this.m_PreviousRadianceTarget);
					this.m_Material.SetTexture("_PrevDepthBuffer", this.m_PreviousDepthTarget);
				}
			}
			this.m_PerFrameRadianceTarget.filterMode = FilterMode.Bilinear;
			this.m_Material.SetTexture("_RadianceBuffer", this.m_PerFrameRadianceTarget);
			if (dest == null)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(src.width, src.height, src.depth, src.format);
				if (this.WillRenderWithTemporalReprojection)
				{
					RenderTexture[] destination = new RenderTexture[]
					{
						temporary,
						this.m_CurrentRadianceTarget
					};
					this.BlitToMRT(src, destination, this.m_Material, 8);
				}
				else
				{
					Graphics.Blit(src, temporary, this.m_Material, 8);
				}
				Graphics.Blit(temporary, null);
				RenderTexture.ReleaseTemporary(temporary);
			}
			else if (this.WillRenderWithTemporalReprojection)
			{
				RenderTexture[] destination2 = new RenderTexture[]
				{
					dest,
					this.m_CurrentRadianceTarget
				};
				this.BlitToMRT(src, destination2, this.m_Material, 8);
			}
			else
			{
				Graphics.Blit(src, dest, this.m_Material, 8);
			}
			if (this.WillRenderWithTemporalReprojection)
			{
				Graphics.Blit(src, this.m_PreviousDepthTarget, this.m_Material, 9);
				Graphics.SetRenderTarget(dest);
				Shader.SetGlobalTexture("_DS_RadianceBuffer", this.m_CurrentRadianceTarget);
				RenderTexture.ReleaseTemporary(this.m_PerFrameRadianceTarget);
			}
			else
			{
				Shader.SetGlobalTexture("_DS_RadianceBuffer", this.m_PerFrameRadianceTarget);
			}
			if (renderTexture != null)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			if (renderTexture2 != null)
			{
				RenderTexture.ReleaseTemporary(renderTexture2);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000371A0 File Offset: 0x000355A0
		private void OnPostRender()
		{
			if (this.WillRenderWithTemporalReprojection)
			{
				RenderTexture currentRadianceTarget = this.m_CurrentRadianceTarget;
				this.m_CurrentRadianceTarget = this.m_PreviousRadianceTarget;
				this.m_PreviousRadianceTarget = currentRadianceTarget;
				Matrix4x4 worldToCameraMatrix = this.m_Camera.worldToCameraMatrix;
				Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.m_Camera.projectionMatrix, true);
				this.m_PreviousViewProjMatrix = gpuprojectionMatrix * worldToCameraMatrix;
				this.m_PreviousInvViewProjMatrix = this.m_PreviousViewProjMatrix.inverse;
			}
			else
			{
				RenderTexture.ReleaseTemporary(this.m_PerFrameRadianceTarget);
			}
			if (this.m_LightVolumeCmdBuffers.Count > 0)
			{
				foreach (KeyValuePair<Light, CommandBuffer> keyValuePair in this.m_LightVolumeCmdBuffers)
				{
					keyValuePair.Value.Clear();
				}
				this.m_LightVolumeCmdBuffers.Clear();
			}
			if (this.m_DirectLight)
			{
				this.m_DirectionalLightCmdBuffer.Clear();
			}
			this.m_RenderNonShadowVolumes.Clear();
			this.m_PerFrameLightVolumes.Clear();
			this.m_PerFrameShadowLightVolumes.Clear();
		}

		// Token: 0x04000C07 RID: 3079
		private static string kClearRadianceCmdBufferName = "DS_Haze_ClearRadiance";

		// Token: 0x04000C08 RID: 3080
		private static string kShadowCascadesCmdBufferName = "DS_Haze_ShadowCascadesCopy";

		// Token: 0x04000C09 RID: 3081
		private static string kDirectionalLightCmdBufferName = "DS_Haze_DirectLight";

		// Token: 0x04000C0A RID: 3082
		private static string kRenderLightVolumeCmdBufferName = "DS_Haze_RenderLightVolume";

		// Token: 0x04000C0B RID: 3083
		private static string kPreviousDepthTargetName = "DS_Haze_PreviousDepthTarget";

		// Token: 0x04000C0C RID: 3084
		private static string kRadianceTarget01Name = "DS_Haze_RadianceTarget_01";

		// Token: 0x04000C0D RID: 3085
		private static string kRadianceTarget02Name = "DS_Haze_RadianceTarget_02";

		// Token: 0x04000C0E RID: 3086
		private static Shader kShader;

		// Token: 0x04000C0F RID: 3087
		[SerializeField]
		private bool m_OverrideTime;

		// Token: 0x04000C10 RID: 3088
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Time = 0.5f;

		// Token: 0x04000C11 RID: 3089
		[SerializeField]
		private bool m_OverrideContextAsset;

		// Token: 0x04000C12 RID: 3090
		[SerializeField]
		private DS_HazeContextAsset m_Context;

		// Token: 0x04000C13 RID: 3091
		[SerializeField]
		private bool m_OverrideContextVariant;

		// Token: 0x04000C14 RID: 3092
		[SerializeField]
		private int m_ContextItemIndex;

		// Token: 0x04000C15 RID: 3093
		[SerializeField]
		private Light m_DirectLight;

		// Token: 0x04000C16 RID: 3094
		[SerializeField]
		private bool m_RenderAtmosphereVolumetrics = true;

		// Token: 0x04000C17 RID: 3095
		[SerializeField]
		private bool m_RenderLocalVolumetrics = true;

		// Token: 0x04000C18 RID: 3096
		[SerializeField]
		private bool m_TemporalReprojection = true;

		// Token: 0x04000C19 RID: 3097
		[SerializeField]
		private DS_HazeView.SizeFactor m_DownsampleFactor = DS_HazeView.SizeFactor.Half;

		// Token: 0x04000C1A RID: 3098
		[SerializeField]
		private DS_HazeView.VolumeSamples m_VolumeSamples;

		// Token: 0x04000C1B RID: 3099
		[SerializeField]
		[Range(100f, 5000f)]
		private int m_GaussianDepthFalloff = 500;

		// Token: 0x04000C1C RID: 3100
		[SerializeField]
		[Range(0f, 0.5f)]
		private float m_UpsampleDepthThreshold = 0.06f;

		// Token: 0x04000C1D RID: 3101
		[SerializeField]
		[Range(0.001f, 1f)]
		private float m_TemporalRejectionScale = 0.1f;

		// Token: 0x04000C1E RID: 3102
		[SerializeField]
		[Range(0.1f, 0.9f)]
		private float m_TemporalBlendFactor = 0.25f;

		// Token: 0x04000C1F RID: 3103
		private ShadowProjection m_ShadowProjectionType = ShadowProjection.StableFit;

		// Token: 0x04000C20 RID: 3104
		[SerializeField]
		private bool m_ApplyAirToSkybox;

		// Token: 0x04000C21 RID: 3105
		[SerializeField]
		private bool m_ApplyHazeToSkybox = true;

		// Token: 0x04000C22 RID: 3106
		[SerializeField]
		private bool m_ApplyFogExtinctionToSkybox = true;

		// Token: 0x04000C23 RID: 3107
		[SerializeField]
		private bool m_ApplyFogLightingToSkybox = true;

		// Token: 0x04000C24 RID: 3108
		[SerializeField]
		private bool m_ShowTemporalRejection;

		// Token: 0x04000C25 RID: 3109
		[SerializeField]
		private bool m_ShowUpsampleThreshold;

		// Token: 0x04000C26 RID: 3110
		private Camera m_Camera;

		// Token: 0x04000C27 RID: 3111
		private RenderTexture m_PerFrameRadianceTarget;

		// Token: 0x04000C28 RID: 3112
		private RenderTexture m_RadianceTarget_01;

		// Token: 0x04000C29 RID: 3113
		private RenderTexture m_RadianceTarget_02;

		// Token: 0x04000C2A RID: 3114
		private RenderTexture m_CurrentRadianceTarget;

		// Token: 0x04000C2B RID: 3115
		private RenderTexture m_PreviousRadianceTarget;

		// Token: 0x04000C2C RID: 3116
		private RenderTexture m_PreviousDepthTarget;

		// Token: 0x04000C2D RID: 3117
		private CommandBuffer m_ShadowCascadesCmdBuffer;

		// Token: 0x04000C2E RID: 3118
		private CommandBuffer m_DirectionalLightCmdBuffer;

		// Token: 0x04000C2F RID: 3119
		private CommandBuffer m_ClearRadianceCmdBuffer;

		// Token: 0x04000C30 RID: 3120
		private CommandBuffer m_RenderNonShadowVolumes;

		// Token: 0x04000C31 RID: 3121
		private Material m_Material;

		// Token: 0x04000C32 RID: 3122
		private Matrix4x4 m_PreviousViewProjMatrix = Matrix4x4.identity;

		// Token: 0x04000C33 RID: 3123
		private Matrix4x4 m_PreviousInvViewProjMatrix = Matrix4x4.identity;

		// Token: 0x04000C34 RID: 3124
		private float m_InterleavedOffsetIndex;

		// Token: 0x04000C35 RID: 3125
		private int m_X;

		// Token: 0x04000C36 RID: 3126
		private int m_Y;

		// Token: 0x04000C37 RID: 3127
		private RenderingPath m_PreviousRenderPath;

		// Token: 0x04000C38 RID: 3128
		private ColorSpace m_ColourSpace;

		// Token: 0x04000C39 RID: 3129
		private List<DS_HazeLightVolume> m_PerFrameLightVolumes = new List<DS_HazeLightVolume>();

		// Token: 0x04000C3A RID: 3130
		private List<DS_HazeLightVolume> m_PerFrameShadowLightVolumes = new List<DS_HazeLightVolume>();

		// Token: 0x04000C3B RID: 3131
		private Dictionary<Light, CommandBuffer> m_LightVolumeCmdBuffers = new Dictionary<Light, CommandBuffer>();

		// Token: 0x020002F4 RID: 756
		private enum SizeFactor
		{
			// Token: 0x04000C3D RID: 3133
			Half = 2,
			// Token: 0x04000C3E RID: 3134
			Quarter = 4
		}

		// Token: 0x020002F5 RID: 757
		private enum VolumeSamples
		{
			// Token: 0x04000C40 RID: 3136
			x16,
			// Token: 0x04000C41 RID: 3137
			x24,
			// Token: 0x04000C42 RID: 3138
			x32
		}
	}
}

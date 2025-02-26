using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LuxWater
{
	// Token: 0x020003E4 RID: 996
	[RequireComponent(typeof(Camera))]
	public class LuxWater_UnderWaterRendering : MonoBehaviour
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x00068574 File Offset: 0x00066974
		private void OnEnable()
		{
			if (LuxWater_UnderWaterRendering.instance != null)
			{
				UnityEngine.Object.Destroy(this);
			}
			else
			{
				LuxWater_UnderWaterRendering.instance = this;
			}
			this.mat = new Material(Shader.Find("Hidden/Lux Water/WaterMask"));
			this.blurMaterial = new Material(Shader.Find("Hidden/Lux Water/BlurEffectConeTap"));
			this.blitMaterial = new Material(Shader.Find("Hidden/Lux Water/UnderWaterPost"));
			if (this.cam == null)
			{
				this.cam = base.GetComponent<Camera>();
			}
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
			this.camTransform = this.cam.transform;
			if (this.FindSunOnEnable)
			{
				if (this.SunGoName != string.Empty)
				{
					this.Sun = GameObject.Find(this.SunGoName).transform;
				}
				else if (this.SunTagName != string.Empty)
				{
					this.Sun = GameObject.FindWithTag(this.SunTagName).transform;
				}
			}
			if (SystemInfo.usesReversedZBuffer)
			{
				this.Projection = -1f;
			}
			else
			{
				this.Projection = 1f;
			}
			this.UnderWaterMaskPID = Shader.PropertyToID("_UnderWaterMask");
			this.Lux_FrustumCornersWSPID = Shader.PropertyToID("_Lux_FrustumCornersWS");
			this.Lux_CameraWSPID = Shader.PropertyToID("_Lux_CameraWS");
			this.GerstnerEnabledPID = Shader.PropertyToID("_GerstnerEnabled");
			this.LuxWaterMask_GerstnerVertexIntensityPID = Shader.PropertyToID("_LuxWaterMask_GerstnerVertexIntensity");
			this.GerstnerVertexIntensityPID = Shader.PropertyToID("_GerstnerVertexIntensity");
			this.LuxWaterMask_GAmplitudePID = Shader.PropertyToID("_LuxWaterMask_GAmplitude");
			this.GAmplitudePID = Shader.PropertyToID("_GAmplitude");
			this.LuxWaterMask_GFinalFrequencyPID = Shader.PropertyToID("_LuxWaterMask_GFinalFrequency");
			this.GFinalFrequencyPID = Shader.PropertyToID("_GFinalFrequency");
			this.LuxWaterMask_GSteepnessPID = Shader.PropertyToID("_LuxWaterMask_GSteepness");
			this.GSteepnessPID = Shader.PropertyToID("_GSteepness");
			this.LuxWaterMask_GFinalSpeedPID = Shader.PropertyToID("_LuxWaterMask_GFinalSpeed");
			this.GFinalSpeedPID = Shader.PropertyToID("_GFinalSpeed");
			this.LuxWaterMask_GDirectionABPID = Shader.PropertyToID("_LuxWaterMask_GDirectionAB");
			this.GDirectionABPID = Shader.PropertyToID("_GDirectionAB");
			this.LuxWaterMask_GDirectionCDPID = Shader.PropertyToID("_LuxWaterMask_GDirectionCD");
			this.GDirectionCDPID = Shader.PropertyToID("_GDirectionCD");
			this.LuxWaterMask_GerstnerSecondaryWaves = Shader.PropertyToID("_LuxWaterMask_GerstnerSecondaryWaves");
			this.GerstnerSecondaryWaves = Shader.PropertyToID("_GerstnerSecondaryWaves");
			this.Lux_UnderWaterAmbientSkyLightPID = Shader.PropertyToID("_Lux_UnderWaterAmbientSkyLight");
			this.Lux_UnderWaterSunColorPID = Shader.PropertyToID("_Lux_UnderWaterSunColor");
			this.Lux_UnderWaterSunDirPID = Shader.PropertyToID("_Lux_UnderWaterSunDir");
			this.Lux_UnderWaterSunDirViewSpacePID = Shader.PropertyToID("_Lux_UnderWaterSunDirViewSpace");
			this.Lux_EdgeLengthPID = Shader.PropertyToID("_LuxWater_EdgeLength");
			this.Lux_MaxDirLightDepthPID = Shader.PropertyToID("_MaxDirLightDepth");
			this.Lux_MaxFogLightDepthPID = Shader.PropertyToID("_MaxFogLightDepth");
			this.Lux_CausticsEnabledPID = Shader.PropertyToID("_CausticsEnabled");
			this.Lux_CausticModePID = Shader.PropertyToID("_CausticMode");
			this.Lux_UnderWaterFogColorPID = Shader.PropertyToID("_Lux_UnderWaterFogColor");
			this.Lux_UnderWaterFogDensityPID = Shader.PropertyToID("_Lux_UnderWaterFogDensity");
			this.Lux_UnderWaterFogAbsorptionCancellationPID = Shader.PropertyToID("_Lux_UnderWaterFogAbsorptionCancellation");
			this.Lux_UnderWaterAbsorptionHeightPID = Shader.PropertyToID("_Lux_UnderWaterAbsorptionHeight");
			this.Lux_UnderWaterAbsorptionMaxHeightPID = Shader.PropertyToID("_Lux_UnderWaterAbsorptionMaxHeight");
			this.Lux_UnderWaterAbsorptionDepthPID = Shader.PropertyToID("_Lux_UnderWaterAbsorptionDepth");
			this.Lux_UnderWaterAbsorptionColorStrengthPID = Shader.PropertyToID("_Lux_UnderWaterAbsorptionColorStrength");
			this.Lux_UnderWaterAbsorptionStrengthPID = Shader.PropertyToID("_Lux_UnderWaterAbsorptionStrength");
			this.Lux_UnderWaterUnderwaterScatteringPowerPID = Shader.PropertyToID("_Lux_UnderWaterUnderwaterScatteringPower");
			this.Lux_UnderWaterUnderwaterScatteringIntensityPID = Shader.PropertyToID("_Lux_UnderWaterUnderwaterScatteringIntensity");
			this.Lux_UnderWaterUnderwaterScatteringColorPID = Shader.PropertyToID("_Lux_UnderWaterUnderwaterScatteringColor");
			this.Lux_UnderWaterUnderwaterScatteringOcclusionPID = Shader.PropertyToID("_Lux_UnderwaterScatteringOcclusion");
			this.Lux_UnderWaterCausticsPID = Shader.PropertyToID("_Lux_UnderWaterCaustics");
			this.Lux_UnderWaterDeferredFogParams = Shader.PropertyToID("_LuxUnderWaterDeferredFogParams");
			this.CausticTexPID = Shader.PropertyToID("_CausticTex");
			this.islinear = (QualitySettings.desiredColorSpace == ColorSpace.Linear);
			if (this.PrewarmedShaders != null && !this.PrewarmedShaders.isWarmedUp)
			{
				this.PrewarmedShaders.WarmUp();
			}
			if (this.Sun != null)
			{
				this.SunLight = this.Sun.GetComponent<Light>();
			}
			this.RegisteredWaterVolumesIDs.Capacity = this.ListCapacity;
			this.RegisteredWaterVolumes.Capacity = this.ListCapacity;
			this.WaterMeshes.Capacity = this.ListCapacity;
			this.WaterTransforms.Capacity = this.ListCapacity;
			this.WaterMaterials.Capacity = this.ListCapacity;
			this.WaterIsOnScreen.Capacity = this.ListCapacity;
			this.WaterUsesSlidingVolume.Capacity = this.ListCapacity;
			this.activeWaterVolumeCameras.Capacity = 2;
			this.SetDeepwaterLighting();
			this.SetDeferredFogParams();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00068A64 File Offset: 0x00066E64
		private void CleanUp()
		{
			LuxWater_UnderWaterRendering.instance = null;
			if (this.UnderWaterMask != null)
			{
				this.UnderWaterMask.Release();
				UnityEngine.Object.Destroy(this.UnderWaterMask);
			}
			if (this.mat)
			{
				UnityEngine.Object.Destroy(this.mat);
			}
			if (this.blurMaterial)
			{
				UnityEngine.Object.Destroy(this.blurMaterial);
			}
			if (this.blitMaterial)
			{
				UnityEngine.Object.Destroy(this.blitMaterial);
			}
			Shader.DisableKeyword("LUXWATER_DEEPWATERLIGHTING");
			Shader.DisableKeyword("LUXWATER_DEFERREDFOG");
			this.RegisteredWaterVolumesIDs.Clear();
			this.RegisteredWaterVolumes.Clear();
			this.WaterMeshes.Clear();
			this.WaterTransforms.Clear();
			this.WaterMaterials.Clear();
			this.WaterIsOnScreen.Clear();
			this.WaterUsesSlidingVolume.Clear();
			this.activeWaterVolumeCameras.Clear();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00068B5B File Offset: 0x00066F5B
		private void OnDisable()
		{
			this.CleanUp();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00068B63 File Offset: 0x00066F63
		private void OnDestroy()
		{
			this.CleanUp();
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00068B6B File Offset: 0x00066F6B
		private void OnValidate()
		{
			this.SetDeepwaterLighting();
			this.SetDeferredFogParams();
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00068B7C File Offset: 0x00066F7C
		public void SetDeferredFogParams()
		{
			if (this.EnableAdvancedDeferredFog)
			{
				Shader.EnableKeyword("LUXWATER_DEFERREDFOG");
				Vector4 value = new Vector4((float)((!this.DoUnderWaterRendering) ? 0 : 1), this.FogDepthShift, this.FogEdgeBlending, 0f);
				Shader.SetGlobalVector(this.Lux_UnderWaterDeferredFogParams, value);
			}
			else
			{
				Shader.DisableKeyword("LUXWATER_DEFERREDFOG");
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00068BE4 File Offset: 0x00066FE4
		public void SetDeepwaterLighting()
		{
			if (this.EnableDeepwaterLighting)
			{
				Shader.EnableKeyword("LUXWATER_DEEPWATERLIGHTING");
				if (this.activeWaterVolume != -1)
				{
					Shader.SetGlobalFloat("_Lux_UnderWaterWaterSurfacePos", this.WaterSurfacePos);
				}
				else
				{
					Shader.SetGlobalFloat("_Lux_UnderWaterWaterSurfacePos", this.DefaultWaterSurfacePosition);
				}
				Shader.SetGlobalFloat("_Lux_UnderWaterDirLightingDepth", this.DirectionalLightingFadeRange);
				Shader.SetGlobalFloat("_Lux_UnderWaterFogLightingDepth", this.FogLightingFadeRange);
			}
			else
			{
				Shader.DisableKeyword("LUXWATER_DEEPWATERLIGHTING");
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00068C68 File Offset: 0x00067068
		public void RegisterWaterVolume(LuxWater_WaterVolume item, int ID, bool visible, bool SlidingVolume)
		{
			this.RegisteredWaterVolumesIDs.Add(ID);
			this.RegisteredWaterVolumes.Add(item);
			this.WaterMeshes.Add(item.WaterVolumeMesh);
			this.WaterMaterials.Add(item.transform.GetComponent<Renderer>().sharedMaterial);
			this.WaterTransforms.Add(item.transform);
			this.WaterIsOnScreen.Add(visible);
			this.WaterUsesSlidingVolume.Add(SlidingVolume);
			int num = this.WaterMaterials.Count - 1;
			Shader.SetGlobalTexture(this.Lux_UnderWaterCausticsPID, this.WaterMaterials[num].GetTexture(this.CausticTexPID));
			this.SetGerstnerWaves(num);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00068D1C File Offset: 0x0006711C
		public void DeRegisterWaterVolume(LuxWater_WaterVolume item, int ID)
		{
			int num = this.RegisteredWaterVolumesIDs.IndexOf(ID);
			if (this.activeWaterVolume == num)
			{
				this.activeWaterVolume = -1;
			}
			if (num == -1)
			{
				return;
			}
			this.RegisteredWaterVolumesIDs.RemoveAt(num);
			this.RegisteredWaterVolumes.RemoveAt(num);
			this.WaterMeshes.RemoveAt(num);
			this.WaterMaterials.RemoveAt(num);
			this.WaterTransforms.RemoveAt(num);
			this.WaterIsOnScreen.RemoveAt(num);
			this.WaterUsesSlidingVolume.RemoveAt(num);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00068DA8 File Offset: 0x000671A8
		public void SetWaterVisible(int ID)
		{
			int num = this.RegisteredWaterVolumesIDs.IndexOf(ID);
			if (num >= 0 && num < this.WaterIsOnScreen.Count)
			{
				this.WaterIsOnScreen[num] = true;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00068DE8 File Offset: 0x000671E8
		public void SetWaterInvisible(int ID)
		{
			int num = this.RegisteredWaterVolumesIDs.IndexOf(ID);
			if (num >= 0 && num < this.WaterIsOnScreen.Count)
			{
				this.WaterIsOnScreen[num] = false;
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00068E28 File Offset: 0x00067228
		public void EnteredWaterVolume(LuxWater_WaterVolume item, int ID, Camera triggerCam, float GridSize)
		{
			this.DoUnderWaterRendering = true;
			int num = this.RegisteredWaterVolumesIDs.IndexOf(ID);
			if (num != this.activeWaterVolume)
			{
				this.activeWaterVolume = num;
				this.activeGridSize = GridSize;
				this.WaterSurfacePos = this.WaterTransforms[this.activeWaterVolume].position.y;
				for (int i = 0; i < this.m_aboveWatersurface.Count; i++)
				{
					this.m_aboveWatersurface[i].renderQueue = 2997;
				}
				for (int j = 0; j < this.m_belowWatersurface.Count; j++)
				{
					this.m_belowWatersurface[j].renderQueue = 3001;
				}
			}
			if (!this.activeWaterVolumeCameras.Contains(triggerCam))
			{
				this.activeWaterVolumeCameras.Add(triggerCam);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00068F0C File Offset: 0x0006730C
		public void LeftWaterVolume(LuxWater_WaterVolume item, int ID, Camera triggerCam)
		{
			this.DoUnderWaterRendering = false;
			int num = this.RegisteredWaterVolumesIDs.IndexOf(ID);
			if (this.activeWaterVolume == num)
			{
				this.activeWaterVolume = -1;
				for (int i = 0; i < this.m_aboveWatersurface.Count; i++)
				{
					this.m_aboveWatersurface[i].renderQueue = 3000;
				}
				for (int j = 0; j < this.m_belowWatersurface.Count; j++)
				{
					this.m_belowWatersurface[j].renderQueue = 2997;
				}
			}
			if (this.activeWaterVolumeCameras.Contains(triggerCam))
			{
				this.activeWaterVolumeCameras.Remove(triggerCam);
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00068FC2 File Offset: 0x000673C2
		private void OnPreCull()
		{
			this.SetDeferredFogParams();
			this.RenderWaterMask(this.cam, false);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00068FD7 File Offset: 0x000673D7
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			this.RenderUnderWater(src, dest, this.cam, false);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00068FE8 File Offset: 0x000673E8
		public void SetGerstnerWaves(int index)
		{
			if (this.WaterMaterials[index].GetFloat(this.GerstnerEnabledPID) == 1f)
			{
				this.mat.EnableKeyword("GERSTNERENABLED");
				this.mat.SetVector(this.LuxWaterMask_GerstnerVertexIntensityPID, this.WaterMaterials[index].GetVector(this.GerstnerVertexIntensityPID));
				this.mat.SetVector(this.LuxWaterMask_GAmplitudePID, this.WaterMaterials[index].GetVector(this.GAmplitudePID));
				this.mat.SetVector(this.LuxWaterMask_GFinalFrequencyPID, this.WaterMaterials[index].GetVector(this.GFinalFrequencyPID));
				this.mat.SetVector(this.LuxWaterMask_GSteepnessPID, this.WaterMaterials[index].GetVector(this.GSteepnessPID));
				this.mat.SetVector(this.LuxWaterMask_GFinalSpeedPID, this.WaterMaterials[index].GetVector(this.GFinalSpeedPID));
				this.mat.SetVector(this.LuxWaterMask_GDirectionABPID, this.WaterMaterials[index].GetVector(this.GDirectionABPID));
				this.mat.SetVector(this.LuxWaterMask_GDirectionCDPID, this.WaterMaterials[index].GetVector(this.GDirectionCDPID));
				this.mat.SetVector(this.LuxWaterMask_GerstnerSecondaryWaves, this.WaterMaterials[index].GetVector(this.GerstnerSecondaryWaves));
			}
			else
			{
				this.mat.DisableKeyword("GERSTNERENABLED");
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0006917C File Offset: 0x0006757C
		public void RenderWaterMask(Camera currentCamera, bool SecondaryCameraRendering)
		{
			Shader.SetGlobalFloat("_Lux_Time", Time.timeSinceLevelLoad);
			currentCamera.depthTextureMode |= DepthTextureMode.Depth;
			this.camTransform = currentCamera.transform;
			if (!this.UnderWaterMask)
			{
				this.UnderWaterMask = new RenderTexture(currentCamera.pixelWidth, currentCamera.pixelHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			}
			else if (this.UnderWaterMask.width != currentCamera.pixelWidth && !SecondaryCameraRendering)
			{
				this.UnderWaterMask = new RenderTexture(currentCamera.pixelWidth, currentCamera.pixelHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			}
			Shader.SetGlobalTexture(this.UnderWaterMaskPID, this.UnderWaterMask);
			Graphics.SetRenderTarget(this.UnderWaterMask);
			currentCamera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), currentCamera.farClipPlane, currentCamera.stereoActiveEye, this.frustumCorners);
			Vector3 v = this.camTransform.TransformVector(this.frustumCorners[0]);
			Vector3 v2 = this.camTransform.TransformVector(this.frustumCorners[1]);
			Vector3 v3 = this.camTransform.TransformVector(this.frustumCorners[2]);
			Vector3 v4 = this.camTransform.TransformVector(this.frustumCorners[3]);
			this.frustumCornersArray.SetRow(0, v);
			this.frustumCornersArray.SetRow(1, v4);
			this.frustumCornersArray.SetRow(2, v2);
			this.frustumCornersArray.SetRow(3, v3);
			Shader.SetGlobalMatrix(this.Lux_FrustumCornersWSPID, this.frustumCornersArray);
			Shader.SetGlobalVector(this.Lux_CameraWSPID, this.camTransform.position);
			this.ambientProbe = RenderSettings.ambientProbe;
			this.ambientProbe.Evaluate(this.directions, this.AmbientLightingSamples);
			if (this.islinear)
			{
				Shader.SetGlobalColor(this.Lux_UnderWaterAmbientSkyLightPID, (this.AmbientLightingSamples[0] * RenderSettings.ambientIntensity).linear);
			}
			else
			{
				Shader.SetGlobalColor(this.Lux_UnderWaterAmbientSkyLightPID, this.AmbientLightingSamples[0] * RenderSettings.ambientIntensity);
			}
			if (this.activeWaterVolumeCameras.Contains(currentCamera) || !this.EnableAdvancedDeferredFog)
			{
			}
			if (this.activeWaterVolume > -1)
			{
				Shader.EnableKeyword("LUXWATERENABLED");
				if (!this.EnableDeepwaterLighting)
				{
					Shader.SetGlobalFloat("_Lux_UnderWaterDirLightingDepth", this.WaterMaterials[this.activeWaterVolume].GetFloat(this.Lux_MaxDirLightDepthPID));
					Shader.SetGlobalFloat("_Lux_UnderWaterFogLightingDepth", this.WaterMaterials[this.activeWaterVolume].GetFloat(this.Lux_MaxFogLightDepthPID));
				}
				Shader.SetGlobalFloat("_Lux_UnderWaterWaterSurfacePos", this.WaterSurfacePos);
			}
			else
			{
				Shader.DisableKeyword("LUXWATERENABLED");
			}
			GL.PushMatrix();
			GL.Clear(true, true, Color.black, 1f);
			this.camProj = currentCamera.projectionMatrix;
			GL.LoadProjectionMatrix(this.camProj);
			Shader.SetGlobalVector("_WorldSpaceCameraPos", this.camTransform.position);
			Shader.SetGlobalVector("_ProjectionParams", new Vector4(this.Projection, currentCamera.nearClipPlane, currentCamera.farClipPlane, 1f / currentCamera.farClipPlane));
			Shader.SetGlobalVector("_ScreenParams", new Vector4((float)currentCamera.pixelWidth, (float)currentCamera.pixelHeight, 1f + 1f / (float)currentCamera.pixelWidth, 1f + 1f / (float)currentCamera.pixelHeight));
			for (int i = 0; i < this.RegisteredWaterVolumes.Count; i++)
			{
				if (this.WaterIsOnScreen[i] || i == this.activeWaterVolume)
				{
					if (this.EnableAdvancedDeferredFog || i == this.activeWaterVolume)
					{
						this.WatervolumeMatrix = this.WaterTransforms[i].localToWorldMatrix;
						if (this.WaterUsesSlidingVolume[i])
						{
							Vector3 position = this.camTransform.position;
							Vector4 column = this.WatervolumeMatrix.GetColumn(3);
							Vector3 lossyScale = this.WaterTransforms[i].lossyScale;
							Vector2 vector = new Vector2(this.activeGridSize * lossyScale.x, this.activeGridSize * lossyScale.z);
							float num = (float)Math.Round((double)(position.x / vector.x));
							float num2 = vector.x * num;
							num = (float)Math.Round((double)(position.z / vector.y));
							float num3 = vector.y * num;
							column.x = num2 + column.x % vector.x;
							column.z = num3 + column.z % vector.y;
							this.WatervolumeMatrix.SetColumn(3, column);
						}
						Material material = this.WaterMaterials[i];
						if (material.GetFloat(this.GerstnerEnabledPID) == 1f)
						{
							this.mat.EnableKeyword("GERSTNERENABLED");
							this.mat.SetVector(this.LuxWaterMask_GerstnerVertexIntensityPID, material.GetVector(this.GerstnerVertexIntensityPID));
							this.mat.SetVector(this.LuxWaterMask_GAmplitudePID, material.GetVector(this.GAmplitudePID));
							this.mat.SetVector(this.LuxWaterMask_GFinalFrequencyPID, material.GetVector(this.GFinalFrequencyPID));
							this.mat.SetVector(this.LuxWaterMask_GSteepnessPID, material.GetVector(this.GSteepnessPID));
							this.mat.SetVector(this.LuxWaterMask_GFinalSpeedPID, material.GetVector(this.GFinalSpeedPID));
							this.mat.SetVector(this.LuxWaterMask_GDirectionABPID, material.GetVector(this.GDirectionABPID));
							this.mat.SetVector(this.LuxWaterMask_GDirectionCDPID, material.GetVector(this.GDirectionCDPID));
							this.mat.SetVector(this.LuxWaterMask_GerstnerSecondaryWaves, material.GetVector(this.GerstnerSecondaryWaves));
						}
						else
						{
							this.mat.DisableKeyword("GERSTNERENABLED");
						}
						bool flag = material.HasProperty(this.Lux_EdgeLengthPID) && SystemInfo.graphicsShaderLevel >= 46;
						if (flag)
						{
							this.mat.SetFloat(this.Lux_EdgeLengthPID, material.GetFloat(this.Lux_EdgeLengthPID));
						}
						if (i == this.activeWaterVolume && this.activeWaterVolumeCameras.Contains(currentCamera))
						{
							if (this.WaterUsesSlidingVolume[i] && flag)
							{
								this.mat.SetPass(5);
							}
							else
							{
								this.mat.SetPass(0);
							}
							Graphics.DrawMeshNow(this.WaterMeshes[i], this.WatervolumeMatrix, 0);
						}
						if ((i == this.activeWaterVolume && this.activeWaterVolumeCameras.Contains(currentCamera)) || this.EnableAdvancedDeferredFog)
						{
							if (flag)
							{
								if (i == this.activeWaterVolume)
								{
									this.mat.SetPass(3);
								}
								else
								{
									this.mat.SetPass(4);
								}
							}
							else if (i == this.activeWaterVolume)
							{
								this.mat.SetPass(1);
							}
							else
							{
								this.mat.SetPass(2);
							}
							Graphics.DrawMeshNow(this.WaterMeshes[i], this.WatervolumeMatrix, 1);
						}
					}
				}
			}
			GL.PopMatrix();
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00069938 File Offset: 0x00067D38
		public void RenderUnderWater(RenderTexture src, RenderTexture dest, Camera currentCamera, bool SecondaryCameraRendering)
		{
			if (this.activeWaterVolumeCameras.Contains(currentCamera))
			{
				if (this.DoUnderWaterRendering && this.activeWaterVolume > -1)
				{
					if (!this.UnderwaterIsSetUp)
					{
						if (this.Sun)
						{
							Vector3 vector = -this.Sun.forward;
							Color a = this.SunLight.color * this.SunLight.intensity;
							if (this.islinear)
							{
								a = a.linear;
							}
							Shader.SetGlobalColor(this.Lux_UnderWaterSunColorPID, a * Mathf.Clamp01(Vector3.Dot(vector, Vector3.up)));
							Shader.SetGlobalVector(this.Lux_UnderWaterSunDirPID, -vector);
							Shader.SetGlobalVector(this.Lux_UnderWaterSunDirViewSpacePID, currentCamera.WorldToViewportPoint(currentCamera.transform.position - vector * 1000f));
						}
						if (this.WaterMaterials[this.activeWaterVolume].GetFloat(this.Lux_CausticsEnabledPID) == 1f)
						{
							this.blitMaterial.EnableKeyword("GEOM_TYPE_FROND");
							if (this.WaterMaterials[this.activeWaterVolume].GetFloat(this.Lux_CausticModePID) == 1f)
							{
								this.blitMaterial.EnableKeyword("GEOM_TYPE_LEAF");
							}
							else
							{
								this.blitMaterial.DisableKeyword("GEOM_TYPE_LEAF");
							}
						}
						else
						{
							this.blitMaterial.DisableKeyword("GEOM_TYPE_FROND");
						}
						if (this.islinear)
						{
							Shader.SetGlobalColor(this.Lux_UnderWaterFogColorPID, this.WaterMaterials[this.activeWaterVolume].GetColor("_Color").linear);
						}
						else
						{
							Shader.SetGlobalColor(this.Lux_UnderWaterFogColorPID, this.WaterMaterials[this.activeWaterVolume].GetColor("_Color"));
						}
						Shader.SetGlobalFloat(this.Lux_UnderWaterFogDensityPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_Density"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterFogAbsorptionCancellationPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_FogAbsorptionCancellation"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterAbsorptionHeightPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_AbsorptionHeight"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterAbsorptionMaxHeightPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_AbsorptionMaxHeight"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterAbsorptionDepthPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_AbsorptionDepth"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterAbsorptionColorStrengthPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_AbsorptionColorStrength"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterAbsorptionStrengthPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_AbsorptionStrength"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterUnderwaterScatteringPowerPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_ScatteringPower"));
						Shader.SetGlobalFloat(this.Lux_UnderWaterUnderwaterScatteringIntensityPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_UnderwaterScatteringIntensity"));
						if (this.islinear)
						{
							Shader.SetGlobalColor(this.Lux_UnderWaterUnderwaterScatteringColorPID, this.WaterMaterials[this.activeWaterVolume].GetColor("_TranslucencyColor").linear);
						}
						else
						{
							Shader.SetGlobalColor(this.Lux_UnderWaterUnderwaterScatteringColorPID, this.WaterMaterials[this.activeWaterVolume].GetColor("_TranslucencyColor"));
						}
						Shader.SetGlobalFloat(this.Lux_UnderWaterUnderwaterScatteringOcclusionPID, this.WaterMaterials[this.activeWaterVolume].GetFloat("_ScatterOcclusion"));
						Shader.SetGlobalTexture(this.Lux_UnderWaterCausticsPID, this.WaterMaterials[this.activeWaterVolume].GetTexture(this.CausticTexPID));
						Shader.SetGlobalFloat("_Lux_UnderWaterCausticsTiling", this.WaterMaterials[this.activeWaterVolume].GetFloat("_CausticsTiling"));
						Shader.SetGlobalFloat("_Lux_UnderWaterCausticsScale", this.WaterMaterials[this.activeWaterVolume].GetFloat("_CausticsScale"));
						Shader.SetGlobalFloat("_Lux_UnderWaterCausticsSpeed", this.WaterMaterials[this.activeWaterVolume].GetFloat("_CausticsSpeed"));
						Shader.SetGlobalFloat("_Lux_UnderWaterCausticsTiling", this.WaterMaterials[this.activeWaterVolume].GetFloat("_CausticsTiling"));
						Shader.SetGlobalFloat("_Lux_UnderWaterCausticsSelfDistortion", this.WaterMaterials[this.activeWaterVolume].GetFloat("_CausticsSelfDistortion"));
						Shader.SetGlobalVector("_Lux_UnderWaterFinalBumpSpeed01", this.WaterMaterials[this.activeWaterVolume].GetVector("_FinalBumpSpeed01"));
						Shader.SetGlobalVector("_Lux_UnderWaterFogDepthAtten", this.WaterMaterials[this.activeWaterVolume].GetVector("_DepthAtten"));
					}
					Graphics.Blit(src, dest, this.blitMaterial, 0);
				}
				else
				{
					Graphics.Blit(src, dest);
				}
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}

		// Token: 0x0400139A RID: 5018
		public static LuxWater_UnderWaterRendering instance;

		// Token: 0x0400139B RID: 5019
		[Space(6f)]
		[LuxWater_HelpBtn("h.d0q6uguuxpy")]
		public Transform Sun;

		// Token: 0x0400139C RID: 5020
		[Space(4f)]
		public bool FindSunOnEnable;

		// Token: 0x0400139D RID: 5021
		public string SunGoName = string.Empty;

		// Token: 0x0400139E RID: 5022
		public string SunTagName = string.Empty;

		// Token: 0x0400139F RID: 5023
		private Light SunLight;

		// Token: 0x040013A0 RID: 5024
		[Space(2f)]
		[Header("Deep Water Lighting")]
		[Space(4f)]
		public bool EnableDeepwaterLighting;

		// Token: 0x040013A1 RID: 5025
		public float DefaultWaterSurfacePosition;

		// Token: 0x040013A2 RID: 5026
		public float DirectionalLightingFadeRange = 64f;

		// Token: 0x040013A3 RID: 5027
		public float FogLightingFadeRange = 64f;

		// Token: 0x040013A4 RID: 5028
		[Space(2f)]
		[Header("Advanced Deferred Fog")]
		[Space(4f)]
		public bool EnableAdvancedDeferredFog;

		// Token: 0x040013A5 RID: 5029
		public float FogDepthShift = 1f;

		// Token: 0x040013A6 RID: 5030
		public float FogEdgeBlending = 0.125f;

		// Token: 0x040013A7 RID: 5031
		[Space(8f)]
		[NonSerialized]
		public int activeWaterVolume = -1;

		// Token: 0x040013A8 RID: 5032
		[NonSerialized]
		public List<Camera> activeWaterVolumeCameras = new List<Camera>();

		// Token: 0x040013A9 RID: 5033
		[NonSerialized]
		public float activeGridSize;

		// Token: 0x040013AA RID: 5034
		[NonSerialized]
		public float WaterSurfacePos;

		// Token: 0x040013AB RID: 5035
		[Space(8f)]
		[NonSerialized]
		public List<int> RegisteredWaterVolumesIDs = new List<int>();

		// Token: 0x040013AC RID: 5036
		[NonSerialized]
		public List<LuxWater_WaterVolume> RegisteredWaterVolumes = new List<LuxWater_WaterVolume>();

		// Token: 0x040013AD RID: 5037
		private List<Mesh> WaterMeshes = new List<Mesh>();

		// Token: 0x040013AE RID: 5038
		private List<Transform> WaterTransforms = new List<Transform>();

		// Token: 0x040013AF RID: 5039
		private List<Material> WaterMaterials = new List<Material>();

		// Token: 0x040013B0 RID: 5040
		private List<bool> WaterIsOnScreen = new List<bool>();

		// Token: 0x040013B1 RID: 5041
		private List<bool> WaterUsesSlidingVolume = new List<bool>();

		// Token: 0x040013B2 RID: 5042
		private RenderTexture UnderWaterMask;

		// Token: 0x040013B3 RID: 5043
		[Space(2f)]
		[Header("Managed transparent Materials")]
		[Space(4f)]
		public List<Material> m_aboveWatersurface = new List<Material>();

		// Token: 0x040013B4 RID: 5044
		public List<Material> m_belowWatersurface = new List<Material>();

		// Token: 0x040013B5 RID: 5045
		[Space(2f)]
		[Header("Optimize")]
		[Space(4f)]
		public ShaderVariantCollection PrewarmedShaders;

		// Token: 0x040013B6 RID: 5046
		public int ListCapacity = 10;

		// Token: 0x040013B7 RID: 5047
		[Space(2f)]
		[Header("Debug")]
		[Space(4f)]
		public bool enableDebug;

		// Token: 0x040013B8 RID: 5048
		[Space(8f)]
		private Material mat;

		// Token: 0x040013B9 RID: 5049
		private Material blurMaterial;

		// Token: 0x040013BA RID: 5050
		private Material blitMaterial;

		// Token: 0x040013BB RID: 5051
		private Camera cam;

		// Token: 0x040013BC RID: 5052
		private bool UnderwaterIsSetUp;

		// Token: 0x040013BD RID: 5053
		private Transform camTransform;

		// Token: 0x040013BE RID: 5054
		private Matrix4x4 frustumCornersArray = Matrix4x4.identity;

		// Token: 0x040013BF RID: 5055
		private SphericalHarmonicsL2 ambientProbe;

		// Token: 0x040013C0 RID: 5056
		private Vector3[] directions = new Vector3[]
		{
			new Vector3(0f, 1f, 0f)
		};

		// Token: 0x040013C1 RID: 5057
		private Color[] AmbientLightingSamples = new Color[1];

		// Token: 0x040013C2 RID: 5058
		private bool DoUnderWaterRendering;

		// Token: 0x040013C3 RID: 5059
		private Matrix4x4 camProj;

		// Token: 0x040013C4 RID: 5060
		private Vector3[] frustumCorners = new Vector3[4];

		// Token: 0x040013C5 RID: 5061
		private float Projection;

		// Token: 0x040013C6 RID: 5062
		private bool islinear;

		// Token: 0x040013C7 RID: 5063
		private Matrix4x4 WatervolumeMatrix;

		// Token: 0x040013C8 RID: 5064
		private int UnderWaterMaskPID;

		// Token: 0x040013C9 RID: 5065
		private int Lux_FrustumCornersWSPID;

		// Token: 0x040013CA RID: 5066
		private int Lux_CameraWSPID;

		// Token: 0x040013CB RID: 5067
		private int GerstnerEnabledPID;

		// Token: 0x040013CC RID: 5068
		private int LuxWaterMask_GerstnerVertexIntensityPID;

		// Token: 0x040013CD RID: 5069
		private int GerstnerVertexIntensityPID;

		// Token: 0x040013CE RID: 5070
		private int LuxWaterMask_GAmplitudePID;

		// Token: 0x040013CF RID: 5071
		private int GAmplitudePID;

		// Token: 0x040013D0 RID: 5072
		private int LuxWaterMask_GFinalFrequencyPID;

		// Token: 0x040013D1 RID: 5073
		private int GFinalFrequencyPID;

		// Token: 0x040013D2 RID: 5074
		private int LuxWaterMask_GSteepnessPID;

		// Token: 0x040013D3 RID: 5075
		private int GSteepnessPID;

		// Token: 0x040013D4 RID: 5076
		private int LuxWaterMask_GFinalSpeedPID;

		// Token: 0x040013D5 RID: 5077
		private int GFinalSpeedPID;

		// Token: 0x040013D6 RID: 5078
		private int LuxWaterMask_GDirectionABPID;

		// Token: 0x040013D7 RID: 5079
		private int GDirectionABPID;

		// Token: 0x040013D8 RID: 5080
		private int LuxWaterMask_GDirectionCDPID;

		// Token: 0x040013D9 RID: 5081
		private int GDirectionCDPID;

		// Token: 0x040013DA RID: 5082
		private int LuxWaterMask_GerstnerSecondaryWaves;

		// Token: 0x040013DB RID: 5083
		private int GerstnerSecondaryWaves;

		// Token: 0x040013DC RID: 5084
		private int Lux_UnderWaterAmbientSkyLightPID;

		// Token: 0x040013DD RID: 5085
		private int Lux_UnderWaterSunColorPID;

		// Token: 0x040013DE RID: 5086
		private int Lux_UnderWaterSunDirPID;

		// Token: 0x040013DF RID: 5087
		private int Lux_UnderWaterSunDirViewSpacePID;

		// Token: 0x040013E0 RID: 5088
		private int Lux_EdgeLengthPID;

		// Token: 0x040013E1 RID: 5089
		private int Lux_CausticsEnabledPID;

		// Token: 0x040013E2 RID: 5090
		private int Lux_CausticModePID;

		// Token: 0x040013E3 RID: 5091
		private int Lux_UnderWaterFogColorPID;

		// Token: 0x040013E4 RID: 5092
		private int Lux_UnderWaterFogDensityPID;

		// Token: 0x040013E5 RID: 5093
		private int Lux_UnderWaterFogAbsorptionCancellationPID;

		// Token: 0x040013E6 RID: 5094
		private int Lux_UnderWaterAbsorptionHeightPID;

		// Token: 0x040013E7 RID: 5095
		private int Lux_UnderWaterAbsorptionMaxHeightPID;

		// Token: 0x040013E8 RID: 5096
		private int Lux_MaxDirLightDepthPID;

		// Token: 0x040013E9 RID: 5097
		private int Lux_MaxFogLightDepthPID;

		// Token: 0x040013EA RID: 5098
		private int Lux_UnderWaterAbsorptionDepthPID;

		// Token: 0x040013EB RID: 5099
		private int Lux_UnderWaterAbsorptionColorStrengthPID;

		// Token: 0x040013EC RID: 5100
		private int Lux_UnderWaterAbsorptionStrengthPID;

		// Token: 0x040013ED RID: 5101
		private int Lux_UnderWaterUnderwaterScatteringPowerPID;

		// Token: 0x040013EE RID: 5102
		private int Lux_UnderWaterUnderwaterScatteringIntensityPID;

		// Token: 0x040013EF RID: 5103
		private int Lux_UnderWaterUnderwaterScatteringColorPID;

		// Token: 0x040013F0 RID: 5104
		private int Lux_UnderWaterUnderwaterScatteringOcclusionPID;

		// Token: 0x040013F1 RID: 5105
		private int Lux_UnderWaterCausticsPID;

		// Token: 0x040013F2 RID: 5106
		private int Lux_UnderWaterDeferredFogParams;

		// Token: 0x040013F3 RID: 5107
		private int CausticTexPID;
	}
}

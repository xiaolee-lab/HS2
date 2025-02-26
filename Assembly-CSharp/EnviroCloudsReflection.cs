using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class EnviroCloudsReflection : MonoBehaviour
{
	// Token: 0x06000F7D RID: 3965 RVA: 0x00055CF8 File Offset: 0x000540F8
	private void Start()
	{
		this.myCam = base.GetComponent<Camera>();
		this.CreateMaterialsAndTextures();
		if (EnviroSky.instance != null)
		{
			this.SetReprojectionPixelSize(EnviroSky.instance.cloudsSettings.reprojectionPixelSize);
		}
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00055D34 File Offset: 0x00054134
	private void CreateMaterialsAndTextures()
	{
		if (this.mat == null)
		{
			this.mat = new Material(Shader.Find("Enviro/RaymarchClouds"));
		}
		if (this.blitMat == null)
		{
			this.blitMat = new Material(Shader.Find("Enviro/Blit"));
		}
		if (this.curlMap == null)
		{
			this.curlMap = (Resources.Load("tex_enviro_curl") as Texture2D);
		}
		if (this.noiseTextureHigh == null)
		{
			this.noiseTextureHigh = (Resources.Load("enviro_clouds_base") as Texture3D);
		}
		if (this.noiseTexture == null)
		{
			this.noiseTexture = (Resources.Load("enviro_clouds_base_low") as Texture3D);
		}
		if (this.detailNoiseTexture == null)
		{
			this.detailNoiseTexture = (Resources.Load("enviro_clouds_detail_low") as Texture3D);
		}
		if (this.detailNoiseTextureHigh == null)
		{
			this.detailNoiseTextureHigh = (Resources.Load("enviro_clouds_detail_high") as Texture3D);
		}
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00055E4C File Offset: 0x0005424C
	private void SetCloudProperties()
	{
		this.mat.SetTexture("_Noise", this.noiseTextureHigh);
		this.mat.SetTexture("_NoiseLow", this.noiseTexture);
		if (EnviroSky.instance.cloudsSettings.detailQuality == EnviroCloudSettings.CloudDetailQuality.Low)
		{
			this.mat.SetTexture("_DetailNoise", this.detailNoiseTexture);
		}
		else
		{
			this.mat.SetTexture("_DetailNoise", this.detailNoiseTextureHigh);
		}
		Camera.MonoOrStereoscopicEye stereoActiveEye = this.myCam.stereoActiveEye;
		if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Mono)
		{
			if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Left)
			{
				if (stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
				{
					this.projection = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
					Matrix4x4 inverse = this.projection.inverse;
					this.mat.SetMatrix("_InverseProjection", inverse);
					this.inverseRotation = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right).inverse;
					this.mat.SetMatrix("_InverseRotation", this.inverseRotation);
				}
			}
			else
			{
				this.projection = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
				Matrix4x4 inverse2 = this.projection.inverse;
				this.mat.SetMatrix("_InverseProjection", inverse2);
				this.inverseRotation = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Left).inverse;
				this.mat.SetMatrix("_InverseRotation", this.inverseRotation);
				if (EnviroSky.instance.singlePassVR)
				{
					Matrix4x4 inverse3 = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right).inverse;
					this.mat.SetMatrix("_InverseProjection_SP", inverse3);
					this.inverseRotationSPVR = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right).inverse;
					this.mat.SetMatrix("_InverseRotation_SP", this.inverseRotationSPVR);
				}
			}
		}
		else
		{
			this.projection = this.myCam.projectionMatrix;
			Matrix4x4 inverse4 = this.projection.inverse;
			this.mat.SetMatrix("_InverseProjection", inverse4);
			this.inverseRotation = this.myCam.cameraToWorldMatrix;
			this.mat.SetMatrix("_InverseRotation", this.inverseRotation);
		}
		this.mat.SetTexture("_CurlNoise", this.curlMap);
		this.mat.SetVector("_Steps", new Vector4((float)EnviroSky.instance.cloudsSettings.raymarchSteps * EnviroSky.instance.cloudsConfig.raymarchingScale, (float)EnviroSky.instance.cloudsSettings.raymarchSteps * EnviroSky.instance.cloudsConfig.raymarchingScale, 0f, 0f));
		this.mat.SetFloat("_BaseNoiseUV", EnviroSky.instance.cloudsSettings.baseNoiseUV);
		this.mat.SetFloat("_DetailNoiseUV", EnviroSky.instance.cloudsSettings.detailNoiseUV);
		this.mat.SetFloat("_PrimAtt", EnviroSky.instance.cloudsSettings.primaryAttenuation);
		this.mat.SetFloat("_SecAtt", EnviroSky.instance.cloudsSettings.secondaryAttenuation);
		this.mat.SetFloat("_SkyBlending", EnviroSky.instance.cloudsConfig.skyBlending);
		this.mat.SetFloat("_HgPhaseFactor", EnviroSky.instance.cloudsSettings.hgPhase);
		this.mat.SetVector("_CloudsParameter", new Vector4(EnviroSky.instance.cloudsSettings.bottomCloudHeight, EnviroSky.instance.cloudsSettings.topCloudHeight, EnviroSky.instance.cloudsSettings.topCloudHeight - EnviroSky.instance.cloudsSettings.bottomCloudHeight, EnviroSky.instance.cloudsSettings.cloudsWorldScale * 10f));
		this.mat.SetFloat("_AmbientLightIntensity", EnviroSky.instance.cloudsSettings.ambientLightIntensity.Evaluate(EnviroSky.instance.GameTime.solarTime));
		this.mat.SetFloat("_SunLightIntensity", EnviroSky.instance.cloudsSettings.directLightIntensity.Evaluate(EnviroSky.instance.GameTime.solarTime));
		this.mat.SetFloat("_AlphaCoef", EnviroSky.instance.cloudsConfig.alphaCoef);
		this.mat.SetFloat("_ExtinctionCoef", EnviroSky.instance.cloudsConfig.scatteringCoef);
		this.mat.SetFloat("_CloudDensityScale", EnviroSky.instance.cloudsConfig.density);
		this.mat.SetColor("_CloudBaseColor", EnviroSky.instance.cloudsConfig.bottomColor);
		this.mat.SetColor("_CloudTopColor", EnviroSky.instance.cloudsConfig.topColor);
		this.mat.SetFloat("_CloudsType", EnviroSky.instance.cloudsConfig.cloudType);
		this.mat.SetFloat("_CloudsCoverage", EnviroSky.instance.cloudsConfig.coverageHeight);
		this.mat.SetVector("_CloudsAnimation", new Vector4(EnviroSky.instance.cloudAnim.x, EnviroSky.instance.cloudAnim.y, 0f, 0f));
		this.mat.SetFloat("_CloudsExposure", EnviroSky.instance.cloudsSettings.cloudsExposure);
		this.mat.SetFloat("_GlobalCoverage", EnviroSky.instance.cloudsConfig.coverage * EnviroSky.instance.cloudsSettings.globalCloudCoverage);
		this.mat.SetColor("_LightColor", EnviroSky.instance.cloudsSettings.volumeCloudsColor.Evaluate(EnviroSky.instance.GameTime.solarTime));
		this.mat.SetColor("_MoonLightColor", EnviroSky.instance.cloudsSettings.volumeCloudsMoonColor.Evaluate(EnviroSky.instance.GameTime.lunarTime));
		this.mat.SetFloat("_Tonemapping", (!this.tonemapping) ? 1f : 0f);
		this.mat.SetFloat("_stepsInDepth", EnviroSky.instance.cloudsSettings.stepsInDepthModificator);
		this.mat.SetFloat("_LODDistance", EnviroSky.instance.cloudsSettings.lodDistance);
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000564AC File Offset: 0x000548AC
	public void SetBlitmaterialProperties()
	{
		Matrix4x4 inverse = this.projection.inverse;
		this.blitMat.SetMatrix("_PreviousRotation", this.previousRotation);
		this.blitMat.SetMatrix("_Projection", this.projection);
		this.blitMat.SetMatrix("_InverseRotation", this.inverseRotation);
		this.blitMat.SetMatrix("_InverseProjection", inverse);
		if (EnviroSky.instance.singlePassVR)
		{
			Matrix4x4 inverse2 = this.projectionSPVR.inverse;
			this.blitMat.SetMatrix("_PreviousRotationSPVR", this.previousRotationSPVR);
			this.blitMat.SetMatrix("_ProjectionSPVR", this.projectionSPVR);
			this.blitMat.SetMatrix("_InverseRotationSPVR", this.inverseRotationSPVR);
			this.blitMat.SetMatrix("_InverseProjectionSPVR", inverse2);
		}
		this.blitMat.SetFloat("_FrameNumber", (float)this.subFrameNumber);
		this.blitMat.SetFloat("_ReprojectionPixelSize", (float)this.reprojectionPixelSize);
		this.blitMat.SetVector("_SubFrameDimension", new Vector2((float)this.subFrameWidth, (float)this.subFrameHeight));
		this.blitMat.SetVector("_FrameDimension", new Vector2((float)this.frameWidth, (float)this.frameHeight));
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00056604 File Offset: 0x00054A04
	public void RenderClouds(RenderTexture tex)
	{
		this.SetCloudProperties();
		Graphics.Blit(null, tex, this.mat);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0005661C File Offset: 0x00054A1C
	private void CreateCloudsRenderTextures(RenderTexture source)
	{
		if (this.subFrameTex != null)
		{
			UnityEngine.Object.DestroyImmediate(this.subFrameTex);
			this.subFrameTex = null;
		}
		if (this.prevFrameTex != null)
		{
			UnityEngine.Object.DestroyImmediate(this.prevFrameTex);
			this.prevFrameTex = null;
		}
		RenderTextureFormat colorFormat = (!this.myCam.allowHDR) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
		if (this.subFrameTex == null)
		{
			RenderTextureDescriptor desc = new RenderTextureDescriptor(this.subFrameWidth, this.subFrameHeight, colorFormat, 0);
			if (EnviroSky.instance.singlePassVR)
			{
				desc.vrUsage = VRTextureUsage.TwoEyes;
			}
			this.subFrameTex = new RenderTexture(desc);
			this.subFrameTex.filterMode = FilterMode.Bilinear;
			this.subFrameTex.hideFlags = HideFlags.HideAndDontSave;
			this.isFirstFrame = true;
		}
		if (this.prevFrameTex == null)
		{
			RenderTextureDescriptor desc2 = new RenderTextureDescriptor(this.frameWidth, this.frameHeight, colorFormat, 0);
			if (EnviroSky.instance.singlePassVR)
			{
				desc2.vrUsage = VRTextureUsage.TwoEyes;
			}
			this.prevFrameTex = new RenderTexture(desc2);
			this.prevFrameTex.filterMode = FilterMode.Bilinear;
			this.prevFrameTex.hideFlags = HideFlags.HideAndDontSave;
			this.isFirstFrame = true;
		}
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0005675C File Offset: 0x00054B5C
	private void Update()
	{
		if (this.currentReprojectionPixelSize != EnviroSky.instance.cloudsSettings.reprojectionPixelSize)
		{
			this.currentReprojectionPixelSize = EnviroSky.instance.cloudsSettings.reprojectionPixelSize;
			this.SetReprojectionPixelSize(EnviroSky.instance.cloudsSettings.reprojectionPixelSize);
		}
		this.mat.SetTexture("_WeatherMap", EnviroSky.instance.weatherMap);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000567C8 File Offset: 0x00054BC8
	[ImageEffectOpaque]
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (EnviroSky.instance == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (EnviroSky.instance.cloudsMode == EnviroSky.EnviroCloudsMode.Volume || EnviroSky.instance.cloudsMode == EnviroSky.EnviroCloudsMode.Both)
		{
			this.StartFrame();
			if (this.subFrameTex == null || this.prevFrameTex == null || this.textureDimensionChanged)
			{
				this.CreateCloudsRenderTextures(source);
			}
			this.RenderClouds(this.subFrameTex);
			if (this.isFirstFrame)
			{
				Graphics.Blit(this.subFrameTex, this.prevFrameTex);
				this.isFirstFrame = false;
			}
			this.blitMat.SetTexture("_MainTex", source);
			this.blitMat.SetTexture("_SubFrame", this.subFrameTex);
			this.blitMat.SetTexture("_PrevFrame", this.prevFrameTex);
			this.SetBlitmaterialProperties();
			Graphics.Blit(source, destination, this.blitMat);
			Graphics.Blit(this.subFrameTex, this.prevFrameTex);
			this.FinalizeFrame();
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000568EC File Offset: 0x00054CEC
	public void SetReprojectionPixelSize(EnviroCloudSettings.ReprojectionPixelSize pSize)
	{
		switch (pSize)
		{
		case EnviroCloudSettings.ReprojectionPixelSize.Off:
			this.reprojectionPixelSize = 1;
			break;
		case EnviroCloudSettings.ReprojectionPixelSize.Low:
			this.reprojectionPixelSize = 2;
			break;
		case EnviroCloudSettings.ReprojectionPixelSize.Medium:
			this.reprojectionPixelSize = 4;
			break;
		case EnviroCloudSettings.ReprojectionPixelSize.High:
			this.reprojectionPixelSize = 8;
			break;
		}
		this.frameList = this.CalculateFrames(this.reprojectionPixelSize);
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00056958 File Offset: 0x00054D58
	public void StartFrame()
	{
		this.textureDimensionChanged = this.UpdateFrameDimensions();
		Camera.MonoOrStereoscopicEye stereoActiveEye = this.myCam.stereoActiveEye;
		if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Mono)
		{
			if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Left)
			{
				if (stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
				{
					this.projection = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
					this.rotation = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
					this.inverseRotation = this.rotation.inverse;
				}
			}
			else
			{
				this.projection = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
				this.rotation = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
				this.inverseRotation = this.rotation.inverse;
				if (EnviroSky.instance.singlePassVR)
				{
					this.projectionSPVR = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
					this.rotationSPVR = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
					this.inverseRotationSPVR = this.rotationSPVR.inverse;
				}
			}
		}
		else
		{
			if (this.resetCameraProjection)
			{
				this.myCam.ResetProjectionMatrix();
			}
			this.projection = this.myCam.projectionMatrix;
			this.rotation = this.myCam.worldToCameraMatrix;
			this.inverseRotation = this.myCam.cameraToWorldMatrix;
		}
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00056A9C File Offset: 0x00054E9C
	public void FinalizeFrame()
	{
		this.renderingCounter++;
		this.previousRotation = this.rotation;
		if (EnviroSky.instance.singlePassVR)
		{
			this.previousRotationSPVR = this.rotationSPVR;
		}
		int num = this.reprojectionPixelSize * this.reprojectionPixelSize;
		this.subFrameNumber = this.frameList[this.renderingCounter % num];
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00056B04 File Offset: 0x00054F04
	private bool UpdateFrameDimensions()
	{
		int num = this.myCam.pixelWidth / EnviroSky.instance.cloudsSettings.cloudsRenderResolution;
		int num2 = this.myCam.pixelHeight / EnviroSky.instance.cloudsSettings.cloudsRenderResolution;
		while (num % this.reprojectionPixelSize != 0)
		{
			num++;
		}
		while (num2 % this.reprojectionPixelSize != 0)
		{
			num2++;
		}
		int num3 = num / this.reprojectionPixelSize;
		int num4 = num2 / this.reprojectionPixelSize;
		if (num != this.frameWidth || num3 != this.subFrameWidth || num2 != this.frameHeight || num4 != this.subFrameHeight)
		{
			this.frameWidth = num;
			this.frameHeight = num2;
			this.subFrameWidth = num3;
			this.subFrameHeight = num4;
			return true;
		}
		this.frameWidth = num;
		this.frameHeight = num2;
		this.subFrameWidth = num3;
		this.subFrameHeight = num4;
		return false;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00056BF4 File Offset: 0x00054FF4
	private int[] CalculateFrames(int reproSize)
	{
		this.subFrameNumber = 0;
		int num = reproSize * reproSize;
		int[] array = new int[num];
		int i;
		for (i = 0; i < num; i++)
		{
			array[i] = i;
		}
		while (i-- > 0)
		{
			int num2 = array[i];
			int num3 = (int)((float)UnityEngine.Random.Range(0, 1) * 1000f) % num;
			array[i] = array[num3];
			array[num3] = num2;
		}
		return array;
	}

	// Token: 0x040010F1 RID: 4337
	public bool resetCameraProjection = true;

	// Token: 0x040010F2 RID: 4338
	public bool tonemapping = true;

	// Token: 0x040010F3 RID: 4339
	private Camera myCam;

	// Token: 0x040010F4 RID: 4340
	private Material mat;

	// Token: 0x040010F5 RID: 4341
	private Material blitMat;

	// Token: 0x040010F6 RID: 4342
	private Material weatherMapMat;

	// Token: 0x040010F7 RID: 4343
	private RenderTexture subFrameTex;

	// Token: 0x040010F8 RID: 4344
	private RenderTexture prevFrameTex;

	// Token: 0x040010F9 RID: 4345
	private Texture2D curlMap;

	// Token: 0x040010FA RID: 4346
	private Texture3D noiseTexture;

	// Token: 0x040010FB RID: 4347
	private Texture3D noiseTextureHigh;

	// Token: 0x040010FC RID: 4348
	private Texture3D detailNoiseTexture;

	// Token: 0x040010FD RID: 4349
	private Texture3D detailNoiseTextureHigh;

	// Token: 0x040010FE RID: 4350
	private Matrix4x4 projection;

	// Token: 0x040010FF RID: 4351
	private Matrix4x4 projectionSPVR;

	// Token: 0x04001100 RID: 4352
	private Matrix4x4 inverseRotation;

	// Token: 0x04001101 RID: 4353
	private Matrix4x4 inverseRotationSPVR;

	// Token: 0x04001102 RID: 4354
	private Matrix4x4 rotation;

	// Token: 0x04001103 RID: 4355
	private Matrix4x4 rotationSPVR;

	// Token: 0x04001104 RID: 4356
	private Matrix4x4 previousRotation;

	// Token: 0x04001105 RID: 4357
	private Matrix4x4 previousRotationSPVR;

	// Token: 0x04001106 RID: 4358
	[HideInInspector]
	public EnviroCloudSettings.ReprojectionPixelSize currentReprojectionPixelSize;

	// Token: 0x04001107 RID: 4359
	private int reprojectionPixelSize;

	// Token: 0x04001108 RID: 4360
	private bool isFirstFrame;

	// Token: 0x04001109 RID: 4361
	private int subFrameNumber;

	// Token: 0x0400110A RID: 4362
	private int[] frameList;

	// Token: 0x0400110B RID: 4363
	private int renderingCounter;

	// Token: 0x0400110C RID: 4364
	private int subFrameWidth;

	// Token: 0x0400110D RID: 4365
	private int subFrameHeight;

	// Token: 0x0400110E RID: 4366
	private int frameWidth;

	// Token: 0x0400110F RID: 4367
	private int frameHeight;

	// Token: 0x04001110 RID: 4368
	private bool textureDimensionChanged;
}

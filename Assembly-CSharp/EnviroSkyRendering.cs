using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000358 RID: 856
[RequireComponent(typeof(Camera))]
public class EnviroSkyRendering : MonoBehaviour
{
	// Token: 0x1400004E RID: 78
	// (add) Token: 0x06000F12 RID: 3858 RVA: 0x0004EB68 File Offset: 0x0004CF68
	// (remove) Token: 0x06000F13 RID: 3859 RVA: 0x0004EB9C File Offset: 0x0004CF9C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<EnviroSkyRendering, Matrix4x4, Matrix4x4> PreRenderEvent;

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0004EBD0 File Offset: 0x0004CFD0
	public CommandBuffer GlobalCommandBuffer
	{
		get
		{
			return this._preLightPass;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0004EBD8 File Offset: 0x0004CFD8
	public CommandBuffer GlobalCommandBufferForward
	{
		get
		{
			return this._afterLightPass;
		}
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0004EBE0 File Offset: 0x0004CFE0
	public static Material GetLightMaterial()
	{
		return EnviroSkyRendering._lightMaterial;
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0004EBE7 File Offset: 0x0004CFE7
	public static Mesh GetPointLightMesh()
	{
		return EnviroSkyRendering._pointLightMesh;
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0004EBEE File Offset: 0x0004CFEE
	public static Mesh GetSpotLightMesh()
	{
		return EnviroSkyRendering._spotLightMesh;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0004EBF5 File Offset: 0x0004CFF5
	public RenderTexture GetVolumeLightBuffer()
	{
		if (EnviroSky.instance.volumeLightSettings.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
		{
			return this._quarterVolumeLightTexture;
		}
		if (EnviroSky.instance.volumeLightSettings.Resolution == EnviroSkyRendering.VolumtericResolution.Half)
		{
			return this._halfVolumeLightTexture;
		}
		return this._volumeLightTexture;
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x0004EC35 File Offset: 0x0004D035
	public RenderTexture GetVolumeLightDepthBuffer()
	{
		if (EnviroSky.instance.volumeLightSettings.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
		{
			return this._quarterDepthBuffer;
		}
		if (EnviroSky.instance.volumeLightSettings.Resolution == EnviroSkyRendering.VolumtericResolution.Half)
		{
			return this._halfDepthBuffer;
		}
		return null;
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x0004EC70 File Offset: 0x0004D070
	public static Texture GetDefaultSpotCookie()
	{
		return EnviroSkyRendering._defaultSpotCookie;
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0004EC78 File Offset: 0x0004D078
	private void Awake()
	{
		this.myCam = base.GetComponent<Camera>();
		if (this.myCam.actualRenderingPath == RenderingPath.Forward)
		{
			this.myCam.depthTextureMode = DepthTextureMode.Depth;
		}
		this._currentResolution = this.Resolution;
		this._material = new Material(Shader.Find("Enviro/VolumeLight"));
		Shader shader = Shader.Find("Hidden/EnviroBilateralBlur");
		if (shader == null)
		{
			throw new Exception("Critical Error: \"Hidden/EnviroBilateralBlur\" shader is missing.");
		}
		this._bilateralBlurMaterial = new Material(shader);
		this._preLightPass = new CommandBuffer();
		this._preLightPass.name = "PreLight";
		this._afterLightPass = new CommandBuffer();
		this._afterLightPass.name = "AfterLight";
		this.ChangeResolution();
		if (EnviroSkyRendering._pointLightMesh == null)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			EnviroSkyRendering._pointLightMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
			UnityEngine.Object.Destroy(gameObject);
		}
		if (EnviroSkyRendering._spotLightMesh == null)
		{
			EnviroSkyRendering._spotLightMesh = this.CreateSpotLightMesh();
		}
		if (EnviroSkyRendering._lightMaterial == null)
		{
			Shader shader2 = Shader.Find("Enviro/VolumeLight");
			if (shader2 == null)
			{
				throw new Exception("Critical Error: \"Enviro/VolumeLight\" shader is missing.");
			}
			EnviroSkyRendering._lightMaterial = new Material(shader2);
		}
		if (EnviroSkyRendering._defaultSpotCookie == null)
		{
			EnviroSkyRendering._defaultSpotCookie = this.DefaultSpotCookie;
		}
		this.LoadNoise3dTexture();
		this.GenerateDitherTexture();
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0004EDE5 File Offset: 0x0004D1E5
	private void Start()
	{
		this.CreateMaterialsAndTextures();
		if (EnviroSky.instance != null)
		{
			this.SetReprojectionPixelSize(EnviroSky.instance.cloudsSettings.reprojectionPixelSize);
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x0004EE14 File Offset: 0x0004D214
	private void OnEnable()
	{
		if (this.myCam == null)
		{
			this.myCam = base.GetComponent<Camera>();
		}
		if (this.myCam.actualRenderingPath == RenderingPath.Forward)
		{
			this.myCam.AddCommandBuffer(CameraEvent.AfterDepthTexture, this._preLightPass);
			this.myCam.AddCommandBuffer(CameraEvent.AfterForwardOpaque, this._afterLightPass);
		}
		else
		{
			this.myCam.AddCommandBuffer(CameraEvent.BeforeLighting, this._preLightPass);
		}
		this.CreateFogMaterial();
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x0004EE94 File Offset: 0x0004D294
	private void OnDisable()
	{
		if (this.myCam.actualRenderingPath == RenderingPath.Forward)
		{
			this.myCam.RemoveCommandBuffer(CameraEvent.AfterDepthTexture, this._preLightPass);
			this.myCam.RemoveCommandBuffer(CameraEvent.AfterForwardOpaque, this._afterLightPass);
		}
		else
		{
			this.myCam.RemoveCommandBuffer(CameraEvent.BeforeLighting, this._preLightPass);
		}
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x0004EEF0 File Offset: 0x0004D2F0
	private void ChangeResolution()
	{
		int pixelWidth = this.myCam.pixelWidth;
		int pixelHeight = this.myCam.pixelHeight;
		if (this._volumeLightTexture != null)
		{
			UnityEngine.Object.Destroy(this._volumeLightTexture);
		}
		this._volumeLightTexture = new RenderTexture(pixelWidth, pixelHeight, 0, RenderTextureFormat.ARGBHalf);
		this._volumeLightTexture.name = "VolumeLightBuffer";
		this._volumeLightTexture.filterMode = FilterMode.Bilinear;
		if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
		{
			if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Half || this.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
			{
				this._volumeLightTexture.vrUsage = VRTextureUsage.None;
			}
			else
			{
				this._volumeLightTexture.vrUsage = VRTextureUsage.TwoEyes;
			}
		}
		if (this._halfDepthBuffer != null)
		{
			UnityEngine.Object.Destroy(this._halfDepthBuffer);
		}
		if (this._halfVolumeLightTexture != null)
		{
			UnityEngine.Object.Destroy(this._halfVolumeLightTexture);
		}
		if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Half || this.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
		{
			this._halfVolumeLightTexture = new RenderTexture(pixelWidth / 2, pixelHeight / 2, 0, RenderTextureFormat.ARGBHalf);
			this._halfVolumeLightTexture.name = "VolumeLightBufferHalf";
			this._halfVolumeLightTexture.filterMode = FilterMode.Bilinear;
			if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
			{
				this._halfVolumeLightTexture.vrUsage = VRTextureUsage.TwoEyes;
			}
			this._halfDepthBuffer = new RenderTexture(pixelWidth / 2, pixelHeight / 2, 0, RenderTextureFormat.RFloat);
			this._halfDepthBuffer.name = "VolumeLightHalfDepth";
			this._halfDepthBuffer.Create();
			this._halfDepthBuffer.filterMode = FilterMode.Point;
		}
		if (this._quarterVolumeLightTexture != null)
		{
			UnityEngine.Object.Destroy(this._quarterVolumeLightTexture);
		}
		if (this._quarterDepthBuffer != null)
		{
			UnityEngine.Object.Destroy(this._quarterDepthBuffer);
		}
		if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
		{
			this._quarterVolumeLightTexture = new RenderTexture(pixelWidth / 4, pixelHeight / 4, 0, RenderTextureFormat.ARGBHalf);
			this._quarterVolumeLightTexture.name = "VolumeLightBufferQuarter";
			this._quarterVolumeLightTexture.filterMode = FilterMode.Bilinear;
			if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
			{
				this._quarterVolumeLightTexture.vrUsage = VRTextureUsage.TwoEyes;
			}
			this._quarterDepthBuffer = new RenderTexture(pixelWidth / 4, pixelHeight / 4, 0, RenderTextureFormat.RFloat);
			this._quarterDepthBuffer.name = "VolumeLightQuarterDepth";
			this._quarterDepthBuffer.Create();
			this._quarterDepthBuffer.filterMode = FilterMode.Point;
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0004F170 File Offset: 0x0004D570
	private void CreateFogMaterial()
	{
		if (this._volumeRenderingMaterial != null)
		{
			UnityEngine.Object.Destroy(this._volumeRenderingMaterial);
		}
		if (!this.simpleFog)
		{
			Shader shader = Shader.Find("Enviro/EnviroFogRendering");
			if (shader == null)
			{
				throw new Exception("Critical Error: \"Enviro/EnviroFogRendering\" shader is missing.");
			}
			this._volumeRenderingMaterial = new Material(shader);
		}
		else
		{
			Shader shader2 = Shader.Find("Enviro/EnviroFogRenderingSimple");
			if (shader2 == null)
			{
				throw new Exception("Critical Error: \"Enviro/EnviroFogRendering\" shader is missing.");
			}
			this._volumeRenderingMaterial = new Material(shader2);
		}
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0004F208 File Offset: 0x0004D608
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
		if (this.blackTexture == null)
		{
			this.blackTexture = (Resources.Load("tex_enviro_black") as Texture2D);
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

	// Token: 0x06000F23 RID: 3875 RVA: 0x0004F348 File Offset: 0x0004D748
	private void OnPreRender()
	{
		if (this.volumeLighting)
		{
			Matrix4x4 matrix4x = Matrix4x4.Perspective(this.myCam.fieldOfView, this.myCam.aspect, 0.01f, this.myCam.farClipPlane);
			Matrix4x4 matrix4x2 = Matrix4x4.Perspective(this.myCam.fieldOfView, this.myCam.aspect, 0.01f, this.myCam.farClipPlane);
			if (this.myCam.stereoEnabled)
			{
				matrix4x = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
				matrix4x = GL.GetGPUProjectionMatrix(matrix4x, true);
				matrix4x2 = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
				matrix4x2 = GL.GetGPUProjectionMatrix(matrix4x2, true);
			}
			else
			{
				matrix4x = Matrix4x4.Perspective(this.myCam.fieldOfView, this.myCam.aspect, 0.01f, this.myCam.farClipPlane);
				matrix4x = GL.GetGPUProjectionMatrix(matrix4x, true);
			}
			if (this.myCam.stereoEnabled)
			{
				this._viewProj = matrix4x * this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
				this._viewProjSP = matrix4x2 * this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
			}
			else
			{
				this._viewProj = matrix4x * this.myCam.worldToCameraMatrix;
				this._viewProjSP = matrix4x2 * this.myCam.worldToCameraMatrix;
			}
			this._preLightPass.Clear();
			this._afterLightPass.Clear();
			bool flag = SystemInfo.graphicsShaderLevel > 40;
			if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
			{
				Texture source = null;
				this._preLightPass.Blit(source, this._halfDepthBuffer, this._bilateralBlurMaterial, (!flag) ? 10 : 4);
				this._preLightPass.Blit(source, this._quarterDepthBuffer, this._bilateralBlurMaterial, (!flag) ? 11 : 6);
				this._preLightPass.SetRenderTarget(this._quarterVolumeLightTexture);
			}
			else if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Half)
			{
				Texture source2 = null;
				this._preLightPass.Blit(source2, this._halfDepthBuffer, this._bilateralBlurMaterial, (!flag) ? 10 : 4);
				this._preLightPass.SetRenderTarget(this._halfVolumeLightTexture);
			}
			else
			{
				this._preLightPass.SetRenderTarget(this._volumeLightTexture);
			}
			this._preLightPass.ClearRenderTarget(false, true, new Color(0f, 0f, 0f, 1f));
			this.UpdateMaterialParameters();
			if (EnviroSkyRendering.PreRenderEvent != null)
			{
				EnviroSkyRendering.PreRenderEvent(this, this._viewProj, this._viewProjSP);
			}
		}
		if (this.myCam.stereoEnabled)
		{
			Matrix4x4 inverse = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Left).inverse;
			Matrix4x4 inverse2 = this.myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right).inverse;
			Matrix4x4 stereoProjectionMatrix = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
			Matrix4x4 stereoProjectionMatrix2 = this.myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
			Matrix4x4 inverse3 = GL.GetGPUProjectionMatrix(stereoProjectionMatrix, true).inverse;
			Matrix4x4 inverse4 = GL.GetGPUProjectionMatrix(stereoProjectionMatrix2, true).inverse;
			if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3)
			{
				ref Matrix4x4 ptr = ref inverse3;
				inverse3[1, 1] = ptr[1, 1] * -1f;
				ptr = ref inverse4;
				inverse4[1, 1] = ptr[1, 1] * -1f;
			}
			Shader.SetGlobalMatrix("_LeftWorldFromView", inverse);
			Shader.SetGlobalMatrix("_RightWorldFromView", inverse2);
			Shader.SetGlobalMatrix("_LeftViewFromScreen", inverse3);
			Shader.SetGlobalMatrix("_RightViewFromScreen", inverse4);
		}
		else
		{
			Matrix4x4 cameraToWorldMatrix = this.myCam.cameraToWorldMatrix;
			Matrix4x4 projectionMatrix = this.myCam.projectionMatrix;
			Matrix4x4 inverse5 = GL.GetGPUProjectionMatrix(projectionMatrix, true).inverse;
			if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3)
			{
				ref Matrix4x4 ptr = ref inverse5;
				inverse5[1, 1] = ptr[1, 1] * -1f;
			}
			Shader.SetGlobalMatrix("_LeftWorldFromView", cameraToWorldMatrix);
			Shader.SetGlobalMatrix("_LeftViewFromScreen", inverse5);
		}
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this.myCam != null)
		{
			Camera.MonoOrStereoscopicEye stereoActiveEye = this.myCam.stereoActiveEye;
			if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Mono)
			{
				if (stereoActiveEye != Camera.MonoOrStereoscopicEye.Left)
				{
					if (stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
					{
						if (EnviroSky.instance.satCamera != null)
						{
							this.RenderCamera(EnviroSky.instance.satCamera, Camera.MonoOrStereoscopicEye.Right);
						}
					}
				}
				else if (EnviroSky.instance.satCamera != null)
				{
					this.RenderCamera(EnviroSky.instance.satCamera, Camera.MonoOrStereoscopicEye.Left);
				}
			}
			else if (EnviroSky.instance.satCamera != null)
			{
				this.RenderCamera(EnviroSky.instance.satCamera, Camera.MonoOrStereoscopicEye.Mono);
			}
			if (EnviroSky.instance.satCamera != null)
			{
				RenderSettings.skybox.SetTexture("_SatTex", EnviroSky.instance.satCamera.targetTexture);
			}
		}
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0004F870 File Offset: 0x0004DC70
	private void RenderCamera(Camera targetCam, Camera.MonoOrStereoscopicEye eye)
	{
		targetCam.fieldOfView = EnviroSky.instance.PlayerCamera.fieldOfView;
		targetCam.aspect = EnviroSky.instance.PlayerCamera.aspect;
		if (eye != Camera.MonoOrStereoscopicEye.Mono)
		{
			if (eye != Camera.MonoOrStereoscopicEye.Left)
			{
				if (eye == Camera.MonoOrStereoscopicEye.Right)
				{
					targetCam.transform.position = EnviroSky.instance.PlayerCamera.transform.position;
					targetCam.transform.rotation = EnviroSky.instance.PlayerCamera.transform.rotation;
					targetCam.projectionMatrix = EnviroSky.instance.PlayerCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
					targetCam.worldToCameraMatrix = EnviroSky.instance.PlayerCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
					targetCam.Render();
				}
			}
			else
			{
				targetCam.transform.position = EnviroSky.instance.PlayerCamera.transform.position;
				targetCam.transform.rotation = EnviroSky.instance.PlayerCamera.transform.rotation;
				targetCam.projectionMatrix = EnviroSky.instance.PlayerCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
				targetCam.worldToCameraMatrix = EnviroSky.instance.PlayerCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
				targetCam.Render();
			}
		}
		else
		{
			targetCam.transform.position = EnviroSky.instance.PlayerCamera.transform.position;
			targetCam.transform.rotation = EnviroSky.instance.PlayerCamera.transform.rotation;
			targetCam.worldToCameraMatrix = EnviroSky.instance.PlayerCamera.worldToCameraMatrix;
			targetCam.Render();
		}
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0004FA08 File Offset: 0x0004DE08
	private void Update()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this._currentResolution != this.Resolution)
		{
			this._currentResolution = this.Resolution;
			this.ChangeResolution();
		}
		if (this.currentReprojectionPixelSize != EnviroSky.instance.cloudsSettings.reprojectionPixelSize)
		{
			this.currentReprojectionPixelSize = EnviroSky.instance.cloudsSettings.reprojectionPixelSize;
			this.SetReprojectionPixelSize(EnviroSky.instance.cloudsSettings.reprojectionPixelSize);
		}
		if (this._volumeLightTexture.width != this.myCam.pixelWidth || this._volumeLightTexture.height != this.myCam.pixelHeight)
		{
			this.ChangeResolution();
		}
		if (this.currentSimpleFog != this.simpleFog)
		{
			this.CreateFogMaterial();
			this.currentSimpleFog = this.simpleFog;
		}
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0004FAEC File Offset: 0x0004DEEC
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
		if (EnviroSky.instance.cloudsSettings.customWeatherMap == null)
		{
			this.mat.SetTexture("_WeatherMap", EnviroSky.instance.weatherMap);
		}
		else
		{
			this.mat.SetTexture("_WeatherMap", EnviroSky.instance.cloudsSettings.customWeatherMap);
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
		this.mat.SetFloat("_Tonemapping", (!EnviroSky.instance.cloudsSettings.tonemapping) ? 1f : 0f);
		this.mat.SetFloat("_stepsInDepth", EnviroSky.instance.cloudsSettings.stepsInDepthModificator);
		this.mat.SetFloat("_LODDistance", EnviroSky.instance.cloudsSettings.lodDistance);
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000501AC File Offset: 0x0004E5AC
	public void SetBlitmaterialProperties()
	{
		Matrix4x4 inverse = this.projection.inverse;
		this.blitMat.SetMatrix("_PreviousRotation", this.previousRotation);
		this.blitMat.SetMatrix("_Projection", this.projection);
		this.blitMat.SetMatrix("_InverseRotation", this.inverseRotation);
		this.blitMat.SetMatrix("_InverseProjection", inverse);
		if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
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

	// Token: 0x06000F28 RID: 3880 RVA: 0x00050314 File Offset: 0x0004E714
	public void RenderClouds(RenderTexture tex)
	{
		this.SetCloudProperties();
		Graphics.Blit(null, tex, this.mat);
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0005032C File Offset: 0x0004E72C
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
			if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
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
			if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
			{
				desc2.vrUsage = VRTextureUsage.TwoEyes;
			}
			this.prevFrameTex = new RenderTexture(desc2);
			this.prevFrameTex.filterMode = FilterMode.Bilinear;
			this.prevFrameTex.hideFlags = HideFlags.HideAndDontSave;
			this.isFirstFrame = true;
		}
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0005048C File Offset: 0x0004E88C
	[ImageEffectOpaque]
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (EnviroSky.instance == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (this.myCam.actualRenderingPath == RenderingPath.Forward)
		{
			this.myCam.depthTextureMode |= DepthTextureMode.Depth;
		}
		int depthBufferBits = source.depth;
		if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal)
		{
			depthBufferBits = 0;
		}
		RenderTextureDescriptor desc = new RenderTextureDescriptor(source.width, source.height, source.format, depthBufferBits);
		desc.msaaSamples = source.antiAliasing;
		if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
		{
			desc.vrUsage = VRTextureUsage.TwoEyes;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(desc);
		if (EnviroSky.instance.cloudsMode == EnviroSky.EnviroCloudsMode.Volume || EnviroSky.instance.cloudsMode == EnviroSky.EnviroCloudsMode.Both)
		{
			this.StartFrame(source.width, source.height);
			if (this.subFrameTex == null || this.prevFrameTex == null || this.textureDimensionChanged)
			{
				this.CreateCloudsRenderTextures(source);
			}
			if (!this.isAddionalCamera)
			{
				EnviroSky.instance.cloudsRenderTarget = this.subFrameTex;
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
			Graphics.Blit(source, temporary, this.blitMat);
			Graphics.Blit(this.subFrameTex, this.prevFrameTex);
			this.FinalizeFrame();
		}
		else
		{
			Graphics.Blit(source, temporary);
		}
		float num = this.myCam.transform.position.y - this.height;
		float z = (num > 0f) ? 0f : 1f;
		FogMode fogMode = RenderSettings.fogMode;
		float fogDensity = RenderSettings.fogDensity;
		float fogStartDistance = RenderSettings.fogStartDistance;
		float fogEndDistance = RenderSettings.fogEndDistance;
		bool flag = fogMode == FogMode.Linear;
		float num2 = (!flag) ? 0f : (fogEndDistance - fogStartDistance);
		float num3 = (Mathf.Abs(num2) <= 0.0001f) ? 0f : (1f / num2);
		Vector4 value;
		value.x = fogDensity * 1.2011224f;
		value.y = fogDensity * 1.442695f;
		value.z = ((!flag) ? 0f : (-num3));
		value.w = ((!flag) ? 0f : (fogEndDistance * num3));
		if (!EnviroSky.instance.fogSettings.useSimpleFog)
		{
			Shader.SetGlobalVector("_FogNoiseVelocity", new Vector4(EnviroSky.instance.fogSettings.noiseVelocity.x, EnviroSky.instance.fogSettings.noiseVelocity.y) * EnviroSky.instance.fogSettings.noiseScale);
			Shader.SetGlobalVector("_FogNoiseData", new Vector4(EnviroSky.instance.fogSettings.noiseScale, EnviroSky.instance.fogSettings.noiseIntensity, EnviroSky.instance.fogSettings.noiseIntensityOffset));
			Shader.SetGlobalTexture("_FogNoiseTexture", this._noiseTexture);
		}
		if (this.volumeLighting)
		{
			if (this.dirVolumeLighting)
			{
				Light component = EnviroSky.instance.Components.DirectLight.GetComponent<Light>();
				int pass = 4;
				this._material.SetPass(pass);
				if (EnviroSky.instance.volumeLightSettings.directLightNoise)
				{
					this._material.EnableKeyword("NOISE");
				}
				else
				{
					this._material.DisableKeyword("NOISE");
				}
				this._material.SetVector("_LightDir", new Vector4(component.transform.forward.x, component.transform.forward.y, component.transform.forward.z, 1f / (component.range * component.range)));
				this._material.SetVector("_LightColor", component.color * component.intensity);
				this._material.SetFloat("_MaxRayLength", EnviroSky.instance.volumeLightSettings.MaxRayLength);
				if (component.cookie == null)
				{
					this._material.EnableKeyword("DIRECTIONAL");
					this._material.DisableKeyword("DIRECTIONAL_COOKIE");
				}
				else
				{
					this._material.EnableKeyword("DIRECTIONAL_COOKIE");
					this._material.DisableKeyword("DIRECTIONAL");
					this._material.SetTexture("_LightTexture0", component.cookie);
				}
				this._material.SetInt("_SampleCount", EnviroSky.instance.volumeLightSettings.SampleCount);
				this._material.SetVector("_NoiseVelocity", new Vector4(EnviroSky.instance.volumeLightSettings.noiseVelocity.x, EnviroSky.instance.volumeLightSettings.noiseVelocity.y) * EnviroSky.instance.volumeLightSettings.noiseScale);
				this._material.SetVector("_NoiseData", new Vector4(EnviroSky.instance.volumeLightSettings.noiseScale, EnviroSky.instance.volumeLightSettings.noiseIntensity, EnviroSky.instance.volumeLightSettings.noiseIntensityOffset));
				this._material.SetVector("_MieG", new Vector4(1f - EnviroSky.instance.volumeLightSettings.Anistropy * EnviroSky.instance.volumeLightSettings.Anistropy, 1f + EnviroSky.instance.volumeLightSettings.Anistropy * EnviroSky.instance.volumeLightSettings.Anistropy, 2f * EnviroSky.instance.volumeLightSettings.Anistropy, 0.07957747f));
				this._material.SetVector("_VolumetricLight", new Vector4(EnviroSky.instance.volumeLightSettings.ScatteringCoef, EnviroSky.instance.volumeLightSettings.ExtinctionCoef, component.range, 1f));
				this._material.SetTexture("_CameraDepthTexture", this.GetVolumeLightDepthBuffer());
				if (component.shadows != LightShadows.None)
				{
					this._material.EnableKeyword("SHADOWS_DEPTH");
					Graphics.Blit(null, this.GetVolumeLightBuffer(), this._material, pass);
				}
				else
				{
					this._material.DisableKeyword("SHADOWS_DEPTH");
					Graphics.Blit(null, this.GetVolumeLightBuffer(), this._material, pass);
				}
			}
			if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Quarter)
			{
				RenderTexture temporary2 = RenderTexture.GetTemporary(this._quarterDepthBuffer.width, this._quarterDepthBuffer.height, 0, RenderTextureFormat.ARGBHalf);
				temporary2.filterMode = FilterMode.Bilinear;
				if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
				{
					temporary2.vrUsage = VRTextureUsage.TwoEyes;
				}
				Graphics.Blit(this._quarterVolumeLightTexture, temporary2, this._bilateralBlurMaterial, 8);
				Graphics.Blit(temporary2, this._quarterVolumeLightTexture, this._bilateralBlurMaterial, 9);
				Graphics.Blit(this._quarterVolumeLightTexture, this._volumeLightTexture, this._bilateralBlurMaterial, 7);
				RenderTexture.ReleaseTemporary(temporary2);
			}
			else if (this.Resolution == EnviroSkyRendering.VolumtericResolution.Half)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(this._halfVolumeLightTexture.width, this._halfVolumeLightTexture.height, 0, RenderTextureFormat.ARGBHalf);
				temporary3.filterMode = FilterMode.Bilinear;
				if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
				{
					temporary3.vrUsage = VRTextureUsage.TwoEyes;
				}
				Graphics.Blit(this._halfVolumeLightTexture, temporary3, this._bilateralBlurMaterial, 2);
				Graphics.Blit(temporary3, this._halfVolumeLightTexture, this._bilateralBlurMaterial, 3);
				Graphics.Blit(this._halfVolumeLightTexture, this._volumeLightTexture, this._bilateralBlurMaterial, 5);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				RenderTexture temporary4 = RenderTexture.GetTemporary(this._volumeLightTexture.width, this._volumeLightTexture.height, 0, RenderTextureFormat.ARGBHalf);
				temporary4.filterMode = FilterMode.Bilinear;
				if (this.myCam.stereoEnabled && EnviroSky.instance.singlePassVR)
				{
					temporary4.vrUsage = VRTextureUsage.TwoEyes;
				}
				Graphics.Blit(this._volumeLightTexture, temporary4, this._bilateralBlurMaterial, 0);
				Graphics.Blit(temporary4, this._volumeLightTexture, this._bilateralBlurMaterial, 1);
				RenderTexture.ReleaseTemporary(temporary4);
			}
			this._volumeRenderingMaterial.EnableKeyword("ENVIROVOLUMELIGHT");
		}
		else
		{
			this._volumeRenderingMaterial.DisableKeyword("ENVIROVOLUMELIGHT");
		}
		Shader.SetGlobalFloat("_EnviroVolumeDensity", EnviroSky.instance.globalVolumeLightIntensity);
		Shader.SetGlobalVector("_SceneFogParams", value);
		Shader.SetGlobalVector("_SceneFogMode", new Vector4((float)fogMode, (float)((!this.useRadialDistance) ? 0 : 1), 0f, 0f));
		Shader.SetGlobalVector("_HeightParams", new Vector4(this.height, num, z, this.heightDensity * 0.5f));
		Shader.SetGlobalVector("_DistanceParams", new Vector4(-Mathf.Max(this.startDistance, 0f), 0f, 0f, 0f));
		this._volumeRenderingMaterial.SetTexture("_MainTex", temporary);
		if (this.volumeLighting)
		{
			Shader.SetGlobalTexture("_EnviroVolumeLightingTex", this._volumeLightTexture);
		}
		else
		{
			Shader.SetGlobalTexture("_EnviroVolumeLightingTex", this.blackTexture);
		}
		Graphics.Blit(temporary, destination, this._volumeRenderingMaterial);
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00050E60 File Offset: 0x0004F260
	private void UpdateMaterialParameters()
	{
		this._bilateralBlurMaterial.SetTexture("_HalfResDepthBuffer", this._halfDepthBuffer);
		this._bilateralBlurMaterial.SetTexture("_HalfResColor", this._halfVolumeLightTexture);
		this._bilateralBlurMaterial.SetTexture("_QuarterResDepthBuffer", this._quarterDepthBuffer);
		this._bilateralBlurMaterial.SetTexture("_QuarterResColor", this._quarterVolumeLightTexture);
		Shader.SetGlobalTexture("_DitherTexture", this._ditheringTexture);
		Shader.SetGlobalTexture("_NoiseTexture", this._noiseTexture);
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x00050EE8 File Offset: 0x0004F2E8
	private void LoadNoise3dTexture()
	{
		TextAsset textAsset = Resources.Load("NoiseVolume") as TextAsset;
		byte[] bytes = textAsset.bytes;
		uint num = BitConverter.ToUInt32(textAsset.bytes, 12);
		uint num2 = BitConverter.ToUInt32(textAsset.bytes, 16);
		uint num3 = BitConverter.ToUInt32(textAsset.bytes, 20);
		uint num4 = BitConverter.ToUInt32(textAsset.bytes, 24);
		uint num5 = BitConverter.ToUInt32(textAsset.bytes, 80);
		uint num6 = BitConverter.ToUInt32(textAsset.bytes, 88);
		if (num6 == 0U)
		{
			num6 = num3 / num2 * 8U;
		}
		this._noiseTexture = new Texture3D((int)num2, (int)num, (int)num4, TextureFormat.RGBA32, false);
		this._noiseTexture.name = "3D Noise";
		Color[] array = new Color[num2 * num * num4];
		uint num7 = 128U;
		if (textAsset.bytes[84] == 68 && textAsset.bytes[85] == 88 && textAsset.bytes[86] == 49 && textAsset.bytes[87] == 48 && (num5 & 4U) != 0U)
		{
			uint num8 = BitConverter.ToUInt32(textAsset.bytes, (int)num7);
			if (num8 >= 60U && num8 <= 65U)
			{
				num6 = 8U;
			}
			else if (num8 >= 48U && num8 <= 52U)
			{
				num6 = 16U;
			}
			else if (num8 >= 27U && num8 <= 32U)
			{
				num6 = 32U;
			}
			num7 += 20U;
		}
		uint num9 = num6 / 8U;
		num3 = (num2 * num6 + 7U) / 8U;
		int num10 = 0;
		while ((long)num10 < (long)((ulong)num4))
		{
			int num11 = 0;
			while ((long)num11 < (long)((ulong)num))
			{
				int num12 = 0;
				while ((long)num12 < (long)((ulong)num2))
				{
					checked
					{
						float num13 = (float)bytes[(int)((IntPtr)(unchecked((ulong)num7 + (ulong)((long)num12 * (long)((ulong)num9)))))] / 255f;
						array[(int)((IntPtr)(unchecked((long)num12 + (long)num11 * (long)((ulong)num2) + (long)num10 * (long)((ulong)num2) * (long)((ulong)num))))] = new Color(num13, num13, num13, num13);
					}
					num12++;
				}
				num7 += num3;
				num11++;
			}
			num10++;
		}
		this._noiseTexture.SetPixels(array);
		this._noiseTexture.Apply();
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00051110 File Offset: 0x0004F510
	private void GenerateDitherTexture()
	{
		if (this._ditheringTexture != null)
		{
			return;
		}
		int num = 8;
		this._ditheringTexture = new Texture2D(num, num, TextureFormat.Alpha8, false, true);
		this._ditheringTexture.filterMode = FilterMode.Point;
		Color32[] array = new Color32[num * num];
		int num2 = 0;
		byte b = 3;
		array[num2++] = new Color32(b, b, b, b);
		b = 192;
		array[num2++] = new Color32(b, b, b, b);
		b = 51;
		array[num2++] = new Color32(b, b, b, b);
		b = 239;
		array[num2++] = new Color32(b, b, b, b);
		b = 15;
		array[num2++] = new Color32(b, b, b, b);
		b = 204;
		array[num2++] = new Color32(b, b, b, b);
		b = 62;
		array[num2++] = new Color32(b, b, b, b);
		b = 251;
		array[num2++] = new Color32(b, b, b, b);
		b = 129;
		array[num2++] = new Color32(b, b, b, b);
		b = 66;
		array[num2++] = new Color32(b, b, b, b);
		b = 176;
		array[num2++] = new Color32(b, b, b, b);
		b = 113;
		array[num2++] = new Color32(b, b, b, b);
		b = 141;
		array[num2++] = new Color32(b, b, b, b);
		b = 78;
		array[num2++] = new Color32(b, b, b, b);
		b = 188;
		array[num2++] = new Color32(b, b, b, b);
		b = 125;
		array[num2++] = new Color32(b, b, b, b);
		b = 35;
		array[num2++] = new Color32(b, b, b, b);
		b = 223;
		array[num2++] = new Color32(b, b, b, b);
		b = 19;
		array[num2++] = new Color32(b, b, b, b);
		b = 207;
		array[num2++] = new Color32(b, b, b, b);
		b = 47;
		array[num2++] = new Color32(b, b, b, b);
		b = 235;
		array[num2++] = new Color32(b, b, b, b);
		b = 31;
		array[num2++] = new Color32(b, b, b, b);
		b = 219;
		array[num2++] = new Color32(b, b, b, b);
		b = 160;
		array[num2++] = new Color32(b, b, b, b);
		b = 98;
		array[num2++] = new Color32(b, b, b, b);
		b = 145;
		array[num2++] = new Color32(b, b, b, b);
		b = 82;
		array[num2++] = new Color32(b, b, b, b);
		b = 172;
		array[num2++] = new Color32(b, b, b, b);
		b = 109;
		array[num2++] = new Color32(b, b, b, b);
		b = 156;
		array[num2++] = new Color32(b, b, b, b);
		b = 94;
		array[num2++] = new Color32(b, b, b, b);
		b = 11;
		array[num2++] = new Color32(b, b, b, b);
		b = 200;
		array[num2++] = new Color32(b, b, b, b);
		b = 58;
		array[num2++] = new Color32(b, b, b, b);
		b = 247;
		array[num2++] = new Color32(b, b, b, b);
		b = 7;
		array[num2++] = new Color32(b, b, b, b);
		b = 196;
		array[num2++] = new Color32(b, b, b, b);
		b = 54;
		array[num2++] = new Color32(b, b, b, b);
		b = 243;
		array[num2++] = new Color32(b, b, b, b);
		b = 137;
		array[num2++] = new Color32(b, b, b, b);
		b = 74;
		array[num2++] = new Color32(b, b, b, b);
		b = 184;
		array[num2++] = new Color32(b, b, b, b);
		b = 121;
		array[num2++] = new Color32(b, b, b, b);
		b = 133;
		array[num2++] = new Color32(b, b, b, b);
		b = 70;
		array[num2++] = new Color32(b, b, b, b);
		b = 180;
		array[num2++] = new Color32(b, b, b, b);
		b = 117;
		array[num2++] = new Color32(b, b, b, b);
		b = 43;
		array[num2++] = new Color32(b, b, b, b);
		b = 231;
		array[num2++] = new Color32(b, b, b, b);
		b = 27;
		array[num2++] = new Color32(b, b, b, b);
		b = 215;
		array[num2++] = new Color32(b, b, b, b);
		b = 39;
		array[num2++] = new Color32(b, b, b, b);
		b = 227;
		array[num2++] = new Color32(b, b, b, b);
		b = 23;
		array[num2++] = new Color32(b, b, b, b);
		b = 211;
		array[num2++] = new Color32(b, b, b, b);
		b = 168;
		array[num2++] = new Color32(b, b, b, b);
		b = 105;
		array[num2++] = new Color32(b, b, b, b);
		b = 153;
		array[num2++] = new Color32(b, b, b, b);
		b = 90;
		array[num2++] = new Color32(b, b, b, b);
		b = 164;
		array[num2++] = new Color32(b, b, b, b);
		b = 102;
		array[num2++] = new Color32(b, b, b, b);
		b = 149;
		array[num2++] = new Color32(b, b, b, b);
		b = 86;
		array[num2++] = new Color32(b, b, b, b);
		this._ditheringTexture.SetPixels32(array);
		this._ditheringTexture.Apply();
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000518D0 File Offset: 0x0004FCD0
	private Mesh CreateSpotLightMesh()
	{
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[50];
		Color32[] array2 = new Color32[50];
		array[0] = new Vector3(0f, 0f, 0f);
		array[1] = new Vector3(0f, 0f, 1f);
		float num = 0f;
		float num2 = 0.3926991f;
		float num3 = 0.9f;
		for (int i = 0; i < 16; i++)
		{
			array[i + 2] = new Vector3(-Mathf.Cos(num) * num3, Mathf.Sin(num) * num3, num3);
			array2[i + 2] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			array[i + 2 + 16] = new Vector3(-Mathf.Cos(num), Mathf.Sin(num), 1f);
			array2[i + 2 + 16] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
			array[i + 2 + 32] = new Vector3(-Mathf.Cos(num) * num3, Mathf.Sin(num) * num3, 1f);
			array2[i + 2 + 32] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			num += num2;
		}
		mesh.vertices = array;
		mesh.colors32 = array2;
		int[] array3 = new int[288];
		int num4 = 0;
		for (int j = 2; j < 17; j++)
		{
			array3[num4++] = 0;
			array3[num4++] = j;
			array3[num4++] = j + 1;
		}
		array3[num4++] = 0;
		array3[num4++] = 17;
		array3[num4++] = 2;
		for (int k = 2; k < 17; k++)
		{
			array3[num4++] = k;
			array3[num4++] = k + 16;
			array3[num4++] = k + 1;
			array3[num4++] = k + 1;
			array3[num4++] = k + 16;
			array3[num4++] = k + 16 + 1;
		}
		array3[num4++] = 2;
		array3[num4++] = 17;
		array3[num4++] = 18;
		array3[num4++] = 18;
		array3[num4++] = 17;
		array3[num4++] = 33;
		for (int l = 18; l < 33; l++)
		{
			array3[num4++] = l;
			array3[num4++] = l + 16;
			array3[num4++] = l + 1;
			array3[num4++] = l + 1;
			array3[num4++] = l + 16;
			array3[num4++] = l + 16 + 1;
		}
		array3[num4++] = 18;
		array3[num4++] = 33;
		array3[num4++] = 34;
		array3[num4++] = 34;
		array3[num4++] = 33;
		array3[num4++] = 49;
		for (int m = 34; m < 49; m++)
		{
			array3[num4++] = 1;
			array3[num4++] = m + 1;
			array3[num4++] = m;
		}
		array3[num4++] = 1;
		array3[num4++] = 34;
		array3[num4++] = 49;
		mesh.triangles = array3;
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00051CB4 File Offset: 0x000500B4
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

	// Token: 0x06000F30 RID: 3888 RVA: 0x00051D20 File Offset: 0x00050120
	public void StartFrame(int width, int height)
	{
		this.textureDimensionChanged = this.UpdateFrameDimensions(width, height);
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
			this.projection = this.myCam.projectionMatrix;
			this.rotation = this.myCam.worldToCameraMatrix;
			this.inverseRotation = this.myCam.cameraToWorldMatrix;
		}
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00051E50 File Offset: 0x00050250
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

	// Token: 0x06000F32 RID: 3890 RVA: 0x00051EB8 File Offset: 0x000502B8
	private bool UpdateFrameDimensions(int width, int height)
	{
		int num = width / EnviroSky.instance.cloudsSettings.cloudsRenderResolution;
		int num2 = height / EnviroSky.instance.cloudsSettings.cloudsRenderResolution;
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

	// Token: 0x06000F33 RID: 3891 RVA: 0x00051F94 File Offset: 0x00050394
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

	// Token: 0x04001001 RID: 4097
	[HideInInspector]
	public bool isAddionalCamera;

	// Token: 0x04001002 RID: 4098
	private Camera myCam;

	// Token: 0x04001003 RID: 4099
	private RenderTexture spSatTex;

	// Token: 0x04001004 RID: 4100
	private Camera spSatCam;

	// Token: 0x04001005 RID: 4101
	private Material mat;

	// Token: 0x04001006 RID: 4102
	private Material blitMat;

	// Token: 0x04001007 RID: 4103
	private RenderTexture subFrameTex;

	// Token: 0x04001008 RID: 4104
	private RenderTexture prevFrameTex;

	// Token: 0x04001009 RID: 4105
	private Texture2D curlMap;

	// Token: 0x0400100A RID: 4106
	private Texture3D noiseTexture;

	// Token: 0x0400100B RID: 4107
	private Texture3D noiseTextureHigh;

	// Token: 0x0400100C RID: 4108
	private Texture3D detailNoiseTexture;

	// Token: 0x0400100D RID: 4109
	private Texture3D detailNoiseTextureHigh;

	// Token: 0x0400100E RID: 4110
	private Matrix4x4 projection;

	// Token: 0x0400100F RID: 4111
	private Matrix4x4 projectionSPVR;

	// Token: 0x04001010 RID: 4112
	private Matrix4x4 inverseRotation;

	// Token: 0x04001011 RID: 4113
	private Matrix4x4 inverseRotationSPVR;

	// Token: 0x04001012 RID: 4114
	private Matrix4x4 rotation;

	// Token: 0x04001013 RID: 4115
	private Matrix4x4 rotationSPVR;

	// Token: 0x04001014 RID: 4116
	private Matrix4x4 previousRotation;

	// Token: 0x04001015 RID: 4117
	private Matrix4x4 previousRotationSPVR;

	// Token: 0x04001016 RID: 4118
	[HideInInspector]
	public EnviroCloudSettings.ReprojectionPixelSize currentReprojectionPixelSize;

	// Token: 0x04001017 RID: 4119
	private int reprojectionPixelSize;

	// Token: 0x04001018 RID: 4120
	private bool isFirstFrame;

	// Token: 0x04001019 RID: 4121
	private int subFrameNumber;

	// Token: 0x0400101A RID: 4122
	private int[] frameList;

	// Token: 0x0400101B RID: 4123
	private int renderingCounter;

	// Token: 0x0400101C RID: 4124
	private int subFrameWidth;

	// Token: 0x0400101D RID: 4125
	private int subFrameHeight;

	// Token: 0x0400101E RID: 4126
	private int frameWidth;

	// Token: 0x0400101F RID: 4127
	private int frameHeight;

	// Token: 0x04001020 RID: 4128
	private bool textureDimensionChanged;

	// Token: 0x04001022 RID: 4130
	private static Mesh _pointLightMesh;

	// Token: 0x04001023 RID: 4131
	private static Mesh _spotLightMesh;

	// Token: 0x04001024 RID: 4132
	private static Material _lightMaterial;

	// Token: 0x04001025 RID: 4133
	private CommandBuffer _preLightPass;

	// Token: 0x04001026 RID: 4134
	public CommandBuffer _afterLightPass;

	// Token: 0x04001027 RID: 4135
	private Matrix4x4 _viewProj;

	// Token: 0x04001028 RID: 4136
	private Matrix4x4 _viewProjSP;

	// Token: 0x04001029 RID: 4137
	[HideInInspector]
	public Material _volumeRenderingMaterial;

	// Token: 0x0400102A RID: 4138
	private Material _bilateralBlurMaterial;

	// Token: 0x0400102B RID: 4139
	private RenderTexture _volumeLightTexture;

	// Token: 0x0400102C RID: 4140
	private RenderTexture _halfVolumeLightTexture;

	// Token: 0x0400102D RID: 4141
	private RenderTexture _quarterVolumeLightTexture;

	// Token: 0x0400102E RID: 4142
	private static Texture _defaultSpotCookie;

	// Token: 0x0400102F RID: 4143
	private RenderTexture _halfDepthBuffer;

	// Token: 0x04001030 RID: 4144
	private RenderTexture _quarterDepthBuffer;

	// Token: 0x04001031 RID: 4145
	private Texture2D _ditheringTexture;

	// Token: 0x04001032 RID: 4146
	private Texture2D blackTexture;

	// Token: 0x04001033 RID: 4147
	private Texture3D _noiseTexture;

	// Token: 0x04001034 RID: 4148
	[HideInInspector]
	public EnviroSkyRendering.VolumtericResolution Resolution = EnviroSkyRendering.VolumtericResolution.Quarter;

	// Token: 0x04001035 RID: 4149
	private EnviroSkyRendering.VolumtericResolution _currentResolution = EnviroSkyRendering.VolumtericResolution.Quarter;

	// Token: 0x04001036 RID: 4150
	[HideInInspector]
	public Texture DefaultSpotCookie;

	// Token: 0x04001037 RID: 4151
	private Material _material;

	// Token: 0x04001038 RID: 4152
	[HideInInspector]
	public bool simpleFog;

	// Token: 0x04001039 RID: 4153
	private bool currentSimpleFog;

	// Token: 0x0400103A RID: 4154
	[HideInInspector]
	public bool volumeLighting = true;

	// Token: 0x0400103B RID: 4155
	[HideInInspector]
	public bool dirVolumeLighting = true;

	// Token: 0x0400103C RID: 4156
	[HideInInspector]
	public bool distanceFog = true;

	// Token: 0x0400103D RID: 4157
	[HideInInspector]
	public bool useRadialDistance;

	// Token: 0x0400103E RID: 4158
	[HideInInspector]
	public bool heightFog = true;

	// Token: 0x0400103F RID: 4159
	[HideInInspector]
	public float height = 1f;

	// Token: 0x04001040 RID: 4160
	[Range(0.001f, 10f)]
	[HideInInspector]
	public float heightDensity = 2f;

	// Token: 0x04001041 RID: 4161
	[HideInInspector]
	public float startDistance;

	// Token: 0x02000359 RID: 857
	public enum VolumtericResolution
	{
		// Token: 0x04001043 RID: 4163
		Full,
		// Token: 0x04001044 RID: 4164
		Half,
		// Token: 0x04001045 RID: 4165
		Quarter
	}
}

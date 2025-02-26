using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AIProject;
using Manager;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200034D RID: 845
[ExecuteInEditMode]
public class EnviroSky : MonoBehaviour
{
	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0004793C File Offset: 0x00045D3C
	public static EnviroSky instance
	{
		get
		{
			if (EnviroSky._instance == null)
			{
				EnviroSky._instance = UnityEngine.Object.FindObjectOfType<EnviroSky>();
			}
			return EnviroSky._instance;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00047960 File Offset: 0x00045D60
	private float OrbitRadius
	{
		get
		{
			return this.DomeTransform.localScale.x;
		}
	}

	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06000E74 RID: 3700 RVA: 0x00047980 File Offset: 0x00045D80
	// (remove) Token: 0x06000E75 RID: 3701 RVA: 0x000479B8 File Offset: 0x00045DB8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.HourPassed OnHourPassed;

	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06000E76 RID: 3702 RVA: 0x000479F0 File Offset: 0x00045DF0
	// (remove) Token: 0x06000E77 RID: 3703 RVA: 0x00047A28 File Offset: 0x00045E28
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.DayPassed OnDayPassed;

	// Token: 0x14000047 RID: 71
	// (add) Token: 0x06000E78 RID: 3704 RVA: 0x00047A60 File Offset: 0x00045E60
	// (remove) Token: 0x06000E79 RID: 3705 RVA: 0x00047A98 File Offset: 0x00045E98
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.YearPassed OnYearPassed;

	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06000E7A RID: 3706 RVA: 0x00047AD0 File Offset: 0x00045ED0
	// (remove) Token: 0x06000E7B RID: 3707 RVA: 0x00047B08 File Offset: 0x00045F08
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.WeatherChanged OnWeatherChanged;

	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06000E7C RID: 3708 RVA: 0x00047B40 File Offset: 0x00045F40
	// (remove) Token: 0x06000E7D RID: 3709 RVA: 0x00047B78 File Offset: 0x00045F78
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.ZoneWeatherChanged OnZoneWeatherChanged;

	// Token: 0x1400004A RID: 74
	// (add) Token: 0x06000E7E RID: 3710 RVA: 0x00047BB0 File Offset: 0x00045FB0
	// (remove) Token: 0x06000E7F RID: 3711 RVA: 0x00047BE8 File Offset: 0x00045FE8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.SeasonChanged OnSeasonChanged;

	// Token: 0x1400004B RID: 75
	// (add) Token: 0x06000E80 RID: 3712 RVA: 0x00047C20 File Offset: 0x00046020
	// (remove) Token: 0x06000E81 RID: 3713 RVA: 0x00047C58 File Offset: 0x00046058
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.isNightE OnNightTime;

	// Token: 0x1400004C RID: 76
	// (add) Token: 0x06000E82 RID: 3714 RVA: 0x00047C90 File Offset: 0x00046090
	// (remove) Token: 0x06000E83 RID: 3715 RVA: 0x00047CC8 File Offset: 0x000460C8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.isDay OnDayTime;

	// Token: 0x1400004D RID: 77
	// (add) Token: 0x06000E84 RID: 3716 RVA: 0x00047D00 File Offset: 0x00046100
	// (remove) Token: 0x06000E85 RID: 3717 RVA: 0x00047D38 File Offset: 0x00046138
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EnviroSky.ZoneChanged OnZoneChanged;

	// Token: 0x06000E86 RID: 3718 RVA: 0x00047D6E File Offset: 0x0004616E
	public virtual void NotifyHourPassed()
	{
		if (this.OnHourPassed != null)
		{
			this.OnHourPassed();
		}
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00047D86 File Offset: 0x00046186
	public virtual void NotifyDayPassed()
	{
		if (this.OnDayPassed != null)
		{
			this.OnDayPassed();
		}
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00047D9E File Offset: 0x0004619E
	public virtual void NotifyYearPassed()
	{
		if (this.OnYearPassed != null)
		{
			this.OnYearPassed();
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00047DB6 File Offset: 0x000461B6
	public virtual void NotifyWeatherChanged(EnviroWeatherPreset type)
	{
		if (this.OnWeatherChanged != null)
		{
			this.OnWeatherChanged(type);
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00047DCF File Offset: 0x000461CF
	public virtual void NotifyZoneWeatherChanged(EnviroWeatherPreset type, EnviroZone zone)
	{
		if (this.OnZoneWeatherChanged != null)
		{
			this.OnZoneWeatherChanged(type, zone);
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x00047DE9 File Offset: 0x000461E9
	public virtual void NotifySeasonChanged(EnviroSeasons.Seasons season)
	{
		if (this.OnSeasonChanged != null)
		{
			this.OnSeasonChanged(season);
		}
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x00047E02 File Offset: 0x00046202
	public virtual void NotifyIsNight()
	{
		if (this.OnNightTime != null)
		{
			this.OnNightTime();
		}
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00047E1A File Offset: 0x0004621A
	public virtual void NotifyIsDay()
	{
		if (this.OnDayTime != null)
		{
			this.OnDayTime();
		}
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00047E32 File Offset: 0x00046232
	public virtual void NotifyZoneChanged(EnviroZone zone)
	{
		if (this.OnZoneChanged != null)
		{
			this.OnZoneChanged(zone);
		}
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x00047E4C File Offset: 0x0004624C
	private void Start()
	{
		this.SetTime(this.GameTime.Years, this.GameTime.Days, this.GameTime.Hours, this.GameTime.Minutes, this.GameTime.Seconds);
		this.lastHourUpdate = (float)Mathf.RoundToInt(this.internalHour);
		this.currentTimeInHours = this.GetInHours(this.internalHour, (float)this.GameTime.Days, (float)this.GameTime.Years);
		this.Weather.weatherFullyChanged = false;
		this.thunder = 0f;
		this.lastCloudsQuality = this.cloudsSettings.cloudsQuality;
		if (Application.isPlaying && this.dontDestroy)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		if (this.weatherMapMat == null)
		{
			this.weatherMapMat = new Material(Shader.Find("Enviro/WeatherMap"));
		}
		if (this.profileLoaded)
		{
			base.InvokeRepeating("UpdateEnviroment", 0f, this.qualitySettings.UpdateInterval);
			this.CreateEffects();
			if (this.PlayerCamera != null && this.Player != null && !this.AssignInRuntime && this.profile != null)
			{
				this.Init();
			}
		}
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00047FB0 File Offset: 0x000463B0
	private void OnEnable()
	{
		this.DomeTransform = base.transform;
		this.Weather.currentActiveWeatherPreset = this.Weather.zones[0].currentActiveZoneWeatherPreset;
		this.Weather.lastActiveWeatherPreset = this.Weather.currentActiveWeatherPreset;
		if (this.profile == null)
		{
			return;
		}
		if (!this.profileLoaded)
		{
			this.ApplyProfile(this.profile);
		}
		this.PreInit();
		if (this.AssignInRuntime)
		{
			this.started = false;
		}
		else if (this.PlayerCamera != null && this.Player != null)
		{
			this.Init();
		}
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00048070 File Offset: 0x00046470
	public void ApplyProfile(EnviroProfile p)
	{
		this.profile = p;
		this.lightSettings = JsonUtility.FromJson<EnviroLightSettings>(JsonUtility.ToJson(p.lightSettings));
		this.volumeLightSettings = JsonUtility.FromJson<EnviroVolumeLightingSettings>(JsonUtility.ToJson(p.volumeLightSettings));
		this.skySettings = JsonUtility.FromJson<EnviroSkySettings>(JsonUtility.ToJson(p.skySettings));
		this.cloudsSettings = JsonUtility.FromJson<EnviroCloudSettings>(JsonUtility.ToJson(p.cloudsSettings));
		this.weatherSettings = JsonUtility.FromJson<EnviroWeatherSettings>(JsonUtility.ToJson(p.weatherSettings));
		this.fogSettings = JsonUtility.FromJson<EnviroFogSettings>(JsonUtility.ToJson(p.fogSettings));
		this.lightshaftsSettings = JsonUtility.FromJson<EnviroLightShaftsSettings>(JsonUtility.ToJson(p.lightshaftsSettings));
		this.audioSettings = JsonUtility.FromJson<EnviroAudioSettings>(JsonUtility.ToJson(p.audioSettings));
		this.satelliteSettings = JsonUtility.FromJson<EnviroSatellitesSettings>(JsonUtility.ToJson(p.satelliteSettings));
		this.qualitySettings = JsonUtility.FromJson<EnviroQualitySettings>(JsonUtility.ToJson(p.qualitySettings));
		this.seasonsSettings = JsonUtility.FromJson<EnviroSeasonSettings>(JsonUtility.ToJson(p.seasonsSettings));
		this.profileLoaded = true;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00048180 File Offset: 0x00046580
	public void SaveProfile()
	{
		this.profile.lightSettings = JsonUtility.FromJson<EnviroLightSettings>(JsonUtility.ToJson(this.lightSettings));
		this.profile.volumeLightSettings = JsonUtility.FromJson<EnviroVolumeLightingSettings>(JsonUtility.ToJson(this.volumeLightSettings));
		this.profile.skySettings = JsonUtility.FromJson<EnviroSkySettings>(JsonUtility.ToJson(this.skySettings));
		this.profile.cloudsSettings = JsonUtility.FromJson<EnviroCloudSettings>(JsonUtility.ToJson(this.cloudsSettings));
		this.profile.weatherSettings = JsonUtility.FromJson<EnviroWeatherSettings>(JsonUtility.ToJson(this.weatherSettings));
		this.profile.fogSettings = JsonUtility.FromJson<EnviroFogSettings>(JsonUtility.ToJson(this.fogSettings));
		this.profile.lightshaftsSettings = JsonUtility.FromJson<EnviroLightShaftsSettings>(JsonUtility.ToJson(this.lightshaftsSettings));
		this.profile.audioSettings = JsonUtility.FromJson<EnviroAudioSettings>(JsonUtility.ToJson(this.audioSettings));
		this.profile.satelliteSettings = JsonUtility.FromJson<EnviroSatellitesSettings>(JsonUtility.ToJson(this.satelliteSettings));
		this.profile.qualitySettings = JsonUtility.FromJson<EnviroQualitySettings>(JsonUtility.ToJson(this.qualitySettings));
		this.profile.seasonsSettings = JsonUtility.FromJson<EnviroSeasonSettings>(JsonUtility.ToJson(this.seasonsSettings));
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x000482B6 File Offset: 0x000466B6
	public void ReInit()
	{
		this.OnEnable();
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x000482C0 File Offset: 0x000466C0
	private void PreInit()
	{
		if (this.GameTime.solarTime < 0.45f)
		{
			this.isNight = true;
		}
		else
		{
			this.isNight = false;
		}
		if (this.serverMode)
		{
			return;
		}
		this.CheckSatellites();
		RenderSettings.fogMode = this.fogSettings.Fogmode;
		this.SetupSkybox();
		RenderSettings.ambientMode = this.lightSettings.ambientMode;
		RenderSettings.fogDensity = 0f;
		RenderSettings.fogStartDistance = 0f;
		RenderSettings.fogEndDistance = 1000f;
		this.Components.GlobalReflectionProbe.size = base.transform.localScale;
		this.Components.GlobalReflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
		if (this.Components.Sun)
		{
			this.SunTransform = this.Components.Sun.transform;
		}
		if (this.Components.Moon)
		{
			this.MoonTransform = this.Components.Moon.transform;
			this.MoonRenderer = this.Components.Moon.GetComponent<Renderer>();
			if (this.MoonRenderer == null)
			{
				this.MoonRenderer = this.Components.Moon.AddComponent<MeshRenderer>();
			}
			this.MoonRenderer.shadowCastingMode = ShadowCastingMode.Off;
			this.MoonRenderer.receiveShadows = false;
			if (this.MoonRenderer.sharedMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.MoonRenderer.sharedMaterial);
			}
			if (this.skySettings.moonPhaseMode == EnviroSkySettings.MoonPhases.Realistic)
			{
				this.MoonShader = new Material(Shader.Find("Enviro/MoonShader"));
			}
			else
			{
				this.MoonShader = new Material(Shader.Find("Enviro/MoonShaderPhased"));
			}
			this.MoonShader.SetTexture("_MainTex", this.skySettings.moonTexture);
			this.MoonRenderer.sharedMaterial = this.MoonShader;
			this.customMoonPhase = this.skySettings.startMoonPhase;
		}
		if (this.Components.cloudsShadowPlane != null)
		{
			UnityEngine.Object.DestroyImmediate(this.Components.cloudsShadowPlane);
		}
		if (this.weatherMap == null)
		{
			this.weatherMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.Default);
			this.weatherMap.wrapMode = TextureWrapMode.Repeat;
		}
		if (this.cloudShadows != null && this.weatherMap != null)
		{
			this.cloudShadows.SetTexture("_MainTex", this.weatherMap);
		}
		if (this.Components.DirectLight)
		{
			if (this.Components.DirectLight.name == "Direct Lght")
			{
				UnityEngine.Object.DestroyImmediate(this.Components.DirectLight.gameObject);
				this.Components.DirectLight = this.CreateDirectionalLight();
			}
			this.MainLight = this.Components.DirectLight.GetComponent<Light>();
			if (this.directVolumeLight == null)
			{
				this.directVolumeLight = this.Components.DirectLight.GetComponent<EnviroVolumeLight>();
			}
			if (this.directVolumeLight == null)
			{
				this.directVolumeLight = this.Components.DirectLight.gameObject.AddComponent<EnviroVolumeLight>();
			}
			if (this.dontDestroy && Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(this.Components.DirectLight);
			}
		}
		else
		{
			GameObject gameObject = GameObject.Find("Enviro Directional Light");
			if (gameObject != null)
			{
				this.Components.DirectLight = gameObject.transform;
			}
			else
			{
				this.Components.DirectLight = this.CreateDirectionalLight();
			}
			this.MainLight = this.Components.DirectLight.GetComponent<Light>();
			if (this.directVolumeLight == null)
			{
				this.directVolumeLight = this.Components.DirectLight.GetComponent<EnviroVolumeLight>();
			}
			if (this.directVolumeLight == null)
			{
				this.directVolumeLight = this.Components.DirectLight.gameObject.AddComponent<EnviroVolumeLight>();
			}
			if (this.dontDestroy && Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(this.Components.DirectLight);
			}
		}
		if (this.cloudShadowMap != null)
		{
			UnityEngine.Object.DestroyImmediate(this.cloudShadowMap);
		}
		this.cloudShadowMap = new RenderTexture(2048, 2048, 0, RenderTextureFormat.Default);
		this.cloudShadowMap.wrapMode = TextureWrapMode.Repeat;
		if (this.cloudShadowMat != null)
		{
			UnityEngine.Object.DestroyImmediate(this.cloudShadowMat);
		}
		this.cloudShadowMat = new Material(Shader.Find("Enviro/ShadowCookie"));
		if (this.cloudsSettings.shadowIntensity > 0f)
		{
			Graphics.Blit(this.weatherMap, this.cloudShadowMap, this.cloudShadowMat);
			this.MainLight.cookie = this.cloudShadowMap;
			this.MainLight.cookieSize = 10000f;
		}
		else
		{
			this.MainLight.cookie = null;
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x000487E8 File Offset: 0x00046BE8
	private void SetupSkybox()
	{
		if (this.skySettings.skyboxMode == EnviroSkySettings.SkyboxModi.Default)
		{
			if (this.skyMat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.skyMat);
			}
			if (this.cloudsMode == EnviroSky.EnviroCloudsMode.None || this.cloudsMode == EnviroSky.EnviroCloudsMode.Volume)
			{
				this.skyMat = new Material(Shader.Find("Enviro/Skybox"));
			}
			else
			{
				this.skyMat = new Material(Shader.Find("Enviro/SkyboxFlatClouds"));
			}
			if (this.skySettings.starsCubeMap != null)
			{
				this.skyMat.SetTexture("_Stars", this.skySettings.starsCubeMap);
			}
			if (this.skySettings.galaxyCubeMap != null)
			{
				this.skyMat.SetTexture("_Galaxy", this.skySettings.galaxyCubeMap);
			}
			RenderSettings.skybox = this.skyMat;
		}
		else if (this.skySettings.skyboxMode == EnviroSkySettings.SkyboxModi.CustomSkybox)
		{
			if (this.skySettings.customSkyboxMaterial != null)
			{
				RenderSettings.skybox = this.skySettings.customSkyboxMaterial;
			}
			this.skyMat = this.skySettings.customSkyboxMaterial;
		}
		this.lastCloudsMode = this.cloudsMode;
		if (this.lightSettings.ambientMode == AmbientMode.Skybox)
		{
			base.StartCoroutine(this.UpdateAmbientLightWithDelay());
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0004894C File Offset: 0x00046D4C
	private IEnumerator UpdateAmbientLightWithDelay()
	{
		yield return 0;
		DynamicGI.UpdateEnvironment();
		yield break;
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00048960 File Offset: 0x00046D60
	private void Init()
	{
		if (this.profile == null)
		{
			return;
		}
		if (this.serverMode)
		{
			this.started = true;
			return;
		}
		this.InitImageEffects();
		if (this.PlayerCamera != null)
		{
			if (this.setCameraClearFlags)
			{
				this.PlayerCamera.clearFlags = CameraClearFlags.Skybox;
			}
			if (this.PlayerCamera.actualRenderingPath == RenderingPath.DeferredShading)
			{
				this.SetCameraHDR(this.PlayerCamera, true);
			}
			else
			{
				this.SetCameraHDR(this.PlayerCamera, this.HDR);
			}
			this.Components.GlobalReflectionProbe.farClipPlane = this.PlayerCamera.farClipPlane;
			if (this.moonCamera == null)
			{
				this.CreateMoonCamera();
			}
			else
			{
				this.moonCamera.cullingMask = 1 << this.moonRenderingLayer;
				this.moonCamera.farClipPlane = this.PlayerCamera.farClipPlane * 0.5f;
				Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != this.moonCamera)
					{
						array[i].cullingMask &= ~(1 << this.moonRenderingLayer);
					}
				}
				this.moonCamera.transform.SetParent(this.Components.Moon.transform, false);
				this.moonCamera.transform.localPosition = new Vector3(0f, 0f, 1.5f);
				this.moonCamera.transform.localEulerAngles = new Vector3(-180f, 0f, 45f);
				this.moonCamera.transform.localScale = Vector3.one;
				this.moonCamera.enabled = false;
			}
		}
		this.CreateMoonTexture();
		this.Components.Moon.layer = this.moonRenderingLayer;
		UnityEngine.Object.DestroyImmediate(GameObject.Find("Enviro Cameras"));
		if (this.satelliteSettings.additionalSatellites.Count > 0)
		{
			this.InitSatCamera();
		}
		this.started = true;
		if (this.MoonShader != null)
		{
			this.MoonShader.SetFloat("_Phase", this.customMoonPhase);
			this.MoonShader.SetColor("_Color", this.skySettings.moonColor);
			this.MoonShader.SetFloat("_Brightness", this.skySettings.moonBrightness * (1f - this.GameTime.solarTime));
		}
		if (this.moonCamera != null)
		{
			this.moonCamera.Render();
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00048C09 File Offset: 0x00047009
	public void SetCameraHDR(Camera cam, bool hdr)
	{
		cam.allowHDR = hdr;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x00048C12 File Offset: 0x00047012
	public bool GetCameraHDR(Camera cam)
	{
		return cam.allowHDR;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00048C1C File Offset: 0x0004701C
	private Transform CreateDirectionalLight()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Enviro Directional Light";
		gameObject.transform.parent = base.transform;
		gameObject.transform.parent = null;
		Light light = gameObject.AddComponent<Light>();
		light.type = LightType.Directional;
		light.shadows = LightShadows.Soft;
		return gameObject.transform;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00048C74 File Offset: 0x00047074
	private void InitImageEffects()
	{
		this.EnviroSkyRender = this.PlayerCamera.gameObject.GetComponent<EnviroSkyRendering>();
		if (this.EnviroSkyRender == null)
		{
			this.EnviroSkyRender = this.PlayerCamera.gameObject.AddComponent<EnviroSkyRendering>();
		}
		EnviroLightShafts[] components = this.PlayerCamera.gameObject.GetComponents<EnviroLightShafts>();
		if (components.Length > 0)
		{
			this.lightShaftsScriptSun = components[0];
		}
		if (this.lightShaftsScriptSun != null)
		{
			UnityEngine.Object.DestroyImmediate(this.lightShaftsScriptSun.sunShaftsMaterial);
			UnityEngine.Object.DestroyImmediate(this.lightShaftsScriptSun.simpleClearMaterial);
			this.lightShaftsScriptSun.sunShaftsMaterial = new Material(Shader.Find("Enviro/Effects/LightShafts"));
			this.lightShaftsScriptSun.sunShaftsShader = this.lightShaftsScriptSun.sunShaftsMaterial.shader;
			this.lightShaftsScriptSun.simpleClearMaterial = new Material(Shader.Find("Enviro/Effects/ClearLightShafts"));
			this.lightShaftsScriptSun.simpleClearShader = this.lightShaftsScriptSun.simpleClearMaterial.shader;
		}
		else
		{
			this.lightShaftsScriptSun = this.PlayerCamera.gameObject.AddComponent<EnviroLightShafts>();
			this.lightShaftsScriptSun.sunShaftsMaterial = new Material(Shader.Find("Enviro/Effects/LightShafts"));
			this.lightShaftsScriptSun.sunShaftsShader = this.lightShaftsScriptSun.sunShaftsMaterial.shader;
			this.lightShaftsScriptSun.simpleClearMaterial = new Material(Shader.Find("Enviro/Effects/ClearLightShafts"));
			this.lightShaftsScriptSun.simpleClearShader = this.lightShaftsScriptSun.simpleClearMaterial.shader;
		}
		if (components.Length > 1)
		{
			this.lightShaftsScriptMoon = components[1];
		}
		if (this.lightShaftsScriptMoon != null)
		{
			UnityEngine.Object.DestroyImmediate(this.lightShaftsScriptMoon.sunShaftsMaterial);
			UnityEngine.Object.DestroyImmediate(this.lightShaftsScriptMoon.simpleClearMaterial);
			this.lightShaftsScriptMoon.sunShaftsMaterial = new Material(Shader.Find("Enviro/Effects/LightShafts"));
			this.lightShaftsScriptMoon.sunShaftsShader = this.lightShaftsScriptMoon.sunShaftsMaterial.shader;
			this.lightShaftsScriptMoon.simpleClearMaterial = new Material(Shader.Find("Enviro/Effects/ClearLightShafts"));
			this.lightShaftsScriptMoon.simpleClearShader = this.lightShaftsScriptMoon.simpleClearMaterial.shader;
		}
		else
		{
			this.lightShaftsScriptMoon = this.PlayerCamera.gameObject.AddComponent<EnviroLightShafts>();
			this.lightShaftsScriptMoon.sunShaftsMaterial = new Material(Shader.Find("Enviro/Effects/LightShafts"));
			this.lightShaftsScriptMoon.sunShaftsShader = this.lightShaftsScriptMoon.sunShaftsMaterial.shader;
			this.lightShaftsScriptMoon.simpleClearMaterial = new Material(Shader.Find("Enviro/Effects/ClearLightShafts"));
			this.lightShaftsScriptMoon.simpleClearShader = this.lightShaftsScriptMoon.simpleClearMaterial.shader;
		}
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00048F34 File Offset: 0x00047334
	public void InitSatCamera()
	{
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].cullingMask &= ~(1 << this.satelliteRenderingLayer);
		}
		UnityEngine.Object.DestroyImmediate(GameObject.Find("Enviro Sat Camera"));
		this.satCamera = new GameObject
		{
			name = "Enviro Sat Camera",
			transform = 
			{
				position = this.PlayerCamera.transform.position,
				rotation = this.PlayerCamera.transform.rotation
			},
			hideFlags = HideFlags.DontSave
		}.AddComponent<Camera>();
		this.satCamera.farClipPlane = this.PlayerCamera.farClipPlane;
		this.satCamera.nearClipPlane = this.PlayerCamera.nearClipPlane;
		this.satCamera.aspect = this.PlayerCamera.aspect;
		this.SetCameraHDR(this.satCamera, this.HDR);
		this.satCamera.useOcclusionCulling = false;
		this.satCamera.renderingPath = RenderingPath.Forward;
		this.satCamera.fieldOfView = this.PlayerCamera.fieldOfView;
		this.satCamera.clearFlags = CameraClearFlags.Color;
		this.satCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
		this.satCamera.cullingMask = 1 << this.satelliteRenderingLayer;
		this.satCamera.depth = this.PlayerCamera.depth + 1f;
		this.satCamera.enabled = true;
		this.PlayerCamera.cullingMask &= ~(1 << this.satelliteRenderingLayer);
		RenderTextureFormat format = (!this.GetCameraHDR(this.satCamera)) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
		this.satRenderTarget = new RenderTexture(Screen.currentResolution.width, Screen.currentResolution.height, 16, format);
		this.satCamera.targetTexture = this.satRenderTarget;
		this.satCamera.enabled = false;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00049150 File Offset: 0x00047550
	private void CreateMoonCamera()
	{
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].cullingMask &= ~(1 << this.moonRenderingLayer);
		}
		UnityEngine.Object.DestroyImmediate(GameObject.Find("Enviro Moon Render Cam"));
		GameObject gameObject = new GameObject();
		gameObject.name = "Enviro Moon Render Cam";
		gameObject.transform.SetParent(this.Components.Moon.transform, false);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 1.5f);
		gameObject.transform.localEulerAngles = new Vector3(-180f, 0f, 45f);
		gameObject.transform.localScale = Vector3.one;
		this.moonCamera = gameObject.AddComponent<Camera>();
		this.moonCamera.farClipPlane = this.PlayerCamera.farClipPlane * 0.5f;
		this.moonCamera.nearClipPlane = 0.01f;
		this.moonCamera.aspect = this.PlayerCamera.aspect;
		this.SetCameraHDR(this.moonCamera, this.HDR);
		this.moonCamera.renderingPath = RenderingPath.Forward;
		this.moonCamera.fieldOfView = this.PlayerCamera.fieldOfView;
		this.moonCamera.clearFlags = CameraClearFlags.Color;
		this.moonCamera.backgroundColor = Color.black;
		this.moonCamera.cullingMask = 1 << this.moonRenderingLayer;
		this.PlayerCamera.cullingMask &= ~(1 << this.moonRenderingLayer);
		this.moonCamera.enabled = false;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000492FC File Offset: 0x000476FC
	private void RenderMoon()
	{
		if (this.currentTimeInHours > this.lastMoonUpdate + 0.1 || (this.currentTimeInHours < this.lastMoonUpdate - 0.1 && this.skySettings.renderMoon))
		{
			this.moonCamera.Render();
			this.lastMoonUpdate = this.currentTimeInHours;
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00049368 File Offset: 0x00047768
	private void CreateMoonTexture()
	{
		if (this.moonRenderTarget != null && this.moonCamera != null)
		{
			this.moonCamera.targetTexture = null;
			UnityEngine.Object.DestroyImmediate(this.moonRenderTarget);
		}
		RenderTextureFormat format = (!this.GetCameraHDR(this.moonCamera)) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
		this.moonRenderTarget = new RenderTexture(512, 512, 0, format);
		this.moonCamera.targetTexture = this.moonRenderTarget;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x000493F0 File Offset: 0x000477F0
	public void CreateEffects()
	{
		this.EffectsHolder = GameObject.Find("Enviro Effects");
		if (this.EffectsHolder != null)
		{
			int childCount = this.EffectsHolder.transform.childCount;
			int num = childCount - 1;
			while (0 <= num)
			{
				UnityEngine.Object.DestroyImmediate(this.EffectsHolder.transform.GetChild(num).gameObject);
				num--;
			}
		}
		else
		{
			this.EffectsHolder = new GameObject("Enviro Effects");
			this.EffectsHolder.transform.parent = base.transform;
			this.EffectsHolder.transform.parent = null;
		}
		this.CreateWeatherEffectHolder();
		if (Application.isPlaying && this.dontDestroy)
		{
			UnityEngine.Object.DontDestroyOnLoad(this.EffectsHolder);
		}
		this.SetEffectsHolderPlace(this.EffectsHolder);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x000494CC File Offset: 0x000478CC
	public int RegisterMe(EnviroVegetationInstance me)
	{
		this.EnviroVegetationInstances.Add(me);
		return this.EnviroVegetationInstances.Count - 1;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x000494E7 File Offset: 0x000478E7
	public void ChangeSeason(EnviroSeasons.Seasons season)
	{
		this.Seasons.currentSeasons = season;
		this.NotifySeasonChanged(season);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000494FC File Offset: 0x000478FC
	private void UpdateSeason()
	{
		if (this.currentDay >= 0f && this.currentDay < this.seasonsSettings.SpringInDays)
		{
			this.Seasons.currentSeasons = EnviroSeasons.Seasons.Spring;
			if (this.Seasons.lastSeason != this.Seasons.currentSeasons)
			{
				this.NotifySeasonChanged(EnviroSeasons.Seasons.Spring);
			}
			this.Seasons.lastSeason = this.Seasons.currentSeasons;
		}
		else if (this.currentDay >= this.seasonsSettings.SpringInDays && this.currentDay < this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays)
		{
			this.Seasons.currentSeasons = EnviroSeasons.Seasons.Summer;
			if (this.Seasons.lastSeason != this.Seasons.currentSeasons)
			{
				this.NotifySeasonChanged(EnviroSeasons.Seasons.Summer);
			}
			this.Seasons.lastSeason = this.Seasons.currentSeasons;
		}
		else if (this.currentDay >= this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays && this.currentDay < this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays + this.seasonsSettings.AutumnInDays)
		{
			this.Seasons.currentSeasons = EnviroSeasons.Seasons.Autumn;
			if (this.Seasons.lastSeason != this.Seasons.currentSeasons)
			{
				this.NotifySeasonChanged(EnviroSeasons.Seasons.Autumn);
			}
			this.Seasons.lastSeason = this.Seasons.currentSeasons;
		}
		else if (this.currentDay >= this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays + this.seasonsSettings.AutumnInDays && this.currentDay <= this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays + this.seasonsSettings.AutumnInDays + this.seasonsSettings.WinterInDays)
		{
			this.Seasons.currentSeasons = EnviroSeasons.Seasons.Winter;
			if (this.Seasons.lastSeason != this.Seasons.currentSeasons)
			{
				this.NotifySeasonChanged(EnviroSeasons.Seasons.Winter);
			}
			this.Seasons.lastSeason = this.Seasons.currentSeasons;
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x00049740 File Offset: 0x00047B40
	private void UpdateEnviroment()
	{
		if (this.Seasons.calcSeasons)
		{
			this.UpdateSeason();
		}
		if (this.EnviroVegetationInstances.Count > 0)
		{
			for (int i = 0; i < this.EnviroVegetationInstances.Count; i++)
			{
				if (this.EnviroVegetationInstances[i] != null)
				{
					this.EnviroVegetationInstances[i].UpdateInstance();
				}
			}
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000497B8 File Offset: 0x00047BB8
	private void CreateSatellite(int id)
	{
		if (this.satelliteSettings.additionalSatellites[id].prefab == null)
		{
			return;
		}
		GameObject gameObject = new GameObject();
		gameObject.name = this.satelliteSettings.additionalSatellites[id].name;
		gameObject.transform.parent = this.Components.satellites;
		this.satellitesRotation.Add(gameObject);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.satelliteSettings.additionalSatellites[id].prefab, gameObject.transform);
		gameObject2.layer = this.satelliteRenderingLayer;
		this.satellites.Add(gameObject2);
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00049868 File Offset: 0x00047C68
	public void CheckSatellites()
	{
		this.satellites = new List<GameObject>();
		int childCount = this.Components.satellites.childCount;
		for (int i = childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.DestroyImmediate(this.Components.satellites.GetChild(i).gameObject);
		}
		this.satellites.Clear();
		this.satellitesRotation.Clear();
		for (int j = 0; j < this.satelliteSettings.additionalSatellites.Count; j++)
		{
			this.CreateSatellite(j);
		}
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00049900 File Offset: 0x00047D00
	private void CalculateSatPositions(float siderealTime)
	{
		for (int i = 0; i < this.satelliteSettings.additionalSatellites.Count; i++)
		{
			Quaternion quaternion = Quaternion.Euler(90f - this.GameTime.Latitude, this.GameTime.Longitude, 0f);
			quaternion *= Quaternion.Euler(this.satelliteSettings.additionalSatellites[i].yRot, siderealTime, this.satelliteSettings.additionalSatellites[i].xRot);
			if (this.satellites.Count >= i)
			{
				this.satellites[i].transform.localPosition = new Vector3(0f, this.satelliteSettings.additionalSatellites[i].orbit, 0f);
			}
			if (this.satellitesRotation.Count >= i)
			{
				this.satellitesRotation[i].transform.localRotation = quaternion;
			}
		}
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00049A04 File Offset: 0x00047E04
	private void UpdateCameraComponents()
	{
		if (this.EnviroSkyRender != null)
		{
			this.EnviroSkyRender.volumeLighting = this.volumeLighting;
			this.EnviroSkyRender.dirVolumeLighting = this.volumeLightSettings.dirVolumeLighting;
			this.EnviroSkyRender.simpleFog = this.fogSettings.useSimpleFog;
			this.EnviroSkyRender.distanceFog = this.fogSettings.distanceFog;
			this.EnviroSkyRender.heightFog = this.fogSettings.heightFog;
			this.EnviroSkyRender.height = this.fogSettings.height;
			this.EnviroSkyRender.heightDensity = this.fogSettings.heightDensity;
			this.EnviroSkyRender.useRadialDistance = this.fogSettings.useRadialDistance;
			this.EnviroSkyRender.startDistance = this.fogSettings.startDistance;
		}
		if (this.lightShaftsScriptSun != null)
		{
			this.lightShaftsScriptSun.resolution = this.lightshaftsSettings.resolution;
			this.lightShaftsScriptSun.screenBlendMode = this.lightshaftsSettings.screenBlendMode;
			this.lightShaftsScriptSun.useDepthTexture = this.lightshaftsSettings.useDepthTexture;
			this.lightShaftsScriptSun.sunThreshold = this.lightshaftsSettings.thresholdColorSun.Evaluate(this.GameTime.solarTime);
			this.lightShaftsScriptSun.sunShaftBlurRadius = this.lightshaftsSettings.blurRadius;
			this.lightShaftsScriptSun.sunShaftIntensity = this.lightshaftsSettings.intensity;
			this.lightShaftsScriptSun.maxRadius = this.lightshaftsSettings.maxRadius;
			this.lightShaftsScriptSun.sunColor = this.lightshaftsSettings.lightShaftsColorSun.Evaluate(this.GameTime.solarTime);
			this.lightShaftsScriptSun.sunTransform = this.Components.Sun.transform;
			if (this.LightShafts.sunLightShafts)
			{
				this.lightShaftsScriptSun.enabled = true;
			}
			else
			{
				this.lightShaftsScriptSun.enabled = false;
			}
		}
		if (this.lightShaftsScriptMoon != null)
		{
			this.lightShaftsScriptMoon.resolution = this.lightshaftsSettings.resolution;
			this.lightShaftsScriptMoon.screenBlendMode = this.lightshaftsSettings.screenBlendMode;
			this.lightShaftsScriptMoon.useDepthTexture = this.lightshaftsSettings.useDepthTexture;
			this.lightShaftsScriptMoon.sunThreshold = this.lightshaftsSettings.thresholdColorMoon.Evaluate(this.GameTime.lunarTime);
			this.lightShaftsScriptMoon.sunShaftBlurRadius = this.lightshaftsSettings.blurRadius;
			this.lightShaftsScriptMoon.sunShaftIntensity = Mathf.Clamp(this.lightshaftsSettings.intensity - this.GameTime.solarTime, 0f, 100f);
			this.lightShaftsScriptMoon.maxRadius = this.lightshaftsSettings.maxRadius;
			this.lightShaftsScriptMoon.sunColor = this.lightshaftsSettings.lightShaftsColorMoon.Evaluate(this.GameTime.lunarTime);
			this.lightShaftsScriptMoon.sunTransform = this.Components.Moon.transform;
			if (this.LightShafts.moonLightShafts)
			{
				this.lightShaftsScriptMoon.enabled = true;
			}
			else
			{
				this.lightShaftsScriptMoon.enabled = false;
			}
		}
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00049D50 File Offset: 0x00048150
	private Vector3 CalculatePosition()
	{
		Vector3 result;
		result.x = this.Player.transform.position.x;
		result.z = this.Player.transform.position.z;
		result.y = this.Player.transform.position.y;
		return result;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00049DBC File Offset: 0x000481BC
	private void RenderFlatCloudsMap()
	{
		if (this.flatCloudsMat == null)
		{
			this.flatCloudsMat = new Material(Shader.Find("Enviro/FlatCloudMap"));
		}
		this.flatCloudsRenderTarget = RenderTexture.GetTemporary((int)((EnviroCloudSettings.FlatCloudResolution)512 * (this.cloudsSettings.flatCloudsResolution + 1)), (int)((EnviroCloudSettings.FlatCloudResolution)512 * (this.cloudsSettings.flatCloudsResolution + 1)), 0, RenderTextureFormat.DefaultHDR);
		this.flatCloudsRenderTarget.wrapMode = TextureWrapMode.Repeat;
		this.flatCloudsMat.SetVector("_CloudAnimation", this.cloudAnimNonScaled);
		this.flatCloudsMat.SetTexture("_NoiseTex", this.cloudsSettings.flatCloudsNoiseTexture);
		this.flatCloudsMat.SetFloat("_CloudScale", this.cloudsSettings.flatCloudsScale);
		this.flatCloudsMat.SetFloat("_Coverage", this.cloudsConfig.flatCoverage);
		this.flatCloudsMat.SetInt("noiseOctaves", this.cloudsSettings.flatCloudsNoiseOctaves);
		this.flatCloudsMat.SetFloat("_Softness", this.cloudsConfig.flatSoftness);
		this.flatCloudsMat.SetFloat("_Brightness", this.cloudsConfig.flatBrightness);
		this.flatCloudsMat.SetFloat("_MorphingSpeed", this.cloudsSettings.flatCloudsMorphingSpeed);
		Graphics.Blit(null, this.flatCloudsRenderTarget, this.flatCloudsMat);
		RenderTexture.ReleaseTemporary(this.flatCloudsRenderTarget);
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00049F24 File Offset: 0x00048324
	private void RenderWeatherMap()
	{
		if (this.cloudsSettings.customWeatherMap == null)
		{
			this.weatherMapMat.SetVector("_WindDir", this.cloudAnimNonScaled);
			this.weatherMapMat.SetFloat("_AnimSpeedScale", this.cloudsSettings.weatherAnimSpeedScale);
			this.weatherMapMat.SetInt("_Tiling", this.cloudsSettings.weatherMapTiling);
			this.weatherMapMat.SetVector("_Location", this.cloudsSettings.locationOffset);
			double value = (double)(EnviroSky.instance.cloudsConfig.coverage * this.cloudsSettings.globalCloudCoverage);
			this.weatherMapMat.SetFloat("_Coverage", (float)Math.Round(value, 4));
			Graphics.Blit(null, this.weatherMap, this.weatherMapMat);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0004A000 File Offset: 0x00048400
	private void RenderCloudMaps()
	{
		switch (this.cloudsMode)
		{
		case EnviroSky.EnviroCloudsMode.Both:
			this.RenderFlatCloudsMap();
			this.RenderWeatherMap();
			break;
		case EnviroSky.EnviroCloudsMode.Volume:
			this.RenderWeatherMap();
			break;
		case EnviroSky.EnviroCloudsMode.Flat:
			this.RenderFlatCloudsMap();
			this.RenderWeatherMap();
			break;
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0004A064 File Offset: 0x00048464
	private void Update()
	{
		if (this.profile == null)
		{
			return;
		}
		if (!this.started && !this.serverMode)
		{
			this.UpdateTime();
			this.UpdateSunAndMoonPosition();
			this.CalculateDirectLight();
			this.UpdateAmbientLight();
			this.UpdateReflections();
			this.RenderMoon();
			if (!this.AssignInRuntime || !(this.PlayerTag != string.Empty) || !(this.CameraTag != string.Empty) || !Application.isPlaying)
			{
				this.started = false;
				return;
			}
			GameObject gameObject = GameObject.FindGameObjectWithTag(this.PlayerTag);
			if (gameObject != null)
			{
				this.Player = gameObject;
			}
			for (int i = 0; i < Camera.allCameras.Length; i++)
			{
				if (Camera.allCameras[i].tag == this.CameraTag)
				{
					this.PlayerCamera = Camera.allCameras[i];
				}
			}
			if (!(this.Player != null) || !(this.PlayerCamera != null))
			{
				this.started = false;
				return;
			}
			this.Init();
			this.started = true;
		}
		this.UpdateTime();
		this.ValidateParameters();
		if (!this.serverMode)
		{
			if (this.lastCloudsMode != this.cloudsMode)
			{
				this.SetupSkybox();
			}
			this.RenderCloudMaps();
			this.UpdateCameraComponents();
			this.UpdateAmbientLight();
			this.UpdateReflections();
			this.UpdateWeather();
			this.UpdateCloudShadows();
			this.UpdateSkyRenderingComponent();
			this.RenderMoon();
			this.UpdateSunAndMoonPosition();
			this.CalculateDirectLight();
			this.CalculateSatPositions(this.LST);
			if (!this.isNight && this.GameTime.solarTime < 0.45f)
			{
				this.isNight = true;
				this.NotifyIsNight();
			}
			else if (this.isNight && this.GameTime.solarTime >= 0.45f)
			{
				this.isNight = false;
				this.NotifyIsDay();
			}
			if (this.lastCloudsQuality != this.cloudsSettings.cloudsQuality && (this.cloudsMode == EnviroSky.EnviroCloudsMode.Volume || this.cloudsMode == EnviroSky.EnviroCloudsMode.Both))
			{
				this.ChangeCloudsQuality(this.cloudsSettings.cloudsQuality);
			}
		}
		else
		{
			this.UpdateWeather();
			if (!this.isNight && this.GameTime.solarTime < 0.45f)
			{
				this.isNight = true;
				this.NotifyIsNight();
			}
			else if (this.isNight && this.GameTime.solarTime >= 0.45f)
			{
				this.isNight = false;
				this.NotifyIsDay();
			}
		}
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0004A324 File Offset: 0x00048724
	private void LateUpdate()
	{
		if (!this.serverMode && this.PlayerCamera != null && this.Player != null)
		{
			base.transform.position = this.Player.transform.position;
			base.transform.localScale = new Vector3(this.PlayerCamera.farClipPlane, this.PlayerCamera.farClipPlane, this.PlayerCamera.farClipPlane);
			this.SetEffectsHolderPlace(this.EffectsHolder);
		}
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x0004A3B8 File Offset: 0x000487B8
	private void SetEffectsHolderPlace(GameObject obj)
	{
		if (obj == null)
		{
			return;
		}
		if (this.Player == null)
		{
			obj.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
			return;
		}
		if (!Singleton<Manager.Resources>.IsInstance() || this.Player == this.PlayerCamera)
		{
			obj.transform.SetPositionAndRotation(this.Player.transform.position, this.Player.transform.rotation);
		}
		else
		{
			Vector3 enviroEffectOffset = Singleton<Manager.Resources>.Instance.LocomotionProfile.EnviroEffectOffset;
			Vector3 position = this.Player.transform.position;
			Vector3 position2 = this.PlayerCamera.transform.position;
			position.y = (position2.y = 0f);
			float y = Vector3.SignedAngle(Vector3.forward, position - position2, Vector3.up);
			Quaternion rotation = Quaternion.Euler(0f, y, 0f);
			obj.transform.SetPositionAndRotation(this.Player.transform.position + rotation * enviroEffectOffset, rotation);
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0004A4F4 File Offset: 0x000488F4
	private void UpdateCloudShadows()
	{
		if (this.cloudsSettings.shadowIntensity == 0f || this.cloudsMode == EnviroSky.EnviroCloudsMode.None || this.cloudsMode == EnviroSky.EnviroCloudsMode.Flat)
		{
			if (this.MainLight.cookie != null)
			{
				this.MainLight.cookie = null;
			}
		}
		else if (this.cloudsSettings.shadowIntensity > 0f)
		{
			this.cloudShadowMap.DiscardContents(true, true);
			this.cloudShadowMat.SetFloat("_shadowIntensity", this.cloudsSettings.shadowIntensity);
			if (this.cloudsMode == EnviroSky.EnviroCloudsMode.Volume || this.cloudsMode == EnviroSky.EnviroCloudsMode.Both)
			{
				this.cloudShadowMat.SetTexture("_MainTex", this.weatherMap);
				Graphics.Blit(this.weatherMap, this.cloudShadowMap, this.cloudShadowMat);
			}
			if (Application.isPlaying)
			{
				this.MainLight.cookie = this.cloudShadowMap;
			}
			else
			{
				this.MainLight.cookie = null;
			}
			this.MainLight.cookieSize = (float)this.cloudsSettings.shadowCookieSize;
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x0004A61C File Offset: 0x00048A1C
	public Vector3 BetaRay()
	{
		Vector3 vector = this.skySettings.waveLength * 1E-09f;
		Vector3 result;
		result.x = 8f * Mathf.Pow(3.1415927f, 3f) * Mathf.Pow(Mathf.Pow(1.0003f, 2f) - 1f, 2f) * 6.105f / (7.635E+25f * Mathf.Pow(vector.x, 4f) * 5.755f) * 2000f;
		result.y = 8f * Mathf.Pow(3.1415927f, 3f) * Mathf.Pow(Mathf.Pow(1.0003f, 2f) - 1f, 2f) * 6.105f / (7.635E+25f * Mathf.Pow(vector.y, 4f) * 5.755f) * 2000f;
		result.z = 8f * Mathf.Pow(3.1415927f, 3f) * Mathf.Pow(Mathf.Pow(1.0003f, 2f) - 1f, 2f) * 6.105f / (7.635E+25f * Mathf.Pow(vector.z, 4f) * 5.755f) * 2000f;
		return result;
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0004A774 File Offset: 0x00048B74
	public Vector3 BetaMie()
	{
		float num = 0.2f * this.skySettings.turbidity * 10f;
		Vector3 result;
		result.x = 434f * num * 3.1415927f * Mathf.Pow(6.2831855f / this.skySettings.waveLength.x, 2f) * this.K.x;
		result.y = 434f * num * 3.1415927f * Mathf.Pow(6.2831855f / this.skySettings.waveLength.y, 2f) * this.K.y;
		result.z = 434f * num * 3.1415927f * Mathf.Pow(6.2831855f / this.skySettings.waveLength.z, 2f) * this.K.z;
		result.x = Mathf.Pow(result.x, -1f);
		result.y = Mathf.Pow(result.y, -1f);
		result.z = Mathf.Pow(result.z, -1f);
		return result;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0004A8A8 File Offset: 0x00048CA8
	public Vector3 GetMieG()
	{
		return new Vector3(1f - this.skySettings.g * this.skySettings.g, 1f + this.skySettings.g * this.skySettings.g, 2f * this.skySettings.g);
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0004A908 File Offset: 0x00048D08
	public Vector3 GetMieGScene()
	{
		return new Vector3(1f - this.fogSettings.g * this.fogSettings.g, 1f + this.fogSettings.g * this.fogSettings.g, 2f * this.fogSettings.g);
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x0004A968 File Offset: 0x00048D68
	private void SetupShader(float setup)
	{
		if (this.skyMat != null)
		{
			this.skyMat.SetVector("_SunDir", -this.SunTransform.transform.forward);
			this.skyMat.SetVector("_MoonDir", this.Components.Moon.transform.forward);
			this.skyMat.SetColor("_MoonColor", this.skySettings.moonColor);
			this.skyMat.SetFloat("_MoonSize", this.skySettings.moonSize);
			this.skyMat.SetFloat("_MoonBrightness", this.skySettings.moonBrightness);
			if (this.skySettings.renderMoon)
			{
				this.skyMat.SetTexture("_MoonTex", this.moonRenderTarget);
			}
			else
			{
				this.skyMat.SetTexture("_MoonTex", null);
			}
			this.skyMat.SetColor("_scatteringColor", this.skySettings.scatteringColor.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetColor("_sunDiskColor", this.skySettings.sunDiskColor.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetColor("_weatherSkyMod", Color.Lerp(this.currentWeatherSkyMod, this.currentInteriorSkyboxMod, this.currentInteriorSkyboxMod.a));
			this.skyMat.SetColor("_weatherFogMod", Color.Lerp(this.currentWeatherFogMod, this.currentInteriorFogColorMod, this.currentInteriorFogColorMod.a));
			this.skyMat.SetVector("_Bm", this.BetaMie() * (this.skySettings.mie * this.Fog.scatteringStrenght));
			this.skyMat.SetVector("_Br", this.BetaRay() * this.skySettings.rayleigh);
			this.skyMat.SetVector("_mieG", this.GetMieG());
			this.skyMat.SetFloat("_SunIntensity", this.skySettings.sunIntensity);
			this.skyMat.SetFloat("_SunDiskSize", this.skySettings.sunDiskScale);
			this.skyMat.SetFloat("_SunDiskIntensity", this.skySettings.sunDiskIntensity);
			this.skyMat.SetFloat("_SunDiskSize", this.skySettings.sunDiskScale);
			this.skyMat.SetFloat("_Exposure", this.skySettings.skyExposure);
			this.skyMat.SetFloat("_SkyLuminance", this.skySettings.skyLuminence.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetFloat("_scatteringPower", this.skySettings.scatteringCurve.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetFloat("_SkyColorPower", this.skySettings.skyColorPower.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetFloat("_StarsIntensity", this.skySettings.starsIntensity.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetFloat("_GalaxyIntensity", this.skySettings.galaxyIntensity.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetColor("_moonGlowColor", this.skySettings.moonGlowColor);
			if (this.skySettings.blackGroundMode)
			{
				this.skyMat.SetInt("_blackGround", 1);
			}
			else
			{
				this.skyMat.SetInt("_blackGround", 0);
			}
			float value = (!this.HDR) ? 0f : 1f;
			this.skyMat.SetFloat("_hdr", value);
			this.skyMat.SetFloat("_moonGlowStrenght", this.skySettings.moonGlow.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetVector("_CloudAnimation", this.cloudAnim);
			if (this.cloudsSettings.cirrusCloudsTexture != null)
			{
				this.skyMat.SetTexture("_CloudMap", this.cloudsSettings.cirrusCloudsTexture);
			}
			this.skyMat.SetColor("_CloudColor", this.cloudsSettings.cirrusCloudsColor.Evaluate(this.GameTime.solarTime));
			this.skyMat.SetFloat("_CloudAltitude", this.cloudsSettings.cirrusCloudsAltitude);
			this.skyMat.SetFloat("_CloudAlpha", this.cloudsConfig.cirrusAlpha);
			this.skyMat.SetFloat("_CloudCoverage", this.cloudsConfig.cirrusCoverage);
			this.skyMat.SetFloat("_CloudColorPower", this.cloudsConfig.cirrusColorPow);
			if (this.flatCloudsRenderTarget != null)
			{
				this.skyMat.SetTexture("_Cloud1Map", this.flatCloudsRenderTarget);
				this.skyMat.SetColor("_Cloud1Color", this.cloudsSettings.flatCloudsColor.Evaluate(this.GameTime.solarTime));
				this.skyMat.SetFloat("_Cloud1Altitude", this.cloudsSettings.flatCloudsAltitude);
				this.skyMat.SetFloat("_Cloud1Alpha", this.cloudsConfig.flatAlpha);
				this.skyMat.SetFloat("_Cloud1ColorPower", this.cloudsConfig.flatColorPow);
			}
		}
		Shader.SetGlobalVector("_SunDir", -this.Components.Sun.transform.forward);
		Shader.SetGlobalVector("_MoonDir", -this.Components.Moon.transform.forward);
		Shader.SetGlobalColor("_scatteringColor", this.skySettings.scatteringColor.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalColor("_sunDiskColor", this.skySettings.sunDiskColor.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalColor("_weatherSkyMod", Color.Lerp(this.currentWeatherSkyMod, this.currentInteriorSkyboxMod, this.currentInteriorSkyboxMod.a));
		Shader.SetGlobalColor("_weatherFogMod", Color.Lerp(this.currentWeatherFogMod, this.currentInteriorFogColorMod, this.currentInteriorFogColorMod.a));
		Shader.SetGlobalFloat("_gameTime", Mathf.Clamp(1f - this.GameTime.solarTime, 0.5f, 1f));
		Shader.SetGlobalFloat("_SkyFogHeight", this.Fog.skyFogHeight);
		Shader.SetGlobalFloat("_scatteringStrenght", this.Fog.scatteringStrenght);
		Shader.SetGlobalFloat("_skyFogIntensity", this.fogSettings.skyFogIntensity);
		Shader.SetGlobalFloat("_SunBlocking", this.Fog.sunBlocking);
		Shader.SetGlobalVector("_EnviroParams", new Vector4(Mathf.Clamp(1f - this.GameTime.solarTime, 0.5f, 1f), (!this.fogSettings.distanceFog) ? 0f : 1f, (!this.fogSettings.heightFog) ? 0f : 1f, (!this.HDR) ? 0f : 1f));
		Shader.SetGlobalVector("_Bm", this.BetaMie() * (this.skySettings.mie * (this.Fog.scatteringStrenght * this.GameTime.solarTime)));
		Shader.SetGlobalVector("_BmScene", this.BetaMie() * (this.fogSettings.mie * (this.Fog.scatteringStrenght * this.GameTime.solarTime)));
		Shader.SetGlobalVector("_Br", this.BetaRay() * this.skySettings.rayleigh);
		Shader.SetGlobalVector("_mieG", this.GetMieG());
		Shader.SetGlobalVector("_mieGScene", this.GetMieGScene());
		Shader.SetGlobalFloat("_SunIntensity", this.skySettings.sunIntensity);
		Shader.SetGlobalFloat("_SunDiskSize", this.skySettings.sunDiskScale);
		Shader.SetGlobalFloat("_SunDiskIntensity", this.skySettings.sunDiskIntensity);
		Shader.SetGlobalFloat("_SunDiskSize", this.skySettings.sunDiskScale);
		Shader.SetGlobalFloat("_Exposure", this.skySettings.skyExposure);
		Shader.SetGlobalFloat("_SkyLuminance", this.skySettings.skyLuminence.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalFloat("_scatteringPower", this.skySettings.scatteringCurve.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalFloat("_SkyColorPower", this.skySettings.skyColorPower.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalFloat("_heightFogIntensity", this.fogSettings.heightFogIntensity);
		Shader.SetGlobalFloat("_distanceFogIntensity", this.fogSettings.distanceFogIntensity);
		if (Application.isPlaying)
		{
			Shader.SetGlobalFloat("_maximumFogDensity", 1f - this.fogSettings.maximumFogDensity);
		}
		else
		{
			Shader.SetGlobalFloat("_maximumFogDensity", 1f);
		}
		Shader.SetGlobalFloat("_lightning", this.thunder);
		float num = 0f;
		if (this.Weather.currentActiveWeatherPreset != null)
		{
			num = this.Weather.currentActiveWeatherPreset.WindStrenght;
		}
		if (this.cloudsSettings.useWindZoneDirection)
		{
			this.cloudsSettings.cloudsWindDirectionX = -this.Components.windZone.transform.forward.x;
			this.cloudsSettings.cloudsWindDirectionY = -this.Components.windZone.transform.forward.z;
		}
		this.cloudAnim += new Vector2(this.cloudsSettings.cloudsTimeScale * (num * this.cloudsSettings.cloudsWindDirectionX) * this.cloudsSettings.cloudsWindStrengthModificator * Time.deltaTime, this.cloudsSettings.cloudsTimeScale * (num * this.cloudsSettings.cloudsWindDirectionY) * this.cloudsSettings.cloudsWindStrengthModificator * Time.deltaTime);
		this.cloudAnimNonScaled += new Vector2(this.cloudsSettings.cloudsTimeScale * (num * this.cloudsSettings.cloudsWindDirectionX) * this.cloudsSettings.cloudsWindStrengthModificator * Time.deltaTime * 0.1f, this.cloudsSettings.cloudsTimeScale * (num * this.cloudsSettings.cloudsWindDirectionY) * this.cloudsSettings.cloudsWindStrengthModificator * Time.deltaTime * 0.1f);
		if (this.cloudAnim.x > 1f)
		{
			this.cloudAnim.x = -1f;
		}
		else if (this.cloudAnim.x < -1f)
		{
			this.cloudAnim.x = 1f;
		}
		if (this.cloudAnim.y > 1f)
		{
			this.cloudAnim.y = -1f;
		}
		else if (this.cloudAnim.y < -1f)
		{
			this.cloudAnim.y = 1f;
		}
		if (this.MoonShader != null)
		{
			this.MoonShader.SetFloat("_Phase", this.customMoonPhase);
			this.MoonShader.SetColor("_Color", this.skySettings.moonColor);
			this.MoonShader.SetFloat("_Brightness", this.skySettings.moonBrightness * (1f - this.GameTime.solarTime));
		}
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0004B5A0 File Offset: 0x000499A0
	private void UpdateSkyRenderingComponent()
	{
		if (this.EnviroSkyRender == null)
		{
			return;
		}
		this.EnviroSkyRender.Resolution = this.volumeLightSettings.Resolution;
		if (this.EnviroSkyRender._volumeRenderingMaterial != null)
		{
			this.EnviroSkyRender._volumeRenderingMaterial.SetTexture("_Clouds", this.cloudsRenderTarget);
			float value = (!this.HDR) ? 0f : 1f;
			this.EnviroSkyRender._volumeRenderingMaterial.SetFloat("_hdr", value);
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0004B638 File Offset: 0x00049A38
	private DateTime CreateSystemDate()
	{
		return default(DateTime).AddYears(this.GameTime.Years - 1).AddDays((double)(this.GameTime.Days - 1));
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0004B67C File Offset: 0x00049A7C
	private void UpdateSunAndMoonPosition()
	{
		DateTime dateTime = this.CreateSystemDate();
		float num = (float)(367 * dateTime.Year - 7 * (dateTime.Year + (dateTime.Month / 12 + 9) / 12) / 4 + 275 * dateTime.Month / 9 + dateTime.Day - 730530);
		num += this.GetUniversalTimeOfDay() / 24f;
		float ecl = 23.4393f - 3.563E-07f * num;
		if (this.skySettings.sunAndMoonPosition == EnviroSkySettings.SunAndMoonCalc.Realistic)
		{
			this.CalculateSunPosition(num, ecl, false);
			this.CalculateMoonPosition(num, ecl);
		}
		else
		{
			this.CalculateSunPosition(num, ecl, true);
		}
		this.CalculateStarsPosition(this.LST);
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0004B734 File Offset: 0x00049B34
	private float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0004B748 File Offset: 0x00049B48
	private void CalculateSunPosition(float d, float ecl, bool simpleMoon)
	{
		float num = 282.9404f + 4.70935E-05f * d;
		float num2 = 0.016709f - 1.151E-09f * d;
		float num3 = 356.047f + 0.98560023f * d;
		float num4 = num3 + num2 * 57.29578f * Mathf.Sin(0.017453292f * num3) * (1f + num2 * Mathf.Cos(0.017453292f * num3));
		float num5 = Mathf.Cos(0.017453292f * num4) - num2;
		float num6 = Mathf.Sin(0.017453292f * num4) * Mathf.Sqrt(1f - num2 * num2);
		float num7 = 57.29578f * Mathf.Atan2(num6, num5);
		float num8 = Mathf.Sqrt(num5 * num5 + num6 * num6);
		float num9 = num7 + num;
		float num10 = num8 * Mathf.Cos(0.017453292f * num9);
		float num11 = num8 * Mathf.Sin(0.017453292f * num9);
		float num12 = num10;
		float num13 = num11 * Mathf.Cos(0.017453292f * ecl);
		float y = num11 * Mathf.Sin(0.017453292f * ecl);
		float f = Mathf.Atan2(y, Mathf.Sqrt(num12 * num12 + num13 * num13));
		float num14 = Mathf.Sin(f);
		float num15 = Mathf.Cos(f);
		float num16 = num9 + 180f;
		float num17 = num16 + this.GetUniversalTimeOfDay() * 15f;
		this.LST = num17 + this.GameTime.Longitude;
		float num18 = this.LST - 57.29578f * Mathf.Atan2(num13, num12);
		float f2 = 0.017453292f * num18;
		float num19 = Mathf.Sin(f2);
		float num20 = Mathf.Cos(f2);
		float num21 = num20 * num15;
		float num22 = num19 * num15;
		float num23 = num14;
		float num24 = Mathf.Sin(0.017453292f * this.GameTime.Latitude);
		float num25 = Mathf.Cos(0.017453292f * this.GameTime.Latitude);
		float num26 = num21 * num24 - num23 * num25;
		float num27 = num22;
		float y2 = num21 * num25 + num23 * num24;
		float num28 = Mathf.Atan2(num27, num26) + 3.1415927f;
		float num29 = Mathf.Atan2(y2, Mathf.Sqrt(num26 * num26 + num27 * num27));
		float num30 = 1.5707964f - num29;
		float phi = num28;
		this.GameTime.solarTime = Mathf.Clamp01(this.Remap(num30, -1.5f, 0f, 1.5f, 1f));
		this.SunTransform.localPosition = this.OrbitalToLocal(num30, phi);
		this.SunTransform.LookAt(this.DomeTransform.position);
		if (simpleMoon)
		{
			this.MoonTransform.localPosition = this.OrbitalToLocal(num30 - 3.1415927f, phi);
			this.MoonTransform.LookAt(this.DomeTransform.position);
		}
		this.SetupShader(num30);
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0004BA08 File Offset: 0x00049E08
	private void CalculateMoonPosition(float d, float ecl)
	{
		float num = 125.1228f - 0.05295381f * d;
		float num2 = 5.1454f;
		float num3 = 318.0634f + 0.16435732f * d;
		float num4 = 60.2666f;
		float num5 = 0.0549f;
		float num6 = 115.3654f + 13.064993f * d;
		float num7 = 0.017453292f * num6;
		float f = num7 + num5 * Mathf.Sin(num7) * (1f + num5 * Mathf.Cos(num7));
		float num8 = num4 * (Mathf.Cos(f) - num5);
		float num9 = num4 * (Mathf.Sqrt(1f - num5 * num5) * Mathf.Sin(f));
		float num10 = 57.29578f * Mathf.Atan2(num9, num8);
		float num11 = Mathf.Sqrt(num8 * num8 + num9 * num9);
		float f2 = 0.017453292f * num;
		float num12 = Mathf.Sin(f2);
		float num13 = Mathf.Cos(f2);
		float f3 = 0.017453292f * (num10 + num3);
		float num14 = Mathf.Sin(f3);
		float num15 = Mathf.Cos(f3);
		float f4 = 0.017453292f * num2;
		float num16 = Mathf.Cos(f4);
		float num17 = num11 * (num13 * num15 - num12 * num14 * num16);
		float num18 = num11 * (num12 * num15 + num13 * num14 * num16);
		float num19 = num11 * (num14 * Mathf.Sin(f4));
		float num20 = Mathf.Cos(0.017453292f * ecl);
		float num21 = Mathf.Sin(0.017453292f * ecl);
		float num22 = num17;
		float num23 = num18 * num20 - num19 * num21;
		float y = num18 * num21 + num19 * num20;
		float num24 = Mathf.Atan2(num23, num22);
		float f5 = Mathf.Atan2(y, Mathf.Sqrt(num22 * num22 + num23 * num23));
		float f6 = 0.017453292f * this.LST - num24;
		float num25 = Mathf.Cos(f6) * Mathf.Cos(f5);
		float num26 = Mathf.Sin(f6) * Mathf.Cos(f5);
		float num27 = Mathf.Sin(f5);
		float f7 = 0.017453292f * this.GameTime.Latitude;
		float num28 = Mathf.Sin(f7);
		float num29 = Mathf.Cos(f7);
		float num30 = num25 * num28 - num27 * num29;
		float num31 = num26;
		float y2 = num25 * num29 + num27 * num28;
		float num32 = Mathf.Atan2(num31, num30) + 3.1415927f;
		float num33 = Mathf.Atan2(y2, Mathf.Sqrt(num30 * num30 + num31 * num31));
		float num34 = 1.5707964f - num33;
		float phi = num32;
		this.MoonTransform.localPosition = this.OrbitalToLocal(num34, phi);
		this.GameTime.lunarTime = Mathf.Clamp01(this.Remap(num34, -1.5f, 0f, 1.5f, 1f));
		this.MoonTransform.LookAt(this.DomeTransform.position);
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0004BCB8 File Offset: 0x0004A0B8
	private void CalculateStarsPosition(float siderealTime)
	{
		if (siderealTime > 24f)
		{
			siderealTime -= 24f;
		}
		else if (siderealTime < 0f)
		{
			siderealTime += 24f;
		}
		Quaternion quaternion = Quaternion.Euler(90f - this.GameTime.Latitude, 0.017453292f * this.GameTime.Longitude, 0f);
		quaternion *= Quaternion.Euler(0f, siderealTime, 0f);
		this.Components.starsRotation.localRotation = quaternion;
		if (RenderSettings.skybox != null)
		{
			RenderSettings.skybox.SetMatrix("_StarsMatrix", this.Components.starsRotation.worldToLocalMatrix);
		}
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0004BD78 File Offset: 0x0004A178
	private Vector3 UpdateSatellitePosition(float orbit, float orbit2, float speed)
	{
		float f = 0.017453292f * this.GameTime.Latitude;
		float num = Mathf.Sin(f);
		float num2 = Mathf.Cos(f);
		float num3 = 0.017453292f * this.GameTime.Longitude;
		float f2 = orbit2 * Mathf.Sin(0.017073873f * ((float)this.GameTime.Days - 81f));
		float num4 = Mathf.Sin(f2);
		float num5 = Mathf.Cos(f2);
		float num6 = (float)((int)(this.GameTime.Longitude / 15f));
		float num7 = 0.2617994f * num6;
		float num8 = this.GetUniversalTimeOfDay() + orbit * Mathf.Sin(0.03333255f * ((float)this.GameTime.Days - 80f)) - speed * Mathf.Sin(0.008849557f * ((float)this.GameTime.Days - 8f)) + 3.8197186f * (num7 - num3);
		float f3 = 0.2617994f * num8;
		float num9 = Mathf.Sin(f3);
		float num10 = Mathf.Cos(f3);
		float f4 = num * num4 - num2 * num5 * num10;
		float num11 = Mathf.Asin(f4);
		float y = -num5 * num9;
		float x = num2 * num4 - num * num5 * num10;
		float num12 = Mathf.Atan2(y, x);
		float theta = 1.5707964f - num11;
		float phi = num12;
		return this.OrbitalToLocal(theta, phi);
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x0004BEC8 File Offset: 0x0004A2C8
	private Vector3 OrbitalToLocal(float theta, float phi)
	{
		float num = Mathf.Sin(theta);
		float y = Mathf.Cos(theta);
		float num2 = Mathf.Sin(phi);
		float num3 = Mathf.Cos(phi);
		Vector3 result;
		result.z = num * num3;
		result.y = y;
		result.x = num * num2;
		return result;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0004BF10 File Offset: 0x0004A310
	private void UpdateReflections()
	{
		this.Components.GlobalReflectionProbe.intensity = this.lightSettings.globalReflectionsIntensity;
		this.Components.GlobalReflectionProbe.size = base.transform.localScale * this.lightSettings.globalReflectionsScale;
		if ((this.currentTimeInHours > this.lastRelfectionUpdate + (double)this.lightSettings.globalReflectionsUpdate || this.currentTimeInHours < this.lastRelfectionUpdate - (double)this.lightSettings.globalReflectionsUpdate) && this.lightSettings.globalReflections)
		{
			this.Components.GlobalReflectionProbe.enabled = true;
			this.lastRelfectionUpdate = this.currentTimeInHours;
			this.Components.GlobalReflectionProbe.RenderProbe();
		}
		else if (!this.lightSettings.globalReflections)
		{
			this.Components.GlobalReflectionProbe.enabled = false;
		}
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0004C004 File Offset: 0x0004A404
	private void UpdateTime()
	{
		if (Application.isPlaying)
		{
			float num;
			if (!this.isNight)
			{
				num = 0.4f / this.GameTime.DayLengthInMinutes;
			}
			else
			{
				num = 0.4f / this.GameTime.NightLengthInMinutes;
			}
			this.hourTime = num * Time.deltaTime;
			switch (this.GameTime.ProgressTime)
			{
			case EnviroTime.TimeProgressMode.None:
				this.SetTime(this.GameTime.Years, this.GameTime.Days, this.GameTime.Hours, this.GameTime.Minutes, this.GameTime.Seconds);
				break;
			case EnviroTime.TimeProgressMode.Simulated:
				this.internalHour += this.hourTime;
				this.SetGameTime();
				this.customMoonPhase += Time.deltaTime / (30f * (this.GameTime.DayLengthInMinutes * 60f)) * 2f;
				break;
			case EnviroTime.TimeProgressMode.OneDay:
				this.internalHour += this.hourTime;
				this.SetGameTime();
				this.customMoonPhase += Time.deltaTime / (30f * (this.GameTime.DayLengthInMinutes * 60f)) * 2f;
				break;
			case EnviroTime.TimeProgressMode.SystemTime:
				this.SetTime(DateTime.Now);
				this.customMoonPhase += Time.deltaTime / 2592000f * 2f;
				break;
			}
		}
		else
		{
			this.SetTime(this.GameTime.Years, this.GameTime.Days, this.GameTime.Hours, this.GameTime.Minutes, this.GameTime.Seconds);
		}
		if (this.customMoonPhase < -1f)
		{
			this.customMoonPhase += 2f;
		}
		else if (this.customMoonPhase > 1f)
		{
			this.customMoonPhase -= 2f;
		}
		if (this.internalHour > this.lastHourUpdate + 1f)
		{
			this.lastHourUpdate = this.internalHour;
			this.NotifyHourPassed();
		}
		if ((float)this.GameTime.Days >= this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays + this.seasonsSettings.AutumnInDays + this.seasonsSettings.WinterInDays)
		{
			this.GameTime.Years = this.GameTime.Years + 1;
			this.GameTime.Days = 0;
			this.NotifyYearPassed();
		}
		this.currentHour = this.internalHour;
		this.currentDay = (float)this.GameTime.Days;
		this.currentYear = (float)this.GameTime.Years;
		this.currentTimeInHours = this.GetInHours(this.internalHour, this.currentDay, this.currentYear);
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0004C304 File Offset: 0x0004A704
	private void SetInternalTime(int year, int dayOfYear, int hour, int minute, int seconds)
	{
		this.GameTime.Years = year;
		this.GameTime.Days = dayOfYear;
		this.GameTime.Minutes = minute;
		this.GameTime.Hours = hour;
		this.internalHour = (float)hour + (float)minute * 0.0166667f + (float)seconds * 0.000277778f;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0004C360 File Offset: 0x0004A760
	private void SetGameTime()
	{
		if (this.internalHour >= 24f)
		{
			this.internalHour -= 24f;
			this.NotifyHourPassed();
			this.lastHourUpdate = this.internalHour;
			if (this.GameTime.ProgressTime != EnviroTime.TimeProgressMode.OneDay)
			{
				this.GameTime.Days = this.GameTime.Days + 1;
				this.NotifyDayPassed();
			}
		}
		else if (this.internalHour < 0f)
		{
			this.internalHour = 24f + this.internalHour;
			this.lastHourUpdate = this.internalHour;
			if (this.GameTime.ProgressTime != EnviroTime.TimeProgressMode.OneDay)
			{
				this.GameTime.Days = this.GameTime.Days - 1;
				this.NotifyDayPassed();
			}
		}
		float num = this.internalHour;
		this.GameTime.Hours = (int)num;
		num -= (float)this.GameTime.Hours;
		this.GameTime.Minutes = (int)(num * 60f);
		num -= (float)this.GameTime.Minutes * 0.0166667f;
		this.GameTime.Seconds = (int)(num * 3600f);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0004C490 File Offset: 0x0004A890
	private void UpdateAmbientLight()
	{
		AmbientMode ambientMode = this.lightSettings.ambientMode;
		if (ambientMode != AmbientMode.Flat)
		{
			if (ambientMode != AmbientMode.Trilight)
			{
				if (ambientMode == AmbientMode.Skybox)
				{
					RenderSettings.ambientIntensity = this.lightSettings.ambientIntensity.Evaluate(this.GameTime.solarTime);
					if (this.lastAmbientSkyUpdate < this.internalHour || this.lastAmbientSkyUpdate > this.internalHour + 0.101f)
					{
						DynamicGI.UpdateEnvironment();
						this.lastAmbientSkyUpdate = this.internalHour + 0.1f;
					}
				}
			}
			else
			{
				Color a = Color.Lerp(this.lightSettings.ambientSkyColor.Evaluate(this.GameTime.solarTime), this.currentWeatherLightMod, this.currentWeatherLightMod.a) * this.lightSettings.ambientIntensity.Evaluate(this.GameTime.solarTime);
				RenderSettings.ambientSkyColor = Color.Lerp(a, this.currentInteriorAmbientLightMod, this.currentInteriorAmbientLightMod.a);
				Color a2 = Color.Lerp(this.lightSettings.ambientEquatorColor.Evaluate(this.GameTime.solarTime), this.currentWeatherLightMod, this.currentWeatherLightMod.a) * this.lightSettings.ambientIntensity.Evaluate(this.GameTime.solarTime);
				RenderSettings.ambientEquatorColor = Color.Lerp(a2, this.currentInteriorAmbientEQLightMod, this.currentInteriorAmbientEQLightMod.a);
				Color a3 = Color.Lerp(this.lightSettings.ambientGroundColor.Evaluate(this.GameTime.solarTime), this.currentWeatherLightMod, this.currentWeatherLightMod.a) * this.lightSettings.ambientIntensity.Evaluate(this.GameTime.solarTime);
				RenderSettings.ambientGroundColor = Color.Lerp(a3, this.currentInteriorAmbientGRLightMod, this.currentInteriorAmbientGRLightMod.a);
			}
		}
		else
		{
			Color a4 = Color.Lerp(this.lightSettings.ambientSkyColor.Evaluate(this.GameTime.solarTime), this.currentWeatherLightMod, this.currentWeatherLightMod.a) * this.lightSettings.ambientIntensity.Evaluate(this.GameTime.solarTime);
			RenderSettings.ambientSkyColor = Color.Lerp(a4, this.currentInteriorAmbientLightMod, this.currentInteriorAmbientLightMod.a);
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0004C6EC File Offset: 0x0004AAEC
	private void CalculateDirectLight()
	{
		Color a = Color.Lerp(this.lightSettings.LightColor.Evaluate(this.GameTime.solarTime), this.currentWeatherLightMod, this.currentWeatherLightMod.a);
		this.MainLight.color = Color.Lerp(a, this.currentInteriorDirectLightMod, this.currentInteriorDirectLightMod.a);
		Shader.SetGlobalColor("_EnviroLighting", this.lightSettings.LightColor.Evaluate(this.GameTime.solarTime));
		Shader.SetGlobalVector("_SunDirection", -this.Components.Sun.transform.forward);
		Shader.SetGlobalVector("_SunPosition", this.Components.Sun.transform.localPosition + -this.Components.Sun.transform.forward * 10000f);
		Shader.SetGlobalVector("_MoonPosition", this.Components.Moon.transform.localPosition);
		float intensity;
		if (!this.isNight)
		{
			intensity = this.lightSettings.directLightSunIntensity.Evaluate(this.GameTime.solarTime);
			this.Components.Sun.transform.LookAt(new Vector3(this.DomeTransform.position.x, this.DomeTransform.position.y - this.lightSettings.directLightAngleOffset, this.DomeTransform.position.z));
			this.Components.DirectLight.rotation = this.Components.Sun.transform.rotation;
		}
		else
		{
			intensity = this.lightSettings.directLightMoonIntensity.Evaluate(this.GameTime.lunarTime);
			this.Components.Moon.transform.LookAt(new Vector3(this.DomeTransform.position.x, this.DomeTransform.position.y - this.lightSettings.directLightAngleOffset, this.DomeTransform.position.z));
			this.Components.DirectLight.rotation = this.Components.Moon.transform.rotation;
		}
		this.MainLight.intensity = intensity;
		this.MainLight.shadowStrength = this.lightSettings.shadowIntensity.Evaluate(this.GameTime.solarTime);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0004C992 File Offset: 0x0004AD92
	private Quaternion LightLookAt(Quaternion inputRotation, Quaternion newRotation)
	{
		return Quaternion.Lerp(inputRotation, newRotation, 500f * Time.deltaTime);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0004C9A8 File Offset: 0x0004ADA8
	private void ValidateParameters()
	{
		this.internalHour = Mathf.Repeat(this.internalHour, 24f);
		this.GameTime.Longitude = Mathf.Clamp(this.GameTime.Longitude, -180f, 180f);
		this.GameTime.Latitude = Mathf.Clamp(this.GameTime.Latitude, -90f, 90f);
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0004CA15 File Offset: 0x0004AE15
	public void RegisterZone(EnviroZone zoneToAdd)
	{
		this.Weather.zones.Add(zoneToAdd);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0004CA28 File Offset: 0x0004AE28
	public void EnterZone(EnviroZone zone)
	{
		this.Weather.currentActiveZone = zone;
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0004CA36 File Offset: 0x0004AE36
	public void ExitZone()
	{
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0004CA38 File Offset: 0x0004AE38
	public void CreateWeatherEffectHolder()
	{
		if (this.Weather.VFXHolder == null)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "VFX";
			gameObject.transform.parent = this.EffectsHolder.transform;
			gameObject.transform.localPosition = Vector3.zero;
			this.Weather.VFXHolder = gameObject;
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0004CAA0 File Offset: 0x0004AEA0
	private void UpdateAudioSource(EnviroWeatherPreset i)
	{
		if (i != null && i.weatherSFX != null)
		{
			if (i.weatherSFX == this.Weather.currentAudioSource.audiosrc.clip)
			{
				if (this.Weather.currentAudioSource.audiosrc.volume < 0.1f)
				{
					this.Weather.currentAudioSource.FadeIn(i.weatherSFX);
				}
				return;
			}
			if (this.Weather.currentAudioSource == this.AudioSourceWeather)
			{
				this.AudioSourceWeather.FadeOut();
				this.AudioSourceWeather2.FadeIn(i.weatherSFX);
				this.Weather.currentAudioSource = this.AudioSourceWeather2;
			}
			else if (this.Weather.currentAudioSource == this.AudioSourceWeather2)
			{
				this.AudioSourceWeather2.FadeOut();
				this.AudioSourceWeather.FadeIn(i.weatherSFX);
				this.Weather.currentAudioSource = this.AudioSourceWeather;
			}
		}
		else
		{
			this.AudioSourceWeather.FadeOut();
			this.AudioSourceWeather2.FadeOut();
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0004CBD8 File Offset: 0x0004AFD8
	private void UpdateClouds(EnviroWeatherPreset i, bool withTransition)
	{
		if (i == null)
		{
			return;
		}
		float num = 500f * Time.deltaTime;
		if (withTransition)
		{
			num = this.weatherSettings.cloudTransitionSpeed * Time.deltaTime;
		}
		this.cloudsConfig.topColor = Color.Lerp(this.cloudsConfig.topColor, i.cloudsConfig.topColor, num);
		this.cloudsConfig.bottomColor = Color.Lerp(this.cloudsConfig.bottomColor, i.cloudsConfig.bottomColor, num);
		this.cloudsConfig.coverage = Mathf.Lerp(this.cloudsConfig.coverage, i.cloudsConfig.coverage, num);
		this.cloudsConfig.coverageHeight = Mathf.Lerp(this.cloudsConfig.coverageHeight, i.cloudsConfig.coverageHeight, num);
		this.cloudsConfig.raymarchingScale = Mathf.Lerp(this.cloudsConfig.raymarchingScale, i.cloudsConfig.raymarchingScale, num);
		this.cloudsConfig.skyBlending = Mathf.Lerp(this.cloudsConfig.skyBlending, i.cloudsConfig.skyBlending, num);
		this.cloudsConfig.density = Mathf.Lerp(this.cloudsConfig.density, i.cloudsConfig.density, num);
		this.cloudsConfig.alphaCoef = Mathf.Lerp(this.cloudsConfig.alphaCoef, i.cloudsConfig.alphaCoef, num);
		this.cloudsConfig.scatteringCoef = Mathf.Lerp(this.cloudsConfig.scatteringCoef, i.cloudsConfig.scatteringCoef, num);
		this.cloudsConfig.cloudType = Mathf.Lerp(this.cloudsConfig.cloudType, i.cloudsConfig.cloudType, num);
		this.cloudsConfig.cirrusAlpha = Mathf.Lerp(this.cloudsConfig.cirrusAlpha, i.cloudsConfig.cirrusAlpha, num);
		this.cloudsConfig.cirrusCoverage = Mathf.Lerp(this.cloudsConfig.cirrusCoverage, i.cloudsConfig.cirrusCoverage, num);
		this.cloudsConfig.cirrusColorPow = Mathf.Lerp(this.cloudsConfig.cirrusColorPow, i.cloudsConfig.cirrusColorPow, num);
		this.cloudsConfig.flatAlpha = Mathf.Lerp(this.cloudsConfig.flatAlpha, i.cloudsConfig.flatAlpha, num);
		this.cloudsConfig.flatCoverage = Mathf.Lerp(this.cloudsConfig.flatCoverage, i.cloudsConfig.flatCoverage, num);
		this.cloudsConfig.flatColorPow = Mathf.Lerp(this.cloudsConfig.flatColorPow, i.cloudsConfig.flatColorPow, num);
		this.cloudsConfig.flatSoftness = Mathf.Lerp(this.cloudsConfig.flatSoftness, i.cloudsConfig.flatSoftness, num);
		this.cloudsConfig.flatBrightness = Mathf.Lerp(this.cloudsConfig.flatBrightness, i.cloudsConfig.flatBrightness, num);
		this.globalVolumeLightIntensity = Mathf.Lerp(this.globalVolumeLightIntensity, i.volumeLightIntensity, num);
		this.currentWeatherSkyMod = Color.Lerp(this.currentWeatherSkyMod, i.weatherSkyMod.Evaluate(this.GameTime.solarTime), num);
		this.currentWeatherFogMod = Color.Lerp(this.currentWeatherFogMod, i.weatherFogMod.Evaluate(this.GameTime.solarTime), num * 10f);
		this.currentWeatherLightMod = Color.Lerp(this.currentWeatherLightMod, i.weatherLightMod.Evaluate(this.GameTime.solarTime), num);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0004CF6C File Offset: 0x0004B36C
	private void UpdateFog(EnviroWeatherPreset i, bool withTransition)
	{
		if (i != null)
		{
			float t = 500f * Time.deltaTime;
			if (withTransition)
			{
				t = this.weatherSettings.fogTransitionSpeed * Time.deltaTime;
			}
			if (this.fogSettings.Fogmode == FogMode.Linear)
			{
				RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, i.fogDistance, t);
				RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, i.fogStartDistance, t);
			}
			else if (this.updateFogDensity)
			{
				RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, i.fogDensity, t) * this.currentInteriorFogMod;
			}
			Color a = Color.Lerp(this.lightSettings.ambientSkyColor.Evaluate(this.GameTime.solarTime), this.customFogColor, this.customFogIntensity);
			RenderSettings.fogColor = Color.Lerp(a, this.currentWeatherFogMod, this.currentWeatherFogMod.a);
			this.fogSettings.heightDensity = Mathf.Lerp(this.fogSettings.heightDensity, i.heightFogDensity, t);
			this.Fog.skyFogHeight = Mathf.Lerp(this.Fog.skyFogHeight, i.SkyFogHeight, t);
			this.Fog.skyFogStrength = Mathf.Lerp(this.Fog.skyFogStrength, i.SkyFogIntensity, t);
			this.fogSettings.skyFogIntensity = Mathf.Lerp(this.fogSettings.skyFogIntensity, i.SkyFogIntensity, t);
			this.Fog.scatteringStrenght = Mathf.Lerp(this.Fog.scatteringStrenght, i.FogScatteringIntensity, t);
			this.Fog.sunBlocking = Mathf.Lerp(this.Fog.sunBlocking, i.fogSunBlocking, t);
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0004D128 File Offset: 0x0004B528
	private void UpdateEffectSystems(EnviroWeatherPrefab id, bool withTransition)
	{
		if (id != null)
		{
			float t = 500f * Time.deltaTime;
			if (withTransition)
			{
				t = this.weatherSettings.effectTransitionSpeed * Time.deltaTime;
			}
			for (int i = 0; i < id.effectSystems.Count; i++)
			{
				if (id.effectSystems[i].isStopped)
				{
					id.effectSystems[i].Play();
				}
				float emissionRate = Mathf.Lerp(EnviroSky.GetEmissionRate(id.effectSystems[i]), id.effectEmmisionRates[i] * this.qualitySettings.GlobalParticleEmissionRates, t) * this.currentInteriorWeatherEffectMod;
				EnviroSky.SetEmissionRate(id.effectSystems[i], emissionRate);
			}
			for (int j = 0; j < this.Weather.WeatherPrefabs.Count; j++)
			{
				if (this.Weather.WeatherPrefabs[j].gameObject != id.gameObject)
				{
					for (int k = 0; k < this.Weather.WeatherPrefabs[j].effectSystems.Count; k++)
					{
						float num = Mathf.Lerp(EnviroSky.GetEmissionRate(this.Weather.WeatherPrefabs[j].effectSystems[k]), 0f, t);
						if (num < 1f)
						{
							num = 0f;
						}
						EnviroSky.SetEmissionRate(this.Weather.WeatherPrefabs[j].effectSystems[k], num);
						if (num == 0f && !this.Weather.WeatherPrefabs[j].effectSystems[k].isStopped)
						{
							this.Weather.WeatherPrefabs[j].effectSystems[k].Stop();
						}
					}
				}
			}
			this.UpdateWeatherVariables(id.weatherPreset);
		}
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0004D330 File Offset: 0x0004B730
	private void UpdateWeatherVariables(EnviroWeatherPreset p)
	{
		this.Components.windZone.windMain = p.WindStrenght;
		if (this.Weather.wetness < p.wetnessLevel)
		{
			this.Weather.wetness = Mathf.Lerp(this.Weather.curWetness, p.wetnessLevel, this.weatherSettings.wetnessAccumulationSpeed * Time.deltaTime);
		}
		else
		{
			this.Weather.wetness = Mathf.Lerp(this.Weather.curWetness, p.wetnessLevel, this.weatherSettings.wetnessDryingSpeed * Time.deltaTime);
		}
		this.Weather.wetness = Mathf.Clamp(this.Weather.wetness, 0f, 1f);
		this.Weather.curWetness = this.Weather.wetness;
		if (this.Weather.snowStrength < p.snowLevel)
		{
			this.Weather.snowStrength = Mathf.Lerp(this.Weather.curSnowStrength, p.snowLevel, this.weatherSettings.snowAccumulationSpeed * Time.deltaTime);
		}
		else
		{
			this.Weather.snowStrength = Mathf.Lerp(this.Weather.curSnowStrength, p.snowLevel, this.weatherSettings.snowMeltingSpeed * Time.deltaTime);
		}
		this.Weather.snowStrength = Mathf.Clamp(this.Weather.snowStrength, 0f, 1f);
		this.Weather.curSnowStrength = this.Weather.snowStrength;
		Shader.SetGlobalFloat("_EnviroGrassSnow", this.Weather.curSnowStrength);
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0004D4DC File Offset: 0x0004B8DC
	public static float GetEmissionRate(ParticleSystem system)
	{
		return system.emission.rateOverTime.constantMax;
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0004D500 File Offset: 0x0004B900
	public static void SetEmissionRate(ParticleSystem sys, float emissionRate)
	{
		ParticleSystem.EmissionModule emission = sys.emission;
		ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
		rateOverTime.constantMax = emissionRate;
		emission.rateOverTime = rateOverTime;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0004D52C File Offset: 0x0004B92C
	private IEnumerator PlayThunderRandom()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(this.Weather.currentActiveWeatherPreset.lightningInterval, this.Weather.currentActiveWeatherPreset.lightningInterval * 2f));
		bool playLightning = this.Weather.currentActiveWeatherPrefab != null && this.Weather.currentActiveWeatherPrefab.weatherPreset.isLightningStorm;
		if (playLightning)
		{
			if (this.Weather.weatherFullyChanged)
			{
				this.PlayLightning();
			}
			base.StartCoroutine(this.PlayThunderRandom());
		}
		else
		{
			base.StartCoroutine(this.PlayThunderRandom());
			this.Components.LightningGenerator.StopLightning();
		}
		yield break;
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0004D548 File Offset: 0x0004B948
	public IEnumerator PlayLightningEffect(Vector3 position)
	{
		this.lightningEffect.transform.position = position;
		this.lightningEffect.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(-80f, -100f), 0f, 0f);
		this.lightningEffect.Play();
		yield return new WaitForSeconds(0.5f);
		this.lightningEffect.Stop();
		yield break;
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0004D56C File Offset: 0x0004B96C
	public void PlayLightning()
	{
		if (this.lightningEffect != null)
		{
			base.StartCoroutine(this.PlayLightningEffect(new Vector3(base.transform.position.x + UnityEngine.Random.Range(-this.weatherSettings.lightningRange, this.weatherSettings.lightningRange), this.weatherSettings.lightningHeight, base.transform.position.z + UnityEngine.Random.Range(-this.weatherSettings.lightningRange, this.weatherSettings.lightningRange))));
		}
		if (this.audioSettings.ThunderSFX != null && 0 < this.audioSettings.ThunderSFX.Count)
		{
			int index = UnityEngine.Random.Range(0, this.audioSettings.ThunderSFX.Count);
			AudioClip audioClip = this.audioSettings.ThunderSFX[index];
			if (audioClip != null)
			{
				this.AudioSourceThunder.clip = audioClip;
				this.AudioSourceThunder.loop = false;
				this.AudioSourceThunder.Play();
			}
		}
		this.Components.LightningGenerator.Lightning();
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0004D698 File Offset: 0x0004BA98
	private void UpdateWeather()
	{
		if (this.Weather.currentActiveWeatherPreset != this.Weather.currentActiveZone.currentActiveZoneWeatherPreset)
		{
			this.Weather.lastActiveWeatherPreset = this.Weather.currentActiveWeatherPreset;
			this.Weather.lastActiveWeatherPrefab = this.Weather.currentActiveWeatherPrefab;
			this.Weather.currentActiveWeatherPreset = this.Weather.currentActiveZone.currentActiveZoneWeatherPreset;
			this.Weather.currentActiveWeatherPrefab = this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab;
			if (this.Weather.currentActiveWeatherPreset != null)
			{
				this.NotifyWeatherChanged(this.Weather.currentActiveWeatherPreset);
				this.Weather.weatherFullyChanged = false;
				if (!this.serverMode)
				{
					bool flag = this.Weather.currentActiveWeatherPrefab != null && this.Weather.currentActiveWeatherPrefab.weatherPreset.isLightningStorm;
					if (flag)
					{
						base.StartCoroutine(this.PlayThunderRandom());
					}
					else
					{
						base.StopCoroutine(this.PlayThunderRandom());
						this.Components.LightningGenerator.StopLightning();
					}
				}
			}
		}
		if (this.Weather.currentActiveWeatherPrefab != null && !this.serverMode)
		{
			this.UpdateClouds(this.Weather.currentActiveWeatherPreset, true);
			this.UpdateFog(this.Weather.currentActiveWeatherPreset, true);
			this.UpdateEffectSystems(this.Weather.currentActiveWeatherPrefab, true);
			if (!this.Weather.weatherFullyChanged)
			{
				this.CalcWeatherTransitionState();
			}
		}
		else if (this.Weather.currentActiveWeatherPrefab != null)
		{
			this.UpdateWeatherVariables(this.Weather.currentActiveWeatherPrefab.weatherPreset);
		}
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0004D86C File Offset: 0x0004BC6C
	public void ForceWeatherUpdate()
	{
		this.Weather.lastActiveWeatherPreset = this.Weather.currentActiveWeatherPreset;
		this.Weather.lastActiveWeatherPrefab = this.Weather.currentActiveWeatherPrefab;
		this.Weather.currentActiveWeatherPreset = this.Weather.currentActiveZone.currentActiveZoneWeatherPreset;
		this.Weather.currentActiveWeatherPrefab = this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab;
		if (this.Weather.currentActiveWeatherPreset != null)
		{
			this.NotifyWeatherChanged(this.Weather.currentActiveWeatherPreset);
			this.Weather.weatherFullyChanged = false;
			if (!this.serverMode)
			{
				bool flag = this.Weather.currentActiveWeatherPrefab != null && this.Weather.currentActiveWeatherPrefab.weatherPreset.isLightningStorm;
				if (flag)
				{
					base.StartCoroutine(this.PlayThunderRandom());
				}
				else
				{
					base.StopCoroutine(this.PlayThunderRandom());
					this.Components.LightningGenerator.StopLightning();
				}
			}
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0004D97C File Offset: 0x0004BD7C
	private void CalcWeatherTransitionState()
	{
		bool weatherFullyChanged = this.cloudsConfig.coverage >= this.Weather.currentActiveWeatherPreset.cloudsConfig.coverage - 0.01f;
		this.Weather.weatherFullyChanged = weatherFullyChanged;
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0004D9CC File Offset: 0x0004BDCC
	public void SetWeatherOverwrite(int weatherId)
	{
		if (weatherId < 0 || weatherId > this.Weather.WeatherPrefabs.Count)
		{
			return;
		}
		if (this.Weather.WeatherPrefabs[weatherId] != this.Weather.currentActiveWeatherPrefab)
		{
			this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab = this.Weather.WeatherPrefabs[weatherId];
			this.Weather.currentActiveZone.currentActiveZoneWeatherPreset = this.Weather.WeatherPrefabs[weatherId].weatherPreset;
			EnviroSky.instance.NotifyZoneWeatherChanged(this.Weather.WeatherPrefabs[weatherId].weatherPreset, this.Weather.currentActiveZone);
		}
		this.UpdateClouds(this.Weather.currentActiveZone.currentActiveZoneWeatherPreset, false);
		this.UpdateFog(this.Weather.currentActiveZone.currentActiveZoneWeatherPreset, false);
		this.UpdateEffectSystems(this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab, false);
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0004DAD4 File Offset: 0x0004BED4
	public void SetWeatherOverwrite(EnviroWeatherPreset preset)
	{
		if (preset == null)
		{
			return;
		}
		if (preset != this.Weather.currentActiveWeatherPreset)
		{
			for (int i = 0; i < this.Weather.WeatherPrefabs.Count; i++)
			{
				if (preset == this.Weather.WeatherPrefabs[i].weatherPreset)
				{
					this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab = this.Weather.WeatherPrefabs[i];
					this.Weather.currentActiveZone.currentActiveZoneWeatherPreset = preset;
					EnviroSky.instance.NotifyZoneWeatherChanged(preset, this.Weather.currentActiveZone);
				}
			}
		}
		this.UpdateClouds(this.Weather.currentActiveZone.currentActiveZoneWeatherPreset, false);
		this.UpdateFog(this.Weather.currentActiveZone.currentActiveZoneWeatherPreset, false);
		this.UpdateEffectSystems(this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab, false);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0004DBD4 File Offset: 0x0004BFD4
	public void ChangeWeather(int weatherId)
	{
		if (weatherId < 0 || weatherId >= this.Weather.WeatherPrefabs.Count)
		{
			return;
		}
		if (this.Weather.WeatherPrefabs[weatherId] != this.Weather.currentActiveWeatherPrefab)
		{
			this.Weather.currentActiveZone.currentActiveZoneWeatherPrefab = this.Weather.WeatherPrefabs[weatherId];
			this.Weather.currentActiveZone.currentActiveZoneWeatherPreset = this.Weather.WeatherPrefabs[weatherId].weatherPreset;
			EnviroSky.instance.NotifyZoneWeatherChanged(this.Weather.WeatherPrefabs[weatherId].weatherPreset, this.Weather.currentActiveZone);
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0004DC98 File Offset: 0x0004C098
	public void ChangeWeather(string weatherName)
	{
		for (int i = 0; i < this.Weather.WeatherPrefabs.Count; i++)
		{
			if (this.Weather.WeatherPrefabs[i].weatherPreset.Name == weatherName && this.Weather.WeatherPrefabs[i] != this.Weather.currentActiveWeatherPrefab)
			{
				this.ChangeWeather(i);
				EnviroSky.instance.NotifyZoneWeatherChanged(this.Weather.WeatherPrefabs[i].weatherPreset, this.Weather.currentActiveZone);
			}
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0004DD44 File Offset: 0x0004C144
	public void ChangeCloudsQuality(EnviroCloudSettings.CloudQuality q)
	{
		if (q == EnviroCloudSettings.CloudQuality.Custom)
		{
			return;
		}
		switch (q)
		{
		case EnviroCloudSettings.CloudQuality.Lowest:
			this.cloudsSettings.bottomCloudHeight = 2000f;
			this.cloudsSettings.topCloudHeight = 4000f;
			this.cloudsSettings.cloudsWorldScale = 120000f;
			this.cloudsSettings.raymarchSteps = 75;
			this.cloudsSettings.stepsInDepthModificator = 0.7f;
			this.cloudsSettings.cloudsRenderResolution = 2;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Medium;
			this.cloudsSettings.baseNoiseUV = 26f;
			this.cloudsSettings.detailNoiseUV = 1f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.Low:
			this.cloudsSettings.bottomCloudHeight = 2000f;
			this.cloudsSettings.topCloudHeight = 4000f;
			this.cloudsSettings.cloudsWorldScale = 120000f;
			this.cloudsSettings.raymarchSteps = 90;
			this.cloudsSettings.stepsInDepthModificator = 0.7f;
			this.cloudsSettings.cloudsRenderResolution = 2;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Low;
			this.cloudsSettings.baseNoiseUV = 30f;
			this.cloudsSettings.detailNoiseUV = 1f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.Medium:
			this.cloudsSettings.bottomCloudHeight = 2000f;
			this.cloudsSettings.topCloudHeight = 4500f;
			this.cloudsSettings.cloudsWorldScale = 120000f;
			this.cloudsSettings.raymarchSteps = 100;
			this.cloudsSettings.stepsInDepthModificator = 0.7f;
			this.cloudsSettings.cloudsRenderResolution = 1;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Medium;
			this.cloudsSettings.baseNoiseUV = 35f;
			this.cloudsSettings.detailNoiseUV = 50f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.High:
			this.cloudsSettings.bottomCloudHeight = 2000f;
			this.cloudsSettings.topCloudHeight = 5000f;
			this.cloudsSettings.cloudsWorldScale = 120000f;
			this.cloudsSettings.raymarchSteps = 128;
			this.cloudsSettings.stepsInDepthModificator = 0.6f;
			this.cloudsSettings.cloudsRenderResolution = 1;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Medium;
			this.cloudsSettings.baseNoiseUV = 40f;
			this.cloudsSettings.detailNoiseUV = 50f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.Ultra:
			this.cloudsSettings.bottomCloudHeight = 2000f;
			this.cloudsSettings.topCloudHeight = 5500f;
			this.cloudsSettings.cloudsWorldScale = 120000f;
			this.cloudsSettings.raymarchSteps = 150;
			this.cloudsSettings.stepsInDepthModificator = 0.5f;
			this.cloudsSettings.cloudsRenderResolution = 1;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Low;
			this.cloudsSettings.baseNoiseUV = 40f;
			this.cloudsSettings.detailNoiseUV = 70f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.VR_Low:
			this.cloudsSettings.bottomCloudHeight = 3000f;
			this.cloudsSettings.topCloudHeight = 4200f;
			this.cloudsSettings.cloudsWorldScale = 30000f;
			this.cloudsSettings.raymarchSteps = 60;
			this.cloudsSettings.cloudsRenderResolution = 2;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Low;
			this.cloudsSettings.baseNoiseUV = 20f;
			this.cloudsSettings.detailNoiseUV = 1f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.VR_Medium:
			this.cloudsSettings.bottomCloudHeight = 3000f;
			this.cloudsSettings.topCloudHeight = 4500f;
			this.cloudsSettings.cloudsWorldScale = 30000f;
			this.cloudsSettings.raymarchSteps = 75;
			this.cloudsSettings.cloudsRenderResolution = 1;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Medium;
			this.cloudsSettings.baseNoiseUV = 22f;
			this.cloudsSettings.detailNoiseUV = 1f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		case EnviroCloudSettings.CloudQuality.VR_High:
			this.cloudsSettings.bottomCloudHeight = 3000f;
			this.cloudsSettings.topCloudHeight = 4500f;
			this.cloudsSettings.cloudsWorldScale = 30000f;
			this.cloudsSettings.raymarchSteps = 80;
			this.cloudsSettings.cloudsRenderResolution = 1;
			this.cloudsSettings.reprojectionPixelSize = EnviroCloudSettings.ReprojectionPixelSize.Medium;
			this.cloudsSettings.baseNoiseUV = 23f;
			this.cloudsSettings.detailNoiseUV = 1f;
			this.cloudsSettings.detailQuality = EnviroCloudSettings.CloudDetailQuality.Low;
			break;
		}
		this.lastCloudsQuality = q;
		this.cloudsSettings.cloudsQuality = q;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0004E220 File Offset: 0x0004C620
	public int GetActiveWeatherID()
	{
		for (int i = 0; i < this.Weather.WeatherPrefabs.Count; i++)
		{
			if (this.Weather.WeatherPrefabs[i].weatherPreset == this.Weather.currentActiveWeatherPreset)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0004E27C File Offset: 0x0004C67C
	public void Save()
	{
		PlayerPrefs.SetFloat("Time_Hours", this.internalHour);
		PlayerPrefs.SetInt("Time_Days", this.GameTime.Days);
		PlayerPrefs.SetInt("Time_Years", this.GameTime.Years);
		for (int i = 0; i < this.Weather.WeatherPrefabs.Count; i++)
		{
			if (this.Weather.WeatherPrefabs[i] == this.Weather.currentActiveWeatherPrefab)
			{
				PlayerPrefs.SetInt("currentWeather", i);
			}
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0004E318 File Offset: 0x0004C718
	public void Load()
	{
		if (PlayerPrefs.HasKey("Time_Hours"))
		{
			this.internalHour = PlayerPrefs.GetFloat("Time_Hours");
		}
		if (PlayerPrefs.HasKey("Time_Days"))
		{
			this.GameTime.Days = PlayerPrefs.GetInt("Time_Days");
		}
		if (PlayerPrefs.HasKey("Time_Years"))
		{
			this.GameTime.Years = PlayerPrefs.GetInt("Time_Years");
		}
		if (PlayerPrefs.HasKey("currentWeather"))
		{
			this.SetWeatherOverwrite(PlayerPrefs.GetInt("currentWeather"));
		}
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0004E3AC File Offset: 0x0004C7AC
	public void SetTime(DateTime date)
	{
		this.GameTime.Years = date.Year;
		this.GameTime.Days = date.DayOfYear;
		this.GameTime.Minutes = date.Minute;
		this.GameTime.Seconds = date.Second;
		this.GameTime.Hours = date.Hour;
		this.internalHour = (float)date.Hour + (float)date.Minute * 0.0166667f + (float)date.Second * 0.000277778f;
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x0004E440 File Offset: 0x0004C840
	public void SetTime(int year, int dayOfYear, int hour, int minute, int seconds)
	{
		this.GameTime.Years = year;
		this.GameTime.Days = dayOfYear;
		this.GameTime.Minutes = minute;
		this.GameTime.Hours = hour;
		this.internalHour = (float)hour + (float)minute * 0.0166667f + (float)seconds * 0.000277778f;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0004E49C File Offset: 0x0004C89C
	public void SetInternalTimeOfDay(float inHours)
	{
		this.internalHour = inHours;
		this.GameTime.Hours = (int)inHours;
		inHours -= (float)this.GameTime.Hours;
		this.GameTime.Minutes = (int)(inHours * 60f);
		inHours -= (float)this.GameTime.Minutes * 0.0166667f;
		this.GameTime.Seconds = (int)(inHours * 3600f);
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0004E509 File Offset: 0x0004C909
	public string GetTimeStringWithSeconds()
	{
		return string.Format("{0:00}:{1:00}:{2:00}", this.GameTime.Hours, this.GameTime.Minutes, this.GameTime.Seconds);
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0004E545 File Offset: 0x0004C945
	public string GetTimeString()
	{
		return string.Format("{0:00}:{1:00}", this.GameTime.Hours, this.GameTime.Minutes);
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0004E571 File Offset: 0x0004C971
	public float GetUniversalTimeOfDay()
	{
		return this.internalHour - (float)this.GameTime.utcOffset;
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0004E588 File Offset: 0x0004C988
	public double GetInHours(float hours, float days, float years)
	{
		return (double)(hours + days * 24f + years * (this.seasonsSettings.SpringInDays + this.seasonsSettings.SummerInDays + this.seasonsSettings.AutumnInDays + this.seasonsSettings.WinterInDays) * 24f);
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0004E5DC File Offset: 0x0004C9DC
	public void AssignAndStart(GameObject player, Camera Camera)
	{
		this.Player = player;
		this.PlayerCamera = Camera;
		this.Init();
		this.started = true;
		if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Simulator != null && player != null)
		{
			Singleton<Map>.Instance.Simulator.SetEnviroParticleTarget(player.transform);
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0004E644 File Offset: 0x0004CA44
	public void StartAsServer()
	{
		this.Player = base.gameObject;
		this.serverMode = true;
		this.Init();
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0004E660 File Offset: 0x0004CA60
	public void ChangeFocus(GameObject player, Camera Camera)
	{
		this.Player = player;
		this.RemoveEnviroCameraComponents(this.PlayerCamera);
		this.PlayerCamera = Camera;
		this.InitImageEffects();
		if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Simulator != null && player != null)
		{
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			if (simulator != null)
			{
				simulator.SetEnviroParticleTarget(player.transform);
			}
		}
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0004E6D8 File Offset: 0x0004CAD8
	private void RemoveEnviroCameraComponents(Camera cam)
	{
		EnviroFog component = cam.GetComponent<EnviroFog>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
		EnviroLightShafts[] components = cam.GetComponents<EnviroLightShafts>();
		for (int i = 0; i < components.Length; i++)
		{
			UnityEngine.Object.Destroy(components[i]);
		}
		EnviroSkyRendering component2 = cam.GetComponent<EnviroSkyRendering>();
		if (component2 != null)
		{
			UnityEngine.Object.Destroy(component2);
		}
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0004E73C File Offset: 0x0004CB3C
	public void Play(EnviroTime.TimeProgressMode progressMode = EnviroTime.TimeProgressMode.Simulated)
	{
		this.SetupSkybox();
		if (!this.Components.DirectLight.gameObject.activeSelf)
		{
			this.Components.DirectLight.gameObject.SetActive(true);
		}
		this.GameTime.ProgressTime = progressMode;
		this.EffectsHolder.SetActive(true);
		this.EnviroSkyRender.enabled = true;
		this.started = true;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0004E7AC File Offset: 0x0004CBAC
	public void Stop(bool disableLight = false, bool stopTime = true)
	{
		if (disableLight)
		{
			this.Components.DirectLight.gameObject.SetActive(false);
		}
		if (stopTime)
		{
			this.GameTime.ProgressTime = EnviroTime.TimeProgressMode.None;
		}
		this.EffectsHolder.SetActive(false);
		this.EnviroSkyRender.enabled = false;
		this.lightShaftsScriptSun.enabled = false;
		this.lightShaftsScriptMoon.enabled = false;
		this.started = false;
	}

	// Token: 0x04000F7C RID: 3964
	private static EnviroSky _instance;

	// Token: 0x04000F7D RID: 3965
	public string prefabVersion = "2.0.4";

	// Token: 0x04000F7E RID: 3966
	[Tooltip("Assign your player gameObject here. Required Field! or enable AssignInRuntime!")]
	public GameObject Player;

	// Token: 0x04000F7F RID: 3967
	[Tooltip("Assign your main camera here. Required Field! or enable AssignInRuntime!")]
	public Camera PlayerCamera;

	// Token: 0x04000F80 RID: 3968
	[Tooltip("If enabled Enviro will search for your Player and Camera by Tag!")]
	public bool AssignInRuntime;

	// Token: 0x04000F81 RID: 3969
	[Tooltip("Your Player Tag")]
	public string PlayerTag = string.Empty;

	// Token: 0x04000F82 RID: 3970
	[Tooltip("Your CameraTag")]
	public string CameraTag = "MainCamera";

	// Token: 0x04000F83 RID: 3971
	[Header("General")]
	[Tooltip("Enable this when using singlepass rendering.")]
	public bool dontDestroy;

	// Token: 0x04000F84 RID: 3972
	[Header("Camera Settings")]
	[Tooltip("Enable HDR Rendering. You want to use a third party tonemapping effect for best results!")]
	public bool HDR = true;

	// Token: 0x04000F85 RID: 3973
	[Header("Layer Setup")]
	[Tooltip("This is the layer id forfor the moon.")]
	public int moonRenderingLayer = 29;

	// Token: 0x04000F86 RID: 3974
	[Tooltip("This is the layer id for additional satellites like moons, planets.")]
	public int satelliteRenderingLayer = 30;

	// Token: 0x04000F87 RID: 3975
	[Tooltip("Activate to set recommended maincamera clear flag.")]
	public bool setCameraClearFlags = true;

	// Token: 0x04000F88 RID: 3976
	[Header("Virtual Reality")]
	[Tooltip("Enable this when using singlepass rendering.")]
	public bool singlePassVR;

	// Token: 0x04000F89 RID: 3977
	[Tooltip("Enable this to activate volume lighing")]
	[HideInInspector]
	public bool volumeLighting = true;

	// Token: 0x04000F8A RID: 3978
	[Tooltip("Enable this to activate global scattering fog. Disabled will also disable volume lighting")]
	[HideInInspector]
	public bool globalFog = true;

	// Token: 0x04000F8B RID: 3979
	[Header("Profile")]
	public EnviroProfile profile;

	// Token: 0x04000F8C RID: 3980
	[Header("Control")]
	public EnviroTime GameTime;

	// Token: 0x04000F8D RID: 3981
	public EnviroAudio Audio;

	// Token: 0x04000F8E RID: 3982
	public EnviroWeather Weather;

	// Token: 0x04000F8F RID: 3983
	public EnviroSeasons Seasons;

	// Token: 0x04000F90 RID: 3984
	public EnviroFogging Fog;

	// Token: 0x04000F91 RID: 3985
	public EnviroLightshafts LightShafts;

	// Token: 0x04000F92 RID: 3986
	[Header("Components")]
	public EnviroComponents Components;

	// Token: 0x04000F93 RID: 3987
	[HideInInspector]
	public bool started;

	// Token: 0x04000F94 RID: 3988
	[HideInInspector]
	public bool isNight = true;

	// Token: 0x04000F95 RID: 3989
	[HideInInspector]
	public EnviroLightSettings lightSettings = new EnviroLightSettings();

	// Token: 0x04000F96 RID: 3990
	[HideInInspector]
	public EnviroVolumeLightingSettings volumeLightSettings = new EnviroVolumeLightingSettings();

	// Token: 0x04000F97 RID: 3991
	[HideInInspector]
	public EnviroSkySettings skySettings = new EnviroSkySettings();

	// Token: 0x04000F98 RID: 3992
	[HideInInspector]
	public EnviroCloudSettings cloudsSettings = new EnviroCloudSettings();

	// Token: 0x04000F99 RID: 3993
	[HideInInspector]
	public EnviroWeatherSettings weatherSettings = new EnviroWeatherSettings();

	// Token: 0x04000F9A RID: 3994
	[HideInInspector]
	public EnviroFogSettings fogSettings = new EnviroFogSettings();

	// Token: 0x04000F9B RID: 3995
	[HideInInspector]
	public EnviroLightShaftsSettings lightshaftsSettings = new EnviroLightShaftsSettings();

	// Token: 0x04000F9C RID: 3996
	[HideInInspector]
	public EnviroSeasonSettings seasonsSettings = new EnviroSeasonSettings();

	// Token: 0x04000F9D RID: 3997
	[HideInInspector]
	public EnviroAudioSettings audioSettings = new EnviroAudioSettings();

	// Token: 0x04000F9E RID: 3998
	[HideInInspector]
	public EnviroSatellitesSettings satelliteSettings = new EnviroSatellitesSettings();

	// Token: 0x04000F9F RID: 3999
	[HideInInspector]
	public EnviroQualitySettings qualitySettings = new EnviroQualitySettings();

	// Token: 0x04000FA0 RID: 4000
	public EnviroSky.EnviroCloudsMode cloudsMode;

	// Token: 0x04000FA1 RID: 4001
	private EnviroSky.EnviroCloudsMode lastCloudsMode;

	// Token: 0x04000FA2 RID: 4002
	private EnviroCloudSettings.CloudQuality lastCloudsQuality;

	// Token: 0x04000FA3 RID: 4003
	private Material cloudShadows;

	// Token: 0x04000FA4 RID: 4004
	[HideInInspector]
	public Camera moonCamera;

	// Token: 0x04000FA5 RID: 4005
	[HideInInspector]
	public Camera satCamera;

	// Token: 0x04000FA6 RID: 4006
	[HideInInspector]
	public EnviroVolumeLight directVolumeLight;

	// Token: 0x04000FA7 RID: 4007
	[HideInInspector]
	public EnviroLightShafts lightShaftsScriptSun;

	// Token: 0x04000FA8 RID: 4008
	[HideInInspector]
	public EnviroLightShafts lightShaftsScriptMoon;

	// Token: 0x04000FA9 RID: 4009
	[HideInInspector]
	public EnviroSkyRendering EnviroSkyRender;

	// Token: 0x04000FAA RID: 4010
	[HideInInspector]
	public GameObject EffectsHolder;

	// Token: 0x04000FAB RID: 4011
	[HideInInspector]
	public EnviroAudioSource AudioSourceWeather;

	// Token: 0x04000FAC RID: 4012
	[HideInInspector]
	public EnviroAudioSource AudioSourceWeather2;

	// Token: 0x04000FAD RID: 4013
	[HideInInspector]
	public EnviroAudioSource AudioSourceAmbient;

	// Token: 0x04000FAE RID: 4014
	[HideInInspector]
	public EnviroAudioSource AudioSourceAmbient2;

	// Token: 0x04000FAF RID: 4015
	[HideInInspector]
	public AudioSource AudioSourceThunder;

	// Token: 0x04000FB0 RID: 4016
	[HideInInspector]
	public EnviroAudioSource AudioSourceZone;

	// Token: 0x04000FB1 RID: 4017
	[HideInInspector]
	public List<EnviroVegetationInstance> EnviroVegetationInstances = new List<EnviroVegetationInstance>();

	// Token: 0x04000FB2 RID: 4018
	[HideInInspector]
	public Color currentWeatherSkyMod;

	// Token: 0x04000FB3 RID: 4019
	[HideInInspector]
	public Color currentWeatherLightMod;

	// Token: 0x04000FB4 RID: 4020
	[HideInInspector]
	public Color currentWeatherFogMod;

	// Token: 0x04000FB5 RID: 4021
	[HideInInspector]
	public Color currentInteriorDirectLightMod;

	// Token: 0x04000FB6 RID: 4022
	[HideInInspector]
	public Color currentInteriorAmbientLightMod;

	// Token: 0x04000FB7 RID: 4023
	[HideInInspector]
	public Color currentInteriorAmbientEQLightMod;

	// Token: 0x04000FB8 RID: 4024
	[HideInInspector]
	public Color currentInteriorAmbientGRLightMod;

	// Token: 0x04000FB9 RID: 4025
	[HideInInspector]
	public Color currentInteriorSkyboxMod;

	// Token: 0x04000FBA RID: 4026
	[HideInInspector]
	public Color currentInteriorFogColorMod = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000FBB RID: 4027
	[HideInInspector]
	public float currentInteriorFogMod = 1f;

	// Token: 0x04000FBC RID: 4028
	[HideInInspector]
	public float currentInteriorWeatherEffectMod = 1f;

	// Token: 0x04000FBD RID: 4029
	[HideInInspector]
	public float currentInteriorZoneAudioVolume = 1f;

	// Token: 0x04000FBE RID: 4030
	[HideInInspector]
	public float currentInteriorZoneAudioFadingSpeed = 1f;

	// Token: 0x04000FBF RID: 4031
	[HideInInspector]
	public float globalVolumeLightIntensity;

	// Token: 0x04000FC0 RID: 4032
	[HideInInspector]
	public EnviroWeatherCloudsConfig cloudsConfig;

	// Token: 0x04000FC1 RID: 4033
	[HideInInspector]
	public float thunder;

	// Token: 0x04000FC2 RID: 4034
	[HideInInspector]
	public List<GameObject> satellites = new List<GameObject>();

	// Token: 0x04000FC3 RID: 4035
	[HideInInspector]
	public List<GameObject> satellitesRotation = new List<GameObject>();

	// Token: 0x04000FC4 RID: 4036
	[HideInInspector]
	public DateTime dateTime;

	// Token: 0x04000FC5 RID: 4037
	[HideInInspector]
	public float internalHour;

	// Token: 0x04000FC6 RID: 4038
	[HideInInspector]
	public float currentHour;

	// Token: 0x04000FC7 RID: 4039
	[HideInInspector]
	public float currentDay;

	// Token: 0x04000FC8 RID: 4040
	[HideInInspector]
	public float currentYear;

	// Token: 0x04000FC9 RID: 4041
	[HideInInspector]
	public double currentTimeInHours;

	// Token: 0x04000FCA RID: 4042
	[HideInInspector]
	public RenderTexture cloudsRenderTarget;

	// Token: 0x04000FCB RID: 4043
	[HideInInspector]
	public RenderTexture flatCloudsRenderTarget;

	// Token: 0x04000FCC RID: 4044
	[HideInInspector]
	public Material flatCloudsMat;

	// Token: 0x04000FCD RID: 4045
	[HideInInspector]
	public RenderTexture weatherMap;

	// Token: 0x04000FCE RID: 4046
	[HideInInspector]
	public RenderTexture moonRenderTarget;

	// Token: 0x04000FCF RID: 4047
	[HideInInspector]
	public RenderTexture satRenderTarget;

	// Token: 0x04000FD0 RID: 4048
	[HideInInspector]
	public float customMoonPhase;

	// Token: 0x04000FD1 RID: 4049
	[HideInInspector]
	public bool updateFogDensity = true;

	// Token: 0x04000FD2 RID: 4050
	[HideInInspector]
	public Color customFogColor = Color.black;

	// Token: 0x04000FD3 RID: 4051
	[HideInInspector]
	public float customFogIntensity;

	// Token: 0x04000FD4 RID: 4052
	[HideInInspector]
	public bool profileLoaded;

	// Token: 0x04000FD5 RID: 4053
	[HideInInspector]
	public bool interiorMode;

	// Token: 0x04000FD6 RID: 4054
	[HideInInspector]
	public EnviroInterior lastInteriorZone;

	// Token: 0x04000FD7 RID: 4055
	[HideInInspector]
	public Vector2 cloudAnim;

	// Token: 0x04000FD8 RID: 4056
	[HideInInspector]
	public Vector2 cloudAnimNonScaled;

	// Token: 0x04000FD9 RID: 4057
	[HideInInspector]
	public Material skyMat;

	// Token: 0x04000FDA RID: 4058
	private Transform DomeTransform;

	// Token: 0x04000FDB RID: 4059
	private Transform SunTransform;

	// Token: 0x04000FDC RID: 4060
	private Light MainLight;

	// Token: 0x04000FDD RID: 4061
	private Transform MoonTransform;

	// Token: 0x04000FDE RID: 4062
	private Renderer MoonRenderer;

	// Token: 0x04000FDF RID: 4063
	private Material MoonShader;

	// Token: 0x04000FE0 RID: 4064
	private float lastHourUpdate;

	// Token: 0x04000FE1 RID: 4065
	private float starsRot;

	// Token: 0x04000FE2 RID: 4066
	private float lastHour;

	// Token: 0x04000FE3 RID: 4067
	private double lastRelfectionUpdate;

	// Token: 0x04000FE4 RID: 4068
	private double lastMoonUpdate;

	// Token: 0x04000FE5 RID: 4069
	private float lastAmbientSkyUpdate;

	// Token: 0x04000FE6 RID: 4070
	private bool serverMode;

	// Token: 0x04000FE7 RID: 4071
	private RenderTexture cloudShadowMap;

	// Token: 0x04000FE8 RID: 4072
	private Material cloudShadowMat;

	// Token: 0x04000FE9 RID: 4073
	private const float pi = 3.1415927f;

	// Token: 0x04000FEA RID: 4074
	private Vector3 K = new Vector3(686f, 678f, 666f);

	// Token: 0x04000FEB RID: 4075
	private const float n = 1.0003f;

	// Token: 0x04000FEC RID: 4076
	private const float N = 2.545E+25f;

	// Token: 0x04000FED RID: 4077
	private const float pn = 0.035f;

	// Token: 0x04000FEE RID: 4078
	private float hourTime;

	// Token: 0x04000FEF RID: 4079
	private float LST;

	// Token: 0x04000FF0 RID: 4080
	private ParticleSystem lightningEffect;

	// Token: 0x04000FF1 RID: 4081
	[HideInInspector]
	public bool showSettings;

	// Token: 0x04000FFB RID: 4091
	private Material weatherMapMat;

	// Token: 0x0200034E RID: 846
	public enum EnviroCloudsMode
	{
		// Token: 0x04000FFD RID: 4093
		None,
		// Token: 0x04000FFE RID: 4094
		Both,
		// Token: 0x04000FFF RID: 4095
		Volume,
		// Token: 0x04001000 RID: 4096
		Flat
	}

	// Token: 0x0200034F RID: 847
	// (Invoke) Token: 0x06000EEE RID: 3822
	public delegate void HourPassed();

	// Token: 0x02000350 RID: 848
	// (Invoke) Token: 0x06000EF2 RID: 3826
	public delegate void DayPassed();

	// Token: 0x02000351 RID: 849
	// (Invoke) Token: 0x06000EF6 RID: 3830
	public delegate void YearPassed();

	// Token: 0x02000352 RID: 850
	// (Invoke) Token: 0x06000EFA RID: 3834
	public delegate void WeatherChanged(EnviroWeatherPreset weatherType);

	// Token: 0x02000353 RID: 851
	// (Invoke) Token: 0x06000EFE RID: 3838
	public delegate void ZoneWeatherChanged(EnviroWeatherPreset weatherType, EnviroZone zone);

	// Token: 0x02000354 RID: 852
	// (Invoke) Token: 0x06000F02 RID: 3842
	public delegate void SeasonChanged(EnviroSeasons.Seasons season);

	// Token: 0x02000355 RID: 853
	// (Invoke) Token: 0x06000F06 RID: 3846
	public delegate void isNightE();

	// Token: 0x02000356 RID: 854
	// (Invoke) Token: 0x06000F0A RID: 3850
	public delegate void isDay();

	// Token: 0x02000357 RID: 855
	// (Invoke) Token: 0x06000F0E RID: 3854
	public delegate void ZoneChanged(EnviroZone zone);
}

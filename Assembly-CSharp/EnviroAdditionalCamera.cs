using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
[ExecuteInEditMode]
[AddComponentMenu("Enviro/AddionalCamera")]
public class EnviroAdditionalCamera : MonoBehaviour
{
	// Token: 0x06000F70 RID: 3952 RVA: 0x00054E8A File Offset: 0x0005328A
	private void OnEnable()
	{
		this.myCam = base.GetComponent<Camera>();
		if (this.myCam != null)
		{
			this.InitImageEffects();
		}
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00054EAF File Offset: 0x000532AF
	private void Update()
	{
		this.UpdateCameraComponents();
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00054EB8 File Offset: 0x000532B8
	private void InitImageEffects()
	{
		this.skyRender = this.myCam.gameObject.GetComponent<EnviroSkyRendering>();
		if (this.skyRender == null)
		{
			this.skyRender = this.myCam.gameObject.AddComponent<EnviroSkyRendering>();
		}
		this.skyRender.isAddionalCamera = true;
		EnviroLightShafts[] components = this.myCam.gameObject.GetComponents<EnviroLightShafts>();
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
			this.lightShaftsScriptSun = this.myCam.gameObject.AddComponent<EnviroLightShafts>();
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
			this.lightShaftsScriptMoon = this.myCam.gameObject.AddComponent<EnviroLightShafts>();
			this.lightShaftsScriptMoon.sunShaftsMaterial = new Material(Shader.Find("Enviro/Effects/LightShafts"));
			this.lightShaftsScriptMoon.sunShaftsShader = this.lightShaftsScriptMoon.sunShaftsMaterial.shader;
			this.lightShaftsScriptMoon.simpleClearMaterial = new Material(Shader.Find("Enviro/Effects/ClearLightShafts"));
			this.lightShaftsScriptMoon.simpleClearShader = this.lightShaftsScriptMoon.simpleClearMaterial.shader;
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00055184 File Offset: 0x00053584
	private void UpdateCameraComponents()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this.skyRender != null)
		{
			this.skyRender.dirVolumeLighting = EnviroSky.instance.volumeLightSettings.dirVolumeLighting;
			this.skyRender.volumeLighting = EnviroSky.instance.volumeLighting;
			this.skyRender.distanceFog = EnviroSky.instance.fogSettings.distanceFog;
			this.skyRender.heightFog = EnviroSky.instance.fogSettings.heightFog;
			this.skyRender.height = EnviroSky.instance.fogSettings.height;
			this.skyRender.heightDensity = EnviroSky.instance.fogSettings.heightDensity;
			this.skyRender.useRadialDistance = EnviroSky.instance.fogSettings.useRadialDistance;
			this.skyRender.startDistance = EnviroSky.instance.fogSettings.startDistance;
		}
		if (this.lightShaftsScriptSun != null)
		{
			this.lightShaftsScriptSun.resolution = EnviroSky.instance.lightshaftsSettings.resolution;
			this.lightShaftsScriptSun.screenBlendMode = EnviroSky.instance.lightshaftsSettings.screenBlendMode;
			this.lightShaftsScriptSun.useDepthTexture = EnviroSky.instance.lightshaftsSettings.useDepthTexture;
			this.lightShaftsScriptSun.sunThreshold = EnviroSky.instance.lightshaftsSettings.thresholdColorSun.Evaluate(EnviroSky.instance.GameTime.solarTime);
			this.lightShaftsScriptSun.sunShaftBlurRadius = EnviroSky.instance.lightshaftsSettings.blurRadius;
			this.lightShaftsScriptSun.sunShaftIntensity = EnviroSky.instance.lightshaftsSettings.intensity;
			this.lightShaftsScriptSun.maxRadius = EnviroSky.instance.lightshaftsSettings.maxRadius;
			this.lightShaftsScriptSun.sunColor = EnviroSky.instance.lightshaftsSettings.lightShaftsColorSun.Evaluate(EnviroSky.instance.GameTime.solarTime);
			this.lightShaftsScriptSun.sunTransform = EnviroSky.instance.Components.Sun.transform;
			if (EnviroSky.instance.LightShafts.sunLightShafts)
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
			this.lightShaftsScriptMoon.resolution = EnviroSky.instance.lightshaftsSettings.resolution;
			this.lightShaftsScriptMoon.screenBlendMode = EnviroSky.instance.lightshaftsSettings.screenBlendMode;
			this.lightShaftsScriptMoon.useDepthTexture = EnviroSky.instance.lightshaftsSettings.useDepthTexture;
			this.lightShaftsScriptMoon.sunThreshold = EnviroSky.instance.lightshaftsSettings.thresholdColorMoon.Evaluate(EnviroSky.instance.GameTime.lunarTime);
			this.lightShaftsScriptMoon.sunShaftBlurRadius = EnviroSky.instance.lightshaftsSettings.blurRadius;
			this.lightShaftsScriptMoon.sunShaftIntensity = Mathf.Clamp(EnviroSky.instance.lightshaftsSettings.intensity - EnviroSky.instance.GameTime.solarTime, 0f, 100f);
			this.lightShaftsScriptMoon.maxRadius = EnviroSky.instance.lightshaftsSettings.maxRadius;
			this.lightShaftsScriptMoon.sunColor = EnviroSky.instance.lightshaftsSettings.lightShaftsColorMoon.Evaluate(EnviroSky.instance.GameTime.lunarTime);
			this.lightShaftsScriptMoon.sunTransform = EnviroSky.instance.Components.Moon.transform;
			if (EnviroSky.instance.LightShafts.moonLightShafts)
			{
				this.lightShaftsScriptMoon.enabled = true;
			}
			else
			{
				this.lightShaftsScriptMoon.enabled = false;
			}
		}
	}

	// Token: 0x040010DB RID: 4315
	private Camera myCam;

	// Token: 0x040010DC RID: 4316
	private EnviroSkyRendering skyRender;

	// Token: 0x040010DD RID: 4317
	private EnviroLightShafts lightShaftsScriptSun;

	// Token: 0x040010DE RID: 4318
	private EnviroLightShafts lightShaftsScriptMoon;
}

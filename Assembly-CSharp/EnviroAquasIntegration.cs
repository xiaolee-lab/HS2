using System;
using UnityEngine;

// Token: 0x02000323 RID: 803
[AddComponentMenu("Enviro/Integration/AQUAS Integration")]
public class EnviroAquasIntegration : MonoBehaviour
{
	// Token: 0x06000E24 RID: 3620 RVA: 0x000449A4 File Offset: 0x00042DA4
	private void Start()
	{
		if (EnviroSky.instance == null)
		{
			base.enabled = false;
			return;
		}
		if (GameObject.Find("UnderWaterCameraEffects") != null)
		{
			this.aquasUnderWater = GameObject.Find("UnderWaterCameraEffects").GetComponent<AQUAS_LensEffects>();
		}
		this.defaultDistanceFog = EnviroSky.instance.fogSettings.distanceFog;
		this.defaultHeightFog = EnviroSky.instance.fogSettings.heightFog;
		this.SetupEnviroWithAQUAS();
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x00044A24 File Offset: 0x00042E24
	private void Update()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this.waterObject != null && this.aquasUnderWater != null)
		{
			if (this.aquasUnderWater.underWater && !this.isUnderWater)
			{
				if (this.deactivateEnviroFogUnderwater)
				{
					EnviroSky.instance.fogSettings.distanceFog = false;
					EnviroSky.instance.fogSettings.heightFog = false;
					EnviroSky.instance.customFogIntensity = this.underwaterFogColorInfluence;
				}
				EnviroSky.instance.updateFogDensity = false;
				EnviroSky.instance.Audio.ambientSFXVolumeMod = -1f;
				EnviroSky.instance.Audio.weatherSFXVolumeMod = -1f;
				this.isUnderWater = true;
			}
			else if (!this.aquasUnderWater.underWater && this.isUnderWater)
			{
				if (this.deactivateEnviroFogUnderwater)
				{
					EnviroSky.instance.updateFogDensity = true;
					EnviroSky.instance.fogSettings.distanceFog = this.defaultDistanceFog;
					EnviroSky.instance.fogSettings.heightFog = this.defaultHeightFog;
					RenderSettings.fogDensity = EnviroSky.instance.Weather.currentActiveWeatherPreset.fogDensity;
					EnviroSky.instance.customFogColor = this.aquasUnderWater.underWaterParameters.fogColor;
					EnviroSky.instance.customFogIntensity = 0f;
				}
				EnviroSky.instance.Audio.ambientSFXVolumeMod = 0f;
				EnviroSky.instance.Audio.weatherSFXVolumeMod = 0f;
				this.isUnderWater = false;
			}
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x00044BC8 File Offset: 0x00042FC8
	public void SetupEnviroWithAQUAS()
	{
		if (this.waterObject != null)
		{
			if (this.deactivateAquasReflectionProbe)
			{
				this.DeactivateReflectionProbe(this.waterObject);
			}
			if (!EnviroSky.instance.fogSettings.distanceFog && !EnviroSky.instance.fogSettings.heightFog)
			{
				this.deactivateEnviroFogUnderwater = false;
			}
			if (this.aquasUnderWater != null)
			{
				this.aquasUnderWater.setAfloatFog = false;
			}
			return;
		}
		base.enabled = false;
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00044C58 File Offset: 0x00043058
	private void DeactivateReflectionProbe(GameObject aquas)
	{
		GameObject gameObject = GameObject.Find(aquas.name + "/Reflection Probe");
		if (gameObject != null)
		{
			gameObject.GetComponent<ReflectionProbe>().enabled = false;
		}
	}

	// Token: 0x04000E1F RID: 3615
	[Header("AQUAS Water Plane")]
	public GameObject waterObject;

	// Token: 0x04000E20 RID: 3616
	[Header("Setup")]
	public bool deactivateAquasReflectionProbe = true;

	// Token: 0x04000E21 RID: 3617
	public bool deactivateEnviroFogUnderwater = true;

	// Token: 0x04000E22 RID: 3618
	[Header("Settings")]
	[Range(0f, 1f)]
	public float underwaterFogColorInfluence = 0.3f;

	// Token: 0x04000E23 RID: 3619
	private AQUAS_LensEffects aquasUnderWater;

	// Token: 0x04000E24 RID: 3620
	private bool isUnderWater;

	// Token: 0x04000E25 RID: 3621
	private bool defaultDistanceFog;

	// Token: 0x04000E26 RID: 3622
	private bool defaultHeightFog;
}

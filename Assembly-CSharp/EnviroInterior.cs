using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200036E RID: 878
[AddComponentMenu("Enviro/Interior Zone")]
public class EnviroInterior : MonoBehaviour
{
	// Token: 0x06000F91 RID: 3985 RVA: 0x00056E14 File Offset: 0x00055214
	private void Start()
	{
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x00056E18 File Offset: 0x00055218
	public void CreateNewTrigger()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Trigger " + this.triggers.Count.ToString();
		gameObject.transform.SetParent(base.transform, false);
		gameObject.AddComponent<BoxCollider>().isTrigger = true;
		EnviroTrigger enviroTrigger = gameObject.AddComponent<EnviroTrigger>();
		enviroTrigger.myZone = this;
		enviroTrigger.name = gameObject.name;
		this.triggers.Add(enviroTrigger);
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00056E98 File Offset: 0x00055298
	public void RemoveTrigger(EnviroTrigger id)
	{
		UnityEngine.Object.DestroyImmediate(id.gameObject);
		this.triggers.Remove(id);
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x00056EB4 File Offset: 0x000552B4
	public void Enter()
	{
		EnviroSky.instance.interiorMode = true;
		EnviroSky.instance.lastInteriorZone = this;
		if (this.directLighting)
		{
			this.fadeOutDirectLight = false;
			this.fadeInDirectLight = true;
		}
		if (this.ambientLighting)
		{
			this.fadeOutAmbientLight = false;
			this.fadeInAmbientLight = true;
		}
		if (this.skybox)
		{
			this.fadeOutSkybox = false;
			this.fadeInSkybox = true;
		}
		if (this.ambientAudio)
		{
			EnviroSky.instance.Audio.ambientSFXVolumeMod = this.ambientVolume;
		}
		if (this.weatherAudio)
		{
			EnviroSky.instance.Audio.weatherSFXVolumeMod = this.weatherVolume;
		}
		if (this.zoneAudioClip != null)
		{
			EnviroSky.instance.currentInteriorZoneAudioFadingSpeed = this.zoneAudioFadingSpeed;
			EnviroSky.instance.AudioSourceZone.FadeIn(this.zoneAudioClip);
			EnviroSky.instance.currentInteriorZoneAudioVolume = this.zoneAudioVolume;
		}
		if (this.fog)
		{
			this.fadeOutFog = false;
			this.fadeInFog = true;
		}
		if (this.fogColor)
		{
			this.fadeOutFogColor = false;
			this.fadeInFogColor = true;
		}
		if (this.weatherEffects)
		{
			this.fadeOutWeather = false;
			this.fadeInWeather = true;
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00056FF4 File Offset: 0x000553F4
	public void Exit()
	{
		EnviroSky.instance.interiorMode = false;
		if (this.directLighting)
		{
			this.fadeInDirectLight = false;
			this.fadeOutDirectLight = true;
		}
		if (this.ambientLighting)
		{
			this.fadeOutAmbientLight = true;
			this.fadeInAmbientLight = false;
		}
		if (this.skybox)
		{
			this.fadeOutSkybox = true;
			this.fadeInSkybox = false;
		}
		if (this.ambientAudio)
		{
			EnviroSky.instance.Audio.ambientSFXVolumeMod = 0f;
		}
		if (this.weatherAudio)
		{
			EnviroSky.instance.Audio.weatherSFXVolumeMod = 0f;
		}
		if (this.zoneAudioClip != null)
		{
			EnviroSky.instance.currentInteriorZoneAudioFadingSpeed = this.zoneAudioFadingSpeed;
			EnviroSky.instance.AudioSourceZone.FadeOut();
		}
		if (this.fog)
		{
			this.fadeOutFog = true;
			this.fadeInFog = false;
		}
		if (this.fogColor)
		{
			this.fadeOutFogColor = true;
			this.fadeInFogColor = false;
		}
		if (this.weatherEffects)
		{
			this.fadeOutWeather = true;
			this.fadeInWeather = false;
		}
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x00057110 File Offset: 0x00055510
	public void StopAllFading()
	{
		if (this.directLighting)
		{
			this.fadeInDirectLight = false;
			this.fadeOutDirectLight = false;
		}
		if (this.ambientLighting)
		{
			this.fadeOutAmbientLight = false;
			this.fadeInAmbientLight = false;
		}
		if (this.zoneAudioClip != null)
		{
			EnviroSky.instance.AudioSourceZone.FadeOut();
		}
		if (this.skybox)
		{
			this.fadeOutSkybox = false;
			this.fadeInSkybox = false;
		}
		if (this.fog)
		{
			this.fadeOutFog = false;
			this.fadeInFog = false;
		}
		if (this.fogColor)
		{
			this.fadeOutFogColor = false;
			this.fadeInFogColor = false;
		}
		if (this.weatherEffects)
		{
			this.fadeOutWeather = false;
			this.fadeInWeather = false;
		}
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x000571D4 File Offset: 0x000555D4
	private void Update()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this.directLighting)
		{
			if (this.fadeInDirectLight)
			{
				this.curDirectLightingMod = Color.Lerp(this.curDirectLightingMod, this.directLightingMod, this.directLightFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorDirectLightMod = this.curDirectLightingMod;
				if (this.curDirectLightingMod == this.directLightingMod)
				{
					this.fadeInDirectLight = false;
				}
			}
			else if (this.fadeOutDirectLight)
			{
				this.curDirectLightingMod = Color.Lerp(this.curDirectLightingMod, this.fadeOutColor, this.directLightFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorDirectLightMod = this.curDirectLightingMod;
				if (this.curDirectLightingMod == this.fadeOutColor)
				{
					this.fadeOutDirectLight = false;
				}
			}
		}
		if (this.ambientLighting)
		{
			if (this.fadeInAmbientLight)
			{
				this.curAmbientLightingMod = Color.Lerp(this.curAmbientLightingMod, this.ambientLightingMod, this.ambientLightFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorAmbientLightMod = this.curAmbientLightingMod;
				if (EnviroSky.instance.lightSettings.ambientMode == AmbientMode.Trilight)
				{
					this.curAmbientEQLightingMod = Color.Lerp(this.curAmbientEQLightingMod, this.ambientEQLightingMod, this.ambientLightFadeSpeed * Time.deltaTime);
					EnviroSky.instance.currentInteriorAmbientEQLightMod = this.curAmbientEQLightingMod;
					this.curAmbientGRLightingMod = Color.Lerp(this.curAmbientGRLightingMod, this.ambientGRLightingMod, this.ambientLightFadeSpeed * Time.deltaTime);
					EnviroSky.instance.currentInteriorAmbientGRLightMod = this.curAmbientGRLightingMod;
				}
				if (this.curAmbientLightingMod == this.ambientLightingMod)
				{
					this.fadeInAmbientLight = false;
				}
			}
			else if (this.fadeOutAmbientLight)
			{
				this.curAmbientLightingMod = Color.Lerp(this.curAmbientLightingMod, this.fadeOutColor, 2f * Time.deltaTime);
				EnviroSky.instance.currentInteriorAmbientLightMod = this.curAmbientLightingMod;
				if (EnviroSky.instance.lightSettings.ambientMode == AmbientMode.Trilight)
				{
					this.curAmbientEQLightingMod = Color.Lerp(this.curAmbientEQLightingMod, this.fadeOutColor, 2f * Time.deltaTime);
					EnviroSky.instance.currentInteriorAmbientEQLightMod = this.curAmbientEQLightingMod;
					this.curAmbientGRLightingMod = Color.Lerp(this.curAmbientGRLightingMod, this.fadeOutColor, 2f * Time.deltaTime);
					EnviroSky.instance.currentInteriorAmbientGRLightMod = this.curAmbientGRLightingMod;
				}
				if (this.curAmbientLightingMod == this.fadeOutColor)
				{
					this.fadeOutAmbientLight = false;
				}
			}
		}
		if (this.skybox)
		{
			if (this.fadeInSkybox)
			{
				this.curskyboxColorMod = Color.Lerp(this.curskyboxColorMod, this.skyboxColorMod, this.skyboxFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorSkyboxMod = this.curskyboxColorMod;
				if (this.curskyboxColorMod == this.skyboxColorMod)
				{
					this.fadeInSkybox = false;
				}
			}
			else if (this.fadeOutSkybox)
			{
				this.curskyboxColorMod = Color.Lerp(this.curskyboxColorMod, this.fadeOutColor, this.skyboxFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorSkyboxMod = this.curskyboxColorMod;
				if (this.curskyboxColorMod == this.fadeOutColor)
				{
					this.fadeOutSkybox = false;
				}
			}
		}
		if (this.fog)
		{
			if (this.fadeInFog)
			{
				EnviroSky.instance.currentInteriorFogMod = Mathf.Lerp(EnviroSky.instance.currentInteriorFogMod, this.minFogMod, this.fogFadeSpeed * Time.deltaTime);
				if ((double)EnviroSky.instance.currentInteriorFogMod <= (double)this.minFogMod + 0.001)
				{
					this.fadeInFog = false;
				}
			}
			else if (this.fadeOutFog)
			{
				EnviroSky.instance.currentInteriorFogMod = Mathf.Lerp(EnviroSky.instance.currentInteriorFogMod, 1f, this.fogFadeSpeed * 2f * Time.deltaTime);
				if ((double)EnviroSky.instance.currentInteriorFogMod >= 0.999)
				{
					this.fadeOutFog = false;
				}
			}
		}
		if (this.fogColor)
		{
			if (this.fadeInFogColor)
			{
				this.curFogColorMod = Color.Lerp(this.curFogColorMod, this.fogColorMod, this.fogFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorFogColorMod = this.curFogColorMod;
				if (this.curFogColorMod == this.fogColorMod)
				{
					this.fadeInFogColor = false;
				}
			}
			else if (this.fadeOutFogColor)
			{
				this.curFogColorMod = Color.Lerp(this.curFogColorMod, this.fadeOutColor, this.fogFadeSpeed * Time.deltaTime);
				EnviroSky.instance.currentInteriorFogColorMod = this.curFogColorMod;
				if (this.curFogColorMod == this.fadeOutColor)
				{
					this.fadeOutFogColor = false;
				}
			}
		}
		if (this.weatherEffects)
		{
			if (this.fadeInWeather)
			{
				EnviroSky.instance.currentInteriorWeatherEffectMod = Mathf.Lerp(EnviroSky.instance.currentInteriorWeatherEffectMod, 0f, this.weatherFadeSpeed * Time.deltaTime);
				if ((double)EnviroSky.instance.currentInteriorWeatherEffectMod <= 0.001)
				{
					this.fadeInWeather = false;
				}
			}
			else if (this.fadeOutWeather)
			{
				EnviroSky.instance.currentInteriorWeatherEffectMod = Mathf.Lerp(EnviroSky.instance.currentInteriorWeatherEffectMod, 1f, this.weatherFadeSpeed * 2f * Time.deltaTime);
				if ((double)EnviroSky.instance.currentInteriorWeatherEffectMod >= 0.999)
				{
					this.fadeOutWeather = false;
				}
			}
		}
	}

	// Token: 0x04001112 RID: 4370
	public EnviroInterior.ZoneTriggerType zoneTriggerType;

	// Token: 0x04001113 RID: 4371
	public bool directLighting;

	// Token: 0x04001114 RID: 4372
	public bool ambientLighting;

	// Token: 0x04001115 RID: 4373
	public bool weatherAudio;

	// Token: 0x04001116 RID: 4374
	public bool ambientAudio;

	// Token: 0x04001117 RID: 4375
	public bool fog;

	// Token: 0x04001118 RID: 4376
	public bool fogColor;

	// Token: 0x04001119 RID: 4377
	public bool skybox;

	// Token: 0x0400111A RID: 4378
	public bool weatherEffects;

	// Token: 0x0400111B RID: 4379
	public Color directLightingMod = Color.black;

	// Token: 0x0400111C RID: 4380
	public Color ambientLightingMod = Color.black;

	// Token: 0x0400111D RID: 4381
	public Color ambientEQLightingMod = Color.black;

	// Token: 0x0400111E RID: 4382
	public Color ambientGRLightingMod = Color.black;

	// Token: 0x0400111F RID: 4383
	private Color curDirectLightingMod;

	// Token: 0x04001120 RID: 4384
	private Color curAmbientLightingMod;

	// Token: 0x04001121 RID: 4385
	private Color curAmbientEQLightingMod;

	// Token: 0x04001122 RID: 4386
	private Color curAmbientGRLightingMod;

	// Token: 0x04001123 RID: 4387
	public float directLightFadeSpeed = 2f;

	// Token: 0x04001124 RID: 4388
	public float ambientLightFadeSpeed = 2f;

	// Token: 0x04001125 RID: 4389
	public Color skyboxColorMod = Color.black;

	// Token: 0x04001126 RID: 4390
	private Color curskyboxColorMod;

	// Token: 0x04001127 RID: 4391
	public float skyboxFadeSpeed = 2f;

	// Token: 0x04001128 RID: 4392
	private bool fadeInDirectLight;

	// Token: 0x04001129 RID: 4393
	private bool fadeOutDirectLight;

	// Token: 0x0400112A RID: 4394
	private bool fadeInAmbientLight;

	// Token: 0x0400112B RID: 4395
	private bool fadeOutAmbientLight;

	// Token: 0x0400112C RID: 4396
	private bool fadeInSkybox;

	// Token: 0x0400112D RID: 4397
	private bool fadeOutSkybox;

	// Token: 0x0400112E RID: 4398
	public float ambientVolume;

	// Token: 0x0400112F RID: 4399
	public float weatherVolume;

	// Token: 0x04001130 RID: 4400
	public AudioClip zoneAudioClip;

	// Token: 0x04001131 RID: 4401
	public float zoneAudioVolume = 1f;

	// Token: 0x04001132 RID: 4402
	public float zoneAudioFadingSpeed = 1f;

	// Token: 0x04001133 RID: 4403
	public Color fogColorMod = Color.black;

	// Token: 0x04001134 RID: 4404
	private Color curFogColorMod;

	// Token: 0x04001135 RID: 4405
	public float fogFadeSpeed = 2f;

	// Token: 0x04001136 RID: 4406
	public float minFogMod;

	// Token: 0x04001137 RID: 4407
	private bool fadeInFog;

	// Token: 0x04001138 RID: 4408
	private bool fadeOutFog;

	// Token: 0x04001139 RID: 4409
	private bool fadeInFogColor;

	// Token: 0x0400113A RID: 4410
	private bool fadeOutFogColor;

	// Token: 0x0400113B RID: 4411
	public float weatherFadeSpeed = 2f;

	// Token: 0x0400113C RID: 4412
	private bool fadeInWeather;

	// Token: 0x0400113D RID: 4413
	private bool fadeOutWeather;

	// Token: 0x0400113E RID: 4414
	public List<EnviroTrigger> triggers = new List<EnviroTrigger>();

	// Token: 0x0400113F RID: 4415
	private Color fadeOutColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x0200036F RID: 879
	public enum ZoneTriggerType
	{
		// Token: 0x04001141 RID: 4417
		Entry_Exit,
		// Token: 0x04001142 RID: 4418
		Zone
	}
}

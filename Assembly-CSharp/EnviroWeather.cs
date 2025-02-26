using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000347 RID: 839
[Serializable]
public class EnviroWeather
{
	// Token: 0x04000F49 RID: 3913
	[Tooltip("If disabled the weather will never change.")]
	public bool updateWeather = true;

	// Token: 0x04000F4A RID: 3914
	[HideInInspector]
	public List<EnviroWeatherPreset> weatherPresets = new List<EnviroWeatherPreset>();

	// Token: 0x04000F4B RID: 3915
	[HideInInspector]
	public List<EnviroWeatherPrefab> WeatherPrefabs = new List<EnviroWeatherPrefab>();

	// Token: 0x04000F4C RID: 3916
	[Tooltip("List of additional zones. Will be updated on startup!")]
	public List<EnviroZone> zones = new List<EnviroZone>();

	// Token: 0x04000F4D RID: 3917
	public EnviroWeatherPreset startWeatherPreset;

	// Token: 0x04000F4E RID: 3918
	[Tooltip("The current active zone.")]
	public EnviroZone currentActiveZone;

	// Token: 0x04000F4F RID: 3919
	[Tooltip("The current active weather conditions.")]
	public EnviroWeatherPrefab currentActiveWeatherPrefab;

	// Token: 0x04000F50 RID: 3920
	public EnviroWeatherPreset currentActiveWeatherPreset;

	// Token: 0x04000F51 RID: 3921
	[HideInInspector]
	public EnviroWeatherPrefab lastActiveWeatherPrefab;

	// Token: 0x04000F52 RID: 3922
	[HideInInspector]
	public EnviroWeatherPreset lastActiveWeatherPreset;

	// Token: 0x04000F53 RID: 3923
	[HideInInspector]
	public GameObject VFXHolder;

	// Token: 0x04000F54 RID: 3924
	[HideInInspector]
	public float wetness;

	// Token: 0x04000F55 RID: 3925
	[HideInInspector]
	public float curWetness;

	// Token: 0x04000F56 RID: 3926
	[HideInInspector]
	public float snowStrength;

	// Token: 0x04000F57 RID: 3927
	[HideInInspector]
	public float curSnowStrength;

	// Token: 0x04000F58 RID: 3928
	[HideInInspector]
	public int thundersfx;

	// Token: 0x04000F59 RID: 3929
	[HideInInspector]
	public EnviroAudioSource currentAudioSource;

	// Token: 0x04000F5A RID: 3930
	[HideInInspector]
	public bool weatherFullyChanged;
}

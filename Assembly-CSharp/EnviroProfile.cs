using System;
using UnityEngine;

// Token: 0x0200033F RID: 831
[Serializable]
public class EnviroProfile : ScriptableObject
{
	// Token: 0x04000F0C RID: 3852
	public string version;

	// Token: 0x04000F0D RID: 3853
	public EnviroLightSettings lightSettings = new EnviroLightSettings();

	// Token: 0x04000F0E RID: 3854
	public EnviroVolumeLightingSettings volumeLightSettings = new EnviroVolumeLightingSettings();

	// Token: 0x04000F0F RID: 3855
	public EnviroSkySettings skySettings = new EnviroSkySettings();

	// Token: 0x04000F10 RID: 3856
	public EnviroCloudSettings cloudsSettings = new EnviroCloudSettings();

	// Token: 0x04000F11 RID: 3857
	public EnviroWeatherSettings weatherSettings = new EnviroWeatherSettings();

	// Token: 0x04000F12 RID: 3858
	public EnviroFogSettings fogSettings = new EnviroFogSettings();

	// Token: 0x04000F13 RID: 3859
	public EnviroLightShaftsSettings lightshaftsSettings = new EnviroLightShaftsSettings();

	// Token: 0x04000F14 RID: 3860
	public EnviroSeasonSettings seasonsSettings = new EnviroSeasonSettings();

	// Token: 0x04000F15 RID: 3861
	public EnviroAudioSettings audioSettings = new EnviroAudioSettings();

	// Token: 0x04000F16 RID: 3862
	public EnviroSatellitesSettings satelliteSettings = new EnviroSatellitesSettings();

	// Token: 0x04000F17 RID: 3863
	public EnviroQualitySettings qualitySettings = new EnviroQualitySettings();

	// Token: 0x04000F18 RID: 3864
	[HideInInspector]
	public EnviroProfile.settingsMode viewMode;

	// Token: 0x04000F19 RID: 3865
	[HideInInspector]
	public bool showPlayerSetup = true;

	// Token: 0x04000F1A RID: 3866
	[HideInInspector]
	public bool showRenderingSetup;

	// Token: 0x04000F1B RID: 3867
	[HideInInspector]
	public bool showComponentsSetup;

	// Token: 0x04000F1C RID: 3868
	[HideInInspector]
	public bool showTimeUI;

	// Token: 0x04000F1D RID: 3869
	[HideInInspector]
	public bool showWeatherUI;

	// Token: 0x04000F1E RID: 3870
	[HideInInspector]
	public bool showAudioUI;

	// Token: 0x04000F1F RID: 3871
	[HideInInspector]
	public bool showEffectsUI;

	// Token: 0x04000F20 RID: 3872
	[HideInInspector]
	public bool modified;

	// Token: 0x02000340 RID: 832
	public enum settingsMode
	{
		// Token: 0x04000F22 RID: 3874
		Lighting,
		// Token: 0x04000F23 RID: 3875
		Sky,
		// Token: 0x04000F24 RID: 3876
		Weather,
		// Token: 0x04000F25 RID: 3877
		Clouds,
		// Token: 0x04000F26 RID: 3878
		Fog,
		// Token: 0x04000F27 RID: 3879
		VolumeLighting,
		// Token: 0x04000F28 RID: 3880
		Lightshafts,
		// Token: 0x04000F29 RID: 3881
		Season,
		// Token: 0x04000F2A RID: 3882
		Satellites,
		// Token: 0x04000F2B RID: 3883
		Audio,
		// Token: 0x04000F2C RID: 3884
		Quality
	}
}

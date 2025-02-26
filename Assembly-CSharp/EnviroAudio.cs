using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
[Serializable]
public class EnviroAudio
{
	// Token: 0x04000F35 RID: 3893
	[Tooltip("The prefab with AudioSources used by Enviro. Will be instantiated at runtime.")]
	public GameObject SFXHolderPrefab;

	// Token: 0x04000F36 RID: 3894
	[Header("Volume Settings:")]
	[Range(0f, 1f)]
	[Tooltip("The volume of ambient sounds played by enviro.")]
	public float ambientSFXVolume = 0.5f;

	// Token: 0x04000F37 RID: 3895
	[Range(0f, 1f)]
	[Tooltip("The volume of weather sounds played by enviro.")]
	public float weatherSFXVolume = 1f;

	// Token: 0x04000F38 RID: 3896
	[HideInInspector]
	public EnviroAudioSource currentAmbientSource;

	// Token: 0x04000F39 RID: 3897
	[HideInInspector]
	public float ambientSFXVolumeMod;

	// Token: 0x04000F3A RID: 3898
	[HideInInspector]
	public float weatherSFXVolumeMod;
}

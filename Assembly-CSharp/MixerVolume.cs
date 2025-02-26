using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02001142 RID: 4418
public static class MixerVolume
{
	// Token: 0x06009233 RID: 37427 RVA: 0x003CA434 File Offset: 0x003C8834
	public static void Set(AudioMixer mixer, MixerVolume.Names name, float volume)
	{
		float value = 20f * Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f));
		mixer.SetFloat(name.ToString(), Mathf.Clamp(value, -80f, 0f));
	}

	// Token: 0x06009234 RID: 37428 RVA: 0x003CA484 File Offset: 0x003C8884
	public static float Get(AudioMixer mixer, MixerVolume.Names name)
	{
		float value = 0f;
		if (!mixer.GetFloat(name.ToString(), out value))
		{
			return 0f;
		}
		return Mathf.InverseLerp(-80f, 0f, value);
	}

	// Token: 0x02001143 RID: 4419
	public enum Names
	{
		// Token: 0x04007647 RID: 30279
		MasterVolume,
		// Token: 0x04007648 RID: 30280
		BGMVolume,
		// Token: 0x04007649 RID: 30281
		PCMVolume,
		// Token: 0x0400764A RID: 30282
		ENVVolume,
		// Token: 0x0400764B RID: 30283
		GameSEVolume,
		// Token: 0x0400764C RID: 30284
		SystemSEVolume
	}
}

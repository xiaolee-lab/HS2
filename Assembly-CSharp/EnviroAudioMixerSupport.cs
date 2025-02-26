using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000369 RID: 873
[AddComponentMenu("Enviro/Utility/Audio Mixer Support")]
public class EnviroAudioMixerSupport : MonoBehaviour
{
	// Token: 0x06000F75 RID: 3957 RVA: 0x00055557 File Offset: 0x00053957
	private void Start()
	{
		if (this.audioMixer != null && EnviroSky.instance != null)
		{
			base.StartCoroutine(this.Setup());
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00055588 File Offset: 0x00053988
	private IEnumerator Setup()
	{
		yield return 0;
		if (EnviroSky.instance.started)
		{
			if (this.ambientMixerGroup != string.Empty)
			{
				EnviroSky.instance.AudioSourceAmbient.audiosrc.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups(this.ambientMixerGroup)[0];
				EnviroSky.instance.AudioSourceAmbient2.audiosrc.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups(this.ambientMixerGroup)[0];
			}
			if (this.weatherMixerGroup != string.Empty)
			{
				EnviroSky.instance.AudioSourceWeather.audiosrc.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups(this.weatherMixerGroup)[0];
				EnviroSky.instance.AudioSourceWeather2.audiosrc.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups(this.weatherMixerGroup)[0];
			}
			if (this.thunderMixerGroup != string.Empty)
			{
				EnviroSky.instance.AudioSourceThunder.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups(this.thunderMixerGroup)[0];
			}
		}
		else
		{
			base.StartCoroutine(this.Setup());
		}
		yield break;
	}

	// Token: 0x040010DF RID: 4319
	[Header("Mixer")]
	public AudioMixer audioMixer;

	// Token: 0x040010E0 RID: 4320
	[Header("Group Names")]
	public string ambientMixerGroup;

	// Token: 0x040010E1 RID: 4321
	public string weatherMixerGroup;

	// Token: 0x040010E2 RID: 4322
	public string thunderMixerGroup;
}

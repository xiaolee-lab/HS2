using System;
using UnityEngine;

namespace MorphAssist
{
	// Token: 0x02001111 RID: 4369
	public class AudioAssist
	{
		// Token: 0x060090EE RID: 37102 RVA: 0x003C55E0 File Offset: 0x003C39E0
		private float RMS(float[] samples)
		{
			float num = 0f;
			for (int i = 0; i < samples.Length; i++)
			{
				num += samples[i] * samples[i];
			}
			num /= (float)samples.Length;
			return Mathf.Sqrt(num);
		}

		// Token: 0x060090EF RID: 37103 RVA: 0x003C5620 File Offset: 0x003C3A20
		public float GetAudioWaveValue(AudioSource audioSource)
		{
			float num = 0f;
			if (!audioSource.clip)
			{
				return num;
			}
			if (audioSource.isPlaying)
			{
				float[] samples = new float[256];
				float max = 1f;
				audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
				float num2 = this.RMS(samples);
				num = Mathf.Clamp(num2 * 50f, 0f, max);
				num = (num + this.beforeVolume) * 0.5f;
				this.beforeVolume = num;
			}
			return num;
		}

		// Token: 0x04007579 RID: 30073
		private float beforeVolume;
	}
}

using System;
using UnityEngine;

namespace FBSAssist
{
	// Token: 0x020010F0 RID: 4336
	public class AudioAssist
	{
		// Token: 0x06008FDD RID: 36829 RVA: 0x003BF26C File Offset: 0x003BD66C
		private float RMS(ref float[] samples)
		{
			float num = 0f;
			for (int i = 0; i < samples.Length; i++)
			{
				num += samples[i] * samples[i];
			}
			num /= (float)samples.Length;
			return Mathf.Sqrt(num);
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x003BF2B0 File Offset: 0x003BD6B0
		public float GetAudioWaveValue(AudioSource audioSource, float correct = 2f)
		{
			float result = 0f;
			if (!audioSource.clip)
			{
				return result;
			}
			if (audioSource.isPlaying)
			{
				float max = 1f;
				int num = audioSource.clip.samples * audioSource.clip.channels - audioSource.timeSamples;
				if (num <= 1024)
				{
					return result;
				}
				audioSource.clip.GetData(this._samples, audioSource.timeSamples);
				float num2 = this.RMS(ref this._samples);
				float num3 = Mathf.Clamp(num2 * correct, 0f, max);
				if (num3 < this.beforeVolume)
				{
					result = num3 * 0.2f + this.beforeVolume * 0.8f;
				}
				else
				{
					result = (num3 + this.beforeVolume) * 0.5f;
				}
				this.beforeVolume = num3;
			}
			return result;
		}

		// Token: 0x040074AA RID: 29866
		private float beforeVolume;

		// Token: 0x040074AB RID: 29867
		private float[] _samples = new float[1024];
	}
}

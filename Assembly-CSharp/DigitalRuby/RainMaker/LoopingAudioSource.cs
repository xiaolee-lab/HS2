using System;
using UnityEngine;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E3 RID: 1251
	public class LoopingAudioSource
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00092494 File Offset: 0x00090894
		public LoopingAudioSource(MonoBehaviour script, AudioClip clip)
		{
			this.AudioSource = script.gameObject.AddComponent<AudioSource>();
			this.AudioSource.loop = true;
			this.AudioSource.clip = clip;
			this.AudioSource.playOnAwake = false;
			this.AudioSource.volume = 0f;
			this.AudioSource.Stop();
			this.TargetVolume = 1f;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00092502 File Offset: 0x00090902
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0009250A File Offset: 0x0009090A
		public AudioSource AudioSource { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00092513 File Offset: 0x00090913
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x0009251B File Offset: 0x0009091B
		public float TargetVolume { get; private set; }

		// Token: 0x06001735 RID: 5941 RVA: 0x00092524 File Offset: 0x00090924
		public void Play(float targetVolume)
		{
			if (!this.AudioSource.isPlaying)
			{
				this.AudioSource.volume = 0f;
				this.AudioSource.Play();
			}
			this.TargetVolume = targetVolume;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00092558 File Offset: 0x00090958
		public void Stop()
		{
			this.TargetVolume = 0f;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00092568 File Offset: 0x00090968
		public void Update()
		{
			if (this.AudioSource.isPlaying)
			{
				float num = Mathf.Lerp(this.AudioSource.volume, this.TargetVolume, Time.deltaTime);
				this.AudioSource.volume = num;
				if (num == 0f)
				{
					this.AudioSource.Stop();
				}
			}
		}
	}
}

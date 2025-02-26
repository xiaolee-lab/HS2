using System;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class EnviroAudioSource : MonoBehaviour
{
	// Token: 0x06000F78 RID: 3960 RVA: 0x0005579C File Offset: 0x00053B9C
	private void Start()
	{
		if (this.audiosrc == null)
		{
			this.audiosrc = base.GetComponent<AudioSource>();
		}
		if (this.myFunction == EnviroAudioSource.AudioSourceFunction.Weather1 || this.myFunction == EnviroAudioSource.AudioSourceFunction.Weather2)
		{
			this.audiosrc.loop = true;
			this.audiosrc.volume = 0f;
		}
		this.currentAmbientVolume = EnviroSky.instance.Audio.ambientSFXVolume;
		this.currentWeatherVolume = EnviroSky.instance.Audio.weatherSFXVolume;
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00055823 File Offset: 0x00053C23
	public void FadeOut()
	{
		this.isFadingOut = true;
		this.isFadingIn = false;
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00055833 File Offset: 0x00053C33
	public void FadeIn(AudioClip clip)
	{
		this.isFadingIn = true;
		this.isFadingOut = false;
		this.audiosrc.clip = clip;
		this.audiosrc.Play();
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0005585C File Offset: 0x00053C5C
	private void Update()
	{
		if (!EnviroSky.instance.started || EnviroSky.instance == null)
		{
			return;
		}
		this.currentAmbientVolume = Mathf.Lerp(this.currentAmbientVolume, EnviroSky.instance.Audio.ambientSFXVolume + EnviroSky.instance.Audio.ambientSFXVolumeMod, 10f * Time.deltaTime);
		this.currentWeatherVolume = Mathf.Lerp(this.currentWeatherVolume, EnviroSky.instance.Audio.weatherSFXVolume + EnviroSky.instance.Audio.weatherSFXVolumeMod, 10f * Time.deltaTime);
		if (this.myFunction == EnviroAudioSource.AudioSourceFunction.Weather1 || this.myFunction == EnviroAudioSource.AudioSourceFunction.Weather2 || this.myFunction == EnviroAudioSource.AudioSourceFunction.Thunder)
		{
			if (this.isFadingIn && this.audiosrc.volume < this.currentWeatherVolume)
			{
				this.audiosrc.volume += EnviroSky.instance.weatherSettings.audioTransitionSpeed * Time.deltaTime;
			}
			else if (this.isFadingIn && this.audiosrc.volume >= this.currentWeatherVolume - 0.01f)
			{
				this.isFadingIn = false;
			}
			if (this.isFadingOut && this.audiosrc.volume > 0f)
			{
				this.audiosrc.volume -= EnviroSky.instance.weatherSettings.audioTransitionSpeed * Time.deltaTime;
			}
			else if (this.isFadingOut && this.audiosrc.volume == 0f)
			{
				this.audiosrc.Stop();
				this.isFadingOut = false;
			}
			if (this.audiosrc.isPlaying && !this.isFadingOut && !this.isFadingIn)
			{
				this.audiosrc.volume = this.currentWeatherVolume;
			}
		}
		else if (this.myFunction == EnviroAudioSource.AudioSourceFunction.Ambient || this.myFunction == EnviroAudioSource.AudioSourceFunction.Ambient2)
		{
			if (this.isFadingIn && this.audiosrc.volume < this.currentAmbientVolume)
			{
				this.audiosrc.volume += EnviroSky.instance.weatherSettings.audioTransitionSpeed * Time.deltaTime;
			}
			else if (this.isFadingIn && this.audiosrc.volume >= this.currentAmbientVolume - 0.01f)
			{
				this.isFadingIn = false;
			}
			if (this.isFadingOut && this.audiosrc.volume > 0f)
			{
				this.audiosrc.volume -= EnviroSky.instance.weatherSettings.audioTransitionSpeed * Time.deltaTime;
			}
			else if (this.isFadingOut && this.audiosrc.volume == 0f)
			{
				this.audiosrc.Stop();
				this.isFadingOut = false;
			}
			if (this.audiosrc.isPlaying && !this.isFadingOut && !this.isFadingIn)
			{
				this.audiosrc.volume = this.currentAmbientVolume;
			}
		}
		else if (this.myFunction == EnviroAudioSource.AudioSourceFunction.ZoneAmbient)
		{
			if (this.isFadingIn && this.audiosrc.volume < EnviroSky.instance.currentInteriorZoneAudioVolume)
			{
				this.audiosrc.volume += EnviroSky.instance.currentInteriorZoneAudioFadingSpeed * Time.deltaTime;
			}
			else if (this.isFadingIn && this.audiosrc.volume >= EnviroSky.instance.currentInteriorZoneAudioVolume - 0.01f)
			{
				this.isFadingIn = false;
			}
			if (this.isFadingOut && this.audiosrc.volume > 0f)
			{
				this.audiosrc.volume -= EnviroSky.instance.currentInteriorZoneAudioFadingSpeed * Time.deltaTime;
			}
			else if (this.isFadingOut && this.audiosrc.volume == 0f)
			{
				this.audiosrc.Stop();
				this.isFadingOut = false;
			}
			if (this.audiosrc.isPlaying && !this.isFadingOut && !this.isFadingIn)
			{
				this.audiosrc.volume = EnviroSky.instance.currentInteriorZoneAudioVolume;
			}
		}
	}

	// Token: 0x040010E3 RID: 4323
	public EnviroAudioSource.AudioSourceFunction myFunction;

	// Token: 0x040010E4 RID: 4324
	public AudioSource audiosrc;

	// Token: 0x040010E5 RID: 4325
	public bool isFadingIn;

	// Token: 0x040010E6 RID: 4326
	public bool isFadingOut;

	// Token: 0x040010E7 RID: 4327
	private float currentAmbientVolume;

	// Token: 0x040010E8 RID: 4328
	private float currentWeatherVolume;

	// Token: 0x040010E9 RID: 4329
	private float currentZoneVolume;

	// Token: 0x0200036B RID: 875
	public enum AudioSourceFunction
	{
		// Token: 0x040010EB RID: 4331
		Weather1,
		// Token: 0x040010EC RID: 4332
		Weather2,
		// Token: 0x040010ED RID: 4333
		Ambient,
		// Token: 0x040010EE RID: 4334
		Ambient2,
		// Token: 0x040010EF RID: 4335
		Thunder,
		// Token: 0x040010F0 RID: 4336
		ZoneAmbient
	}
}

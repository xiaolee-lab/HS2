using System;
using UnityEngine;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E2 RID: 1250
	public class BaseRainScript : MonoBehaviour
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x00091D98 File Offset: 0x00090198
		private void UpdateWind()
		{
			if (this.EnableWind)
			{
				if (this.WindZone != null)
				{
					this.WindZone.gameObject.SetActive(true);
					if (this.FollowCamera)
					{
						this.WindZone.transform.position = this.Camera.transform.position;
					}
					if (!this.Camera.orthographic)
					{
						this.WindZone.transform.Translate(0f, this.WindZone.radius, 0f);
					}
					if (this.nextWindTime < Time.time)
					{
						this.WindZone.windMain = UnityEngine.Random.Range(this.WindSpeedRange.x, this.WindSpeedRange.y);
						this.WindZone.windTurbulence = UnityEngine.Random.Range(this.WindSpeedRange.x, this.WindSpeedRange.y);
						if (this.Camera.orthographic)
						{
							int num = UnityEngine.Random.Range(0, 2);
							this.WindZone.transform.rotation = Quaternion.Euler(0f, (num != 0) ? -90f : 90f, 0f);
						}
						else
						{
							this.WindZone.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(0f, 360f), 0f);
						}
						this.nextWindTime = Time.time + UnityEngine.Random.Range(this.WindChangeInterval.x, this.WindChangeInterval.y);
						this.audioSourceWind.Play(this.WindZone.windMain / this.WindSpeedRange.z * this.WindSoundVolumeModifier);
					}
				}
			}
			else
			{
				if (this.WindZone != null)
				{
					this.WindZone.gameObject.SetActive(false);
				}
				this.audioSourceWind.Stop();
			}
			this.audioSourceWind.Update();
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00091FA4 File Offset: 0x000903A4
		private void CheckForRainChange()
		{
			if (this.lastRainIntensityValue != this.RainIntensity)
			{
				this.lastRainIntensityValue = this.RainIntensity;
				if (this.RainIntensity <= 0.01f)
				{
					if (this.audioSourceRainCurrent != null)
					{
						this.audioSourceRainCurrent.Stop();
						this.audioSourceRainCurrent = null;
					}
					if (this.RainFallParticleSystem != null)
					{
						this.RainFallParticleSystem.emission.enabled = false;
						this.RainFallParticleSystem.Stop();
					}
					if (this.RainMistParticleSystem != null)
					{
						this.RainMistParticleSystem.emission.enabled = false;
						this.RainMistParticleSystem.Stop();
					}
				}
				else
				{
					LoopingAudioSource loopingAudioSource;
					if (this.RainIntensity >= 0.67f)
					{
						loopingAudioSource = this.audioSourceRainHeavy;
					}
					else if (this.RainIntensity >= 0.33f)
					{
						loopingAudioSource = this.audioSourceRainMedium;
					}
					else
					{
						loopingAudioSource = this.audioSourceRainLight;
					}
					if (this.audioSourceRainCurrent != loopingAudioSource)
					{
						if (this.audioSourceRainCurrent != null)
						{
							this.audioSourceRainCurrent.Stop();
						}
						this.audioSourceRainCurrent = loopingAudioSource;
						this.audioSourceRainCurrent.Play(1f);
					}
					if (this.RainFallParticleSystem != null)
					{
						ParticleSystem.EmissionModule emission = this.RainFallParticleSystem.emission;
						bool enabled = true;
						this.RainFallParticleSystem.GetComponent<Renderer>().enabled = enabled;
						emission.enabled = enabled;
						if (!this.RainFallParticleSystem.isPlaying)
						{
							this.RainFallParticleSystem.Play();
						}
						ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
						rateOverTime.mode = ParticleSystemCurveMode.Constant;
						float num = this.RainFallEmissionRate();
						rateOverTime.constantMax = num;
						rateOverTime.constantMin = num;
						emission.rateOverTime = rateOverTime;
					}
					if (this.RainMistParticleSystem != null)
					{
						ParticleSystem.EmissionModule emission2 = this.RainMistParticleSystem.emission;
						bool enabled = true;
						this.RainMistParticleSystem.GetComponent<Renderer>().enabled = enabled;
						emission2.enabled = enabled;
						if (!this.RainMistParticleSystem.isPlaying)
						{
							this.RainMistParticleSystem.Play();
						}
						float num2;
						if (this.RainIntensity < this.RainMistThreshold)
						{
							num2 = 0f;
						}
						else
						{
							num2 = this.MistEmissionRate();
						}
						ParticleSystem.MinMaxCurve rateOverTime2 = emission2.rateOverTime;
						rateOverTime2.mode = ParticleSystemCurveMode.Constant;
						float num = num2;
						rateOverTime2.constantMax = num;
						rateOverTime2.constantMin = num;
						emission2.rateOverTime = rateOverTime2;
					}
				}
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00092210 File Offset: 0x00090610
		protected virtual void Start()
		{
			if (this.Camera == null)
			{
				this.Camera = Camera.main;
			}
			this.audioSourceRainLight = new LoopingAudioSource(this, this.RainSoundLight);
			this.audioSourceRainMedium = new LoopingAudioSource(this, this.RainSoundMedium);
			this.audioSourceRainHeavy = new LoopingAudioSource(this, this.RainSoundHeavy);
			this.audioSourceWind = new LoopingAudioSource(this, this.WindSound);
			if (this.RainFallParticleSystem != null)
			{
				this.RainFallParticleSystem.emission.enabled = false;
				Renderer component = this.RainFallParticleSystem.GetComponent<Renderer>();
				component.enabled = false;
				this.rainMaterial = new Material(component.material);
				this.rainMaterial.EnableKeyword("SOFTPARTICLES_OFF");
				component.material = this.rainMaterial;
			}
			if (this.RainExplosionParticleSystem != null)
			{
				this.RainExplosionParticleSystem.emission.enabled = false;
				Renderer component2 = this.RainExplosionParticleSystem.GetComponent<Renderer>();
				this.rainExplosionMaterial = new Material(component2.material);
				this.rainExplosionMaterial.EnableKeyword("SOFTPARTICLES_OFF");
				component2.material = this.rainExplosionMaterial;
			}
			if (this.RainMistParticleSystem != null)
			{
				this.RainMistParticleSystem.emission.enabled = false;
				Renderer component3 = this.RainMistParticleSystem.GetComponent<Renderer>();
				component3.enabled = false;
				this.rainMistMaterial = new Material(component3.material);
				if (this.UseRainMistSoftParticles)
				{
					this.rainMistMaterial.EnableKeyword("SOFTPARTICLES_ON");
				}
				else
				{
					this.rainMistMaterial.EnableKeyword("SOFTPARTICLES_OFF");
				}
				component3.material = this.rainMistMaterial;
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000923CE File Offset: 0x000907CE
		protected virtual void Update()
		{
			this.CheckForRainChange();
			this.UpdateWind();
			this.audioSourceRainLight.Update();
			this.audioSourceRainMedium.Update();
			this.audioSourceRainHeavy.Update();
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00092400 File Offset: 0x00090800
		protected virtual float RainFallEmissionRate()
		{
			return (float)this.RainFallParticleSystem.main.maxParticles / this.RainFallParticleSystem.main.startLifetime.constant * this.RainIntensity;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00092444 File Offset: 0x00090844
		protected virtual float MistEmissionRate()
		{
			return (float)this.RainMistParticleSystem.main.maxParticles / this.RainMistParticleSystem.main.startLifetime.constant * this.RainIntensity * this.RainIntensity;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0009248F File Offset: 0x0009088F
		protected virtual bool UseRainMistSoftParticles
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A86 RID: 6790
		[Tooltip("Camera the rain should hover over, defaults to main camera")]
		public Camera Camera;

		// Token: 0x04001A87 RID: 6791
		[Tooltip("Whether rain should follow the camera. If false, rain must be moved manually and will not follow the camera.")]
		public bool FollowCamera = true;

		// Token: 0x04001A88 RID: 6792
		[Tooltip("Light rain looping clip")]
		public AudioClip RainSoundLight;

		// Token: 0x04001A89 RID: 6793
		[Tooltip("Medium rain looping clip")]
		public AudioClip RainSoundMedium;

		// Token: 0x04001A8A RID: 6794
		[Tooltip("Heavy rain looping clip")]
		public AudioClip RainSoundHeavy;

		// Token: 0x04001A8B RID: 6795
		[Tooltip("Intensity of rain (0-1)")]
		[Range(0f, 1f)]
		public float RainIntensity;

		// Token: 0x04001A8C RID: 6796
		[Tooltip("Rain particle system")]
		public ParticleSystem RainFallParticleSystem;

		// Token: 0x04001A8D RID: 6797
		[Tooltip("Particles system for when rain hits something")]
		public ParticleSystem RainExplosionParticleSystem;

		// Token: 0x04001A8E RID: 6798
		[Tooltip("Particle system to use for rain mist")]
		public ParticleSystem RainMistParticleSystem;

		// Token: 0x04001A8F RID: 6799
		[Tooltip("The threshold for intensity (0 - 1) at which mist starts to appear")]
		[Range(0f, 1f)]
		public float RainMistThreshold = 0.5f;

		// Token: 0x04001A90 RID: 6800
		[Tooltip("Wind looping clip")]
		public AudioClip WindSound;

		// Token: 0x04001A91 RID: 6801
		[Tooltip("Wind sound volume modifier, use this to lower your sound if it's too loud.")]
		public float WindSoundVolumeModifier = 0.5f;

		// Token: 0x04001A92 RID: 6802
		[Tooltip("Wind zone that will affect and follow the rain")]
		public WindZone WindZone;

		// Token: 0x04001A93 RID: 6803
		[Tooltip("X = minimum wind speed. Y = maximum wind speed. Z = sound multiplier. Wind speed is divided by Z to get sound multiplier value. Set Z to lower than Y to increase wind sound volume, or higher to decrease wind sound volume.")]
		public Vector3 WindSpeedRange = new Vector3(50f, 500f, 500f);

		// Token: 0x04001A94 RID: 6804
		[Tooltip("How often the wind speed and direction changes (minimum and maximum change interval in seconds)")]
		public Vector2 WindChangeInterval = new Vector2(5f, 30f);

		// Token: 0x04001A95 RID: 6805
		[Tooltip("Whether wind should be enabled.")]
		public bool EnableWind = true;

		// Token: 0x04001A96 RID: 6806
		protected LoopingAudioSource audioSourceRainLight;

		// Token: 0x04001A97 RID: 6807
		protected LoopingAudioSource audioSourceRainMedium;

		// Token: 0x04001A98 RID: 6808
		protected LoopingAudioSource audioSourceRainHeavy;

		// Token: 0x04001A99 RID: 6809
		protected LoopingAudioSource audioSourceRainCurrent;

		// Token: 0x04001A9A RID: 6810
		protected LoopingAudioSource audioSourceWind;

		// Token: 0x04001A9B RID: 6811
		protected Material rainMaterial;

		// Token: 0x04001A9C RID: 6812
		protected Material rainExplosionMaterial;

		// Token: 0x04001A9D RID: 6813
		protected Material rainMistMaterial;

		// Token: 0x04001A9E RID: 6814
		private float lastRainIntensityValue = -1f;

		// Token: 0x04001A9F RID: 6815
		private float nextWindTime;
	}
}

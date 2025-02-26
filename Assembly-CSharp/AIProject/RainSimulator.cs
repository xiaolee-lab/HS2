using System;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F03 RID: 3843
	public class RainSimulator : MonoBehaviour
	{
		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06007DA9 RID: 32169 RVA: 0x0035AE40 File Offset: 0x00359240
		// (set) Token: 0x06007DAA RID: 32170 RVA: 0x0035AE48 File Offset: 0x00359248
		public Camera Camera
		{
			get
			{
				return this._camera;
			}
			set
			{
				this._camera = value;
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06007DAB RID: 32171 RVA: 0x0035AE51 File Offset: 0x00359251
		// (set) Token: 0x06007DAC RID: 32172 RVA: 0x0035AE59 File Offset: 0x00359259
		public bool FollowsCamera
		{
			get
			{
				return this._followsCamera;
			}
			set
			{
				this._followsCamera = value;
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x0035AE62 File Offset: 0x00359262
		public RainSimulator.RainAudioClipGroup AudioClips
		{
			[CompilerGenerated]
			get
			{
				return this._audioClips;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06007DAE RID: 32174 RVA: 0x0035AE6A File Offset: 0x0035926A
		// (set) Token: 0x06007DAF RID: 32175 RVA: 0x0035AE72 File Offset: 0x00359272
		public float RainIntensity
		{
			get
			{
				return this._rainIntensity;
			}
			set
			{
				this._rainIntensity = value;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06007DB0 RID: 32176 RVA: 0x0035AE7B File Offset: 0x0035927B
		public RainSimulator.RainParticleGroup Particles
		{
			[CompilerGenerated]
			get
			{
				return this._particles;
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06007DB1 RID: 32177 RVA: 0x0035AE83 File Offset: 0x00359283
		// (set) Token: 0x06007DB2 RID: 32178 RVA: 0x0035AE8B File Offset: 0x0035928B
		public WindZone WindZone
		{
			get
			{
				return this._windZone;
			}
			set
			{
				this._windZone = value;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06007DB3 RID: 32179 RVA: 0x0035AE94 File Offset: 0x00359294
		// (set) Token: 0x06007DB4 RID: 32180 RVA: 0x0035AE9C File Offset: 0x0035929C
		public bool EnableWind
		{
			get
			{
				return this._enableWind;
			}
			set
			{
				this._enableWind = value;
			}
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x0035AEA8 File Offset: 0x003592A8
		protected virtual void Start()
		{
			if (this._camera == null)
			{
				this._camera = Camera.main;
			}
			this._audioSourceRainLight = new RainSimulator.LoopingAudioSource(this, this._audioClips.RainLight);
			this._audioSourceRainMedium = new RainSimulator.LoopingAudioSource(this, this._audioClips.RainMedium);
			this._audioSourceRainHeavy = new RainSimulator.LoopingAudioSource(this, this._audioClips.RainHeavy);
			this._audioSourceWind = new RainSimulator.LoopingAudioSource(this, this._audioClips.Wind);
			this._particles.Init(this.UseRainMistSoftPartilces);
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x0035AF71 File Offset: 0x00359371
		protected virtual void OnUpdate()
		{
			this.CheckForRainChange();
			this.UpdateWind();
			this._audioSourceRainLight.Update();
			this._audioSourceRainMedium.Update();
			this._audioSourceRainHeavy.Update();
			this.UpdateRain();
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x0035AFA8 File Offset: 0x003593A8
		private void UpdateRain()
		{
			if (this._followsCamera)
			{
				if (this._particles.Fall != null)
				{
					ParticleSystem fall = this._particles.Fall;
					fall.shape.shapeType = ParticleSystemShapeType.ConeVolume;
					fall.transform.position = this._camera.transform.position;
					fall.transform.Translate(this._rainOffset);
					fall.transform.rotation = Quaternion.Euler(0f, this.Camera.transform.eulerAngles.y, 0f);
				}
				if (this._particles.Mist != null)
				{
					ParticleSystem mist = this._particles.Mist;
					mist.shape.shapeType = ParticleSystemShapeType.Hemisphere;
					Vector3 position = this._camera.transform.position;
					position.y += this._mistOffset.y;
					mist.transform.position = position;
				}
			}
			else
			{
				if (this._particles.Fall != null)
				{
					this._particles.Fall.shape.shapeType = ParticleSystemShapeType.Box;
				}
				if (this._particles.Mist != null)
				{
					this._particles.Mist.shape.shapeType = ParticleSystemShapeType.Box;
					Vector3 vector = this._particles.Fall.transform.position;
					vector += this._mistOffset;
					vector.y -= this._rainOffset.y;
					this._particles.Mist.transform.position = vector;
				}
			}
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x0035B174 File Offset: 0x00359574
		private void UpdateWind()
		{
			if (this._enableWind)
			{
				if (this._windZone != null)
				{
					this._windZone.gameObject.SetActive(true);
					if (this._followsCamera)
					{
						this._windZone.transform.position = this._camera.transform.position;
					}
					if (!this._camera.orthographic)
					{
						this._windZone.transform.Translate(0f, this._windZone.radius, 0f);
					}
					if (this._nextWindTime < Time.time)
					{
						Threshold windSpeedRange = Singleton<Map>.Instance.EnvironmentProfile.WindSpeedRange;
						this._windZone.windMain = windSpeedRange.RandomValue;
						this._windZone.windTurbulence = windSpeedRange.RandomValue;
						if (this.Camera.orthographic)
						{
							int num = UnityEngine.Random.Range(0, 2);
							this._windZone.transform.rotation = Quaternion.Euler(0f, (num != 0) ? -90f : 90f, 0f);
						}
						else
						{
							this._windZone.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(0f, 360f), 0f);
						}
						this._nextWindTime = Time.time + Singleton<Map>.Instance.EnvironmentProfile.WindChangeIntervalThreshold.RandomValue;
						this._audioSourceWind.Play(this._windZone.windMain / Singleton<Map>.Instance.EnvironmentProfile.WindSoundMultiplier * Singleton<Map>.Instance.EnvironmentProfile.WindSoundVolumeModifier);
					}
				}
			}
			else
			{
				if (this._windZone != null)
				{
					this._windZone.gameObject.SetActive(false);
				}
				this._audioSourceWind.Stop();
			}
			this._audioSourceWind.Update();
		}

		// Token: 0x06007DB9 RID: 32185 RVA: 0x0035B374 File Offset: 0x00359774
		private void CheckForRainChange()
		{
			float num = Singleton<Map>.Instance.EnvironmentProfile.RainIntensity * this._rainIntensity;
			if (this._lastRainIntensityValue != num)
			{
				this._lastRainIntensityValue = num;
				if (num <= 0.01f)
				{
					if (this._audioSourceRainCurrent != null)
					{
						this._audioSourceRainCurrent.Stop();
						this._audioSourceRainCurrent = null;
					}
					if (this._particles.Fall != null)
					{
						this._particles.Fall.emission.enabled = false;
						this._particles.Fall.Stop();
					}
					if (this._particles.Mist != null)
					{
						this._particles.Mist.emission.enabled = false;
						this._particles.Mist.Stop();
					}
				}
				else
				{
					RainSimulator.LoopingAudioSource loopingAudioSource;
					if (num >= 0.67f)
					{
						loopingAudioSource = this._audioSourceRainHeavy;
					}
					else if (num >= 0.33f)
					{
						loopingAudioSource = this._audioSourceRainMedium;
					}
					else
					{
						loopingAudioSource = this._audioSourceRainLight;
					}
					if (this._audioSourceRainCurrent != loopingAudioSource)
					{
						if (this._audioSourceRainCurrent != null)
						{
							this._audioSourceRainCurrent.Stop();
						}
						this._audioSourceRainCurrent = loopingAudioSource;
						this._audioSourceRainCurrent.Play(1f);
					}
					if (this._particles.Fall != null)
					{
						ParticleSystem.EmissionModule emission = this._particles.Fall.emission;
						bool enabled = true;
						this._particles.Fall.GetComponent<Renderer>().enabled = enabled;
						emission.enabled = enabled;
						if (!this._particles.Fall.isPlaying)
						{
							this._particles.Fall.Play();
						}
						ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
						rateOverTime.mode = ParticleSystemCurveMode.Constant;
						float num2 = this.RainFallEmissionRate();
						rateOverTime.constantMax = num2;
						rateOverTime.constantMin = num2;
					}
					if (this._particles.Mist != null)
					{
						ParticleSystem mist = this._particles.Mist;
						ParticleSystem.EmissionModule emission2 = mist.emission;
						bool enabled = true;
						mist.GetComponent<Renderer>().enabled = enabled;
						emission2.enabled = enabled;
						if (mist.isPlaying)
						{
							mist.Play();
						}
						float num3;
						if (num < Singleton<Map>.Instance.EnvironmentProfile.RainMistThreshold)
						{
							num3 = 0f;
						}
						else
						{
							num3 = this.MistEmissionRate();
						}
						ParticleSystem.MinMaxCurve rateOverTime2 = emission2.rateOverTime;
						rateOverTime2.mode = ParticleSystemCurveMode.Constant;
						float num2 = num3;
						rateOverTime2.constantMax = num2;
						rateOverTime2.constantMin = num2;
						emission2.rateOverTime = rateOverTime2;
					}
				}
			}
		}

		// Token: 0x06007DBA RID: 32186 RVA: 0x0035B614 File Offset: 0x00359A14
		protected virtual float RainFallEmissionRate()
		{
			ParticleSystem.MainModule main = this._particles.Fall.main;
			return (float)main.maxParticles / main.startLifetime.constant * Singleton<Map>.Instance.EnvironmentProfile.RainIntensity * this._rainIntensity;
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x0035B664 File Offset: 0x00359A64
		protected virtual float MistEmissionRate()
		{
			ParticleSystem.MainModule main = this._particles.Mist.main;
			float num = Singleton<Map>.Instance.EnvironmentProfile.RainIntensity * this._rainIntensity;
			return (float)main.maxParticles / main.startLifetime.constant * num * num;
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06007DBC RID: 32188 RVA: 0x0035B6B5 File Offset: 0x00359AB5
		protected virtual bool UseRainMistSoftPartilces
		{
			[CompilerGenerated]
			get
			{
				return true;
			}
		}

		// Token: 0x0400658C RID: 25996
		[SerializeField]
		private Camera _camera;

		// Token: 0x0400658D RID: 25997
		[SerializeField]
		private bool _followsCamera = true;

		// Token: 0x0400658E RID: 25998
		[SerializeField]
		private RainSimulator.RainAudioClipGroup _audioClips;

		// Token: 0x0400658F RID: 25999
		[SerializeField]
		[Range(0f, 1f)]
		private float _rainIntensity;

		// Token: 0x04006590 RID: 26000
		[SerializeField]
		private RainSimulator.RainParticleGroup _particles;

		// Token: 0x04006591 RID: 26001
		[SerializeField]
		private WindZone _windZone;

		// Token: 0x04006592 RID: 26002
		[SerializeField]
		private bool _enableWind = true;

		// Token: 0x04006593 RID: 26003
		[SerializeField]
		private Vector3 _rainOffset = new Vector3(0f, 25f, -7f);

		// Token: 0x04006594 RID: 26004
		[SerializeField]
		private Vector3 _mistOffset = new Vector3(0f, 3f, 0f);

		// Token: 0x04006595 RID: 26005
		protected RainSimulator.LoopingAudioSource _audioSourceRainLight;

		// Token: 0x04006596 RID: 26006
		protected RainSimulator.LoopingAudioSource _audioSourceRainMedium;

		// Token: 0x04006597 RID: 26007
		protected RainSimulator.LoopingAudioSource _audioSourceRainHeavy;

		// Token: 0x04006598 RID: 26008
		protected RainSimulator.LoopingAudioSource _audioSourceRainCurrent;

		// Token: 0x04006599 RID: 26009
		protected RainSimulator.LoopingAudioSource _audioSourceWind;

		// Token: 0x0400659A RID: 26010
		private float _lastRainIntensityValue = -1f;

		// Token: 0x0400659B RID: 26011
		private float _nextWindTime;

		// Token: 0x02000F04 RID: 3844
		[Serializable]
		public class RainAudioClipGroup
		{
			// Token: 0x170018B6 RID: 6326
			// (get) Token: 0x06007DC0 RID: 32192 RVA: 0x0035B6D0 File Offset: 0x00359AD0
			// (set) Token: 0x06007DC1 RID: 32193 RVA: 0x0035B6D8 File Offset: 0x00359AD8
			public AudioClip RainLight
			{
				get
				{
					return this._rainAudioLight;
				}
				set
				{
					this._rainAudioLight = value;
				}
			}

			// Token: 0x170018B7 RID: 6327
			// (get) Token: 0x06007DC2 RID: 32194 RVA: 0x0035B6E1 File Offset: 0x00359AE1
			// (set) Token: 0x06007DC3 RID: 32195 RVA: 0x0035B6E9 File Offset: 0x00359AE9
			public AudioClip RainMedium
			{
				get
				{
					return this._rainAudioMedium;
				}
				set
				{
					this._rainAudioMedium = value;
				}
			}

			// Token: 0x170018B8 RID: 6328
			// (get) Token: 0x06007DC4 RID: 32196 RVA: 0x0035B6F2 File Offset: 0x00359AF2
			// (set) Token: 0x06007DC5 RID: 32197 RVA: 0x0035B6FA File Offset: 0x00359AFA
			public AudioClip RainHeavy
			{
				get
				{
					return this._rainAudioHeavy;
				}
				set
				{
					this._rainAudioHeavy = value;
				}
			}

			// Token: 0x170018B9 RID: 6329
			// (get) Token: 0x06007DC6 RID: 32198 RVA: 0x0035B703 File Offset: 0x00359B03
			// (set) Token: 0x06007DC7 RID: 32199 RVA: 0x0035B70B File Offset: 0x00359B0B
			public AudioClip Wind
			{
				get
				{
					return this._wind;
				}
				set
				{
					this._wind = value;
				}
			}

			// Token: 0x0400659C RID: 26012
			[SerializeField]
			private AudioClip _rainAudioLight;

			// Token: 0x0400659D RID: 26013
			[SerializeField]
			private AudioClip _rainAudioMedium;

			// Token: 0x0400659E RID: 26014
			[SerializeField]
			private AudioClip _rainAudioHeavy;

			// Token: 0x0400659F RID: 26015
			[SerializeField]
			private AudioClip _wind;
		}

		// Token: 0x02000F05 RID: 3845
		[Serializable]
		public class RainParticleGroup
		{
			// Token: 0x170018BA RID: 6330
			// (get) Token: 0x06007DC9 RID: 32201 RVA: 0x0035B71C File Offset: 0x00359B1C
			// (set) Token: 0x06007DCA RID: 32202 RVA: 0x0035B724 File Offset: 0x00359B24
			public ParticleSystem Fall
			{
				get
				{
					return this._fall;
				}
				set
				{
					this._fall = value;
				}
			}

			// Token: 0x170018BB RID: 6331
			// (get) Token: 0x06007DCB RID: 32203 RVA: 0x0035B72D File Offset: 0x00359B2D
			// (set) Token: 0x06007DCC RID: 32204 RVA: 0x0035B735 File Offset: 0x00359B35
			public ParticleSystem Explosion
			{
				get
				{
					return this._explosion;
				}
				set
				{
					this._explosion = value;
				}
			}

			// Token: 0x170018BC RID: 6332
			// (get) Token: 0x06007DCD RID: 32205 RVA: 0x0035B73E File Offset: 0x00359B3E
			// (set) Token: 0x06007DCE RID: 32206 RVA: 0x0035B746 File Offset: 0x00359B46
			public ParticleSystem Mist
			{
				get
				{
					return this._mist;
				}
				set
				{
					this._mist = value;
				}
			}

			// Token: 0x06007DCF RID: 32207 RVA: 0x0035B750 File Offset: 0x00359B50
			public void Init(bool useMistSoftParticles)
			{
				if (this._fall != null)
				{
					this._fall.emission.enabled = false;
					Renderer component = this._fall.GetComponent<Renderer>();
					component.enabled = false;
					this._fallMaterial = new Material(component.material);
					this._fallMaterial.EnableKeyword("SOFTPARTICLES_OFF");
					component.material = this._fallMaterial;
				}
				if (this._explosion != null)
				{
					this._explosion.emission.enabled = false;
					Renderer component2 = this._fall.GetComponent<Renderer>();
					component2.enabled = false;
					this._explosionMaterial = new Material(component2.material);
					this._explosionMaterial.EnableKeyword("SOFTPARTICLES_OFF");
					component2.material = this._explosionMaterial;
				}
				if (this._mist != null)
				{
					this._mist.emission.enabled = false;
					Renderer component3 = this._mist.GetComponent<Renderer>();
					component3.enabled = false;
					this._mistMaterial = new Material(component3.material);
					if (useMistSoftParticles)
					{
						this._mistMaterial.EnableKeyword("SOFTPARTICLES_ON");
					}
					else
					{
						this._mistMaterial.EnableKeyword("SOFTPARTICLES_OFF");
					}
					component3.material = this._mistMaterial;
				}
			}

			// Token: 0x040065A0 RID: 26016
			[SerializeField]
			private ParticleSystem _fall;

			// Token: 0x040065A1 RID: 26017
			[SerializeField]
			private ParticleSystem _explosion;

			// Token: 0x040065A2 RID: 26018
			[SerializeField]
			private ParticleSystem _mist;

			// Token: 0x040065A3 RID: 26019
			protected Material _fallMaterial;

			// Token: 0x040065A4 RID: 26020
			protected Material _explosionMaterial;

			// Token: 0x040065A5 RID: 26021
			protected Material _mistMaterial;
		}

		// Token: 0x02000F06 RID: 3846
		public class LoopingAudioSource
		{
			// Token: 0x06007DD0 RID: 32208 RVA: 0x0035B8AC File Offset: 0x00359CAC
			public LoopingAudioSource(MonoBehaviour script, AudioClip clip)
			{
				this.AudioSource = script.gameObject.AddComponent<AudioSource>();
				this.AudioSource.loop = true;
				this.AudioSource.clip = clip;
				this.AudioSource.playOnAwake = false;
				this.AudioSource.volume = 0f;
				this.AudioSource.Stop();
				this.AudioSource.outputAudioMixerGroup = Sound.Mixer.FindMatchingGroups("ENV")[0];
				this.TargetVolume = 1f;
			}

			// Token: 0x170018BD RID: 6333
			// (get) Token: 0x06007DD1 RID: 32209 RVA: 0x0035B936 File Offset: 0x00359D36
			// (set) Token: 0x06007DD2 RID: 32210 RVA: 0x0035B93E File Offset: 0x00359D3E
			public AudioSource AudioSource { get; private set; }

			// Token: 0x170018BE RID: 6334
			// (get) Token: 0x06007DD3 RID: 32211 RVA: 0x0035B947 File Offset: 0x00359D47
			// (set) Token: 0x06007DD4 RID: 32212 RVA: 0x0035B94F File Offset: 0x00359D4F
			public float TargetVolume { get; private set; }

			// Token: 0x06007DD5 RID: 32213 RVA: 0x0035B958 File Offset: 0x00359D58
			public void Play(float volume)
			{
				if (!this.AudioSource.isPlaying)
				{
					this.AudioSource.volume = 0f;
					this.AudioSource.Play();
				}
				this.TargetVolume = volume;
			}

			// Token: 0x06007DD6 RID: 32214 RVA: 0x0035B98C File Offset: 0x00359D8C
			public void Stop()
			{
				this.TargetVolume = 0f;
			}

			// Token: 0x06007DD7 RID: 32215 RVA: 0x0035B99C File Offset: 0x00359D9C
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
}

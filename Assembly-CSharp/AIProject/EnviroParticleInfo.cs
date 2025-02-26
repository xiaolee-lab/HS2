using System;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F00 RID: 3840
	public class EnviroParticleInfo : IDisposable
	{
		// Token: 0x06007D95 RID: 32149 RVA: 0x0035A7B4 File Offset: 0x00358BB4
		public EnviroParticleInfo(int _prefabInstanceID, ParticleSystem _particle)
		{
			this.prefabInstanceID = _prefabInstanceID;
			this.particle = _particle;
			this.disposable = null;
			this.EmissionMax = ((!(this.particle == null)) ? this.particle.emission.rateOverTime.constantMax : 0f);
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x0035A818 File Offset: 0x00358C18
		// (set) Token: 0x06007D97 RID: 32151 RVA: 0x0035A820 File Offset: 0x00358C20
		public float EmissionMax { get; private set; }

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x0035A829 File Offset: 0x00358C29
		// (set) Token: 0x06007D99 RID: 32153 RVA: 0x0035A831 File Offset: 0x00358C31
		public EnviroParticleInfo.FadeType fadeType { get; private set; }

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06007D9A RID: 32154 RVA: 0x0035A83C File Offset: 0x00358C3C
		// (set) Token: 0x06007D9B RID: 32155 RVA: 0x0035A880 File Offset: 0x00358C80
		public float Emission
		{
			get
			{
				return (!(this.particle == null)) ? this.particle.emission.rateOverTime.constantMax : 0f;
			}
			set
			{
				if (this.particle != null)
				{
					ParticleSystem.EmissionModule emission = this.particle.emission;
					ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
					rateOverTime.constantMax = value;
					emission.rateOverTime = rateOverTime;
				}
			}
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x0035A8C4 File Offset: 0x00358CC4
		public void PlayFadeIn(float _fadeTime)
		{
			if (this.particle == null)
			{
				return;
			}
			this.fadeType = EnviroParticleInfo.FadeType.FadeIn;
			this.Dispose();
			if (_fadeTime <= 0f)
			{
				this.Emission = this.EmissionMax;
				if (!this.particle.isPlaying)
				{
					this.particle.Play(true);
				}
				this.fadeType = EnviroParticleInfo.FadeType.Play;
				return;
			}
			float _startEmission = this.Emission;
			this.particle.Play(true);
			this.disposable = ObservableEasing.Linear(_fadeTime, this.particle.main.useUnscaledTime).TakeUntilDestroy(this.particle).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> time)
			{
				this.Emission = Mathf.Lerp(_startEmission, this.EmissionMax, time.Value);
			}, delegate()
			{
				this.fadeType = EnviroParticleInfo.FadeType.Play;
				this.Emission = this.EmissionMax;
				this.disposable = null;
			});
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x0035A9A0 File Offset: 0x00358DA0
		public void PlayFadeOut(float _fadeTime)
		{
			if (this.particle == null)
			{
				return;
			}
			this.fadeType = EnviroParticleInfo.FadeType.FadeOut;
			this.Dispose();
			if (_fadeTime <= 0f)
			{
				this.Emission = 0f;
				if (!this.particle.isStopped)
				{
					this.particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
				}
				this.fadeType = EnviroParticleInfo.FadeType.Stop;
				return;
			}
			float _startEmission = this.Emission;
			this.disposable = ObservableEasing.Linear(_fadeTime, this.particle.main.useUnscaledTime).TakeUntilDestroy(this.particle).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> time)
			{
				this.Emission = Mathf.Lerp(_startEmission, 0f, time.Value);
			}, delegate()
			{
				this.disposable = null;
				if (this.particle != null)
				{
					if (!this.particle.isStopped)
					{
						this.particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
					}
					this.Emission = 0f;
				}
				this.fadeType = EnviroParticleInfo.FadeType.Stop;
			});
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x0035AA6D File Offset: 0x00358E6D
		public void Dispose()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
				this.disposable = null;
			}
		}

		// Token: 0x04006580 RID: 25984
		public int prefabInstanceID;

		// Token: 0x04006581 RID: 25985
		public ParticleSystem particle;

		// Token: 0x04006583 RID: 25987
		public IDisposable disposable;

		// Token: 0x02000F01 RID: 3841
		public enum FadeType
		{
			// Token: 0x04006586 RID: 25990
			Stop,
			// Token: 0x04006587 RID: 25991
			Play,
			// Token: 0x04006588 RID: 25992
			FadeIn,
			// Token: 0x04006589 RID: 25993
			FadeOut
		}
	}
}

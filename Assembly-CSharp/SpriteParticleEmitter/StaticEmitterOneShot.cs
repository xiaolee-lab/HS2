using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SpriteParticleEmitter
{
	// Token: 0x02000596 RID: 1430
	public class StaticEmitterOneShot : StaticSpriteEmitter
	{
		// Token: 0x14000082 RID: 130
		// (add) Token: 0x0600211D RID: 8477 RVA: 0x000B4CC4 File Offset: 0x000B30C4
		// (remove) Token: 0x0600211E RID: 8478 RVA: 0x000B4CFC File Offset: 0x000B30FC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnAvailableToPlay;

		// Token: 0x0600211F RID: 8479 RVA: 0x000B4D32 File Offset: 0x000B3132
		protected override void Awake()
		{
			base.Awake();
			this.SilentEmissionEnded = false;
			if (this.SilentEmitOnAwake)
			{
				this.EmitSilently();
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000B4D54 File Offset: 0x000B3154
		public override void CacheSprite(bool relativeToParent = false)
		{
			base.CacheSprite(this.SimulationSpace == ParticleSystemSimulationSpace.World);
			if (this.mainModule.maxParticles < this.particlesCacheCount)
			{
				this.mainModule.maxParticles = Mathf.CeilToInt((float)this.particlesCacheCount);
			}
			this.SilentEmissionEnded = false;
			this.hasSilentEmissionAlreadyBeenShot = false;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000B4DAB File Offset: 0x000B31AB
		public void EmitSilently()
		{
			base.StartCoroutine(this.EmitParticlesSilently());
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000B4DBC File Offset: 0x000B31BC
		private IEnumerator EmitParticlesSilently()
		{
			this.hasSilentEmissionAlreadyBeenShot = false;
			this.SilentEmissionEnded = false;
			this.isPlaying = false;
			float time = Time.realtimeSinceStartup;
			float LastTimeSaved = Time.realtimeSinceStartup;
			float waitTimeMax = 1000f / this.WantedFPSDuringSilentEmission;
			this.particlesSystem.Clear();
			this.particlesSystem.Pause();
			Color[] colorCache = this.particleInitColorCache;
			Vector3[] posCache = this.particleInitPositionsCache;
			float pStartSize = this.particleStartSize;
			int length = this.particlesCacheCount;
			ParticleSystem ps = this.particlesSystem;
			for (int i = 0; i < length; i++)
			{
				if (i % 3 == 0)
				{
					LastTimeSaved = Time.realtimeSinceStartup;
				}
				ParticleSystem.EmitParams em = default(ParticleSystem.EmitParams);
				if (this.UsePixelSourceColor)
				{
					em.startColor = colorCache[i];
				}
				em.startSize = pStartSize;
				em.position = posCache[i];
				ps.Emit(em, 1);
				if (LastTimeSaved - time > waitTimeMax)
				{
					this.particlesSystem.Pause();
					time = LastTimeSaved;
					yield return null;
				}
			}
			this.particlesSystem.Pause();
			this.SilentEmissionEnded = true;
			if (this.OnAvailableToPlay != null)
			{
				this.OnAvailableToPlay();
			}
			yield break;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000B4DD7 File Offset: 0x000B31D7
		public void SetHideSpriteOnPlay(bool hideOriginalSprite)
		{
			this.HideOriginalSpriteOnPlay = hideOriginalSprite;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000B4DE0 File Offset: 0x000B31E0
		private bool PlayOneShot()
		{
			if (this.HideOriginalSpriteOnPlay)
			{
				this.spriteRenderer.enabled = false;
			}
			if (!this.SilentEmissionEnded)
			{
				return false;
			}
			this.particlesSystem.Play();
			this.isPlaying = true;
			this.hasSilentEmissionAlreadyBeenShot = true;
			return true;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000B4E20 File Offset: 0x000B3220
		public override void Play()
		{
			if (!this.IsAvailableToPlay())
			{
				return;
			}
			if (!this.hasSilentEmissionAlreadyBeenShot)
			{
				if (!this.isPlaying)
				{
					this.PlayOneShot();
				}
			}
			else if (!this.isPlaying)
			{
				this.particlesSystem.Play();
				this.isPlaying = true;
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000B4E78 File Offset: 0x000B3278
		public override void Stop()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000B4E97 File Offset: 0x000B3297
		public override void Pause()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000B4EB6 File Offset: 0x000B32B6
		public void Reset()
		{
			this.EmitSilently();
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000B4EBE File Offset: 0x000B32BE
		public override bool IsAvailableToPlay()
		{
			return this.hasCachingEnded && !this.isPlaying && this.SilentEmissionEnded;
		}

		// Token: 0x04002091 RID: 8337
		[Tooltip("Must the script disable referenced spriteRenderer component?")]
		public bool HideOriginalSpriteOnPlay = true;

		// Token: 0x04002092 RID: 8338
		[Header("Silent Emission")]
		[Tooltip("Should start Silent Emitting as soon as has cache ended? (Refer to manual for further explanation)")]
		public bool SilentEmitOnAwake = true;

		// Token: 0x04002093 RID: 8339
		[Tooltip("Silent emission can be expensive. This defines the lower limit fps can go before continue silent emission on next frame (Refer to manual for further explanation)")]
		public float WantedFPSDuringSilentEmission = 60f;

		// Token: 0x04002094 RID: 8340
		protected bool SilentEmissionEnded;

		// Token: 0x04002095 RID: 8341
		protected bool hasSilentEmissionAlreadyBeenShot;
	}
}

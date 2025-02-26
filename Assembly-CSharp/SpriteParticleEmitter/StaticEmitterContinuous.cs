using System;
using System.Diagnostics;
using UnityEngine;

namespace SpriteParticleEmitter
{
	// Token: 0x02000594 RID: 1428
	public class StaticEmitterContinuous : StaticSpriteEmitter
	{
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06002109 RID: 8457 RVA: 0x000B43FC File Offset: 0x000B27FC
		// (remove) Token: 0x0600210A RID: 8458 RVA: 0x000B4434 File Offset: 0x000B2834
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnAvailableToPlay;

		// Token: 0x0600210B RID: 8459 RVA: 0x000B446A File Offset: 0x000B286A
		protected override void Update()
		{
			base.Update();
			if (this.isPlaying && this.hasCachingEnded)
			{
				this.Emit();
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000B448E File Offset: 0x000B288E
		public override void CacheSprite(bool relativeToParent = false)
		{
			base.CacheSprite(false);
			if (this.OnAvailableToPlay != null)
			{
				this.OnAvailableToPlay();
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000B44B0 File Offset: 0x000B28B0
		protected void Emit()
		{
			if (!this.hasCachingEnded)
			{
				return;
			}
			this.ParticlesToEmitThisFrame += this.EmissionRate * Time.deltaTime;
			Vector3 position = this.spriteRenderer.gameObject.transform.position;
			Vector3 b = position;
			Quaternion rotation = this.spriteRenderer.gameObject.transform.rotation;
			Vector3 lossyScale = this.spriteRenderer.gameObject.transform.lossyScale;
			ParticleSystemSimulationSpace simulationSpace = this.SimulationSpace;
			int particlesCacheCount = this.particlesCacheCount;
			float particleStartSize = this.particleStartSize;
			int num = (int)this.ParticlesToEmitThisFrame;
			if (this.particlesCacheCount <= 0)
			{
				return;
			}
			Color[] particleInitColorCache = this.particleInitColorCache;
			Vector3[] particleInitPositionsCache = this.particleInitPositionsCache;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < num; i++)
			{
				int num2 = UnityEngine.Random.Range(0, particlesCacheCount);
				if (this.useBetweenFramesPrecision)
				{
					float t = UnityEngine.Random.Range(0f, 1f);
					b = Vector3.Lerp(this.lastTransformPosition, position, t);
				}
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = particleInitColorCache[num2];
				}
				emitParams.startSize = particleStartSize;
				if (simulationSpace == ParticleSystemSimulationSpace.World)
				{
					Vector3 vector = particleInitPositionsCache[num2];
					zero.x = vector.x * lossyScale.x;
					zero.y = vector.y * lossyScale.y;
					emitParams.position = rotation * zero + b;
					this.particlesSystem.Emit(emitParams, 1);
				}
				else
				{
					emitParams.position = particleInitPositionsCache[num2];
					this.particlesSystem.Emit(emitParams, 1);
				}
			}
			this.ParticlesToEmitThisFrame -= (float)num;
			this.lastTransformPosition = position;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000B4693 File Offset: 0x000B2A93
		public override void Play()
		{
			if (!this.isPlaying)
			{
				this.particlesSystem.Play();
			}
			this.isPlaying = true;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000B46B2 File Offset: 0x000B2AB2
		public override void Stop()
		{
			this.isPlaying = false;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000B46BB File Offset: 0x000B2ABB
		public override void Pause()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}
	}
}

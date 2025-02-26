using System;
using System.Diagnostics;
using UnityEngine;

namespace SpriteParticleEmitter
{
	// Token: 0x02000595 RID: 1429
	public class StaticEmitterContinuousUI : StaticUIImageEmitter
	{
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06002112 RID: 8466 RVA: 0x000B4704 File Offset: 0x000B2B04
		// (remove) Token: 0x06002113 RID: 8467 RVA: 0x000B473C File Offset: 0x000B2B3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnAvailableToPlay;

		// Token: 0x06002114 RID: 8468 RVA: 0x000B4772 File Offset: 0x000B2B72
		protected override void Awake()
		{
			base.Awake();
			this.currentRectTransform = base.GetComponent<RectTransform>();
			this.targetRectTransform = this.imageRenderer.GetComponent<RectTransform>();
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000B4797 File Offset: 0x000B2B97
		protected override void Update()
		{
			base.Update();
			if (this.isPlaying && this.hasCachingEnded)
			{
				this.ProcessPositionAndScale();
				this.Emit();
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000B47C4 File Offset: 0x000B2BC4
		private void ProcessPositionAndScale()
		{
			if (this.matchImageRendererPostionData)
			{
				this.currentRectTransform.position = new Vector3(this.targetRectTransform.position.x, this.targetRectTransform.position.y, this.targetRectTransform.position.z);
			}
			this.currentRectTransform.pivot = this.targetRectTransform.pivot;
			if (this.matchImageRendererPostionData)
			{
				this.currentRectTransform.anchoredPosition = this.targetRectTransform.anchoredPosition;
				this.currentRectTransform.anchorMin = this.targetRectTransform.anchorMin;
				this.currentRectTransform.anchorMax = this.targetRectTransform.anchorMax;
				this.currentRectTransform.offsetMin = this.targetRectTransform.offsetMin;
				this.currentRectTransform.offsetMax = this.targetRectTransform.offsetMax;
			}
			if (this.matchImageRendererScale)
			{
				this.currentRectTransform.localScale = this.targetRectTransform.localScale;
			}
			this.currentRectTransform.rotation = this.targetRectTransform.rotation;
			this.currentRectTransform.sizeDelta = new Vector2(this.targetRectTransform.rect.width, this.targetRectTransform.rect.height);
			float x = (1f - this.currentRectTransform.pivot.x) * this.currentRectTransform.rect.width - this.currentRectTransform.rect.width / 2f;
			float y = (1f - this.currentRectTransform.pivot.y) * -this.currentRectTransform.rect.height + this.currentRectTransform.rect.height / 2f;
			this.offsetXY = new Vector2(x, y);
			Sprite sprite = this.imageRenderer.sprite;
			this.wMult = sprite.pixelsPerUnit * (this.currentRectTransform.rect.width / sprite.rect.size.x);
			this.hMult = sprite.pixelsPerUnit * (this.currentRectTransform.rect.height / sprite.rect.size.y);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000B4A4F File Offset: 0x000B2E4F
		public override void CacheSprite(bool relativeToParent = false)
		{
			base.CacheSprite(false);
			if (this.OnAvailableToPlay != null)
			{
				this.OnAvailableToPlay();
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000B4A70 File Offset: 0x000B2E70
		protected void Emit()
		{
			if (!this.hasCachingEnded)
			{
				return;
			}
			this.ParticlesToEmitThisFrame += this.EmissionRate * Time.deltaTime;
			Vector3 position = this.currentRectTransform.position;
			Quaternion rotation = this.currentRectTransform.rotation;
			Vector3 localScale = this.currentRectTransform.localScale;
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
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = particleInitColorCache[num2];
				}
				emitParams.startSize = particleStartSize;
				Vector3 vector = particleInitPositionsCache[num2];
				if (simulationSpace == ParticleSystemSimulationSpace.World)
				{
					zero.x = vector.x * this.wMult * localScale.x + this.offsetXY.x;
					zero.y = vector.y * this.hMult * localScale.y - this.offsetXY.y;
					emitParams.position = rotation * zero + position;
					this.particlesSystem.Emit(emitParams, 1);
				}
				else
				{
					zero.x = vector.x * this.wMult + this.offsetXY.x;
					zero.y = vector.y * this.hMult - this.offsetXY.y;
					emitParams.position = zero;
					this.particlesSystem.Emit(emitParams, 1);
				}
			}
			this.ParticlesToEmitThisFrame -= (float)num;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000B4C5B File Offset: 0x000B305B
		public override void Play()
		{
			if (!this.isPlaying)
			{
				this.particlesSystem.Play();
			}
			this.isPlaying = true;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000B4C7A File Offset: 0x000B307A
		public override void Stop()
		{
			this.isPlaying = false;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000B4C83 File Offset: 0x000B3083
		public override void Pause()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x04002089 RID: 8329
		[Header("Emission")]
		[Tooltip("Particles to emit per second")]
		public float EmissionRate = 1000f;

		// Token: 0x0400208A RID: 8330
		protected float ParticlesToEmitThisFrame;

		// Token: 0x0400208C RID: 8332
		private RectTransform targetRectTransform;

		// Token: 0x0400208D RID: 8333
		private RectTransform currentRectTransform;

		// Token: 0x0400208E RID: 8334
		protected Vector2 offsetXY;

		// Token: 0x0400208F RID: 8335
		protected float wMult = 100f;

		// Token: 0x04002090 RID: 8336
		protected float hMult = 100f;
	}
}

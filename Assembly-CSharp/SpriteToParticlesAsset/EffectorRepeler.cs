using System;
using UnityEngine;

namespace SpriteToParticlesAsset
{
	// Token: 0x02000598 RID: 1432
	[ExecuteInEditMode]
	public class EffectorRepeler : MonoBehaviour
	{
		// Token: 0x0600212F RID: 8495 RVA: 0x000B53F0 File Offset: 0x000B37F0
		private void Awake()
		{
			this.emitter = base.GetComponent<SpriteToParticles>();
			if (this.emitter && this.emitter.particlesSystem)
			{
				this.ps = this.emitter.particlesSystem;
			}
			if (!this.repelerCenter)
			{
				this.repelerCenter = base.transform;
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000B545B File Offset: 0x000B385B
		public void SetRepelCenterTransform(Transform repeler)
		{
			this.repelerCenter = repeler;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000B5464 File Offset: 0x000B3864
		private void LateUpdate()
		{
			if (!this.ps)
			{
				if (!this.emitter || !this.emitter.particlesSystem)
				{
					return;
				}
				this.ps = this.emitter.particlesSystem;
			}
			if (this.particles == null || this.particles.Length < this.ps.particleCount)
			{
				this.particles = new ParticleSystem.Particle[this.ps.particleCount];
			}
			int num = this.ps.GetParticles(this.particles);
			bool flag = this.ps.main.simulationSpace == ParticleSystemSimulationSpace.Local;
			if (flag)
			{
				this.center = this.repelerCenter.localPosition;
			}
			else
			{
				this.center = this.repelerCenter.position;
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 a = this.particles[i].position - this.center;
				this.particles[i].velocity = a * this.strength;
			}
			this.ps.SetParticles(this.particles, num);
		}

		// Token: 0x0400209C RID: 8348
		[Tooltip("Repeler force intensity. A negative strength will attract particles instead of repeling them.")]
		public float strength = 1f;

		// Token: 0x0400209D RID: 8349
		[Tooltip("Transform at which the particles will repel from. If none is set it will use the current Sprite position.")]
		public Transform repelerCenter;

		// Token: 0x0400209E RID: 8350
		private SpriteToParticles emitter;

		// Token: 0x0400209F RID: 8351
		private ParticleSystem ps;

		// Token: 0x040020A0 RID: 8352
		private ParticleSystem.Particle[] particles;

		// Token: 0x040020A1 RID: 8353
		private Vector3 center;
	}
}

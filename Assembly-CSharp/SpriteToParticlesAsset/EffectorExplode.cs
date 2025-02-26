using System;
using UnityEngine;

namespace SpriteToParticlesAsset
{
	// Token: 0x02000597 RID: 1431
	public class EffectorExplode : MonoBehaviour
	{
		// Token: 0x0600212B RID: 8491 RVA: 0x000B5178 File Offset: 0x000B3578
		private void Awake()
		{
			this.emitter = base.GetComponent<SpriteToParticles>();
			if (this.emitter && this.emitter.particlesSystem)
			{
				this.ps = this.emitter.particlesSystem;
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000B51C8 File Offset: 0x000B35C8
		public void ExplodeAt(Vector3 sourcePos, float radius, float angle, float startRot, float strenght)
		{
			if (!this.ps)
			{
				if (!this.emitter || !this.emitter.particlesSystem)
				{
					return;
				}
				this.ps = this.emitter.particlesSystem;
			}
			this.emitter.EmitAll(true);
			if (this.particles == null || this.particles.Length < this.ps.particleCount)
			{
				this.particles = new ParticleSystem.Particle[this.ps.particleCount];
			}
			int num = this.ps.GetParticles(this.particles);
			float num2 = radius / 2f;
			Vector2 v = new Vector2(Mathf.Cos(0.017453292f * startRot), Mathf.Sin(0.017453292f * startRot));
			for (int i = 0; i < num; i++)
			{
				ParticleSystem.Particle particle = this.particles[i];
				float num3 = Vector3.Distance(sourcePos, particle.position);
				if (num3 < num2)
				{
					Vector3 vector = particle.position - sourcePos;
					float num4 = Vector3.Angle(v, vector);
					if (Vector3.Cross(v, vector).z < 0f)
					{
						num4 = 360f - num4;
					}
					if (num4 < angle)
					{
						vector.Normalize();
						float num5 = radius - num3;
						float d = UnityEngine.Random.Range(num5 / 2f, num5);
						particle.velocity += vector * d * strenght;
						this.particles[i] = particle;
					}
				}
			}
			this.ps.SetParticles(this.particles, num);
			this.exploded = true;
			if (this.destroyObjectAfterExplosionIn >= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject, this.destroyObjectAfterExplosionIn);
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000B53B5 File Offset: 0x000B37B5
		public void ExplodeTest()
		{
			this.ExplodeAt(base.transform.position, 10f, 360f, 0f, 2f);
		}

		// Token: 0x04002097 RID: 8343
		[Tooltip("Weather the system is being used for Sprite or Image component. ")]
		public float destroyObjectAfterExplosionIn = 10f;

		// Token: 0x04002098 RID: 8344
		private SpriteToParticles emitter;

		// Token: 0x04002099 RID: 8345
		private ParticleSystem ps;

		// Token: 0x0400209A RID: 8346
		private ParticleSystem.Particle[] particles;

		// Token: 0x0400209B RID: 8347
		[HideInInspector]
		public bool exploded;
	}
}

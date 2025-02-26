using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E4 RID: 1252
	public class RainCollision : MonoBehaviour
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x000925D6 File Offset: 0x000909D6
		private void Start()
		{
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000925D8 File Offset: 0x000909D8
		private void Update()
		{
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000925DC File Offset: 0x000909DC
		private void Emit(ParticleSystem p, ref Vector3 pos)
		{
			for (int num = UnityEngine.Random.Range(2, 5); num != 0; num--)
			{
				float y = UnityEngine.Random.Range(1f, 3f);
				float z = UnityEngine.Random.Range(-2f, 2f);
				float x = UnityEngine.Random.Range(-2f, 2f);
				float startSize = UnityEngine.Random.Range(0.05f, 0.1f);
				p.Emit(new ParticleSystem.EmitParams
				{
					position = pos,
					velocity = new Vector3(x, y, z),
					startLifetime = 0.75f,
					startSize = startSize,
					startColor = RainCollision.color
				}, 1);
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00092690 File Offset: 0x00090A90
		private void OnParticleCollision(GameObject obj)
		{
			if (this.RainExplosion != null && this.RainParticleSystem != null)
			{
				int num = this.RainParticleSystem.GetCollisionEvents(obj, this.collisionEvents);
				for (int i = 0; i < num; i++)
				{
					Vector3 intersection = this.collisionEvents[i].intersection;
					this.Emit(this.RainExplosion, ref intersection);
				}
			}
		}

		// Token: 0x04001AA2 RID: 6818
		private static readonly Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04001AA3 RID: 6819
		private readonly List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

		// Token: 0x04001AA4 RID: 6820
		public ParticleSystem RainExplosion;

		// Token: 0x04001AA5 RID: 6821
		public ParticleSystem RainParticleSystem;
	}
}

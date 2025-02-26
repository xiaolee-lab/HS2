using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200037F RID: 895
	public class GrenadeObject : MonoBehaviour
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x00058FD1 File Offset: 0x000573D1
		public void Throw()
		{
			this.Impact = false;
			this.throwing = true;
			this.explodeTimeoutMax = 5f;
			this.ExplodeFinished = false;
			this.flashing = -1;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00058FFC File Offset: 0x000573FC
		public void Explode()
		{
			if (this.explosionInProgress)
			{
				return;
			}
			this.explosionInProgress = true;
			this.throwing = false;
			if (!this.Impact)
			{
				this.explodeTimeoutMax = 5f;
			}
			else
			{
				this.exploder.transform.position = base.transform.position;
				this.exploder.ExplodeSelf = false;
				this.exploder.UseForceVector = false;
				this.exploder.Radius = 5f;
				this.exploder.TargetFragments = 200;
				this.exploder.Force = 20f;
				this.exploder.ExplodeRadius(new ExploderObject.OnExplosion(this.OnExplode));
				this.ExplodeFinished = false;
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000590C0 File Offset: 0x000574C0
		private void OnExplode(float timeMS, ExploderObject.ExplosionState state)
		{
			if (state == ExploderObject.ExplosionState.ExplosionStarted)
			{
				ExploderUtils.SetVisible(base.gameObject, false);
				this.SourceExplosion.PlayOneShot(this.ExplosionSound);
				this.Flash.gameObject.transform.position = base.gameObject.transform.position;
				this.Flash.gameObject.transform.position += Vector3.up;
				this.flashing = 10;
			}
			if (state == ExploderObject.ExplosionState.ExplosionFinished)
			{
				this.ExplodeFinished = true;
				this.explosionInProgress = false;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00059157 File Offset: 0x00057557
		private void OnCollisionEnter(Collision other)
		{
			this.Impact = true;
			if (!this.throwing && !this.ExplodeFinished)
			{
				this.Explode();
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0005917C File Offset: 0x0005757C
		private void Update()
		{
			if (this.flashing >= 0)
			{
				if (this.flashing > 0)
				{
					this.Flash.intensity = 5f;
					this.flashing--;
				}
				else
				{
					this.Flash.intensity = 0f;
					this.flashing = -1;
				}
			}
			this.explodeTimeoutMax -= Time.deltaTime;
			if (!this.ExplodeFinished && this.explodeTimeoutMax < 0f)
			{
				this.Impact = true;
				this.Explode();
			}
		}

		// Token: 0x04001191 RID: 4497
		public AudioClip ExplosionSound;

		// Token: 0x04001192 RID: 4498
		public AudioSource SourceExplosion;

		// Token: 0x04001193 RID: 4499
		public Light Flash;

		// Token: 0x04001194 RID: 4500
		public bool ExplodeFinished;

		// Token: 0x04001195 RID: 4501
		public bool Impact;

		// Token: 0x04001196 RID: 4502
		private bool throwing;

		// Token: 0x04001197 RID: 4503
		private float explodeTimeoutMax;

		// Token: 0x04001198 RID: 4504
		private bool explosionInProgress;

		// Token: 0x04001199 RID: 4505
		public ExploderObject exploder;

		// Token: 0x0400119A RID: 4506
		private int flashing;
	}
}

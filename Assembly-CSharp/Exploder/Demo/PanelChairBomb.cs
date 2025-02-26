using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000381 RID: 897
	public class PanelChairBomb : UseObject
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x000593A8 File Offset: 0x000577A8
		public override void Use()
		{
			base.Use();
			this.Exploder.transform.position = this.ChairBomb.transform.position;
			this.Exploder.ExplodeSelf = false;
			this.Exploder.UseForceVector = false;
			this.Exploder.Radius = 10f;
			this.Exploder.TargetFragments = 300;
			this.Exploder.Force = 30f;
			this.Exploder.ExplodeRadius(new ExploderObject.OnExplosion(this.OnExplode));
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0005943C File Offset: 0x0005783C
		private void OnExplode(float timeMS, ExploderObject.ExplosionState state)
		{
			if (state == ExploderObject.ExplosionState.ExplosionStarted)
			{
				this.SourceExplosion.PlayOneShot(this.ExplosionSound);
				this.Flash.gameObject.transform.position = this.ChairBomb.transform.position;
				this.Flash.gameObject.transform.position += Vector3.up;
				this.flashing = 10;
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000594B4 File Offset: 0x000578B4
		private void Update()
		{
			if (this.flashing > 0)
			{
				this.Flash.intensity = 5f;
				this.flashing--;
			}
			else
			{
				this.Flash.intensity = 0f;
			}
		}

		// Token: 0x040011A0 RID: 4512
		public ExploderObject Exploder;

		// Token: 0x040011A1 RID: 4513
		public GameObject ChairBomb;

		// Token: 0x040011A2 RID: 4514
		public AudioSource SourceExplosion;

		// Token: 0x040011A3 RID: 4515
		public AudioClip ExplosionSound;

		// Token: 0x040011A4 RID: 4516
		public Light Flash;

		// Token: 0x040011A5 RID: 4517
		private int flashing;
	}
}

using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200037E RID: 894
	public class GrenadeController : MonoBehaviour
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x00058E4D File Offset: 0x0005724D
		private void Start()
		{
			this.throwTimer = float.MaxValue;
			this.explodeTimer = float.MaxValue;
			this.exploding = false;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00058E6C File Offset: 0x0005726C
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.G) && !this.exploding)
			{
				this.throwTimer = 0.4f;
				this.Source.PlayOneShot(this.Throw);
				this.explodeTimer = 2f;
				this.exploding = true;
				this.Grenade.Throw();
				ExploderUtils.SetVisible(base.gameObject, false);
			}
			this.throwTimer -= Time.deltaTime;
			if (this.throwTimer < 0f)
			{
				this.throwTimer = float.MaxValue;
				ExploderUtils.SetVisible(this.Grenade.gameObject, true);
				ExploderUtils.SetActive(this.Grenade.gameObject, true);
				this.Grenade.transform.position = base.gameObject.transform.position;
				this.Grenade.GetComponent<Rigidbody>().velocity = this.MainCamera.transform.forward * 20f;
			}
			this.explodeTimer -= Time.deltaTime;
			if (this.explodeTimer < 0f)
			{
				this.Grenade.Explode();
				this.explodeTimer = float.MaxValue;
			}
			if (this.Grenade.ExplodeFinished)
			{
				this.exploding = false;
				ExploderUtils.SetVisible(base.gameObject, true);
			}
		}

		// Token: 0x0400118A RID: 4490
		public AudioClip Throw;

		// Token: 0x0400118B RID: 4491
		public AudioSource Source;

		// Token: 0x0400118C RID: 4492
		public GrenadeObject Grenade;

		// Token: 0x0400118D RID: 4493
		public Camera MainCamera;

		// Token: 0x0400118E RID: 4494
		private float explodeTimer;

		// Token: 0x0400118F RID: 4495
		private float throwTimer;

		// Token: 0x04001190 RID: 4496
		private bool exploding;
	}
}

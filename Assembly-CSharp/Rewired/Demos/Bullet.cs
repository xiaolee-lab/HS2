using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000523 RID: 1315
	[AddComponentMenu("")]
	public class Bullet : MonoBehaviour
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0009C088 File Offset: 0x0009A488
		private void Start()
		{
			if (this.lifeTime > 0f)
			{
				this.deathTime = Time.time + this.lifeTime;
				this.die = true;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0009C0B3 File Offset: 0x0009A4B3
		private void Update()
		{
			if (this.die && Time.time >= this.deathTime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04001C32 RID: 7218
		public float lifeTime = 3f;

		// Token: 0x04001C33 RID: 7219
		private bool die;

		// Token: 0x04001C34 RID: 7220
		private float deathTime;
	}
}

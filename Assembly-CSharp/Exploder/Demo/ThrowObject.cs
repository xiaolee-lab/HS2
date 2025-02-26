using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200038B RID: 907
	public class ThrowObject : MonoBehaviour
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x0005A2E1 File Offset: 0x000586E1
		private void Start()
		{
			this.destroyTimer = 10f;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005A2EE File Offset: 0x000586EE
		private void Update()
		{
			this.destroyTimer -= Time.deltaTime;
			if (this.destroyTimer < 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x040011D9 RID: 4569
		private float destroyTimer;
	}
}

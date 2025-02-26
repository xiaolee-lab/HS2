using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x0200041F RID: 1055
	public class ETFXLightFade : MonoBehaviour
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x00076518 File Offset: 0x00074918
		private void Start()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li = base.gameObject.GetComponent<Light>();
				this.initIntensity = this.li.intensity;
			}
			else
			{
				MonoBehaviour.print("No light object found on " + base.gameObject.name);
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0007657C File Offset: 0x0007497C
		private void Update()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li.intensity -= this.initIntensity * (Time.deltaTime / this.life);
				if (this.killAfterLife && this.li.intensity <= 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject.GetComponent<Light>());
				}
			}
		}

		// Token: 0x04001575 RID: 5493
		[Header("Seconds to dim the light")]
		public float life = 0.2f;

		// Token: 0x04001576 RID: 5494
		public bool killAfterLife = true;

		// Token: 0x04001577 RID: 5495
		private Light li;

		// Token: 0x04001578 RID: 5496
		private float initIntensity;
	}
}

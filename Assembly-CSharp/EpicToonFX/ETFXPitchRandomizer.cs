using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000420 RID: 1056
	public class ETFXPitchRandomizer : MonoBehaviour
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x00076606 File Offset: 0x00074A06
		private void Start()
		{
			base.transform.GetComponent<AudioSource>().pitch *= 1f + UnityEngine.Random.Range(-this.randomPercent / 100f, this.randomPercent / 100f);
		}

		// Token: 0x04001579 RID: 5497
		public float randomPercent = 10f;
	}
}

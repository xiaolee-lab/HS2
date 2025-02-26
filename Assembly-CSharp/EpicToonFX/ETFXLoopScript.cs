using System;
using System.Collections;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x0200041B RID: 1051
	public class ETFXLoopScript : MonoBehaviour
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x00075D0C File Offset: 0x0007410C
		private void Start()
		{
			this.PlayEffect();
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00075D14 File Offset: 0x00074114
		public void PlayEffect()
		{
			base.StartCoroutine("EffectLoop");
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00075D24 File Offset: 0x00074124
		private IEnumerator EffectLoop()
		{
			GameObject effectPlayer = UnityEngine.Object.Instantiate<GameObject>(this.chosenEffect, base.transform.position, base.transform.rotation);
			if (this.spawnWithoutLight = (true && effectPlayer.GetComponent<Light>()))
			{
				effectPlayer.GetComponent<Light>().enabled = false;
			}
			if (this.spawnWithoutSound = (true && effectPlayer.GetComponent<AudioSource>()))
			{
				effectPlayer.GetComponent<AudioSource>().enabled = false;
			}
			yield return new WaitForSeconds(this.loopTimeLimit);
			UnityEngine.Object.Destroy(effectPlayer);
			this.PlayEffect();
			yield break;
		}

		// Token: 0x0400155B RID: 5467
		public GameObject chosenEffect;

		// Token: 0x0400155C RID: 5468
		public float loopTimeLimit = 2f;

		// Token: 0x0400155D RID: 5469
		[Header("Spawn without")]
		public bool spawnWithoutLight = true;

		// Token: 0x0400155E RID: 5470
		public bool spawnWithoutSound = true;
	}
}

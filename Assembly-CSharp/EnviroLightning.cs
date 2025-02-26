using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class EnviroLightning : MonoBehaviour
{
	// Token: 0x06000F99 RID: 3993 RVA: 0x0005778E File Offset: 0x00055B8E
	public void Lightning()
	{
		base.StartCoroutine(this.LightningBolt());
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0005779D File Offset: 0x00055B9D
	public void StopLightning()
	{
		base.StopAllCoroutines();
		base.GetComponent<Light>().enabled = false;
		EnviroSky.instance.thunder = 0f;
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000577C0 File Offset: 0x00055BC0
	public IEnumerator LightningBolt()
	{
		base.GetComponent<Light>().enabled = true;
		float defaultIntensity = base.GetComponent<Light>().intensity;
		int flashCount = UnityEngine.Random.Range(2, 5);
		for (int thisFlash = 0; thisFlash < flashCount; thisFlash++)
		{
			base.GetComponent<Light>().intensity = defaultIntensity * UnityEngine.Random.Range(1f, 1.5f);
			EnviroSky.instance.thunder = UnityEngine.Random.Range(5f, 10f);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.1f));
			base.GetComponent<Light>().intensity = defaultIntensity;
			EnviroSky.instance.thunder = 1f;
		}
		base.GetComponent<Light>().enabled = false;
		yield break;
	}
}

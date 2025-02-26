using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class AQUAS_SmallBubbleBehaviour : MonoBehaviour
{
	// Token: 0x060000A9 RID: 169 RVA: 0x0000932D File Offset: 0x0000772D
	private void Start()
	{
		this.updriftFactor = UnityEngine.Random.Range(-this.averageUpdrift * 0.75f, this.averageUpdrift * 0.75f);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00009354 File Offset: 0x00007754
	private void Update()
	{
		base.transform.Translate(Vector3.up * Time.deltaTime * (this.averageUpdrift + this.updriftFactor), Space.World);
		if (this.mainCamera.transform.position.y > this.waterLevel || base.transform.position.y > this.waterLevel)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040001E9 RID: 489
	public float averageUpdrift;

	// Token: 0x040001EA RID: 490
	public float waterLevel;

	// Token: 0x040001EB RID: 491
	public GameObject mainCamera;

	// Token: 0x040001EC RID: 492
	private float updriftFactor;
}

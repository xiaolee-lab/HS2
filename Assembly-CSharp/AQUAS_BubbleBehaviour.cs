using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class AQUAS_BubbleBehaviour : MonoBehaviour
{
	// Token: 0x06000072 RID: 114 RVA: 0x00006228 File Offset: 0x00004628
	private void Start()
	{
		this.maxSmallBubbleCount = UnityEngine.Random.Range(20, 30);
		this.smallBubbleCount = 0;
		this.smallBubbleBehaviour = this.smallBubble.GetComponent<AQUAS_SmallBubbleBehaviour>();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00006254 File Offset: 0x00004654
	private void Update()
	{
		base.transform.Translate(Vector3.up * Time.deltaTime * this.averageUpdrift, Space.World);
		this.SmallBubbleSpawner();
		if (this.mainCamera.transform.position.y > this.waterLevel || base.transform.position.y > this.waterLevel)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000062DC File Offset: 0x000046DC
	private void SmallBubbleSpawner()
	{
		if (this.smallBubbleCount <= this.maxSmallBubbleCount)
		{
			this.smallBubble.transform.localScale = base.transform.localScale * UnityEngine.Random.Range(0.05f, 0.2f);
			this.smallBubbleBehaviour.averageUpdrift = this.averageUpdrift * 0.5f;
			this.smallBubbleBehaviour.waterLevel = this.waterLevel;
			this.smallBubbleBehaviour.mainCamera = this.mainCamera;
			UnityEngine.Object.Instantiate<GameObject>(this.smallBubble, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), base.transform.position.y - UnityEngine.Random.Range(0.01f, 1f), base.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f)), Quaternion.identity);
			this.smallBubbleCount++;
		}
	}

	// Token: 0x0400015F RID: 351
	public float averageUpdrift;

	// Token: 0x04000160 RID: 352
	public float waterLevel;

	// Token: 0x04000161 RID: 353
	public GameObject mainCamera;

	// Token: 0x04000162 RID: 354
	public GameObject smallBubble;

	// Token: 0x04000163 RID: 355
	private int smallBubbleCount;

	// Token: 0x04000164 RID: 356
	private int maxSmallBubbleCount;

	// Token: 0x04000165 RID: 357
	private AQUAS_SmallBubbleBehaviour smallBubbleBehaviour;
}

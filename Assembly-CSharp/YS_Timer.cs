using System;
using UnityEngine;

// Token: 0x020011BE RID: 4542
public class YS_Timer : MonoBehaviour
{
	// Token: 0x060094FD RID: 38141 RVA: 0x003D6E47 File Offset: 0x003D5247
	private void Start()
	{
	}

	// Token: 0x060094FE RID: 38142 RVA: 0x003D6E4C File Offset: 0x003D524C
	private void Update()
	{
		this.cnt += Time.deltaTime;
		while (this.cnt > this.time)
		{
			this.cnt -= this.time;
		}
		this.rate = this.cnt / this.time;
	}

	// Token: 0x040077BA RID: 30650
	public float time = 1f;

	// Token: 0x040077BB RID: 30651
	public float rate;

	// Token: 0x040077BC RID: 30652
	private float cnt;
}

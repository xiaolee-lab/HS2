using System;
using UnityEngine;

// Token: 0x020011BA RID: 4538
public class RandomTimer
{
	// Token: 0x060094ED RID: 38125 RVA: 0x003D6B2E File Offset: 0x003D4F2E
	public void Init(float min, float max)
	{
		this.randMin = min;
		this.randMax = max;
		this.next = UnityEngine.Random.Range(this.randMin, this.randMax);
	}

	// Token: 0x060094EE RID: 38126 RVA: 0x003D6B58 File Offset: 0x003D4F58
	public bool Check()
	{
		this.cnt += Time.deltaTime;
		if (this.cnt >= this.next)
		{
			this.next = UnityEngine.Random.Range(this.randMin, this.randMax);
			this.cnt = 0f;
			return true;
		}
		return false;
	}

	// Token: 0x040077B0 RID: 30640
	private float randMin = 1000f;

	// Token: 0x040077B1 RID: 30641
	private float randMax = 1000f;

	// Token: 0x040077B2 RID: 30642
	private float next;

	// Token: 0x040077B3 RID: 30643
	private float cnt;
}

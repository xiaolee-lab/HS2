using System;
using UnityEngine;

// Token: 0x020011BB RID: 4539
public class LoopRateTimer
{
	// Token: 0x060094F0 RID: 38128 RVA: 0x003D6BB5 File Offset: 0x003D4FB5
	public void Init(float looptime)
	{
		this.loop = looptime;
	}

	// Token: 0x060094F1 RID: 38129 RVA: 0x003D6BC0 File Offset: 0x003D4FC0
	public float Check()
	{
		if (this.loop <= 0f)
		{
			return 0f;
		}
		this.cnt += Time.deltaTime * (180f / this.loop);
		while (this.cnt >= 180f)
		{
			this.cnt -= 180f;
		}
		return Mathf.Sin(this.cnt * 0.017453292f);
	}

	// Token: 0x040077B4 RID: 30644
	private float loop;

	// Token: 0x040077B5 RID: 30645
	private float cnt;
}

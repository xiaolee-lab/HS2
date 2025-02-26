using System;
using System.Linq;

// Token: 0x020011B9 RID: 4537
public class ShuffleRand
{
	// Token: 0x060094E7 RID: 38119 RVA: 0x003D6A15 File Offset: 0x003D4E15
	public ShuffleRand(int num = -1)
	{
		if (num != -1)
		{
			this.Init(num);
		}
	}

	// Token: 0x060094E8 RID: 38120 RVA: 0x003D6A2B File Offset: 0x003D4E2B
	public void Init(int num)
	{
		this.no = new int[num];
		this.Shuffle();
	}

	// Token: 0x060094E9 RID: 38121 RVA: 0x003D6A40 File Offset: 0x003D4E40
	private void Shuffle()
	{
		if (this.no.Length == 0)
		{
			return;
		}
		int[] array = new int[this.no.Length];
		for (int j = 0; j < this.no.Length; j++)
		{
			array[j] = j;
		}
		this.no = (from i in array
		orderby Guid.NewGuid()
		select i).ToArray<int>();
		this.cnt = 0;
	}

	// Token: 0x060094EA RID: 38122 RVA: 0x003D6ABC File Offset: 0x003D4EBC
	public int Get()
	{
		if (this.no.Length == 0)
		{
			return -1;
		}
		if (this.cnt >= this.no.Length)
		{
			this.Shuffle();
		}
		return this.no[this.cnt++];
	}

	// Token: 0x040077AD RID: 30637
	private int[] no;

	// Token: 0x040077AE RID: 30638
	private int cnt;
}

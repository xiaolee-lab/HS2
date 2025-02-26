using System;

// Token: 0x020004C9 RID: 1225
public class PA_UIFrameStats
{
	// Token: 0x060016A5 RID: 5797 RVA: 0x0008B1AF File Offset: 0x000895AF
	public void Clear()
	{
		this._wtbCnt = 0;
		this._wtbU1Cnt = 0;
		this._wtbNormCnt = 0;
		this._totalVertCount = 0;
	}

	// Token: 0x04001968 RID: 6504
	public int _wtbCnt;

	// Token: 0x04001969 RID: 6505
	public int _wtbU1Cnt;

	// Token: 0x0400196A RID: 6506
	public int _wtbNormCnt;

	// Token: 0x0400196B RID: 6507
	public int _totalVertCount;
}

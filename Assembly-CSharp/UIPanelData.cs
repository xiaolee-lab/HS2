using System;

// Token: 0x020004C5 RID: 1221
public class UIPanelData
{
	// Token: 0x0600168D RID: 5773 RVA: 0x0008A668 File Offset: 0x00088A68
	internal void Enlarge(UIPanelData value)
	{
		this.mElapsedTicks += value.mElapsedTicks;
		this.mCalls += value.mCalls;
		this.mRebuildCount += value.mRebuildCount;
		this.mDrawCallNum += value.mDrawCallNum;
	}

	// Token: 0x04001950 RID: 6480
	public double mElapsedTicks;

	// Token: 0x04001951 RID: 6481
	public int mCalls;

	// Token: 0x04001952 RID: 6482
	public int mRebuildCount;

	// Token: 0x04001953 RID: 6483
	public int mDrawCallNum;
}

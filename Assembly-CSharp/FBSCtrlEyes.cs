using System;

// Token: 0x020010F6 RID: 4342
[Serializable]
public class FBSCtrlEyes : FBSBase
{
	// Token: 0x06008FF9 RID: 36857 RVA: 0x003BFDA6 File Offset: 0x003BE1A6
	public void CalcBlend(float blinkRate)
	{
		if (0f <= blinkRate)
		{
			this.openRate = blinkRate;
		}
		base.CalculateBlendShape();
	}
}

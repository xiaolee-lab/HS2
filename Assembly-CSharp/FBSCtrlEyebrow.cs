using System;

// Token: 0x020010F5 RID: 4341
[Serializable]
public class FBSCtrlEyebrow : FBSBase
{
	// Token: 0x06008FF7 RID: 36855 RVA: 0x003BFD79 File Offset: 0x003BE179
	public void CalcBlend(float blinkRate)
	{
		if (0f <= blinkRate && this.SyncBlink)
		{
			this.openRate = blinkRate;
		}
		base.CalculateBlendShape();
	}

	// Token: 0x040074C2 RID: 29890
	public bool SyncBlink = true;
}

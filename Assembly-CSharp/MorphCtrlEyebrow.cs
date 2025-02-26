using System;

// Token: 0x0200111A RID: 4378
[Serializable]
public class MorphCtrlEyebrow : MorphFaceBase
{
	// Token: 0x06009111 RID: 37137 RVA: 0x003C6849 File Offset: 0x003C4C49
	public void CalcBlend(float blinkRate)
	{
		if (0f <= blinkRate && this.SyncBlink)
		{
			this.openRate = blinkRate;
		}
		base.CalculateBlendVertex();
	}

	// Token: 0x040075A2 RID: 30114
	public bool SyncBlink = true;
}

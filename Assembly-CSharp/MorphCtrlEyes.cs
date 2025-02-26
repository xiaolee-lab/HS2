using System;

// Token: 0x0200111B RID: 4379
[Serializable]
public class MorphCtrlEyes : MorphFaceBase
{
	// Token: 0x06009113 RID: 37139 RVA: 0x003C6876 File Offset: 0x003C4C76
	public void CalcBlend(float blinkRate)
	{
		if (0f <= blinkRate)
		{
			this.openRate = blinkRate;
		}
		base.CalculateBlendVertex();
	}
}

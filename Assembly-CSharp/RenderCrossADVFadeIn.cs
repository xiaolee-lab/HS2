using System;

// Token: 0x020010B7 RID: 4279
public class RenderCrossADVFadeIn : BaseRenderCrossFade
{
	// Token: 0x06008ECE RID: 36558 RVA: 0x003B683C File Offset: 0x003B4C3C
	protected override void Awake()
	{
		base.Awake();
		this.isAlphaAdd = false;
		base.Capture();
	}
}

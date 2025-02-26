using System;

// Token: 0x020010B8 RID: 4280
public class RenderCrossADVFadeOut : BaseRenderCrossFade
{
	// Token: 0x06008ED0 RID: 36560 RVA: 0x003B6859 File Offset: 0x003B4C59
	protected override void Awake()
	{
		base.Awake();
		this.isAlphaAdd = true;
		base.Capture();
	}
}

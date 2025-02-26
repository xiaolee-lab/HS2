using System;
using UnityEngine;

// Token: 0x020007DF RID: 2015
public class ShapeHeadInfoMale : ShapeHeadInfoFemale
{
	// Token: 0x060031A2 RID: 12706 RVA: 0x0012E2B6 File Offset: 0x0012C6B6
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoPath, string cateInfoPath, Transform trfObj)
	{
		base.InitShapeInfo(manifest, assetBundleAnmKey, assetBundleCategory, anmKeyInfoPath, cateInfoPath, trfObj);
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x0012E2C7 File Offset: 0x0012C6C7
	public override void ForceUpdate()
	{
		base.ForceUpdate();
	}

	// Token: 0x060031A4 RID: 12708 RVA: 0x0012E2CF File Offset: 0x0012C6CF
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x0012E2D7 File Offset: 0x0012C6D7
	public override void UpdateAlways()
	{
		base.UpdateAlways();
	}
}

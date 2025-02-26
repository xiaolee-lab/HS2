using System;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
public class ShapeBodyInfoMale : ShapeBodyInfoFemale
{
	// Token: 0x06003193 RID: 12691 RVA: 0x0012C40E File Offset: 0x0012A80E
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoPath, string cateInfoPath, Transform trfObj)
	{
		base.InitShapeInfo(manifest, assetBundleAnmKey, assetBundleCategory, anmKeyInfoPath, cateInfoPath, trfObj);
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x0012C41F File Offset: 0x0012A81F
	public override void ForceUpdate()
	{
		base.ForceUpdate();
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x0012C427 File Offset: 0x0012A827
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x0012C42F File Offset: 0x0012A82F
	public override void UpdateAlways()
	{
		base.UpdateAlways();
	}
}

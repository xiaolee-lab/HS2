using System;
using System.Collections.Generic;
using IllusionUtility.SetUtility;
using UnityEngine;

// Token: 0x020010CC RID: 4300
public class ShapeInfoSample : ShapeInfoBase
{
	// Token: 0x06008F37 RID: 36663 RVA: 0x003B832C File Offset: 0x003B672C
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string resAnmKeyInfoPath, string resCateInfoPath, Transform trfObj)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		ShapeInfoSample.DstBoneName[] array = (ShapeInfoSample.DstBoneName[])Enum.GetValues(typeof(ShapeInfoSample.DstBoneName));
		foreach (ShapeInfoSample.DstBoneName value in array)
		{
			dictionary[value.ToString()] = (int)value;
		}
		Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
		ShapeInfoSample.SrcBoneName[] array3 = (ShapeInfoSample.SrcBoneName[])Enum.GetValues(typeof(ShapeInfoSample.SrcBoneName));
		foreach (ShapeInfoSample.SrcBoneName value2 in array3)
		{
			dictionary2[value2.ToString()] = (int)value2;
		}
		base.InitShapeInfoBase(manifest, assetBundleAnmKey, assetBundleCategory, resAnmKeyInfoPath, resCateInfoPath, trfObj, dictionary, dictionary2, null);
		this.initEnd = true;
	}

	// Token: 0x06008F38 RID: 36664 RVA: 0x003B83F8 File Offset: 0x003B67F8
	public override void ForceUpdate()
	{
		this.Update();
	}

	// Token: 0x06008F39 RID: 36665 RVA: 0x003B8400 File Offset: 0x003B6800
	public override void Update()
	{
		if (!this.initEnd)
		{
			return;
		}
		this.dictDst[0].trfBone.SetLocalScale(this.dictSrc[0].vctScl.x, this.dictSrc[0].vctScl.y, this.dictSrc[0].vctScl.z);
		this.dictDst[1].trfBone.SetLocalScale(this.dictSrc[1].vctScl.x, this.dictSrc[1].vctScl.y, this.dictSrc[1].vctScl.z);
		this.dictDst[2].trfBone.SetLocalScale(this.dictSrc[1].vctScl.x * this.dictSrc[2].vctScl.x, this.dictSrc[1].vctScl.y, this.dictSrc[1].vctScl.z * this.dictSrc[3].vctScl.z);
		this.dictDst[3].trfBone.SetLocalScale(this.dictSrc[1].vctScl.x * this.dictSrc[4].vctScl.x, this.dictSrc[1].vctScl.y, this.dictSrc[1].vctScl.z * this.dictSrc[5].vctScl.z);
		this.dictDst[4].trfBone.SetLocalPositionX(this.dictSrc[6].vctPos.x);
		this.dictDst[4].trfBone.SetLocalPositionY(this.dictSrc[7].vctPos.y);
		this.dictDst[4].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[8].vctRot.z);
		this.dictDst[4].trfBone.SetLocalScale(this.dictSrc[1].vctScl.x, this.dictSrc[1].vctScl.y, this.dictSrc[1].vctScl.z);
		this.dictDst[5].trfBone.SetLocalPositionY(this.dictSrc[9].vctPos.y);
		this.dictDst[5].trfBone.SetLocalRotation(this.dictSrc[10].vctRot.x, 0f, 0f);
		this.dictDst[5].trfBone.SetLocalScale(1f, this.dictSrc[10].vctScl.y, 1f);
	}

	// Token: 0x06008F3A RID: 36666 RVA: 0x003B8756 File Offset: 0x003B6B56
	public override void UpdateAlways()
	{
	}

	// Token: 0x040073AD RID: 29613
	private bool initEnd;

	// Token: 0x020010CD RID: 4301
	public enum DstBoneName
	{
		// Token: 0x040073AF RID: 29615
		cf_J_Root,
		// Token: 0x040073B0 RID: 29616
		cf_J_Root_s,
		// Token: 0x040073B1 RID: 29617
		cf_J_Spine01_s,
		// Token: 0x040073B2 RID: 29618
		cf_J_Spine02_s,
		// Token: 0x040073B3 RID: 29619
		cf_J_Spine03_s,
		// Token: 0x040073B4 RID: 29620
		cf_J_Spine02_ss
	}

	// Token: 0x020010CE RID: 4302
	public enum SrcBoneName
	{
		// Token: 0x040073B6 RID: 29622
		cf_J_Root_height,
		// Token: 0x040073B7 RID: 29623
		cf_J_Root_1,
		// Token: 0x040073B8 RID: 29624
		cf_J_Spine01_s_sx,
		// Token: 0x040073B9 RID: 29625
		cf_J_Spine01_s_sz,
		// Token: 0x040073BA RID: 29626
		cf_J_Spine02_s_sx,
		// Token: 0x040073BB RID: 29627
		cf_J_Spine02_s_sz,
		// Token: 0x040073BC RID: 29628
		cf_J_Spine03_s_tx,
		// Token: 0x040073BD RID: 29629
		cf_J_Spine03_s_ty,
		// Token: 0x040073BE RID: 29630
		cf_J_Spine03_s_rz,
		// Token: 0x040073BF RID: 29631
		cf_J_Spine02_ss_ty,
		// Token: 0x040073C0 RID: 29632
		cf_J_Spine02_ss_rx
	}
}

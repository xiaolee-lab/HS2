using System;
using System.Collections.Generic;
using IllusionUtility.SetUtility;
using Manager;
using UnityEngine;

// Token: 0x020007D9 RID: 2009
public class ShapeHandInfo : ShapeInfoBase
{
	// Token: 0x06003198 RID: 12696 RVA: 0x0012C440 File Offset: 0x0012A840
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoName, string cateInfoName, Transform trfObj)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		ShapeHandInfo.DstName[] array = (ShapeHandInfo.DstName[])Enum.GetValues(typeof(ShapeHandInfo.DstName));
		foreach (ShapeHandInfo.DstName value in array)
		{
			dictionary[value.ToString()] = (int)value;
		}
		Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
		ShapeHandInfo.SrcName[] array3 = (ShapeHandInfo.SrcName[])Enum.GetValues(typeof(ShapeHandInfo.SrcName));
		foreach (ShapeHandInfo.SrcName value2 in array3)
		{
			dictionary2[value2.ToString()] = (int)value2;
		}
		base.InitShapeInfoBase(manifest, assetBundleAnmKey, assetBundleCategory, anmKeyInfoName, cateInfoName, trfObj, dictionary, dictionary2, new Action<string, string>(Singleton<Character>.Instance.AddLoadAssetBundle));
		base.InitEnd = true;
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x0012C51B File Offset: 0x0012A91B
	public override void ForceUpdate()
	{
		this.Update();
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x0012C523 File Offset: 0x0012A923
	public override void Update()
	{
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x0012C528 File Offset: 0x0012A928
	public override void UpdateAlways()
	{
		if (!base.InitEnd)
		{
			return;
		}
		if (this.dictSrc.Count == 0)
		{
			return;
		}
		ShapeInfoBase.BoneInfo boneInfo = null;
		if ((this.updateMask & 1) != 0)
		{
			int num = 0;
			int num2 = 14;
			for (int i = num; i <= num2; i++)
			{
				if (this.dictDst.TryGetValue(i, out boneInfo))
				{
					boneInfo.trfBone.SetLocalRotation(this.dictSrc[i].vctRot.x, this.dictSrc[i].vctRot.y, this.dictSrc[i].vctRot.z);
				}
			}
		}
		if ((this.updateMask & 2) != 0)
		{
			int num3 = 15;
			int num4 = 29;
			for (int j = num3; j <= num4; j++)
			{
				if (this.dictDst.TryGetValue(j, out boneInfo))
				{
					boneInfo.trfBone.SetLocalRotation(this.dictSrc[j].vctRot.x, this.dictSrc[j].vctRot.y, this.dictSrc[j].vctRot.z);
				}
			}
		}
	}

	// Token: 0x0400310A RID: 12554
	public const int UPDATE_MASK_HAND_L = 1;

	// Token: 0x0400310B RID: 12555
	public const int UPDATE_MASK_HAND_R = 2;

	// Token: 0x0400310C RID: 12556
	public int updateMask;

	// Token: 0x020007DA RID: 2010
	public enum DstName
	{
		// Token: 0x0400310E RID: 12558
		cf_J_Hand_Index01_L,
		// Token: 0x0400310F RID: 12559
		cf_J_Hand_Index02_L,
		// Token: 0x04003110 RID: 12560
		cf_J_Hand_Index03_L,
		// Token: 0x04003111 RID: 12561
		cf_J_Hand_Little01_L,
		// Token: 0x04003112 RID: 12562
		cf_J_Hand_Little02_L,
		// Token: 0x04003113 RID: 12563
		cf_J_Hand_Little03_L,
		// Token: 0x04003114 RID: 12564
		cf_J_Hand_Middle01_L,
		// Token: 0x04003115 RID: 12565
		cf_J_Hand_Middle02_L,
		// Token: 0x04003116 RID: 12566
		cf_J_Hand_Middle03_L,
		// Token: 0x04003117 RID: 12567
		cf_J_Hand_Ring01_L,
		// Token: 0x04003118 RID: 12568
		cf_J_Hand_Ring02_L,
		// Token: 0x04003119 RID: 12569
		cf_J_Hand_Ring03_L,
		// Token: 0x0400311A RID: 12570
		cf_J_Hand_Thumb01_L,
		// Token: 0x0400311B RID: 12571
		cf_J_Hand_Thumb02_L,
		// Token: 0x0400311C RID: 12572
		cf_J_Hand_Thumb03_L,
		// Token: 0x0400311D RID: 12573
		cf_J_Hand_Index01_R,
		// Token: 0x0400311E RID: 12574
		cf_J_Hand_Index02_R,
		// Token: 0x0400311F RID: 12575
		cf_J_Hand_Index03_R,
		// Token: 0x04003120 RID: 12576
		cf_J_Hand_Little01_R,
		// Token: 0x04003121 RID: 12577
		cf_J_Hand_Little02_R,
		// Token: 0x04003122 RID: 12578
		cf_J_Hand_Little03_R,
		// Token: 0x04003123 RID: 12579
		cf_J_Hand_Middle01_R,
		// Token: 0x04003124 RID: 12580
		cf_J_Hand_Middle02_R,
		// Token: 0x04003125 RID: 12581
		cf_J_Hand_Middle03_R,
		// Token: 0x04003126 RID: 12582
		cf_J_Hand_Ring01_R,
		// Token: 0x04003127 RID: 12583
		cf_J_Hand_Ring02_R,
		// Token: 0x04003128 RID: 12584
		cf_J_Hand_Ring03_R,
		// Token: 0x04003129 RID: 12585
		cf_J_Hand_Thumb01_R,
		// Token: 0x0400312A RID: 12586
		cf_J_Hand_Thumb02_R,
		// Token: 0x0400312B RID: 12587
		cf_J_Hand_Thumb03_R
	}

	// Token: 0x020007DB RID: 2011
	public enum SrcName
	{
		// Token: 0x0400312D RID: 12589
		cf_J_Hand_Index01_L,
		// Token: 0x0400312E RID: 12590
		cf_J_Hand_Index02_L,
		// Token: 0x0400312F RID: 12591
		cf_J_Hand_Index03_L,
		// Token: 0x04003130 RID: 12592
		cf_J_Hand_Little01_L,
		// Token: 0x04003131 RID: 12593
		cf_J_Hand_Little02_L,
		// Token: 0x04003132 RID: 12594
		cf_J_Hand_Little03_L,
		// Token: 0x04003133 RID: 12595
		cf_J_Hand_Middle01_L,
		// Token: 0x04003134 RID: 12596
		cf_J_Hand_Middle02_L,
		// Token: 0x04003135 RID: 12597
		cf_J_Hand_Middle03_L,
		// Token: 0x04003136 RID: 12598
		cf_J_Hand_Ring01_L,
		// Token: 0x04003137 RID: 12599
		cf_J_Hand_Ring02_L,
		// Token: 0x04003138 RID: 12600
		cf_J_Hand_Ring03_L,
		// Token: 0x04003139 RID: 12601
		cf_J_Hand_Thumb01_L,
		// Token: 0x0400313A RID: 12602
		cf_J_Hand_Thumb02_L,
		// Token: 0x0400313B RID: 12603
		cf_J_Hand_Thumb03_L,
		// Token: 0x0400313C RID: 12604
		cf_J_Hand_Index01_R,
		// Token: 0x0400313D RID: 12605
		cf_J_Hand_Index02_R,
		// Token: 0x0400313E RID: 12606
		cf_J_Hand_Index03_R,
		// Token: 0x0400313F RID: 12607
		cf_J_Hand_Little01_R,
		// Token: 0x04003140 RID: 12608
		cf_J_Hand_Little02_R,
		// Token: 0x04003141 RID: 12609
		cf_J_Hand_Little03_R,
		// Token: 0x04003142 RID: 12610
		cf_J_Hand_Middle01_R,
		// Token: 0x04003143 RID: 12611
		cf_J_Hand_Middle02_R,
		// Token: 0x04003144 RID: 12612
		cf_J_Hand_Middle03_R,
		// Token: 0x04003145 RID: 12613
		cf_J_Hand_Ring01_R,
		// Token: 0x04003146 RID: 12614
		cf_J_Hand_Ring02_R,
		// Token: 0x04003147 RID: 12615
		cf_J_Hand_Ring03_R,
		// Token: 0x04003148 RID: 12616
		cf_J_Hand_Thumb01_R,
		// Token: 0x04003149 RID: 12617
		cf_J_Hand_Thumb02_R,
		// Token: 0x0400314A RID: 12618
		cf_J_Hand_Thumb03_R
	}
}

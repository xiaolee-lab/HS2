using System;
using System.Collections.Generic;
using IllusionUtility.SetUtility;
using Manager;
using UnityEngine;

// Token: 0x020007DC RID: 2012
public class ShapeHeadInfoFemale : ShapeInfoBase
{
	// Token: 0x0600319D RID: 12701 RVA: 0x0012C674 File Offset: 0x0012AA74
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoPath, string cateInfoPath, Transform trfObj)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		ShapeHeadInfoFemale.DstName[] array = (ShapeHeadInfoFemale.DstName[])Enum.GetValues(typeof(ShapeHeadInfoFemale.DstName));
		foreach (ShapeHeadInfoFemale.DstName value in array)
		{
			dictionary[value.ToString()] = (int)value;
		}
		Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
		ShapeHeadInfoFemale.SrcName[] array3 = (ShapeHeadInfoFemale.SrcName[])Enum.GetValues(typeof(ShapeHeadInfoFemale.SrcName));
		foreach (ShapeHeadInfoFemale.SrcName value2 in array3)
		{
			dictionary2[value2.ToString()] = (int)value2;
		}
		base.InitShapeInfoBase(manifest, assetBundleAnmKey, assetBundleCategory, anmKeyInfoPath, cateInfoPath, trfObj, dictionary, dictionary2, new Action<string, string>(Singleton<Character>.Instance.AddLoadAssetBundle));
		base.InitEnd = true;
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x0012C74F File Offset: 0x0012AB4F
	public override void ForceUpdate()
	{
		this.Update();
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x0012C758 File Offset: 0x0012AB58
	public override void Update()
	{
		if (!base.InitEnd)
		{
			return;
		}
		if (this.dictSrc.Count == 0)
		{
			return;
		}
		this.dictDst[51].trfBone.SetLocalPositionY(this.dictSrc[100].vctPos.y);
		this.dictDst[51].trfBone.SetLocalPositionZ(this.dictSrc[101].vctPos.z + this.dictSrc[100].vctPos.z);
		this.dictDst[19].trfBone.SetLocalPositionX(this.dictSrc[41].vctPos.x);
		this.dictDst[19].trfBone.SetLocalPositionY(this.dictSrc[43].vctPos.y);
		this.dictDst[19].trfBone.SetLocalPositionZ(this.dictSrc[44].vctPos.z);
		this.dictDst[19].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[35].vctRot.z);
		this.dictDst[20].trfBone.SetLocalPositionX(this.dictSrc[42].vctPos.x);
		this.dictDst[20].trfBone.SetLocalPositionY(this.dictSrc[43].vctPos.y);
		this.dictDst[20].trfBone.SetLocalPositionZ(this.dictSrc[44].vctPos.z);
		this.dictDst[20].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[36].vctRot.z);
		this.dictDst[17].trfBone.SetLocalScale(this.dictSrc[37].vctScl.x, this.dictSrc[39].vctScl.y, 1f);
		this.dictDst[15].trfBone.SetLocalRotation(0f, this.dictSrc[33].vctRot.y, 0f);
		this.dictDst[18].trfBone.SetLocalScale(this.dictSrc[38].vctScl.x, this.dictSrc[40].vctScl.y, 1f);
		this.dictDst[16].trfBone.SetLocalRotation(0f, this.dictSrc[34].vctRot.y, 0f);
		this.dictDst[21].trfBone.SetLocalRotation(this.dictSrc[47].vctRot.x, this.dictSrc[45].vctRot.y + this.dictSrc[47].vctRot.y, 0f);
		this.dictDst[23].trfBone.SetLocalRotation(this.dictSrc[49].vctRot.x, this.dictSrc[51].vctRot.y, this.dictSrc[51].vctRot.z);
		this.dictDst[25].trfBone.SetLocalPositionX(this.dictSrc[53].vctPos.x);
		this.dictDst[25].trfBone.SetLocalRotation(this.dictSrc[55].vctRot.x, this.dictSrc[53].vctRot.y, 0f);
		this.dictDst[27].trfBone.SetLocalRotation(this.dictSrc[57].vctRot.x, this.dictSrc[59].vctRot.y, this.dictSrc[59].vctRot.z);
		this.dictDst[22].trfBone.SetLocalRotation(this.dictSrc[48].vctRot.x, this.dictSrc[46].vctRot.y + this.dictSrc[48].vctRot.y, 0f);
		this.dictDst[24].trfBone.SetLocalRotation(this.dictSrc[50].vctRot.x, this.dictSrc[52].vctRot.y, this.dictSrc[52].vctRot.z);
		this.dictDst[26].trfBone.SetLocalPositionX(this.dictSrc[54].vctPos.x);
		this.dictDst[26].trfBone.SetLocalRotation(this.dictSrc[56].vctRot.x, this.dictSrc[54].vctRot.y, 0f);
		this.dictDst[28].trfBone.SetLocalRotation(this.dictSrc[58].vctRot.x, this.dictSrc[60].vctRot.y, this.dictSrc[60].vctRot.z);
		this.dictDst[29].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[61].vctRot.z);
		this.dictDst[30].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[62].vctRot.z);
		this.dictDst[31].trfBone.SetLocalScale(this.dictSrc[63].vctScl.x, 1f, 1f);
		this.dictDst[34].trfBone.SetLocalPositionY(this.dictSrc[66].vctPos.y);
		this.dictDst[35].trfBone.SetLocalPositionZ(this.dictSrc[67].vctPos.z);
		this.dictDst[2].trfBone.SetLocalPositionX(this.dictSrc[4].vctPos.x);
		this.dictDst[2].trfBone.SetLocalPositionY(this.dictSrc[6].vctPos.y);
		this.dictDst[2].trfBone.SetLocalPositionZ(this.dictSrc[7].vctPos.z + this.dictSrc[8].vctPos.z);
		this.dictDst[3].trfBone.SetLocalPositionX(this.dictSrc[5].vctPos.x);
		this.dictDst[3].trfBone.SetLocalPositionY(this.dictSrc[6].vctPos.y);
		this.dictDst[3].trfBone.SetLocalPositionZ(this.dictSrc[7].vctPos.z + this.dictSrc[8].vctPos.z);
		this.dictDst[0].trfBone.SetLocalPositionX(this.dictSrc[0].vctPos.x);
		this.dictDst[0].trfBone.SetLocalPositionY(this.dictSrc[2].vctPos.y);
		this.dictDst[0].trfBone.SetLocalPositionZ(this.dictSrc[3].vctPos.z);
		this.dictDst[1].trfBone.SetLocalPositionX(this.dictSrc[1].vctPos.x);
		this.dictDst[1].trfBone.SetLocalPositionY(this.dictSrc[2].vctPos.y);
		this.dictDst[1].trfBone.SetLocalPositionZ(this.dictSrc[3].vctPos.z);
		this.dictDst[33].trfBone.SetLocalPositionZ(this.dictSrc[65].vctPos.z);
		this.dictDst[32].trfBone.SetLocalScale(this.dictSrc[64].vctScl.x, 1f, 1f);
		this.dictDst[42].trfBone.SetLocalPositionY(this.dictSrc[78].vctPos.y);
		this.dictDst[42].trfBone.SetLocalPositionZ(this.dictSrc[78].vctPos.z + this.dictSrc[72].vctPos.z);
		this.dictDst[41].trfBone.SetLocalScale(this.dictSrc[76].vctScl.x, this.dictSrc[77].vctScl.y, 1f);
		this.dictDst[40].trfBone.SetLocalPositionY(this.dictSrc[73].vctPos.y);
		this.dictDst[39].trfBone.SetLocalPositionY(this.dictSrc[79].vctPos.y);
		this.dictDst[39].trfBone.SetLocalPositionZ(this.dictSrc[79].vctPos.z);
		this.dictDst[39].trfBone.SetLocalScale(this.dictSrc[79].vctScl.x, 1f, 1f);
		this.dictDst[37].trfBone.SetLocalPositionY(this.dictSrc[74].vctPos.y);
		this.dictDst[37].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[74].vctRot.z);
		this.dictDst[38].trfBone.SetLocalPositionY(this.dictSrc[75].vctPos.y);
		this.dictDst[38].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[75].vctRot.z);
		this.dictDst[5].trfBone.SetLocalPositionY(this.dictSrc[13].vctPos.y);
		this.dictDst[4].trfBone.SetLocalPositionY(this.dictSrc[11].vctPos.y + this.dictSrc[9].vctPos.y);
		this.dictDst[4].trfBone.SetLocalPositionZ(this.dictSrc[12].vctPos.z + this.dictSrc[9].vctPos.z);
		this.dictDst[4].trfBone.SetLocalRotation(this.dictSrc[9].vctRot.x, 0f, 0f);
		this.dictDst[4].trfBone.SetLocalScale(this.dictSrc[10].vctScl.x, 1f, 1f);
		this.dictDst[6].trfBone.SetLocalPositionY(this.dictSrc[15].vctPos.y);
		this.dictDst[6].trfBone.SetLocalPositionZ(this.dictSrc[16].vctPos.z);
		this.dictDst[6].trfBone.SetLocalScale(this.dictSrc[14].vctScl.x, this.dictSrc[14].vctScl.y, this.dictSrc[14].vctScl.z);
		this.dictDst[48].trfBone.SetLocalPositionY(this.dictSrc[90].vctPos.y);
		this.dictDst[48].trfBone.SetLocalPositionZ(this.dictSrc[91].vctPos.z + this.dictSrc[92].vctPos.z + this.dictSrc[90].vctPos.z + this.dictSrc[88].vctPos.z);
		this.dictDst[48].trfBone.SetLocalRotation(this.dictSrc[88].vctRot.x, 0f, 0f);
		this.dictDst[47].trfBone.SetLocalScale(this.dictSrc[89].vctScl.x, 1f, 1f);
		this.dictDst[46].trfBone.SetLocalPositionY(this.dictSrc[84].vctPos.y + this.dictSrc[86].vctPos.y + this.dictSrc[83].vctPos.y);
		this.dictDst[46].trfBone.SetLocalPositionZ(this.dictSrc[84].vctPos.z + this.dictSrc[87].vctPos.z + this.dictSrc[83].vctPos.z);
		this.dictDst[45].trfBone.SetLocalRotation(this.dictSrc[84].vctRot.x + this.dictSrc[83].vctRot.x, 0f, 0f);
		this.dictDst[45].trfBone.SetLocalScale(this.dictSrc[85].vctScl.x, this.dictSrc[85].vctScl.y, this.dictSrc[85].vctScl.z);
		this.dictDst[49].trfBone.SetLocalPositionX(this.dictSrc[96].vctPos.x);
		this.dictDst[49].trfBone.SetLocalPositionY(this.dictSrc[98].vctPos.y);
		this.dictDst[49].trfBone.SetLocalPositionZ(this.dictSrc[99].vctPos.z);
		this.dictDst[49].trfBone.SetLocalRotation(this.dictSrc[93].vctRot.x, 0f, this.dictSrc[94].vctRot.z);
		this.dictDst[50].trfBone.SetLocalPositionX(this.dictSrc[97].vctPos.x);
		this.dictDst[50].trfBone.SetLocalPositionY(this.dictSrc[98].vctPos.y);
		this.dictDst[50].trfBone.SetLocalPositionZ(this.dictSrc[99].vctPos.z);
		this.dictDst[50].trfBone.SetLocalRotation(this.dictSrc[93].vctRot.x, 0f, this.dictSrc[95].vctRot.z);
		this.dictDst[44].trfBone.SetLocalPositionY(this.dictSrc[81].vctPos.y);
		this.dictDst[44].trfBone.SetLocalPositionZ(this.dictSrc[81].vctPos.z);
		this.dictDst[44].trfBone.SetLocalScale(this.dictSrc[81].vctScl.x, this.dictSrc[81].vctScl.y, this.dictSrc[81].vctScl.z);
		this.dictDst[43].trfBone.SetLocalPositionY(this.dictSrc[80].vctPos.y);
		this.dictDst[43].trfBone.SetLocalPositionZ(this.dictSrc[82].vctPos.z);
		this.dictDst[43].trfBone.SetLocalRotation(this.dictSrc[80].vctRot.x, 0f, 0f);
		this.dictDst[36].trfBone.SetLocalPositionY(this.dictSrc[70].vctPos.y + this.dictSrc[68].vctPos.y + this.dictSrc[69].vctPos.y);
		this.dictDst[36].trfBone.SetLocalPositionZ(this.dictSrc[70].vctPos.z + this.dictSrc[71].vctPos.z + this.dictSrc[69].vctPos.z);
		this.dictDst[36].trfBone.SetLocalRotation(this.dictSrc[70].vctRot.x + this.dictSrc[68].vctRot.x + this.dictSrc[69].vctRot.x, 0f, 0f);
		this.dictDst[7].trfBone.SetLocalRotation(0f, this.dictSrc[17].vctRot.y, this.dictSrc[19].vctRot.z);
		this.dictDst[7].trfBone.SetLocalScale(this.dictSrc[21].vctScl.x, this.dictSrc[21].vctScl.y, this.dictSrc[21].vctScl.z);
		this.dictDst[13].trfBone.SetLocalPositionX(this.dictSrc[31].vctPos.x);
		this.dictDst[13].trfBone.SetLocalPositionY(this.dictSrc[31].vctPos.y);
		this.dictDst[13].trfBone.SetLocalPositionZ(this.dictSrc[31].vctPos.z);
		this.dictDst[13].trfBone.SetLocalRotation(this.dictSrc[31].vctRot.x, this.dictSrc[31].vctRot.y, 0f);
		this.dictDst[13].trfBone.SetLocalScale(this.dictSrc[31].vctScl.x, this.dictSrc[31].vctScl.y, this.dictSrc[31].vctScl.z);
		this.dictDst[9].trfBone.SetLocalPositionY(this.dictSrc[23].vctPos.y);
		this.dictDst[9].trfBone.SetLocalPositionZ(this.dictSrc[23].vctPos.z);
		this.dictDst[9].trfBone.SetLocalScale(this.dictSrc[23].vctScl.x, this.dictSrc[23].vctScl.y, this.dictSrc[23].vctScl.z);
		this.dictDst[8].trfBone.SetLocalRotation(0f, this.dictSrc[18].vctRot.y, this.dictSrc[20].vctRot.z);
		this.dictDst[8].trfBone.SetLocalScale(this.dictSrc[22].vctScl.x, this.dictSrc[22].vctScl.y, this.dictSrc[22].vctScl.z);
		this.dictDst[14].trfBone.SetLocalPositionX(this.dictSrc[32].vctPos.x);
		this.dictDst[14].trfBone.SetLocalPositionY(this.dictSrc[32].vctPos.y);
		this.dictDst[14].trfBone.SetLocalPositionZ(this.dictSrc[32].vctPos.z);
		this.dictDst[14].trfBone.SetLocalRotation(this.dictSrc[32].vctRot.x, this.dictSrc[32].vctRot.y, 0f);
		this.dictDst[14].trfBone.SetLocalScale(this.dictSrc[32].vctScl.x, this.dictSrc[32].vctScl.y, this.dictSrc[32].vctScl.z);
		this.dictDst[10].trfBone.SetLocalPositionY(this.dictSrc[24].vctPos.y);
		this.dictDst[10].trfBone.SetLocalPositionZ(this.dictSrc[24].vctPos.z);
		this.dictDst[10].trfBone.SetLocalScale(this.dictSrc[24].vctScl.x, this.dictSrc[24].vctScl.y, this.dictSrc[24].vctScl.z);
		this.dictDst[11].trfBone.SetLocalPositionY(this.dictSrc[25].vctPos.y);
		this.dictDst[11].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[27].vctRot.z);
		this.dictDst[11].trfBone.SetLocalScale(this.dictSrc[29].vctScl.x, this.dictSrc[29].vctScl.y, this.dictSrc[29].vctScl.z);
		this.dictDst[12].trfBone.SetLocalPositionY(this.dictSrc[26].vctPos.y);
		this.dictDst[12].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[28].vctRot.z);
		this.dictDst[12].trfBone.SetLocalScale(this.dictSrc[30].vctScl.x, this.dictSrc[30].vctScl.y, this.dictSrc[30].vctScl.z);
	}

	// Token: 0x060031A0 RID: 12704 RVA: 0x0012E2A0 File Offset: 0x0012C6A0
	public override void UpdateAlways()
	{
		if (!base.InitEnd)
		{
			return;
		}
	}

	// Token: 0x020007DD RID: 2013
	public enum DstName
	{
		// Token: 0x0400314C RID: 12620
		cf_J_CheekLow_L,
		// Token: 0x0400314D RID: 12621
		cf_J_CheekLow_R,
		// Token: 0x0400314E RID: 12622
		cf_J_CheekUp_L,
		// Token: 0x0400314F RID: 12623
		cf_J_CheekUp_R,
		// Token: 0x04003150 RID: 12624
		cf_J_Chin_rs,
		// Token: 0x04003151 RID: 12625
		cf_J_ChinLow,
		// Token: 0x04003152 RID: 12626
		cf_J_ChinTip_s,
		// Token: 0x04003153 RID: 12627
		cf_J_EarBase_s_L,
		// Token: 0x04003154 RID: 12628
		cf_J_EarBase_s_R,
		// Token: 0x04003155 RID: 12629
		cf_J_EarLow_L,
		// Token: 0x04003156 RID: 12630
		cf_J_EarLow_R,
		// Token: 0x04003157 RID: 12631
		cf_J_EarRing_L,
		// Token: 0x04003158 RID: 12632
		cf_J_EarRing_R,
		// Token: 0x04003159 RID: 12633
		cf_J_EarUp_L,
		// Token: 0x0400315A RID: 12634
		cf_J_EarUp_R,
		// Token: 0x0400315B RID: 12635
		cf_J_Eye_r_L,
		// Token: 0x0400315C RID: 12636
		cf_J_Eye_r_R,
		// Token: 0x0400315D RID: 12637
		cf_J_Eye_s_L,
		// Token: 0x0400315E RID: 12638
		cf_J_Eye_s_R,
		// Token: 0x0400315F RID: 12639
		cf_J_Eye_t_L,
		// Token: 0x04003160 RID: 12640
		cf_J_Eye_t_R,
		// Token: 0x04003161 RID: 12641
		cf_J_Eye01_L,
		// Token: 0x04003162 RID: 12642
		cf_J_Eye01_R,
		// Token: 0x04003163 RID: 12643
		cf_J_Eye02_L,
		// Token: 0x04003164 RID: 12644
		cf_J_Eye02_R,
		// Token: 0x04003165 RID: 12645
		cf_J_Eye03_L,
		// Token: 0x04003166 RID: 12646
		cf_J_Eye03_R,
		// Token: 0x04003167 RID: 12647
		cf_J_Eye04_L,
		// Token: 0x04003168 RID: 12648
		cf_J_Eye04_R,
		// Token: 0x04003169 RID: 12649
		cf_J_EyePos_rz_L,
		// Token: 0x0400316A RID: 12650
		cf_J_EyePos_rz_R,
		// Token: 0x0400316B RID: 12651
		cf_J_FaceBase,
		// Token: 0x0400316C RID: 12652
		cf_J_FaceLow_s,
		// Token: 0x0400316D RID: 12653
		cf_J_FaceLowBase,
		// Token: 0x0400316E RID: 12654
		cf_J_FaceUp_ty,
		// Token: 0x0400316F RID: 12655
		cf_J_FaceUp_tz,
		// Token: 0x04003170 RID: 12656
		cf_J_megane,
		// Token: 0x04003171 RID: 12657
		cf_J_Mouth_L,
		// Token: 0x04003172 RID: 12658
		cf_J_Mouth_R,
		// Token: 0x04003173 RID: 12659
		cf_J_MouthLow,
		// Token: 0x04003174 RID: 12660
		cf_J_Mouthup,
		// Token: 0x04003175 RID: 12661
		cf_J_MouthBase_s,
		// Token: 0x04003176 RID: 12662
		cf_J_MouthBase_tr,
		// Token: 0x04003177 RID: 12663
		cf_J_Nose_t,
		// Token: 0x04003178 RID: 12664
		cf_J_Nose_tip,
		// Token: 0x04003179 RID: 12665
		cf_J_NoseBase_s,
		// Token: 0x0400317A RID: 12666
		cf_J_NoseBase_trs,
		// Token: 0x0400317B RID: 12667
		cf_J_NoseBridge_s,
		// Token: 0x0400317C RID: 12668
		cf_J_NoseBridge_t,
		// Token: 0x0400317D RID: 12669
		cf_J_NoseWing_tx_L,
		// Token: 0x0400317E RID: 12670
		cf_J_NoseWing_tx_R,
		// Token: 0x0400317F RID: 12671
		cf_J_MouthCavity
	}

	// Token: 0x020007DE RID: 2014
	public enum SrcName
	{
		// Token: 0x04003181 RID: 12673
		cf_s_CheekLow_tx_L,
		// Token: 0x04003182 RID: 12674
		cf_s_CheekLow_tx_R,
		// Token: 0x04003183 RID: 12675
		cf_s_CheekLow_ty,
		// Token: 0x04003184 RID: 12676
		cf_s_CheekLow_tz,
		// Token: 0x04003185 RID: 12677
		cf_s_CheekUp_tx_L,
		// Token: 0x04003186 RID: 12678
		cf_s_CheekUp_tx_R,
		// Token: 0x04003187 RID: 12679
		cf_s_CheekUp_ty,
		// Token: 0x04003188 RID: 12680
		cf_s_CheekUp_tz_00,
		// Token: 0x04003189 RID: 12681
		cf_s_CheekUp_tz_01,
		// Token: 0x0400318A RID: 12682
		cf_s_Chin_rx,
		// Token: 0x0400318B RID: 12683
		cf_s_Chin_sx,
		// Token: 0x0400318C RID: 12684
		cf_s_Chin_ty,
		// Token: 0x0400318D RID: 12685
		cf_s_Chin_tz,
		// Token: 0x0400318E RID: 12686
		cf_s_ChinLow,
		// Token: 0x0400318F RID: 12687
		cf_s_ChinTip_sx,
		// Token: 0x04003190 RID: 12688
		cf_s_ChinTip_ty,
		// Token: 0x04003191 RID: 12689
		cf_s_ChinTip_tz,
		// Token: 0x04003192 RID: 12690
		cf_s_EarBase_ry_L,
		// Token: 0x04003193 RID: 12691
		cf_s_EarBase_ry_R,
		// Token: 0x04003194 RID: 12692
		cf_s_EarBase_rz_L,
		// Token: 0x04003195 RID: 12693
		cf_s_EarBase_rz_R,
		// Token: 0x04003196 RID: 12694
		cf_s_EarBase_s_L,
		// Token: 0x04003197 RID: 12695
		cf_s_EarBase_s_R,
		// Token: 0x04003198 RID: 12696
		cf_s_EarLow_L,
		// Token: 0x04003199 RID: 12697
		cf_s_EarLow_R,
		// Token: 0x0400319A RID: 12698
		cf_s_EarRing_L,
		// Token: 0x0400319B RID: 12699
		cf_s_EarRing_R,
		// Token: 0x0400319C RID: 12700
		cf_s_EarRing_rz_L,
		// Token: 0x0400319D RID: 12701
		cf_s_EarRing_rz_R,
		// Token: 0x0400319E RID: 12702
		cf_s_EarRing_s_L,
		// Token: 0x0400319F RID: 12703
		cf_s_EarRing_s_R,
		// Token: 0x040031A0 RID: 12704
		cf_s_EarUp_L,
		// Token: 0x040031A1 RID: 12705
		cf_s_EarUp_R,
		// Token: 0x040031A2 RID: 12706
		cf_s_Eye_ry_L,
		// Token: 0x040031A3 RID: 12707
		cf_s_Eye_ry_R,
		// Token: 0x040031A4 RID: 12708
		cf_s_Eye_rz_L,
		// Token: 0x040031A5 RID: 12709
		cf_s_Eye_rz_R,
		// Token: 0x040031A6 RID: 12710
		cf_s_Eye_sx_L,
		// Token: 0x040031A7 RID: 12711
		cf_s_Eye_sx_R,
		// Token: 0x040031A8 RID: 12712
		cf_s_Eye_sy_L,
		// Token: 0x040031A9 RID: 12713
		cf_s_Eye_sy_R,
		// Token: 0x040031AA RID: 12714
		cf_s_Eye_tx_L,
		// Token: 0x040031AB RID: 12715
		cf_s_Eye_tx_R,
		// Token: 0x040031AC RID: 12716
		cf_s_Eye_ty,
		// Token: 0x040031AD RID: 12717
		cf_s_Eye_tz,
		// Token: 0x040031AE RID: 12718
		cf_s_Eye01_L,
		// Token: 0x040031AF RID: 12719
		cf_s_Eye01_R,
		// Token: 0x040031B0 RID: 12720
		cf_s_Eye01_rx_L,
		// Token: 0x040031B1 RID: 12721
		cf_s_Eye01_rx_R,
		// Token: 0x040031B2 RID: 12722
		cf_s_Eye02_L,
		// Token: 0x040031B3 RID: 12723
		cf_s_Eye02_R,
		// Token: 0x040031B4 RID: 12724
		cf_s_Eye02_ry_L,
		// Token: 0x040031B5 RID: 12725
		cf_s_Eye02_ry_R,
		// Token: 0x040031B6 RID: 12726
		cf_s_Eye03_L,
		// Token: 0x040031B7 RID: 12727
		cf_s_Eye03_R,
		// Token: 0x040031B8 RID: 12728
		cf_s_Eye03_rx_L,
		// Token: 0x040031B9 RID: 12729
		cf_s_Eye03_rx_R,
		// Token: 0x040031BA RID: 12730
		cf_s_Eye04_L,
		// Token: 0x040031BB RID: 12731
		cf_s_Eye04_R,
		// Token: 0x040031BC RID: 12732
		cf_s_Eye04_ry_L,
		// Token: 0x040031BD RID: 12733
		cf_s_Eye04_ry_R,
		// Token: 0x040031BE RID: 12734
		cf_s_EyePos_rz_L,
		// Token: 0x040031BF RID: 12735
		cf_s_EyePos_rz_R,
		// Token: 0x040031C0 RID: 12736
		cf_s_FaceBase_sx,
		// Token: 0x040031C1 RID: 12737
		cf_s_FaceLow_sx,
		// Token: 0x040031C2 RID: 12738
		cf_s_FaceLow_tz,
		// Token: 0x040031C3 RID: 12739
		cf_s_FaceUp_ty,
		// Token: 0x040031C4 RID: 12740
		cf_s_FaceUp_tz,
		// Token: 0x040031C5 RID: 12741
		cf_s_megane_rx_nosebridge,
		// Token: 0x040031C6 RID: 12742
		cf_s_megane_ty_eye,
		// Token: 0x040031C7 RID: 12743
		cf_s_megane_ty_nose,
		// Token: 0x040031C8 RID: 12744
		cf_s_megane_tz_nosebridge,
		// Token: 0x040031C9 RID: 12745
		cf_s_MouthBase_tz,
		// Token: 0x040031CA RID: 12746
		cf_s_Mouthup,
		// Token: 0x040031CB RID: 12747
		cf_s_Mouth_L,
		// Token: 0x040031CC RID: 12748
		cf_s_Mouth_R,
		// Token: 0x040031CD RID: 12749
		cf_s_MouthBase_sx,
		// Token: 0x040031CE RID: 12750
		cf_s_MouthBase_sy,
		// Token: 0x040031CF RID: 12751
		cf_s_MouthBase_ty,
		// Token: 0x040031D0 RID: 12752
		cf_s_MouthLow,
		// Token: 0x040031D1 RID: 12753
		cf_s_Nose_rx,
		// Token: 0x040031D2 RID: 12754
		cf_s_Nose_tip,
		// Token: 0x040031D3 RID: 12755
		cf_s_Nose_tz,
		// Token: 0x040031D4 RID: 12756
		cf_s_NoseBase,
		// Token: 0x040031D5 RID: 12757
		cf_s_NoseBase_rx,
		// Token: 0x040031D6 RID: 12758
		cf_s_NoseBase_sx,
		// Token: 0x040031D7 RID: 12759
		cf_s_NoseBase_ty,
		// Token: 0x040031D8 RID: 12760
		cf_s_NoseBase_tz,
		// Token: 0x040031D9 RID: 12761
		cf_s_NoseBridge_rx,
		// Token: 0x040031DA RID: 12762
		cf_s_NoseBridge_sx,
		// Token: 0x040031DB RID: 12763
		cf_s_NoseBridge_ty,
		// Token: 0x040031DC RID: 12764
		cf_s_NoseBridge_tz_00,
		// Token: 0x040031DD RID: 12765
		cf_s_NoseBridge_tz_01,
		// Token: 0x040031DE RID: 12766
		cf_s_NoseWing_rx,
		// Token: 0x040031DF RID: 12767
		cf_s_NoseWing_rz_L,
		// Token: 0x040031E0 RID: 12768
		cf_s_NoseWing_rz_R,
		// Token: 0x040031E1 RID: 12769
		cf_s_NoseWing_tx_L,
		// Token: 0x040031E2 RID: 12770
		cf_s_NoseWing_tx_R,
		// Token: 0x040031E3 RID: 12771
		cf_s_NoseWing_ty,
		// Token: 0x040031E4 RID: 12772
		cf_s_NoseWing_tz,
		// Token: 0x040031E5 RID: 12773
		cf_s_MouthC_ty,
		// Token: 0x040031E6 RID: 12774
		cf_s_MouthC_tz
	}
}

using System;
using System.Collections.Generic;
using IllusionUtility.SetUtility;
using Manager;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
public class ShapeBodyInfoFemale : ShapeInfoBase
{
	// Token: 0x0600318E RID: 12686 RVA: 0x00127A3C File Offset: 0x00125E3C
	public override void InitShapeInfo(string manifest, string assetBundleAnmKey, string assetBundleCategory, string anmKeyInfoName, string cateInfoName, Transform trfObj)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		ShapeBodyInfoFemale.DstName[] array = (ShapeBodyInfoFemale.DstName[])Enum.GetValues(typeof(ShapeBodyInfoFemale.DstName));
		foreach (ShapeBodyInfoFemale.DstName value in array)
		{
			dictionary[value.ToString()] = (int)value;
		}
		Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
		ShapeBodyInfoFemale.SrcName[] array3 = (ShapeBodyInfoFemale.SrcName[])Enum.GetValues(typeof(ShapeBodyInfoFemale.SrcName));
		foreach (ShapeBodyInfoFemale.SrcName value2 in array3)
		{
			dictionary2[value2.ToString()] = (int)value2;
		}
		base.InitShapeInfoBase(manifest, assetBundleAnmKey, assetBundleCategory, anmKeyInfoName, cateInfoName, trfObj, dictionary, dictionary2, new Action<string, string>(Singleton<Character>.Instance.AddLoadAssetBundle));
		base.InitEnd = true;
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x00127B17 File Offset: 0x00125F17
	public override void ForceUpdate()
	{
		this.Update();
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x00127B20 File Offset: 0x00125F20
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
		float num = this.dictSrc[193].vctPos.y + this.dictSrc[194].vctPos.y;
		float num2 = this.dictSrc[193].vctScl.y * this.dictSrc[194].vctScl.y;
		float x = this.dictSrc[194].vctScl.x;
		float z = this.dictSrc[194].vctPos.z;
		float num3 = this.dictSrc[195].vctPos.y + this.dictSrc[196].vctPos.y;
		float num4 = this.dictSrc[195].vctPos.z + this.dictSrc[196].vctPos.z;
		float num5 = this.dictSrc[195].vctScl.y * this.dictSrc[196].vctScl.y;
		float num6 = this.dictSrc[195].vctScl.x * this.dictSrc[196].vctScl.x;
		float num7 = this.dictSrc[197].vctPos.x + this.dictSrc[198].vctPos.x;
		float num8 = this.dictSrc[197].vctRot.z + this.dictSrc[198].vctRot.z;
		float x2 = this.dictSrc[197].vctScl.x * this.dictSrc[198].vctScl.x;
		float y = this.dictSrc[197].vctScl.y * this.dictSrc[198].vctScl.y;
		float num9 = this.dictSrc[199].vctScl.x * this.dictSrc[200].vctScl.x;
		float num10 = this.dictSrc[199].vctScl.y * this.dictSrc[200].vctScl.y;
		float x3 = this.dictSrc[200].vctPos.x;
		float num11 = this.dictSrc[199].vctPos.z + this.dictSrc[200].vctPos.z;
		float x4 = this.dictSrc[201].vctPos.x;
		float y2 = this.dictSrc[201].vctPos.y;
		float z2 = this.dictSrc[201].vctPos.z;
		float x5 = this.dictSrc[201].vctScl.x;
		float y3 = this.dictSrc[201].vctScl.y;
		float x6 = this.dictSrc[202].vctPos.x;
		float y4 = this.dictSrc[202].vctPos.y;
		float z3 = this.dictSrc[202].vctPos.z;
		float x7 = this.dictSrc[202].vctScl.x;
		float x8 = this.dictSrc[202].vctRot.x;
		float z4 = this.dictSrc[204].vctPos.z;
		float x9 = this.dictSrc[204].vctScl.x;
		if ((this.updateMask & 16) != 0)
		{
			this.dictDst[71].trfBone.SetLocalScale(this.dictSrc[32].vctScl.x, this.dictSrc[32].vctScl.y, this.dictSrc[32].vctScl.z);
			this.dictDst[67].trfBone.SetLocalScale(this.dictSrc[150].vctScl.x, 1f, this.dictSrc[150].vctScl.z);
			this.dictDst[72].trfBone.SetLocalPositionZ(this.dictSrc[161].vctPos.z + this.dictSrc[162].vctPos.z + this.dictSrc[163].vctPos.z + this.dictSrc[164].vctPos.z);
			this.dictDst[72].trfBone.SetLocalRotation(this.dictSrc[163].vctRot.x + this.dictSrc[164].vctRot.x, 0f, 0f);
			this.dictDst[73].trfBone.SetLocalPositionX(this.dictSrc[165].vctPos.x);
			this.dictDst[73].trfBone.SetLocalPositionZ(this.dictSrc[165].vctPos.z + this.dictSrc[166].vctPos.z + this.dictSrc[167].vctPos.z + this.dictSrc[168].vctPos.z);
			this.dictDst[73].trfBone.SetLocalRotation(this.dictSrc[166].vctRot.x + this.dictSrc[167].vctRot.x + this.dictSrc[168].vctRot.x, 0f, 0f);
			this.dictDst[74].trfBone.SetLocalPositionX(this.dictSrc[169].vctPos.x + this.dictSrc[170].vctPos.x);
			this.dictDst[74].trfBone.SetLocalPositionZ(this.dictSrc[169].vctPos.z + this.dictSrc[170].vctPos.z + this.dictSrc[171].vctPos.z + this.dictSrc[172].vctPos.z);
			this.dictDst[74].trfBone.SetLocalRotation(this.dictSrc[169].vctRot.x + this.dictSrc[170].vctRot.x, 0f, 0f);
			this.dictDst[75].trfBone.SetLocalPositionX(this.dictSrc[173].vctPos.x);
			this.dictDst[75].trfBone.SetLocalPositionZ(this.dictSrc[173].vctPos.z + this.dictSrc[174].vctPos.z + this.dictSrc[175].vctPos.z + this.dictSrc[176].vctPos.z);
			this.dictDst[75].trfBone.SetLocalRotation(this.dictSrc[174].vctRot.x + this.dictSrc[175].vctRot.x + this.dictSrc[176].vctRot.x, 0f, 0f);
			this.dictDst[76].trfBone.SetLocalPositionZ(this.dictSrc[177].vctPos.z + this.dictSrc[178].vctPos.z + this.dictSrc[179].vctPos.z + this.dictSrc[180].vctPos.z);
			this.dictDst[76].trfBone.SetLocalRotation(this.dictSrc[179].vctRot.x + this.dictSrc[180].vctRot.x, 0f, 0f);
			this.dictDst[77].trfBone.SetLocalPositionX(this.dictSrc[181].vctPos.x + this.dictSrc[182].vctPos.x);
			this.dictDst[77].trfBone.SetLocalPositionZ(this.dictSrc[181].vctPos.z + this.dictSrc[182].vctPos.z + this.dictSrc[183].vctPos.z + this.dictSrc[184].vctPos.z);
			this.dictDst[77].trfBone.SetLocalRotation(this.dictSrc[182].vctRot.x + this.dictSrc[183].vctRot.x + this.dictSrc[184].vctRot.x, 0f, 0f);
			this.dictDst[78].trfBone.SetLocalPositionX(this.dictSrc[185].vctPos.x + this.dictSrc[186].vctPos.x);
			this.dictDst[78].trfBone.SetLocalPositionZ(this.dictSrc[185].vctPos.z + this.dictSrc[186].vctPos.z + this.dictSrc[187].vctPos.z + this.dictSrc[188].vctPos.z);
			this.dictDst[78].trfBone.SetLocalRotation(this.dictSrc[185].vctRot.x + this.dictSrc[186].vctRot.x, 0f, 0f);
			this.dictDst[79].trfBone.SetLocalPositionX(this.dictSrc[189].vctPos.x + this.dictSrc[190].vctPos.x);
			this.dictDst[79].trfBone.SetLocalPositionZ(this.dictSrc[189].vctPos.z + this.dictSrc[190].vctPos.z + this.dictSrc[191].vctPos.z + this.dictSrc[192].vctPos.z);
			this.dictDst[79].trfBone.SetLocalRotation(this.dictSrc[190].vctRot.x + this.dictSrc[191].vctRot.x + this.dictSrc[192].vctRot.x, 0f, 0f);
			this.dictDst[66].trfBone.SetLocalPositionZ(this.dictSrc[149].vctPos.z + this.dictSrc[148].vctPos.z);
			this.dictDst[66].trfBone.SetLocalRotation(this.dictSrc[149].vctRot.x, 0f, 0f);
			this.dictDst[62].trfBone.SetLocalPositionX(this.dictSrc[136].vctPos.x);
			this.dictDst[62].trfBone.SetLocalScale(this.dictSrc[132].vctScl.x, this.dictSrc[134].vctScl.y * this.dictSrc[132].vctScl.z, this.dictSrc[134].vctScl.z * this.dictSrc[132].vctScl.y);
			this.dictDst[63].trfBone.SetLocalPositionX(this.dictSrc[137].vctPos.x);
			this.dictDst[63].trfBone.SetLocalScale(this.dictSrc[133].vctScl.x, this.dictSrc[135].vctScl.y * this.dictSrc[133].vctScl.z, this.dictSrc[135].vctScl.z * this.dictSrc[133].vctScl.y);
			this.dictDst[6].trfBone.SetLocalPositionX(this.dictSrc[16].vctPos.x);
			this.dictDst[6].trfBone.SetLocalPositionY(this.dictSrc[14].vctPos.y + this.dictSrc[16].vctPos.y);
			this.dictDst[6].trfBone.SetLocalRotation(0f, this.dictSrc[14].vctRot.y, this.dictSrc[14].vctRot.z + this.dictSrc[16].vctRot.z);
			this.dictDst[6].trfBone.SetLocalScale(1f, this.dictSrc[14].vctScl.y * this.dictSrc[12].vctScl.y, this.dictSrc[14].vctScl.z * this.dictSrc[12].vctScl.z);
			this.dictDst[7].trfBone.SetLocalPositionX(this.dictSrc[17].vctPos.x);
			this.dictDst[7].trfBone.SetLocalPositionY(this.dictSrc[15].vctPos.y + this.dictSrc[17].vctPos.y);
			this.dictDst[7].trfBone.SetLocalRotation(0f, this.dictSrc[15].vctRot.y, this.dictSrc[15].vctRot.z + this.dictSrc[17].vctRot.z);
			this.dictDst[7].trfBone.SetLocalScale(1f, this.dictSrc[15].vctScl.y * this.dictSrc[13].vctScl.y, this.dictSrc[15].vctScl.z * this.dictSrc[13].vctScl.z);
			this.dictDst[8].trfBone.SetLocalPositionY(this.dictSrc[20].vctPos.y);
			this.dictDst[8].trfBone.SetLocalScale(1f, this.dictSrc[20].vctScl.y * this.dictSrc[18].vctScl.y, this.dictSrc[20].vctScl.z * this.dictSrc[18].vctScl.z);
			this.dictDst[9].trfBone.SetLocalPositionY(this.dictSrc[21].vctPos.y);
			this.dictDst[9].trfBone.SetLocalScale(1f, this.dictSrc[21].vctScl.y * this.dictSrc[19].vctScl.y, this.dictSrc[21].vctScl.z * this.dictSrc[19].vctScl.z);
			this.dictDst[10].trfBone.SetLocalScale(1f, this.dictSrc[24].vctScl.y * this.dictSrc[22].vctScl.y, this.dictSrc[24].vctScl.z * this.dictSrc[22].vctScl.z);
			this.dictDst[11].trfBone.SetLocalScale(1f, this.dictSrc[25].vctScl.y * this.dictSrc[23].vctScl.y, this.dictSrc[25].vctScl.z * this.dictSrc[23].vctScl.z);
			this.dictDst[0].trfBone.SetLocalScale(1f, this.dictSrc[2].vctScl.y * this.dictSrc[0].vctScl.y, this.dictSrc[2].vctScl.z * this.dictSrc[0].vctScl.z);
			this.dictDst[1].trfBone.SetLocalScale(1f, this.dictSrc[3].vctScl.y * this.dictSrc[1].vctScl.y, this.dictSrc[3].vctScl.z * this.dictSrc[1].vctScl.z);
			this.dictDst[2].trfBone.SetLocalScale(1f, this.dictSrc[6].vctScl.y * this.dictSrc[4].vctScl.y, this.dictSrc[6].vctScl.z * this.dictSrc[4].vctScl.z);
			this.dictDst[3].trfBone.SetLocalScale(1f, this.dictSrc[7].vctScl.y * this.dictSrc[5].vctScl.y, this.dictSrc[7].vctScl.z * this.dictSrc[5].vctScl.z);
			this.dictDst[4].trfBone.SetLocalScale(1f, this.dictSrc[10].vctScl.y * this.dictSrc[8].vctScl.y, this.dictSrc[10].vctScl.z * this.dictSrc[8].vctScl.z);
			this.dictDst[5].trfBone.SetLocalScale(1f, this.dictSrc[11].vctScl.y * this.dictSrc[9].vctScl.y, this.dictSrc[11].vctScl.z * this.dictSrc[9].vctScl.z);
			this.dictDst[12].trfBone.SetLocalScale(this.dictSrc[26].vctScl.x, this.dictSrc[26].vctScl.y, this.dictSrc[26].vctScl.z);
			this.dictDst[13].trfBone.SetLocalScale(this.dictSrc[27].vctScl.x, this.dictSrc[27].vctScl.y, this.dictSrc[27].vctScl.z);
			this.dictDst[14].trfBone.SetLocalScale(1f, this.dictSrc[28].vctScl.y, this.dictSrc[28].vctScl.z);
			this.dictDst[15].trfBone.SetLocalScale(1f, this.dictSrc[29].vctScl.y, this.dictSrc[29].vctScl.z);
			this.dictDst[17].trfBone.SetLocalScale(this.dictSrc[33].vctScl.x * this.dictSrc[34].vctScl.x, 1f, this.dictSrc[33].vctScl.z * this.dictSrc[35].vctScl.z);
			this.dictDst[18].trfBone.SetLocalScale(this.dictSrc[36].vctScl.x * this.dictSrc[37].vctScl.x, 1f, this.dictSrc[36].vctScl.z * this.dictSrc[38].vctScl.z);
			this.dictDst[68].trfBone.SetLocalScale(this.dictSrc[151].vctScl.x * this.dictSrc[152].vctScl.x, 1f, this.dictSrc[151].vctScl.z * this.dictSrc[153].vctScl.z);
			this.dictDst[68].trfBone.SetLocalPositionY(this.dictSrc[154].vctPos.y);
			this.dictDst[68].trfBone.SetLocalPositionZ(this.dictSrc[152].vctPos.z + this.dictSrc[153].vctPos.z);
			this.dictDst[69].trfBone.SetLocalScale(this.dictSrc[155].vctScl.x * this.dictSrc[156].vctScl.x, 1f, this.dictSrc[155].vctScl.z * this.dictSrc[157].vctScl.z);
			this.dictDst[70].trfBone.SetLocalScale(this.dictSrc[158].vctScl.x * this.dictSrc[159].vctScl.x, 1f, this.dictSrc[158].vctScl.z * this.dictSrc[160].vctScl.z);
			this.dictDst[61].trfBone.SetLocalScale(this.dictSrc[127].vctScl.x * this.dictSrc[128].vctScl.x, 1f, this.dictSrc[127].vctScl.z * this.dictSrc[129].vctScl.z);
			this.dictDst[16].trfBone.SetLocalScale(this.dictSrc[30].vctScl.x * this.dictSrc[31].vctScl.x, this.dictSrc[30].vctScl.y * this.dictSrc[31].vctScl.y, this.dictSrc[30].vctScl.z * this.dictSrc[31].vctScl.z);
		}
		if ((this.updateMask & 1) != 0)
		{
			float x10 = this.dictSrc[203].vctPos.x;
			float y5 = this.dictSrc[203].vctPos.y;
			float z5 = this.dictSrc[203].vctPos.z;
			float x11 = this.dictSrc[203].vctScl.x;
			this.dictDst[47].trfBone.SetLocalPositionX(this.dictSrc[95].vctPos.x + this.dictSrc[99].vctPos.x);
			this.dictDst[47].trfBone.SetLocalPositionY(this.dictSrc[97].vctPos.y + this.dictSrc[103].vctPos.y);
			this.dictDst[47].trfBone.SetLocalPositionZ(this.dictSrc[97].vctPos.z + this.dictSrc[101].vctPos.z);
			this.dictDst[47].trfBone.SetLocalRotation(this.dictSrc[97].vctRot.x + this.dictSrc[101].vctRot.x + this.dictSrc[103].vctRot.x + this.dictSrc[93].vctRot.x, this.dictSrc[99].vctRot.y + this.dictSrc[103].vctRot.y, 0f);
			this.dictDst[47].trfBone.SetLocalScale(this.dictSrc[91].vctScl.x, this.dictSrc[91].vctScl.y, this.dictSrc[91].vctScl.z);
			this.dictDst[45].trfBone.SetLocalPositionX(this.dictSrc[93].vctPos.x);
			this.dictDst[45].trfBone.SetLocalPositionY(this.dictSrc[93].vctPos.y);
			this.dictDst[45].trfBone.SetLocalPositionZ(this.dictSrc[93].vctPos.z);
			this.dictDst[45].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[93].vctRot.z);
			this.dictDst[45].trfBone.SetLocalScale(this.dictSrc[93].vctScl.x, this.dictSrc[93].vctScl.y, this.dictSrc[93].vctScl.z);
			this.dictDst[43].trfBone.SetLocalPositionX(this.dictSrc[111].vctPos.x + this.dictSrc[109].vctPos.x);
			this.dictDst[43].trfBone.SetLocalPositionZ(0.65f + this.dictSrc[107].vctPos.z + this.dictSrc[109].vctPos.z);
			this.dictDst[43].trfBone.SetLocalRotation(this.dictSrc[107].vctRot.x, this.dictSrc[109].vctRot.y, 0f);
			this.dictDst[49].trfBone.SetLocalPositionY(this.dictSrc[105].vctPos.y);
			this.dictDst[49].trfBone.SetLocalPositionZ(this.dictSrc[105].vctPos.z);
			this.dictDst[49].trfBone.SetLocalRotation(this.dictSrc[105].vctRot.x, this.dictSrc[105].vctRot.y, this.dictSrc[105].vctRot.z);
			this.dictDst[49].trfBone.SetLocalScale(this.dictSrc[105].vctScl.x, this.dictSrc[105].vctScl.y, this.dictSrc[105].vctScl.z);
			this.dictDst[51].trfBone.SetLocalPositionX(this.dictSrc[105].vctPos.x);
			this.dictDst[51].trfBone.SetLocalPositionZ(0.3f + this.dictSrc[113].vctPos.z);
			this.dictDst[51].trfBone.SetLocalRotation(this.dictSrc[117].vctRot.x, 0f, 0f);
			this.dictDst[53].trfBone.SetLocalPositionY(this.dictSrc[115].vctPos.y);
			this.dictDst[53].trfBone.SetLocalPositionZ(this.dictSrc[115].vctPos.z);
			this.dictDst[53].trfBone.SetLocalRotation(this.dictSrc[115].vctRot.x, 0f, 0f);
			this.dictDst[53].trfBone.SetLocalScale(this.dictSrc[115].vctScl.x, this.dictSrc[115].vctScl.y, this.dictSrc[115].vctScl.z);
			this.dictDst[55].trfBone.SetLocalPositionZ(0.3f + this.dictSrc[119].vctPos.z);
			this.dictDst[55].trfBone.SetLocalRotation(this.dictSrc[123].vctRot.x, 0f, 0f);
			this.dictDst[57].trfBone.SetLocalPositionZ(this.dictSrc[121].vctPos.z);
			this.dictDst[57].trfBone.SetLocalScale(this.dictSrc[121].vctScl.x, this.dictSrc[121].vctScl.y, this.dictSrc[121].vctScl.z);
			this.dictDst[59].trfBone.SetLocalPositionZ(this.dictSrc[125].vctPos.z);
			this.dictDst[59].trfBone.SetLocalScale(this.dictSrc[125].vctScl.x, this.dictSrc[125].vctScl.y, this.dictSrc[125].vctScl.z);
			this.dictDst[37].trfBone.SetLocalPositionZ(this.dictSrc[83].vctPos.z + this.dictSrc[81].vctPos.z);
			this.dictDst[37].trfBone.SetLocalScale(this.dictSrc[83].vctScl.x * this.dictSrc[81].vctScl.x, this.dictSrc[83].vctScl.y * this.dictSrc[81].vctScl.y, this.dictSrc[83].vctScl.z);
			this.dictDst[80].trfBone.SetLocalPositionX(x10);
			this.dictDst[80].trfBone.SetLocalPositionY(y5);
			this.dictDst[80].trfBone.SetLocalPositionZ(z5);
			this.dictDst[80].trfBone.SetLocalScale(x11, 1f, x11);
		}
		if ((this.updateMask & 4) != 0)
		{
			this.dictDst[39].trfBone.SetLocalPositionZ(this.dictSrc[85].vctPos.z);
			this.dictDst[39].trfBone.SetLocalScale(this.dictSrc[85].vctScl.x, this.dictSrc[85].vctScl.y, this.dictSrc[85].vctScl.z);
			this.dictDst[41].trfBone.SetLocalPositionZ(this.dictSrc[87].vctPos.z + this.dictSrc[89].vctPos.z);
			this.dictDst[41].trfBone.SetLocalScale(this.dictSrc[79].vctScl.x * this.dictSrc[89].vctScl.x, this.dictSrc[79].vctScl.y * this.dictSrc[89].vctScl.y, this.dictSrc[79].vctScl.z * this.dictSrc[89].vctScl.z);
		}
		if ((this.updateMask & 2) != 0)
		{
			float x12 = this.dictSrc[203].vctPos.x;
			float y6 = this.dictSrc[203].vctPos.y;
			float z6 = this.dictSrc[203].vctPos.z;
			float x13 = this.dictSrc[203].vctScl.x;
			this.dictDst[48].trfBone.SetLocalPositionX(this.dictSrc[96].vctPos.x + this.dictSrc[100].vctPos.x);
			this.dictDst[48].trfBone.SetLocalPositionY(this.dictSrc[98].vctPos.y + this.dictSrc[104].vctPos.y);
			this.dictDst[48].trfBone.SetLocalPositionZ(this.dictSrc[98].vctPos.z + this.dictSrc[102].vctPos.z);
			this.dictDst[48].trfBone.SetLocalRotation(this.dictSrc[98].vctRot.x + this.dictSrc[102].vctRot.x + this.dictSrc[104].vctRot.x + this.dictSrc[94].vctRot.x, this.dictSrc[100].vctRot.y + this.dictSrc[104].vctRot.y, 0f);
			this.dictDst[48].trfBone.SetLocalScale(this.dictSrc[92].vctScl.x, this.dictSrc[92].vctScl.y, this.dictSrc[92].vctScl.z);
			this.dictDst[46].trfBone.SetLocalPositionX(this.dictSrc[94].vctPos.x);
			this.dictDst[46].trfBone.SetLocalPositionY(this.dictSrc[94].vctPos.y);
			this.dictDst[46].trfBone.SetLocalPositionZ(this.dictSrc[94].vctPos.z);
			this.dictDst[46].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[94].vctRot.z);
			this.dictDst[46].trfBone.SetLocalScale(this.dictSrc[94].vctScl.x, this.dictSrc[94].vctScl.y, this.dictSrc[94].vctScl.z);
			this.dictDst[44].trfBone.SetLocalPositionX(this.dictSrc[112].vctPos.x + this.dictSrc[110].vctPos.x);
			this.dictDst[44].trfBone.SetLocalPositionZ(0.65f + this.dictSrc[108].vctPos.z + this.dictSrc[110].vctPos.z);
			this.dictDst[44].trfBone.SetLocalRotation(this.dictSrc[108].vctRot.x, this.dictSrc[110].vctRot.y, 0f);
			this.dictDst[50].trfBone.SetLocalPositionY(this.dictSrc[106].vctPos.y);
			this.dictDst[50].trfBone.SetLocalPositionZ(this.dictSrc[106].vctPos.z);
			this.dictDst[50].trfBone.SetLocalRotation(this.dictSrc[106].vctRot.x, this.dictSrc[106].vctRot.y, this.dictSrc[106].vctRot.z);
			this.dictDst[50].trfBone.SetLocalScale(this.dictSrc[106].vctScl.x, this.dictSrc[106].vctScl.y, this.dictSrc[106].vctScl.z);
			this.dictDst[52].trfBone.SetLocalPositionX(this.dictSrc[106].vctPos.x);
			this.dictDst[52].trfBone.SetLocalPositionZ(0.3f + this.dictSrc[114].vctPos.z);
			this.dictDst[52].trfBone.SetLocalRotation(this.dictSrc[118].vctRot.x, 0f, 0f);
			this.dictDst[54].trfBone.SetLocalPositionY(this.dictSrc[116].vctPos.y);
			this.dictDst[54].trfBone.SetLocalPositionZ(this.dictSrc[116].vctPos.z);
			this.dictDst[54].trfBone.SetLocalRotation(this.dictSrc[116].vctRot.x, 0f, 0f);
			this.dictDst[54].trfBone.SetLocalScale(this.dictSrc[116].vctScl.x, this.dictSrc[116].vctScl.y, this.dictSrc[116].vctScl.z);
			this.dictDst[56].trfBone.SetLocalPositionZ(0.3f + this.dictSrc[120].vctPos.z);
			this.dictDst[56].trfBone.SetLocalRotation(this.dictSrc[124].vctRot.x, 0f, 0f);
			this.dictDst[58].trfBone.SetLocalPositionZ(this.dictSrc[122].vctPos.z);
			this.dictDst[58].trfBone.SetLocalScale(this.dictSrc[122].vctScl.x, this.dictSrc[122].vctScl.y, this.dictSrc[122].vctScl.z);
			this.dictDst[60].trfBone.SetLocalPositionZ(this.dictSrc[126].vctPos.z);
			this.dictDst[60].trfBone.SetLocalScale(this.dictSrc[126].vctScl.x, this.dictSrc[126].vctScl.y, this.dictSrc[126].vctScl.z);
			this.dictDst[38].trfBone.SetLocalPositionZ(this.dictSrc[84].vctPos.z + this.dictSrc[82].vctPos.z);
			this.dictDst[38].trfBone.SetLocalScale(this.dictSrc[84].vctScl.x * this.dictSrc[82].vctScl.x, this.dictSrc[84].vctScl.y * this.dictSrc[82].vctScl.y, this.dictSrc[84].vctScl.z);
			this.dictDst[81].trfBone.SetLocalPositionX(x12);
			this.dictDst[81].trfBone.SetLocalPositionY(y6);
			this.dictDst[81].trfBone.SetLocalPositionZ(z6);
			this.dictDst[81].trfBone.SetLocalScale(x13, 1f, x13);
		}
		if ((this.updateMask & 8) != 0)
		{
			this.dictDst[40].trfBone.SetLocalPositionZ(this.dictSrc[86].vctPos.z);
			this.dictDst[40].trfBone.SetLocalScale(this.dictSrc[86].vctScl.x, this.dictSrc[86].vctScl.y, this.dictSrc[86].vctScl.z);
			this.dictDst[42].trfBone.SetLocalPositionZ(this.dictSrc[88].vctPos.z + this.dictSrc[90].vctPos.z);
			this.dictDst[42].trfBone.SetLocalScale(this.dictSrc[80].vctScl.x * this.dictSrc[90].vctScl.x, this.dictSrc[80].vctScl.y * this.dictSrc[90].vctScl.y, this.dictSrc[80].vctScl.z * this.dictSrc[90].vctScl.z);
		}
		if ((this.updateMask & 16) != 0)
		{
			this.dictDst[35].trfBone.SetLocalScale(this.dictSrc[77].vctScl.x, 1f, this.dictSrc[77].vctScl.z);
			this.dictDst[36].trfBone.SetLocalScale(this.dictSrc[78].vctScl.x, 1f, this.dictSrc[78].vctScl.z);
			this.dictDst[29].trfBone.SetLocalPositionX(this.dictSrc[63].vctPos.x + this.dictSrc[57].vctPos.x);
			this.dictDst[29].trfBone.SetLocalPositionZ(this.dictSrc[63].vctPos.z);
			this.dictDst[29].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[57].vctRot.z);
			this.dictDst[29].trfBone.SetLocalScale(this.dictSrc[61].vctScl.x * this.dictSrc[63].vctScl.x * this.dictSrc[57].vctScl.x, 1f, this.dictSrc[61].vctScl.z * this.dictSrc[63].vctScl.z * this.dictSrc[59].vctScl.z);
			this.dictDst[30].trfBone.SetLocalPositionX(this.dictSrc[64].vctPos.x + this.dictSrc[58].vctPos.x);
			this.dictDst[30].trfBone.SetLocalPositionZ(this.dictSrc[64].vctPos.z);
			this.dictDst[30].trfBone.SetLocalRotation(0f, 0f, this.dictSrc[58].vctRot.z);
			this.dictDst[30].trfBone.SetLocalScale(this.dictSrc[62].vctScl.x * this.dictSrc[64].vctScl.x * this.dictSrc[58].vctScl.x, 1f, this.dictSrc[62].vctScl.z * this.dictSrc[64].vctScl.z * this.dictSrc[60].vctScl.z);
			this.dictDst[31].trfBone.SetLocalScale(this.dictSrc[67].vctScl.x * this.dictSrc[69].vctScl.x * this.dictSrc[65].vctScl.x, 1f, this.dictSrc[67].vctScl.z * this.dictSrc[69].vctScl.z * this.dictSrc[65].vctScl.z);
			this.dictDst[32].trfBone.SetLocalScale(this.dictSrc[68].vctScl.x * this.dictSrc[70].vctScl.x * this.dictSrc[66].vctScl.x, 1f, this.dictSrc[68].vctScl.z * this.dictSrc[70].vctScl.z * this.dictSrc[66].vctScl.z);
			this.dictDst[33].trfBone.SetLocalScale(this.dictSrc[73].vctScl.x * this.dictSrc[75].vctScl.x * this.dictSrc[71].vctScl.x, 1f, this.dictSrc[73].vctScl.z * this.dictSrc[75].vctScl.z * this.dictSrc[71].vctScl.z);
			this.dictDst[34].trfBone.SetLocalScale(this.dictSrc[74].vctScl.x * this.dictSrc[76].vctScl.x * this.dictSrc[72].vctScl.x, 1f, this.dictSrc[74].vctScl.z * this.dictSrc[76].vctScl.z * this.dictSrc[72].vctScl.z);
			this.dictDst[19].trfBone.SetLocalPositionZ(this.dictSrc[39].vctPos.z);
			this.dictDst[19].trfBone.SetLocalScale(this.dictSrc[39].vctScl.x, 1f, this.dictSrc[39].vctScl.z);
			this.dictDst[20].trfBone.SetLocalPositionZ(this.dictSrc[40].vctPos.z);
			this.dictDst[20].trfBone.SetLocalScale(this.dictSrc[40].vctScl.x, 1f, this.dictSrc[40].vctScl.z);
			this.dictDst[21].trfBone.SetLocalPositionZ(this.dictSrc[43].vctPos.z);
			this.dictDst[21].trfBone.SetLocalScale(this.dictSrc[45].vctScl.x * this.dictSrc[43].vctScl.x * this.dictSrc[41].vctScl.x, 1f, this.dictSrc[45].vctScl.z * this.dictSrc[43].vctScl.z * this.dictSrc[41].vctScl.z);
			this.dictDst[22].trfBone.SetLocalPositionZ(this.dictSrc[44].vctPos.z);
			this.dictDst[22].trfBone.SetLocalScale(this.dictSrc[46].vctScl.x * this.dictSrc[44].vctScl.x * this.dictSrc[42].vctScl.x, 1f, this.dictSrc[46].vctScl.z * this.dictSrc[44].vctScl.z * this.dictSrc[42].vctScl.z);
			this.dictDst[23].trfBone.SetLocalRotation(this.dictSrc[49].vctRot.x, 0f, 0f);
			this.dictDst[23].trfBone.SetLocalScale(this.dictSrc[47].vctScl.x * this.dictSrc[49].vctScl.x, 1f, this.dictSrc[47].vctScl.z * this.dictSrc[49].vctScl.z);
			this.dictDst[24].trfBone.SetLocalRotation(this.dictSrc[50].vctRot.x, 0f, 0f);
			this.dictDst[24].trfBone.SetLocalScale(this.dictSrc[48].vctScl.x * this.dictSrc[50].vctScl.x, 1f, this.dictSrc[48].vctScl.z * this.dictSrc[50].vctScl.z);
			this.dictDst[25].trfBone.SetLocalScale(this.dictSrc[51].vctScl.x * this.dictSrc[53].vctScl.x, 1f, this.dictSrc[51].vctScl.z * this.dictSrc[53].vctScl.z);
			this.dictDst[26].trfBone.SetLocalScale(this.dictSrc[52].vctScl.x * this.dictSrc[54].vctScl.x, 1f, this.dictSrc[52].vctScl.z * this.dictSrc[54].vctScl.z);
			this.dictDst[27].trfBone.SetLocalPositionX(this.dictSrc[55].vctPos.x);
			this.dictDst[27].trfBone.SetLocalPositionZ(this.dictSrc[55].vctPos.z);
			this.dictDst[27].trfBone.SetLocalRotation(this.dictSrc[55].vctRot.x, 0f, this.dictSrc[55].vctRot.z);
			this.dictDst[27].trfBone.SetLocalScale(this.dictSrc[55].vctScl.x, 1f, this.dictSrc[55].vctScl.z);
			this.dictDst[28].trfBone.SetLocalPositionX(this.dictSrc[56].vctPos.x);
			this.dictDst[28].trfBone.SetLocalPositionZ(this.dictSrc[55].vctPos.z);
			this.dictDst[28].trfBone.SetLocalRotation(this.dictSrc[56].vctRot.x, 0f, this.dictSrc[56].vctRot.z);
			this.dictDst[28].trfBone.SetLocalScale(this.dictSrc[56].vctScl.x, 1f, this.dictSrc[56].vctScl.z);
			this.dictDst[64].trfBone.SetLocalPosition(this.dictSrc[144].vctPos.x, this.dictSrc[146].vctPos.y + this.dictSrc[144].vctPos.y, this.dictSrc[142].vctPos.z + this.dictSrc[144].vctPos.z);
			this.dictDst[64].trfBone.SetLocalRotation(this.dictSrc[146].vctRot.x, 0f, 0f);
			this.dictDst[64].trfBone.SetLocalScale(this.dictSrc[140].vctScl.x * this.dictSrc[142].vctScl.x * this.dictSrc[144].vctScl.x, this.dictSrc[144].vctScl.y, this.dictSrc[138].vctScl.z * this.dictSrc[140].vctScl.z * this.dictSrc[142].vctScl.z * this.dictSrc[144].vctScl.z);
			this.dictDst[65].trfBone.SetLocalPosition(this.dictSrc[145].vctPos.x, this.dictSrc[147].vctPos.y + this.dictSrc[145].vctPos.y, this.dictSrc[143].vctPos.z + this.dictSrc[145].vctPos.z);
			this.dictDst[65].trfBone.SetLocalRotation(this.dictSrc[147].vctRot.x, 0f, 0f);
			this.dictDst[65].trfBone.SetLocalScale(this.dictSrc[141].vctScl.x * this.dictSrc[143].vctScl.x * this.dictSrc[145].vctScl.x, this.dictSrc[145].vctScl.y, this.dictSrc[139].vctScl.z * this.dictSrc[141].vctScl.z * this.dictSrc[143].vctScl.z * this.dictSrc[145].vctScl.z);
			this.dictDst[82].trfBone.SetLocalPositionY(num + num3);
			this.dictDst[82].trfBone.SetLocalPositionZ(z + num4);
			this.dictDst[82].trfBone.SetLocalScale(x * num6 * this.dictSrc[205].vctScl.x, num2 * num5, x * num6);
			this.dictDst[83].trfBone.SetLocalPositionX(-num7);
			this.dictDst[83].trfBone.SetLocalPositionY(this.dictSrc[197].vctPos.y);
			this.dictDst[83].trfBone.SetLocalRotation(this.dictSrc[197].vctRot.x, 0f, -num8);
			this.dictDst[83].trfBone.SetLocalScale(x2, y, 1f);
			this.dictDst[84].trfBone.SetLocalPositionX(num7);
			this.dictDst[84].trfBone.SetLocalPositionY(this.dictSrc[197].vctPos.y);
			this.dictDst[84].trfBone.SetLocalRotation(this.dictSrc[197].vctRot.x, 0f, num8);
			this.dictDst[84].trfBone.SetLocalScale(x2, y, 1f);
			this.dictDst[85].trfBone.SetLocalPositionX(-(x3 + x4 + x6));
			this.dictDst[85].trfBone.SetLocalPositionY(y2 + y4);
			this.dictDst[85].trfBone.SetLocalPositionZ(num11 + z2 + z3 + z4);
			this.dictDst[85].trfBone.SetLocalRotation(x8, 0f, 0f);
			this.dictDst[85].trfBone.SetLocalScale(num9 * x5 * x7 * x9 / this.dictSrc[205].vctScl.x, num10 * y3, num9 * x5 * x7);
			this.dictDst[86].trfBone.SetLocalPositionX(x3 + x4 + x6);
			this.dictDst[86].trfBone.SetLocalPositionY(y2 + y4);
			this.dictDst[86].trfBone.SetLocalPositionZ(num11 + z2 + z3 + z4);
			this.dictDst[86].trfBone.SetLocalRotation(x8, 0f, 0f);
			this.dictDst[86].trfBone.SetLocalScale(num9 * x5 * x7 * x9 / this.dictSrc[205].vctScl.x, num10 * y3, num9 * x5 * x7);
			this.dictDst[87].trfBone.SetLocalPositionX(this.dictSrc[206].vctPos.x + this.dictSrc[209].vctPos.x);
			this.dictDst[87].trfBone.SetLocalScale(this.dictSrc[206].vctScl.x * this.dictSrc[209].vctScl.x, 1f, this.dictSrc[208].vctScl.z * this.dictSrc[209].vctScl.z);
			this.dictDst[88].trfBone.SetLocalPositionX(this.dictSrc[207].vctPos.x + this.dictSrc[210].vctPos.x);
			this.dictDst[88].trfBone.SetLocalScale(this.dictSrc[207].vctScl.x * this.dictSrc[210].vctScl.x, 1f, this.dictSrc[208].vctScl.z * this.dictSrc[210].vctScl.z);
		}
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x0012C3F8 File Offset: 0x0012A7F8
	public override void UpdateAlways()
	{
		if (!base.InitEnd)
		{
			return;
		}
	}

	// Token: 0x04002FD5 RID: 12245
	public const int UPDATE_MASK_BUST_L = 1;

	// Token: 0x04002FD6 RID: 12246
	public const int UPDATE_MASK_BUST_R = 2;

	// Token: 0x04002FD7 RID: 12247
	public const int UPDATE_MASK_NIP_L = 4;

	// Token: 0x04002FD8 RID: 12248
	public const int UPDATE_MASK_NIP_R = 8;

	// Token: 0x04002FD9 RID: 12249
	public const int UPDATE_MASK_ETC = 16;

	// Token: 0x04002FDA RID: 12250
	public const int UPDATE_MASK_ALL = 31;

	// Token: 0x04002FDB RID: 12251
	public int updateMask = 31;

	// Token: 0x020007D6 RID: 2006
	public enum DstName
	{
		// Token: 0x04002FDD RID: 12253
		cf_J_ArmElbo_low_s_L,
		// Token: 0x04002FDE RID: 12254
		cf_J_ArmElbo_low_s_R,
		// Token: 0x04002FDF RID: 12255
		cf_J_ArmLow01_s_L,
		// Token: 0x04002FE0 RID: 12256
		cf_J_ArmLow01_s_R,
		// Token: 0x04002FE1 RID: 12257
		cf_J_ArmLow02_s_L,
		// Token: 0x04002FE2 RID: 12258
		cf_J_ArmLow02_s_R,
		// Token: 0x04002FE3 RID: 12259
		cf_J_ArmUp01_s_L,
		// Token: 0x04002FE4 RID: 12260
		cf_J_ArmUp01_s_R,
		// Token: 0x04002FE5 RID: 12261
		cf_J_ArmUp02_s_L,
		// Token: 0x04002FE6 RID: 12262
		cf_J_ArmUp02_s_R,
		// Token: 0x04002FE7 RID: 12263
		cf_J_ArmUp03_s_L,
		// Token: 0x04002FE8 RID: 12264
		cf_J_ArmUp03_s_R,
		// Token: 0x04002FE9 RID: 12265
		cf_J_Hand_s_L,
		// Token: 0x04002FEA RID: 12266
		cf_J_Hand_s_R,
		// Token: 0x04002FEB RID: 12267
		cf_J_Hand_Wrist_s_L,
		// Token: 0x04002FEC RID: 12268
		cf_J_Hand_Wrist_s_R,
		// Token: 0x04002FED RID: 12269
		cf_J_Head_s,
		// Token: 0x04002FEE RID: 12270
		cf_J_Kosi01_s,
		// Token: 0x04002FEF RID: 12271
		cf_J_Kosi02_s,
		// Token: 0x04002FF0 RID: 12272
		cf_J_LegKnee_back_s_L,
		// Token: 0x04002FF1 RID: 12273
		cf_J_LegKnee_back_s_R,
		// Token: 0x04002FF2 RID: 12274
		cf_J_LegKnee_low_s_L,
		// Token: 0x04002FF3 RID: 12275
		cf_J_LegKnee_low_s_R,
		// Token: 0x04002FF4 RID: 12276
		cf_J_LegLow01_s_L,
		// Token: 0x04002FF5 RID: 12277
		cf_J_LegLow01_s_R,
		// Token: 0x04002FF6 RID: 12278
		cf_J_LegLow02_s_L,
		// Token: 0x04002FF7 RID: 12279
		cf_J_LegLow02_s_R,
		// Token: 0x04002FF8 RID: 12280
		cf_J_LegLow03_s_L,
		// Token: 0x04002FF9 RID: 12281
		cf_J_LegLow03_s_R,
		// Token: 0x04002FFA RID: 12282
		cf_J_LegUp01_s_L,
		// Token: 0x04002FFB RID: 12283
		cf_J_LegUp01_s_R,
		// Token: 0x04002FFC RID: 12284
		cf_J_LegUp02_s_L,
		// Token: 0x04002FFD RID: 12285
		cf_J_LegUp02_s_R,
		// Token: 0x04002FFE RID: 12286
		cf_J_LegUp03_s_L,
		// Token: 0x04002FFF RID: 12287
		cf_J_LegUp03_s_R,
		// Token: 0x04003000 RID: 12288
		cf_J_LegUpDam_s_L,
		// Token: 0x04003001 RID: 12289
		cf_J_LegUpDam_s_R,
		// Token: 0x04003002 RID: 12290
		cf_J_Mune_Nip01_s_L,
		// Token: 0x04003003 RID: 12291
		cf_J_Mune_Nip01_s_R,
		// Token: 0x04003004 RID: 12292
		cf_J_Mune_Nip02_s_L,
		// Token: 0x04003005 RID: 12293
		cf_J_Mune_Nip02_s_R,
		// Token: 0x04003006 RID: 12294
		cf_J_Mune_Nipacs01_L,
		// Token: 0x04003007 RID: 12295
		cf_J_Mune_Nipacs01_R,
		// Token: 0x04003008 RID: 12296
		cf_J_Mune00_d_L,
		// Token: 0x04003009 RID: 12297
		cf_J_Mune00_d_R,
		// Token: 0x0400300A RID: 12298
		cf_J_Mune00_s_L,
		// Token: 0x0400300B RID: 12299
		cf_J_Mune00_s_R,
		// Token: 0x0400300C RID: 12300
		cf_J_Mune00_t_L,
		// Token: 0x0400300D RID: 12301
		cf_J_Mune00_t_R,
		// Token: 0x0400300E RID: 12302
		cf_J_Mune01_s_L,
		// Token: 0x0400300F RID: 12303
		cf_J_Mune01_s_R,
		// Token: 0x04003010 RID: 12304
		cf_J_Mune01_t_L,
		// Token: 0x04003011 RID: 12305
		cf_J_Mune01_t_R,
		// Token: 0x04003012 RID: 12306
		cf_J_Mune02_s_L,
		// Token: 0x04003013 RID: 12307
		cf_J_Mune02_s_R,
		// Token: 0x04003014 RID: 12308
		cf_J_Mune02_t_L,
		// Token: 0x04003015 RID: 12309
		cf_J_Mune02_t_R,
		// Token: 0x04003016 RID: 12310
		cf_J_Mune03_s_L,
		// Token: 0x04003017 RID: 12311
		cf_J_Mune03_s_R,
		// Token: 0x04003018 RID: 12312
		cf_J_Mune04_s_L,
		// Token: 0x04003019 RID: 12313
		cf_J_Mune04_s_R,
		// Token: 0x0400301A RID: 12314
		cf_J_Neck_s,
		// Token: 0x0400301B RID: 12315
		cf_J_Shoulder02_s_L,
		// Token: 0x0400301C RID: 12316
		cf_J_Shoulder02_s_R,
		// Token: 0x0400301D RID: 12317
		cf_J_Siri_s_L,
		// Token: 0x0400301E RID: 12318
		cf_J_Siri_s_R,
		// Token: 0x0400301F RID: 12319
		cf_J_sk_siri_dam,
		// Token: 0x04003020 RID: 12320
		cf_J_sk_top,
		// Token: 0x04003021 RID: 12321
		cf_J_Spine01_s,
		// Token: 0x04003022 RID: 12322
		cf_J_Spine02_s,
		// Token: 0x04003023 RID: 12323
		cf_J_Spine03_s,
		// Token: 0x04003024 RID: 12324
		cf_N_height,
		// Token: 0x04003025 RID: 12325
		cf_J_sk_00_00_dam,
		// Token: 0x04003026 RID: 12326
		cf_J_sk_01_00_dam,
		// Token: 0x04003027 RID: 12327
		cf_J_sk_02_00_dam,
		// Token: 0x04003028 RID: 12328
		cf_J_sk_03_00_dam,
		// Token: 0x04003029 RID: 12329
		cf_J_sk_04_00_dam,
		// Token: 0x0400302A RID: 12330
		cf_J_sk_05_00_dam,
		// Token: 0x0400302B RID: 12331
		cf_J_sk_06_00_dam,
		// Token: 0x0400302C RID: 12332
		cf_J_sk_07_00_dam,
		// Token: 0x0400302D RID: 12333
		cf_hit_Mune02_s_L,
		// Token: 0x0400302E RID: 12334
		cf_hit_Mune02_s_R,
		// Token: 0x0400302F RID: 12335
		cf_hit_Kosi02_s,
		// Token: 0x04003030 RID: 12336
		cf_hit_LegUp01_s_L,
		// Token: 0x04003031 RID: 12337
		cf_hit_LegUp01_s_R,
		// Token: 0x04003032 RID: 12338
		cf_hit_Siri_s_L,
		// Token: 0x04003033 RID: 12339
		cf_hit_Siri_s_R,
		// Token: 0x04003034 RID: 12340
		cf_J_Legsk_root_L,
		// Token: 0x04003035 RID: 12341
		cf_J_Legsk_root_R
	}

	// Token: 0x020007D7 RID: 2007
	public enum SrcName
	{
		// Token: 0x04003037 RID: 12343
		cf_s_ArmElbo_low_s_L,
		// Token: 0x04003038 RID: 12344
		cf_s_ArmElbo_low_s_R,
		// Token: 0x04003039 RID: 12345
		cf_s_ArmElbo_up_s_L,
		// Token: 0x0400303A RID: 12346
		cf_s_ArmElbo_up_s_R,
		// Token: 0x0400303B RID: 12347
		cf_s_ArmLow01_h_L,
		// Token: 0x0400303C RID: 12348
		cf_s_ArmLow01_h_R,
		// Token: 0x0400303D RID: 12349
		cf_s_ArmLow01_s_L,
		// Token: 0x0400303E RID: 12350
		cf_s_ArmLow01_s_R,
		// Token: 0x0400303F RID: 12351
		cf_s_ArmLow02_h_L,
		// Token: 0x04003040 RID: 12352
		cf_s_ArmLow02_h_R,
		// Token: 0x04003041 RID: 12353
		cf_s_ArmLow02_s_L,
		// Token: 0x04003042 RID: 12354
		cf_s_ArmLow02_s_R,
		// Token: 0x04003043 RID: 12355
		cf_s_ArmUp01_h_L,
		// Token: 0x04003044 RID: 12356
		cf_s_ArmUp01_h_R,
		// Token: 0x04003045 RID: 12357
		cf_s_ArmUp01_s_L,
		// Token: 0x04003046 RID: 12358
		cf_s_ArmUp01_s_R,
		// Token: 0x04003047 RID: 12359
		cf_s_ArmUp01_s_tx_L,
		// Token: 0x04003048 RID: 12360
		cf_s_ArmUp01_s_tx_R,
		// Token: 0x04003049 RID: 12361
		cf_s_ArmUp02_h_L,
		// Token: 0x0400304A RID: 12362
		cf_s_ArmUp02_h_R,
		// Token: 0x0400304B RID: 12363
		cf_s_ArmUp02_s_L,
		// Token: 0x0400304C RID: 12364
		cf_s_ArmUp02_s_R,
		// Token: 0x0400304D RID: 12365
		cf_s_ArmUp03_h_L,
		// Token: 0x0400304E RID: 12366
		cf_s_ArmUp03_h_R,
		// Token: 0x0400304F RID: 12367
		cf_s_ArmUp03_s_L,
		// Token: 0x04003050 RID: 12368
		cf_s_ArmUp03_s_R,
		// Token: 0x04003051 RID: 12369
		cf_s_Hand_h_L,
		// Token: 0x04003052 RID: 12370
		cf_s_Hand_h_R,
		// Token: 0x04003053 RID: 12371
		cf_s_Hand_Wrist_s_L,
		// Token: 0x04003054 RID: 12372
		cf_s_Hand_Wrist_s_R,
		// Token: 0x04003055 RID: 12373
		cf_s_Head_h,
		// Token: 0x04003056 RID: 12374
		cf_s_Head_s,
		// Token: 0x04003057 RID: 12375
		cf_s_height,
		// Token: 0x04003058 RID: 12376
		cf_s_Kosi01_h,
		// Token: 0x04003059 RID: 12377
		cf_s_Kosi01_s,
		// Token: 0x0400305A RID: 12378
		cf_s_Kosi01_s_sz,
		// Token: 0x0400305B RID: 12379
		cf_s_Kosi02_h,
		// Token: 0x0400305C RID: 12380
		cf_s_Kosi02_s,
		// Token: 0x0400305D RID: 12381
		cf_s_Kosi02_s_sz,
		// Token: 0x0400305E RID: 12382
		cf_s_LegKnee_back_s_L,
		// Token: 0x0400305F RID: 12383
		cf_s_LegKnee_back_s_R,
		// Token: 0x04003060 RID: 12384
		cf_s_LegKnee_h_L,
		// Token: 0x04003061 RID: 12385
		cf_s_LegKnee_h_R,
		// Token: 0x04003062 RID: 12386
		cf_s_LegKnee_low_s_L,
		// Token: 0x04003063 RID: 12387
		cf_s_LegKnee_low_s_R,
		// Token: 0x04003064 RID: 12388
		cf_s_LegKnee_up_s_L,
		// Token: 0x04003065 RID: 12389
		cf_s_LegKnee_up_s_R,
		// Token: 0x04003066 RID: 12390
		cf_s_LegLow01_h_L,
		// Token: 0x04003067 RID: 12391
		cf_s_LegLow01_h_R,
		// Token: 0x04003068 RID: 12392
		cf_s_LegLow01_s_L,
		// Token: 0x04003069 RID: 12393
		cf_s_LegLow01_s_R,
		// Token: 0x0400306A RID: 12394
		cf_s_LegLow02_h_L,
		// Token: 0x0400306B RID: 12395
		cf_s_LegLow02_h_R,
		// Token: 0x0400306C RID: 12396
		cf_s_LegLow02_s_L,
		// Token: 0x0400306D RID: 12397
		cf_s_LegLow02_s_R,
		// Token: 0x0400306E RID: 12398
		cf_s_LegLow03_s_L,
		// Token: 0x0400306F RID: 12399
		cf_s_LegLow03_s_R,
		// Token: 0x04003070 RID: 12400
		cf_s_LegUp01_blend_s_L,
		// Token: 0x04003071 RID: 12401
		cf_s_LegUp01_blend_s_R,
		// Token: 0x04003072 RID: 12402
		cf_s_LegUp01_blend_ss_L,
		// Token: 0x04003073 RID: 12403
		cf_s_LegUp01_blend_ss_R,
		// Token: 0x04003074 RID: 12404
		cf_s_LegUp01_h_L,
		// Token: 0x04003075 RID: 12405
		cf_s_LegUp01_h_R,
		// Token: 0x04003076 RID: 12406
		cf_s_LegUp01_s_L,
		// Token: 0x04003077 RID: 12407
		cf_s_LegUp01_s_R,
		// Token: 0x04003078 RID: 12408
		cf_s_LegUp02_blend_s_L,
		// Token: 0x04003079 RID: 12409
		cf_s_LegUp02_blend_s_R,
		// Token: 0x0400307A RID: 12410
		cf_s_LegUp02_h_L,
		// Token: 0x0400307B RID: 12411
		cf_s_LegUp02_h_R,
		// Token: 0x0400307C RID: 12412
		cf_s_LegUp02_s_L,
		// Token: 0x0400307D RID: 12413
		cf_s_LegUp02_s_R,
		// Token: 0x0400307E RID: 12414
		cf_s_LegUp03_blend_s_L,
		// Token: 0x0400307F RID: 12415
		cf_s_LegUp03_blend_s_R,
		// Token: 0x04003080 RID: 12416
		cf_s_LegUp03_h_L,
		// Token: 0x04003081 RID: 12417
		cf_s_LegUp03_h_R,
		// Token: 0x04003082 RID: 12418
		cf_s_LegUp03_s_L,
		// Token: 0x04003083 RID: 12419
		cf_s_LegUp03_s_R,
		// Token: 0x04003084 RID: 12420
		cf_s_LegUpDam_s_L,
		// Token: 0x04003085 RID: 12421
		cf_s_LegUpDam_s_R,
		// Token: 0x04003086 RID: 12422
		cf_s_Mune_Nip_dam_L,
		// Token: 0x04003087 RID: 12423
		cf_s_Mune_Nip_dam_R,
		// Token: 0x04003088 RID: 12424
		cf_s_Mune_Nip01_s_L,
		// Token: 0x04003089 RID: 12425
		cf_s_Mune_Nip01_s_R,
		// Token: 0x0400308A RID: 12426
		cf_s_Mune_Nip01_ss_L,
		// Token: 0x0400308B RID: 12427
		cf_s_Mune_Nip01_ss_R,
		// Token: 0x0400308C RID: 12428
		cf_s_Mune_Nip02_s_L,
		// Token: 0x0400308D RID: 12429
		cf_s_Mune_Nip02_s_R,
		// Token: 0x0400308E RID: 12430
		cf_s_Mune_Nipacs01_L,
		// Token: 0x0400308F RID: 12431
		cf_s_Mune_Nipacs01_R,
		// Token: 0x04003090 RID: 12432
		cf_s_Mune_Nipacs02_L,
		// Token: 0x04003091 RID: 12433
		cf_s_Mune_Nipacs02_R,
		// Token: 0x04003092 RID: 12434
		cf_s_Mune00_h_L,
		// Token: 0x04003093 RID: 12435
		cf_s_Mune00_h_R,
		// Token: 0x04003094 RID: 12436
		cf_s_Mune00_s_L,
		// Token: 0x04003095 RID: 12437
		cf_s_Mune00_s_R,
		// Token: 0x04003096 RID: 12438
		cf_s_Mune00_ss_02_L,
		// Token: 0x04003097 RID: 12439
		cf_s_Mune00_ss_02_R,
		// Token: 0x04003098 RID: 12440
		cf_s_Mune00_ss_02sz_L,
		// Token: 0x04003099 RID: 12441
		cf_s_Mune00_ss_02sz_R,
		// Token: 0x0400309A RID: 12442
		cf_s_Mune00_ss_03_L,
		// Token: 0x0400309B RID: 12443
		cf_s_Mune00_ss_03_R,
		// Token: 0x0400309C RID: 12444
		cf_s_Mune00_ss_03sz_L,
		// Token: 0x0400309D RID: 12445
		cf_s_Mune00_ss_03sz_R,
		// Token: 0x0400309E RID: 12446
		cf_s_Mune00_ss_ty_L,
		// Token: 0x0400309F RID: 12447
		cf_s_Mune00_ss_ty_R,
		// Token: 0x040030A0 RID: 12448
		cf_s_Mune01_s_L,
		// Token: 0x040030A1 RID: 12449
		cf_s_Mune01_s_R,
		// Token: 0x040030A2 RID: 12450
		cf_s_Mune01_s_rx_L,
		// Token: 0x040030A3 RID: 12451
		cf_s_Mune01_s_rx_R,
		// Token: 0x040030A4 RID: 12452
		cf_s_Mune01_s_ry_L,
		// Token: 0x040030A5 RID: 12453
		cf_s_Mune01_s_ry_R,
		// Token: 0x040030A6 RID: 12454
		cf_s_Mune01_s_tx_L,
		// Token: 0x040030A7 RID: 12455
		cf_s_Mune01_s_tx_R,
		// Token: 0x040030A8 RID: 12456
		cf_s_Mune01_s_tz_L,
		// Token: 0x040030A9 RID: 12457
		cf_s_Mune01_s_tz_R,
		// Token: 0x040030AA RID: 12458
		cf_s_Mune02_s_L,
		// Token: 0x040030AB RID: 12459
		cf_s_Mune02_s_R,
		// Token: 0x040030AC RID: 12460
		cf_s_Mune02_s_rx_L,
		// Token: 0x040030AD RID: 12461
		cf_s_Mune02_s_rx_R,
		// Token: 0x040030AE RID: 12462
		cf_s_Mune02_s_tz_L,
		// Token: 0x040030AF RID: 12463
		cf_s_Mune02_s_tz_R,
		// Token: 0x040030B0 RID: 12464
		cf_s_Mune03_s_L,
		// Token: 0x040030B1 RID: 12465
		cf_s_Mune03_s_R,
		// Token: 0x040030B2 RID: 12466
		cf_s_Mune03_s_rx_L,
		// Token: 0x040030B3 RID: 12467
		cf_s_Mune03_s_rx_R,
		// Token: 0x040030B4 RID: 12468
		cf_s_Mune04_s_L,
		// Token: 0x040030B5 RID: 12469
		cf_s_Mune04_s_R,
		// Token: 0x040030B6 RID: 12470
		cf_s_Neck_h,
		// Token: 0x040030B7 RID: 12471
		cf_s_Neck_s,
		// Token: 0x040030B8 RID: 12472
		cf_s_Neck_s_sz,
		// Token: 0x040030B9 RID: 12473
		cf_s_Shoulder_h_L,
		// Token: 0x040030BA RID: 12474
		cf_s_Shoulder_h_R,
		// Token: 0x040030BB RID: 12475
		cf_s_Shoulder02_h_L,
		// Token: 0x040030BC RID: 12476
		cf_s_Shoulder02_h_R,
		// Token: 0x040030BD RID: 12477
		cf_s_Shoulder02_s_L,
		// Token: 0x040030BE RID: 12478
		cf_s_Shoulder02_s_R,
		// Token: 0x040030BF RID: 12479
		cf_s_Shoulder02_s_tx_L,
		// Token: 0x040030C0 RID: 12480
		cf_s_Shoulder02_s_tx_R,
		// Token: 0x040030C1 RID: 12481
		cf_s_Siri_kosi01_s_L,
		// Token: 0x040030C2 RID: 12482
		cf_s_Siri_kosi01_s_R,
		// Token: 0x040030C3 RID: 12483
		cf_s_Siri_kosi02_s_L,
		// Token: 0x040030C4 RID: 12484
		cf_s_Siri_kosi02_s_R,
		// Token: 0x040030C5 RID: 12485
		cf_s_Siri_legup01_s_L,
		// Token: 0x040030C6 RID: 12486
		cf_s_Siri_legup01_s_R,
		// Token: 0x040030C7 RID: 12487
		cf_s_Siri_s_L,
		// Token: 0x040030C8 RID: 12488
		cf_s_Siri_s_R,
		// Token: 0x040030C9 RID: 12489
		cf_s_Siri_s_ty_L,
		// Token: 0x040030CA RID: 12490
		cf_s_Siri_s_ty_R,
		// Token: 0x040030CB RID: 12491
		cf_s_sk_siri_dam,
		// Token: 0x040030CC RID: 12492
		cf_s_sk_siri_ty_dam,
		// Token: 0x040030CD RID: 12493
		cf_s_sk_top_h,
		// Token: 0x040030CE RID: 12494
		cf_s_Spine01_h,
		// Token: 0x040030CF RID: 12495
		cf_s_Spine01_s,
		// Token: 0x040030D0 RID: 12496
		cf_s_Spine01_s_sz,
		// Token: 0x040030D1 RID: 12497
		cf_s_Spine01_s_ty,
		// Token: 0x040030D2 RID: 12498
		cf_s_Spine02_h,
		// Token: 0x040030D3 RID: 12499
		cf_s_Spine02_s,
		// Token: 0x040030D4 RID: 12500
		cf_s_Spine02_s_sz,
		// Token: 0x040030D5 RID: 12501
		cf_s_Spine03_h,
		// Token: 0x040030D6 RID: 12502
		cf_s_Spine03_s,
		// Token: 0x040030D7 RID: 12503
		cf_s_Spine03_s_sz,
		// Token: 0x040030D8 RID: 12504
		cf_s_sk_00_sx01,
		// Token: 0x040030D9 RID: 12505
		cf_s_sk_00_sx02,
		// Token: 0x040030DA RID: 12506
		cf_s_sk_00_sz01,
		// Token: 0x040030DB RID: 12507
		cf_s_sk_00_sz02,
		// Token: 0x040030DC RID: 12508
		cf_s_sk_01_sx01,
		// Token: 0x040030DD RID: 12509
		cf_s_sk_01_sx02,
		// Token: 0x040030DE RID: 12510
		cf_s_sk_01_sz01,
		// Token: 0x040030DF RID: 12511
		cf_s_sk_01_sz02,
		// Token: 0x040030E0 RID: 12512
		cf_s_sk_02_sx01,
		// Token: 0x040030E1 RID: 12513
		cf_s_sk_02_sx02,
		// Token: 0x040030E2 RID: 12514
		cf_s_sk_02_sz01,
		// Token: 0x040030E3 RID: 12515
		cf_s_sk_02_sz02,
		// Token: 0x040030E4 RID: 12516
		cf_s_sk_03_sx01,
		// Token: 0x040030E5 RID: 12517
		cf_s_sk_03_sx02,
		// Token: 0x040030E6 RID: 12518
		cf_s_sk_03_sz01,
		// Token: 0x040030E7 RID: 12519
		cf_s_sk_03_sz02,
		// Token: 0x040030E8 RID: 12520
		cf_s_sk_04_sx01,
		// Token: 0x040030E9 RID: 12521
		cf_s_sk_04_sx02,
		// Token: 0x040030EA RID: 12522
		cf_s_sk_04_sz01,
		// Token: 0x040030EB RID: 12523
		cf_s_sk_04_sz02,
		// Token: 0x040030EC RID: 12524
		cf_s_sk_05_sx01,
		// Token: 0x040030ED RID: 12525
		cf_s_sk_05_sx02,
		// Token: 0x040030EE RID: 12526
		cf_s_sk_05_sz01,
		// Token: 0x040030EF RID: 12527
		cf_s_sk_05_sz02,
		// Token: 0x040030F0 RID: 12528
		cf_s_sk_06_sx01,
		// Token: 0x040030F1 RID: 12529
		cf_s_sk_06_sx02,
		// Token: 0x040030F2 RID: 12530
		cf_s_sk_06_sz01,
		// Token: 0x040030F3 RID: 12531
		cf_s_sk_06_sz02,
		// Token: 0x040030F4 RID: 12532
		cf_s_sk_07_sx01,
		// Token: 0x040030F5 RID: 12533
		cf_s_sk_07_sx02,
		// Token: 0x040030F6 RID: 12534
		cf_s_sk_07_sz01,
		// Token: 0x040030F7 RID: 12535
		cf_s_sk_07_sz02,
		// Token: 0x040030F8 RID: 12536
		cf_hit_Kosi02_Kosi01sx_a,
		// Token: 0x040030F9 RID: 12537
		cf_hit_Kosi02_Kosi01sz_a,
		// Token: 0x040030FA RID: 12538
		cf_hit_Kosi02_Kosi02sx_a,
		// Token: 0x040030FB RID: 12539
		cf_hit_Kosi02_Kosi02sz_a,
		// Token: 0x040030FC RID: 12540
		cf_hit_LegUp01_Kosi02sz_a,
		// Token: 0x040030FD RID: 12541
		cf_hit_LegUp01_Kosi02sx_a,
		// Token: 0x040030FE RID: 12542
		cf_hit_Siri_Kosi02sz_a,
		// Token: 0x040030FF RID: 12543
		cf_hit_Siri_Kosi02sx_a,
		// Token: 0x04003100 RID: 12544
		cf_hit_Siri_size_a,
		// Token: 0x04003101 RID: 12545
		cf_hit_Siri_rot_a,
		// Token: 0x04003102 RID: 12546
		cf_hit_Mune_size_a,
		// Token: 0x04003103 RID: 12547
		cf_hit_Siri_LegUp01,
		// Token: 0x04003104 RID: 12548
		cf_hit_height,
		// Token: 0x04003105 RID: 12549
		cf_s_legskroot_kosi02_sx_L,
		// Token: 0x04003106 RID: 12550
		cf_s_legskroot_kosi02_sx_R,
		// Token: 0x04003107 RID: 12551
		cf_s_legskroot_kosi02_sz,
		// Token: 0x04003108 RID: 12552
		cf_s_legskroot_leg01_L,
		// Token: 0x04003109 RID: 12553
		cf_s_legskroot_leg01_R
	}
}

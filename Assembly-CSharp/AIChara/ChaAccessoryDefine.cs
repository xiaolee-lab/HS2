using System;
using System.Collections.Generic;

namespace AIChara
{
	// Token: 0x020007AE RID: 1966
	public static class ChaAccessoryDefine
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x001087B8 File Offset: 0x00106BB8
		static ChaAccessoryDefine()
		{
			int length = Enum.GetValues(typeof(ChaAccessoryDefine.AccessoryType)).Length;
			int num = ChaAccessoryDefine.AccessoryTypeName.Length;
			if (length == num)
			{
				for (int i = 0; i < length; i++)
				{
					ChaAccessoryDefine.dictAccessoryType[i] = ChaAccessoryDefine.AccessoryTypeName[i];
				}
			}
			ChaAccessoryDefine.dictAccessoryParent = new Dictionary<int, string>();
			length = Enum.GetValues(typeof(ChaAccessoryDefine.AccessoryParentKey)).Length;
			num = ChaAccessoryDefine.AccessoryParentName.Length;
			if (length == num)
			{
				for (int j = 0; j < length; j++)
				{
					ChaAccessoryDefine.dictAccessoryParent[j] = ChaAccessoryDefine.AccessoryParentName[j];
				}
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x00108AE8 File Offset: 0x00106EE8
		public static string GetAccessoryTypeName(ChaListDefine.CategoryNo cate)
		{
			switch (cate)
			{
			case ChaListDefine.CategoryNo.ao_none:
				return ChaAccessoryDefine.AccessoryTypeName[0];
			case ChaListDefine.CategoryNo.ao_head:
				return ChaAccessoryDefine.AccessoryTypeName[1];
			case ChaListDefine.CategoryNo.ao_ear:
				return ChaAccessoryDefine.AccessoryTypeName[2];
			case ChaListDefine.CategoryNo.ao_glasses:
				return ChaAccessoryDefine.AccessoryTypeName[3];
			case ChaListDefine.CategoryNo.ao_face:
				return ChaAccessoryDefine.AccessoryTypeName[4];
			case ChaListDefine.CategoryNo.ao_neck:
				return ChaAccessoryDefine.AccessoryTypeName[5];
			case ChaListDefine.CategoryNo.ao_shoulder:
				return ChaAccessoryDefine.AccessoryTypeName[6];
			case ChaListDefine.CategoryNo.ao_chest:
				return ChaAccessoryDefine.AccessoryTypeName[7];
			case ChaListDefine.CategoryNo.ao_waist:
				return ChaAccessoryDefine.AccessoryTypeName[8];
			case ChaListDefine.CategoryNo.ao_back:
				return ChaAccessoryDefine.AccessoryTypeName[9];
			case ChaListDefine.CategoryNo.ao_arm:
				return ChaAccessoryDefine.AccessoryTypeName[10];
			case ChaListDefine.CategoryNo.ao_hand:
				return ChaAccessoryDefine.AccessoryTypeName[11];
			case ChaListDefine.CategoryNo.ao_leg:
				return ChaAccessoryDefine.AccessoryTypeName[12];
			case ChaListDefine.CategoryNo.ao_kokan:
				return ChaAccessoryDefine.AccessoryTypeName[13];
			default:
				return "不明";
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00108BB8 File Offset: 0x00106FB8
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x00108BBF File Offset: 0x00106FBF
		public static Dictionary<int, string> dictAccessoryType { get; private set; } = new Dictionary<int, string>();

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x00108BC7 File Offset: 0x00106FC7
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x00108BCE File Offset: 0x00106FCE
		public static Dictionary<int, string> dictAccessoryParent { get; private set; }

		// Token: 0x06002E8B RID: 11915 RVA: 0x00108BD8 File Offset: 0x00106FD8
		public static ChaAccessoryDefine.AccessoryParentKey GetReverseParent(ChaAccessoryDefine.AccessoryParentKey key)
		{
			switch (key)
			{
			case ChaAccessoryDefine.AccessoryParentKey.N_Tikubi_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Tikubi_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Tikubi_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Tikubi_L;
			default:
				switch (key)
				{
				case ChaAccessoryDefine.AccessoryParentKey.N_Hair_twin_L:
					return ChaAccessoryDefine.AccessoryParentKey.N_Hair_twin_R;
				case ChaAccessoryDefine.AccessoryParentKey.N_Hair_twin_R:
					return ChaAccessoryDefine.AccessoryParentKey.N_Hair_twin_L;
				case ChaAccessoryDefine.AccessoryParentKey.N_Hair_pin_L:
					return ChaAccessoryDefine.AccessoryParentKey.N_Hair_pin_R;
				case ChaAccessoryDefine.AccessoryParentKey.N_Hair_pin_R:
					return ChaAccessoryDefine.AccessoryParentKey.N_Hair_pin_L;
				case ChaAccessoryDefine.AccessoryParentKey.N_Earring_L:
					return ChaAccessoryDefine.AccessoryParentKey.N_Earring_R;
				case ChaAccessoryDefine.AccessoryParentKey.N_Earring_R:
					return ChaAccessoryDefine.AccessoryParentKey.N_Earring_L;
				}
				return ChaAccessoryDefine.AccessoryParentKey.none;
			case ChaAccessoryDefine.AccessoryParentKey.N_Back_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Back_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Back_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Back_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Waist_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Waist_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Waist_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Waist_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Leg_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Leg_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Knee_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Knee_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Ankle_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Ankle_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Foot_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Foot_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Leg_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Leg_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Knee_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Knee_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Ankle_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Ankle_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Foot_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Foot_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Shoulder_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Shoulder_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Elbo_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Elbo_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Arm_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Arm_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Wrist_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Wrist_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Shoulder_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Shoulder_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Elbo_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Elbo_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Arm_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Arm_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Wrist_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Wrist_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Hand_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Hand_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Index_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Index_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Middle_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Middle_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Ring_L:
				return ChaAccessoryDefine.AccessoryParentKey.N_Ring_R;
			case ChaAccessoryDefine.AccessoryParentKey.N_Hand_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Hand_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Index_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Index_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Middle_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Middle_L;
			case ChaAccessoryDefine.AccessoryParentKey.N_Ring_R:
				return ChaAccessoryDefine.AccessoryParentKey.N_Ring_L;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x00108D1C File Offset: 0x0010711C
		public static string GetReverseParent(string key)
		{
			switch (key)
			{
			case "N_Hair_twin_L":
				return "N_Hair_twin_R";
			case "N_Hair_pin_L":
				return "N_Hair_pin_R";
			case "N_Earring_L":
				return "N_Earring_R";
			case "N_Tikubi_L":
				return "N_Tikubi_R";
			case "N_Back_L":
				return "N_Back_R";
			case "N_Waist_L":
				return "N_Waist_R";
			case "N_Leg_L":
				return "N_Leg_R";
			case "N_Knee_L":
				return "N_Knee_R";
			case "N_Ankle_L":
				return "N_Ankle_R";
			case "N_Foot_L":
				return "N_Foot_R";
			case "N_Shoulder_L":
				return "N_Shoulder_R";
			case "N_Elbo_L":
				return "N_Elbo_R";
			case "N_Arm_L":
				return "N_Arm_R";
			case "N_Wrist_L":
				return "N_Wrist_R";
			case "N_Hand_L":
				return "N_Hand_R";
			case "N_Index_L":
				return "N_Index_R";
			case "N_Middle_L":
				return "N_Middle_R";
			case "N_Ring_L":
				return "N_Ring_R";
			case "N_Hair_twin_R":
				return "N_Hair_twin_L";
			case "N_Hair_pin_R":
				return "N_Hair_pin_L";
			case "N_Earring_R":
				return "N_Earring_L";
			case "N_Tikubi_R":
				return "N_Tikubi_L";
			case "N_Back_R":
				return "N_Back_L";
			case "N_Waist_R":
				return "N_Waist_L";
			case "N_Leg_R":
				return "N_Leg_L";
			case "N_Knee_R":
				return "N_Knee_L";
			case "N_Ankle_R":
				return "N_Ankle_L";
			case "N_Foot_R":
				return "N_Foot_L";
			case "N_Shoulder_R":
				return "N_Shoulder_L";
			case "N_Elbo_R":
				return "N_Elbo_L";
			case "N_Arm_R":
				return "N_Arm_L";
			case "N_Wrist_R":
				return "N_Wrist_L";
			case "N_Hand_R":
				return "N_Hand_L";
			case "N_Index_R":
				return "N_Index_L";
			case "N_Middle_R":
				return "N_Middle_L";
			case "N_Ring_R":
				return "N_Ring_L";
			}
			return string.Empty;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x001090A8 File Offset: 0x001074A8
		public static bool CheckPartsOfHead(string keyName)
		{
			ChaAccessoryDefine.AccessoryParentKey n;
			return Enum.TryParse<ChaAccessoryDefine.AccessoryParentKey>(keyName, out n) && MathfEx.RangeEqualOn<int>(1, (int)n, 14);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x001090D4 File Offset: 0x001074D4
		public static int GetAccessoryParentInt(string keyName)
		{
			ChaAccessoryDefine.AccessoryParentKey result;
			if (Enum.TryParse<ChaAccessoryDefine.AccessoryParentKey>(keyName, out result))
			{
				return (int)result;
			}
			return -1;
		}

		// Token: 0x04002D4C RID: 11596
		public static readonly int[] AccessoryDefaultIndex = new int[14];

		// Token: 0x04002D4D RID: 11597
		public static readonly string[] AccessoryTypeName = new string[]
		{
			"なし",
			"頭",
			"耳",
			"眼鏡",
			"顔",
			"首",
			"肩",
			"胸",
			"腰",
			"背中",
			"腕",
			"手",
			"脚",
			"下腹部"
		};

		// Token: 0x04002D4F RID: 11599
		public static readonly string[] AccessoryParentName = new string[]
		{
			"未設定",
			"ポニー",
			"ツイン左",
			"ツイン右",
			"ヘアピン左",
			"ヘアピン右",
			"帽子",
			"額",
			"頭中心",
			"顔",
			"左耳",
			"右耳",
			"眼鏡",
			"鼻",
			"口",
			"首",
			"胸上",
			"胸上中央",
			"左胸",
			"右胸",
			"背中中央",
			"背中左",
			"背中右",
			"腰",
			"腰前",
			"腰後ろ",
			"腰左",
			"腰右",
			"左太もも",
			"左ひざ",
			"左足首",
			"かかと左",
			"右太もも",
			"右ひざ",
			"右足首",
			"かかと右",
			"左肩",
			"左肘",
			"左上腕",
			"左手首",
			"右肩",
			"右肘",
			"右上腕",
			"右手首",
			"左手",
			"左人差指",
			"左中指",
			"左薬指",
			"右手",
			"右人差指",
			"右中指",
			"右薬指",
			"下腹部①",
			"下腹部②",
			"下腹部③"
		};

		// Token: 0x020007AF RID: 1967
		public enum AccessoryType
		{
			// Token: 0x04002D53 RID: 11603
			None,
			// Token: 0x04002D54 RID: 11604
			Head,
			// Token: 0x04002D55 RID: 11605
			Ear,
			// Token: 0x04002D56 RID: 11606
			Glasses,
			// Token: 0x04002D57 RID: 11607
			Face,
			// Token: 0x04002D58 RID: 11608
			Neck,
			// Token: 0x04002D59 RID: 11609
			Shoulder,
			// Token: 0x04002D5A RID: 11610
			Chest,
			// Token: 0x04002D5B RID: 11611
			Waist,
			// Token: 0x04002D5C RID: 11612
			Back,
			// Token: 0x04002D5D RID: 11613
			Arm,
			// Token: 0x04002D5E RID: 11614
			Hand,
			// Token: 0x04002D5F RID: 11615
			Leg,
			// Token: 0x04002D60 RID: 11616
			Kokan
		}

		// Token: 0x020007B0 RID: 1968
		public enum AccessoryParentKey
		{
			// Token: 0x04002D62 RID: 11618
			none,
			// Token: 0x04002D63 RID: 11619
			N_Hair_pony,
			// Token: 0x04002D64 RID: 11620
			N_Hair_twin_L,
			// Token: 0x04002D65 RID: 11621
			N_Hair_twin_R,
			// Token: 0x04002D66 RID: 11622
			N_Hair_pin_L,
			// Token: 0x04002D67 RID: 11623
			N_Hair_pin_R,
			// Token: 0x04002D68 RID: 11624
			N_Head_top,
			// Token: 0x04002D69 RID: 11625
			N_Hitai,
			// Token: 0x04002D6A RID: 11626
			N_Head,
			// Token: 0x04002D6B RID: 11627
			N_Face,
			// Token: 0x04002D6C RID: 11628
			N_Earring_L,
			// Token: 0x04002D6D RID: 11629
			N_Earring_R,
			// Token: 0x04002D6E RID: 11630
			N_Megane,
			// Token: 0x04002D6F RID: 11631
			N_Nose,
			// Token: 0x04002D70 RID: 11632
			N_Mouth,
			// Token: 0x04002D71 RID: 11633
			N_Neck,
			// Token: 0x04002D72 RID: 11634
			N_Chest_f,
			// Token: 0x04002D73 RID: 11635
			N_Chest,
			// Token: 0x04002D74 RID: 11636
			N_Tikubi_L,
			// Token: 0x04002D75 RID: 11637
			N_Tikubi_R,
			// Token: 0x04002D76 RID: 11638
			N_Back,
			// Token: 0x04002D77 RID: 11639
			N_Back_L,
			// Token: 0x04002D78 RID: 11640
			N_Back_R,
			// Token: 0x04002D79 RID: 11641
			N_Waist,
			// Token: 0x04002D7A RID: 11642
			N_Waist_f,
			// Token: 0x04002D7B RID: 11643
			N_Waist_b,
			// Token: 0x04002D7C RID: 11644
			N_Waist_L,
			// Token: 0x04002D7D RID: 11645
			N_Waist_R,
			// Token: 0x04002D7E RID: 11646
			N_Leg_L,
			// Token: 0x04002D7F RID: 11647
			N_Knee_L,
			// Token: 0x04002D80 RID: 11648
			N_Ankle_L,
			// Token: 0x04002D81 RID: 11649
			N_Foot_L,
			// Token: 0x04002D82 RID: 11650
			N_Leg_R,
			// Token: 0x04002D83 RID: 11651
			N_Knee_R,
			// Token: 0x04002D84 RID: 11652
			N_Ankle_R,
			// Token: 0x04002D85 RID: 11653
			N_Foot_R,
			// Token: 0x04002D86 RID: 11654
			N_Shoulder_L,
			// Token: 0x04002D87 RID: 11655
			N_Elbo_L,
			// Token: 0x04002D88 RID: 11656
			N_Arm_L,
			// Token: 0x04002D89 RID: 11657
			N_Wrist_L,
			// Token: 0x04002D8A RID: 11658
			N_Shoulder_R,
			// Token: 0x04002D8B RID: 11659
			N_Elbo_R,
			// Token: 0x04002D8C RID: 11660
			N_Arm_R,
			// Token: 0x04002D8D RID: 11661
			N_Wrist_R,
			// Token: 0x04002D8E RID: 11662
			N_Hand_L,
			// Token: 0x04002D8F RID: 11663
			N_Index_L,
			// Token: 0x04002D90 RID: 11664
			N_Middle_L,
			// Token: 0x04002D91 RID: 11665
			N_Ring_L,
			// Token: 0x04002D92 RID: 11666
			N_Hand_R,
			// Token: 0x04002D93 RID: 11667
			N_Index_R,
			// Token: 0x04002D94 RID: 11668
			N_Middle_R,
			// Token: 0x04002D95 RID: 11669
			N_Ring_R,
			// Token: 0x04002D96 RID: 11670
			N_Dan,
			// Token: 0x04002D97 RID: 11671
			N_Kokan,
			// Token: 0x04002D98 RID: 11672
			N_Ana
		}
	}
}

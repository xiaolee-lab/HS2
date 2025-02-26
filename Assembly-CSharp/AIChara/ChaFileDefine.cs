using System;

namespace AIChara
{
	// Token: 0x020007FA RID: 2042
	public static class ChaFileDefine
	{
		// Token: 0x06003358 RID: 13144 RVA: 0x00131E30 File Offset: 0x00130230
		public static string GetMonthInEnglish(int month)
		{
			string[] array = new string[]
			{
				"Jan.",
				"Feb.",
				"Mar.",
				"Apr.",
				"May",
				"June",
				"July",
				"Aug.",
				"Sept.",
				"Oct.",
				"Nov.",
				"Dec."
			};
			return array[month];
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x00131EAC File Offset: 0x001302AC
		public static string GetBirthdayStr(int month, int day, string culture = "ja-JP")
		{
			string result = string.Empty;
			if (culture != null)
			{
				if (!(culture == "ja-JP"))
				{
					if (!(culture == "en-US"))
					{
						if (!(culture == "zh-CN"))
						{
							if (!(culture == "zh-TW"))
							{
							}
						}
					}
					else
					{
						result = ChaFileDefine.GetMonthInEnglish(month) + " " + day.ToString();
					}
				}
				else
				{
					result = month.ToString() + "月" + day.ToString() + "日";
				}
			}
			return result;
		}

		// Token: 0x040032A8 RID: 12968
		public static readonly Version ChaFileVersion = new Version("1.0.0");

		// Token: 0x040032A9 RID: 12969
		public static readonly Version ChaFileCustomVersion = new Version("0.0.0");

		// Token: 0x040032AA RID: 12970
		public static readonly Version ChaFileFaceVersion = new Version("0.0.2");

		// Token: 0x040032AB RID: 12971
		public static readonly Version ChaFileBodyVersion = new Version("0.0.1");

		// Token: 0x040032AC RID: 12972
		public static readonly Version ChaFileHairVersion = new Version("0.0.3");

		// Token: 0x040032AD RID: 12973
		public static readonly Version ChaFileCoordinateVersion = new Version("0.0.0");

		// Token: 0x040032AE RID: 12974
		public static readonly Version ChaFileClothesVersion = new Version("0.0.0");

		// Token: 0x040032AF RID: 12975
		public static readonly Version ChaFileAccessoryVersion = new Version("0.0.0");

		// Token: 0x040032B0 RID: 12976
		public static readonly Version ChaFileParameterVersion = new Version("0.0.1");

		// Token: 0x040032B1 RID: 12977
		public static readonly Version ChaFileParameterVersion2 = new Version("0.0.0");

		// Token: 0x040032B2 RID: 12978
		public static readonly Version ChaFileStatusVersion = new Version("0.0.0");

		// Token: 0x040032B3 RID: 12979
		public static readonly Version ChaFileGameInfoVersion = new Version("0.0.0");

		// Token: 0x040032B4 RID: 12980
		public static readonly Version ChaFileGameInfoVersion2 = new Version("0.0.0");

		// Token: 0x040032B5 RID: 12981
		public const float MaleDefaultHeight = 0.75f;

		// Token: 0x040032B6 RID: 12982
		public const int AccessoryCategoryTypeNone = 120;

		// Token: 0x040032B7 RID: 12983
		public const int AccessoryColorNum = 4;

		// Token: 0x040032B8 RID: 12984
		public const int AccessoryCorrectNum = 2;

		// Token: 0x040032B9 RID: 12985
		public const int AccessorySlotNum = 20;

		// Token: 0x040032BA RID: 12986
		public const int CustomPaintNum = 2;

		// Token: 0x040032BB RID: 12987
		public const int ParamAttributeNum = 3;

		// Token: 0x040032BC RID: 12988
		public const int ClothesColorNum = 3;

		// Token: 0x040032BD RID: 12989
		public const int ClothesMaterialNum = 3;

		// Token: 0x040032BE RID: 12990
		public const int FlavorKindNum = 8;

		// Token: 0x040032BF RID: 12991
		public const int DesireKindNum = 16;

		// Token: 0x040032C0 RID: 12992
		public const int SkillSlotNum = 5;

		// Token: 0x040032C1 RID: 12993
		public const float NipStandLimit = 0.8f;

		// Token: 0x040032C2 RID: 12994
		public const float NipStandPlusH = 0.2f;

		// Token: 0x040032C3 RID: 12995
		public const float SkinGlossLimit = 0.8f;

		// Token: 0x040032C4 RID: 12996
		public const float SkinGlossPlusH = 0.2f;

		// Token: 0x040032C5 RID: 12997
		public const float VoicePitchMin = 0.94f;

		// Token: 0x040032C6 RID: 12998
		public const float VoicePitchMax = 1.06f;

		// Token: 0x040032C7 RID: 12999
		public const string CharaFileFemaleDir = "chara/female/";

		// Token: 0x040032C8 RID: 13000
		public const string CharaFileMaleDir = "chara/male/";

		// Token: 0x040032C9 RID: 13001
		public const string OldCharaFileFemaleDir = "chara/old/female/";

		// Token: 0x040032CA RID: 13002
		public const string OldCharaFileEtcDir = "chara/old/etc/";

		// Token: 0x040032CB RID: 13003
		public const string CoordinateFileFemaleDir = "coordinate/female/";

		// Token: 0x040032CC RID: 13004
		public const string CoordinateFileMaleDir = "coordinate/male/";

		// Token: 0x040032CD RID: 13005
		public const int LoadError_Tag = -1;

		// Token: 0x040032CE RID: 13006
		public const int LoadError_Version = -2;

		// Token: 0x040032CF RID: 13007
		public const int LoadError_ProductNo = -3;

		// Token: 0x040032D0 RID: 13008
		public const int LoadError_EndOfStream = -4;

		// Token: 0x040032D1 RID: 13009
		public const int LoadError_OnlyPNG = -5;

		// Token: 0x040032D2 RID: 13010
		public const int LoadError_FileNotExist = -6;

		// Token: 0x040032D3 RID: 13011
		public const int LoadError_ETC = -999;

		// Token: 0x040032D4 RID: 13012
		public const int M_DefHeadID = 0;

		// Token: 0x040032D5 RID: 13013
		public const int M_DefBodyID = 0;

		// Token: 0x040032D6 RID: 13014
		public const int M_DefHairBackID = 0;

		// Token: 0x040032D7 RID: 13015
		public const int M_DefHairFrontID = 2;

		// Token: 0x040032D8 RID: 13016
		public const int M_DefHairSideID = 0;

		// Token: 0x040032D9 RID: 13017
		public const int M_DefHairOptionID = 0;

		// Token: 0x040032DA RID: 13018
		public const int M_DefClothesTopID = 0;

		// Token: 0x040032DB RID: 13019
		public const int M_DefClothesBotID = 0;

		// Token: 0x040032DC RID: 13020
		public const int M_DefClothesGlovesID = 0;

		// Token: 0x040032DD RID: 13021
		public const int M_DefClothesShoesID = 0;

		// Token: 0x040032DE RID: 13022
		public const int F_DefHeadID = 0;

		// Token: 0x040032DF RID: 13023
		public const int F_DefBodyID = 0;

		// Token: 0x040032E0 RID: 13024
		public const int F_DefHairBackID = 0;

		// Token: 0x040032E1 RID: 13025
		public const int F_DefHairFrontID = 1;

		// Token: 0x040032E2 RID: 13026
		public const int F_DefHairSideID = 0;

		// Token: 0x040032E3 RID: 13027
		public const int F_DefHairOptionID = 0;

		// Token: 0x040032E4 RID: 13028
		public const int F_DefClothesTopID = 0;

		// Token: 0x040032E5 RID: 13029
		public const int F_DefClothesBotID = 0;

		// Token: 0x040032E6 RID: 13030
		public const int F_DefClothesInnerTID = 0;

		// Token: 0x040032E7 RID: 13031
		public const int F_DefClothesInnerBID = 0;

		// Token: 0x040032E8 RID: 13032
		public const int F_DefClothesGlovesID = 0;

		// Token: 0x040032E9 RID: 13033
		public const int F_DefClothesPanstID = 0;

		// Token: 0x040032EA RID: 13034
		public const int F_DefClothesSocksID = 0;

		// Token: 0x040032EB RID: 13035
		public const int F_DefClothesShoesID = 0;

		// Token: 0x040032EC RID: 13036
		public const string Default_Chara_UserID = "illusion-2019-1025-xxxx-aisyoujyocha";

		// Token: 0x040032ED RID: 13037
		public static readonly string[] cf_bodyshapename = new string[]
		{
			"身長",
			"胸サイズ",
			"胸上下位置",
			"胸の左右開き",
			"胸の左右位置",
			"胸上下角度",
			"胸の尖り",
			"乳輪の膨らみ",
			"乳首太さ",
			"頭サイズ",
			"首周り幅",
			"首周り奥",
			"胴体肩周り幅",
			"胴体肩周り奥",
			"胴体上幅",
			"胴体上奥",
			"胴体下幅",
			"胴体下奥",
			"ウエスト位置",
			"腰上幅",
			"腰上奥",
			"腰下幅",
			"腰下奥",
			"尻",
			"尻角度",
			"太もも上",
			"太もも下",
			"ふくらはぎ",
			"足首",
			"肩",
			"上腕",
			"前腕",
			"乳首立ち"
		};

		// Token: 0x040032EE RID: 13038
		public static readonly int[] cf_BustShapeMaskID = new int[]
		{
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			32
		};

		// Token: 0x040032EF RID: 13039
		public static readonly int[] cf_ShapeMaskBust = new int[]
		{
			0,
			1,
			2,
			3,
			4
		};

		// Token: 0x040032F0 RID: 13040
		public static readonly int[] cf_ShapeMaskNip = new int[]
		{
			5,
			6
		};

		// Token: 0x040032F1 RID: 13041
		public const int cf_ShapeMaskNipStand = 7;

		// Token: 0x040032F2 RID: 13042
		public static readonly float[] cf_bodyInitValue = new float[]
		{
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0f
		};

		// Token: 0x040032F3 RID: 13043
		public static readonly string[] cf_headshapename = new string[]
		{
			"顔全体横幅",
			"顔上部前後",
			"顔上部上下",
			"顔下部前後",
			"顔下部横幅",
			"顎横幅",
			"顎上下",
			"顎前後",
			"顎角度",
			"顎下部上下",
			"顎先幅",
			"顎先上下",
			"顎先前後",
			"頬下部上下",
			"頬下部前後",
			"頬下部幅",
			"頬上部上下",
			"頬上部前後",
			"頬上部幅",
			"目上下",
			"目横位置",
			"目前後",
			"目の横幅",
			"目の縦幅",
			"目の角度Z軸",
			"目の角度Y軸",
			"目頭左右位置",
			"目尻左右位置",
			"目頭上下位置",
			"目尻上下位置",
			"まぶた形状１",
			"まぶた形状２",
			"鼻全体上下",
			"鼻全体前後",
			"鼻全体角度X軸",
			"鼻全体横幅",
			"鼻筋高さ",
			"鼻筋横幅",
			"鼻筋形状",
			"小鼻横幅",
			"小鼻上下",
			"小鼻前後",
			"小鼻角度X軸",
			"小鼻角度Z軸",
			"鼻先高さ",
			"鼻先角度X軸",
			"鼻先サイズ",
			"口上下",
			"口横幅",
			"口縦幅",
			"口前後位置",
			"口形状上",
			"口形状下",
			"口形状口角",
			"耳サイズ",
			"耳角度Y軸",
			"耳角度Z軸",
			"耳上部形状",
			"耳下部形状"
		};

		// Token: 0x040032F4 RID: 13044
		public static readonly int[] cf_MouthShapeMaskID = new int[]
		{
			3,
			6,
			11,
			47,
			48,
			49,
			50,
			51,
			52,
			53
		};

		// Token: 0x040032F5 RID: 13045
		public static readonly float[] cf_MouthShapeDefault = new float[]
		{
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f
		};

		// Token: 0x040032F6 RID: 13046
		public static readonly float[] cf_faceInitValue = new float[]
		{
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f,
			0.5f
		};

		// Token: 0x040032F7 RID: 13047
		public static readonly string[] cp_statename = new string[]
		{
			"普通",
			"好意",
			"享楽",
			"嫌悪",
			"隷属",
			"壊れ",
			"依存"
		};

		// Token: 0x040032F8 RID: 13048
		public static readonly string[] cp_traitname = new string[]
		{
			"なし",
			"綺麗好き",
			"ものぐさ",
			"疲れやすい",
			"疲れ知らず",
			"頻尿",
			"グラスハート",
			"不屈の精神",
			"欲求不満",
			"思うがまま",
			"感受性豊か"
		};

		// Token: 0x040032F9 RID: 13049
		public static readonly string[] cp_mindname = new string[]
		{
			"なし",
			"気になる",
			"好きかも",
			"一目惚れ",
			"話しづらい",
			"苦手",
			"嫌い",
			"指示されたい",
			"命令されたい",
			"逆らえない",
			"楽しい",
			"虐めたい",
			"服従させたい",
			"・・・・",
			"ズットイッショ"
		};

		// Token: 0x040032FA RID: 13050
		public static readonly string[] cp_hattributename = new string[]
		{
			"なし",
			"性欲旺盛",
			"S",
			"M",
			"胸が敏感",
			"お尻が敏感",
			"股間が敏感",
			"キスに弱い",
			"潔癖症",
			"Hに抵抗がある",
			"寂しがり屋",
			"バイセクシャル"
		};

		// Token: 0x020007FB RID: 2043
		public enum BodyShapeIdx
		{
			// Token: 0x040032FC RID: 13052
			Height,
			// Token: 0x040032FD RID: 13053
			BustSize,
			// Token: 0x040032FE RID: 13054
			BustY,
			// Token: 0x040032FF RID: 13055
			BustRotX,
			// Token: 0x04003300 RID: 13056
			BustX,
			// Token: 0x04003301 RID: 13057
			BustRotY,
			// Token: 0x04003302 RID: 13058
			BustSharp,
			// Token: 0x04003303 RID: 13059
			AreolaBulge,
			// Token: 0x04003304 RID: 13060
			NipWeight,
			// Token: 0x04003305 RID: 13061
			HeadSize,
			// Token: 0x04003306 RID: 13062
			NeckW,
			// Token: 0x04003307 RID: 13063
			NeckZ,
			// Token: 0x04003308 RID: 13064
			BodyShoulderW,
			// Token: 0x04003309 RID: 13065
			BodyShoulderZ,
			// Token: 0x0400330A RID: 13066
			BodyUpW,
			// Token: 0x0400330B RID: 13067
			BodyUpZ,
			// Token: 0x0400330C RID: 13068
			BodyLowW,
			// Token: 0x0400330D RID: 13069
			BodyLowZ,
			// Token: 0x0400330E RID: 13070
			WaistY,
			// Token: 0x0400330F RID: 13071
			WaistUpW,
			// Token: 0x04003310 RID: 13072
			WaistUpZ,
			// Token: 0x04003311 RID: 13073
			WaistLowW,
			// Token: 0x04003312 RID: 13074
			WaistLowZ,
			// Token: 0x04003313 RID: 13075
			Hip,
			// Token: 0x04003314 RID: 13076
			HipRotX,
			// Token: 0x04003315 RID: 13077
			ThighUp,
			// Token: 0x04003316 RID: 13078
			ThighLow,
			// Token: 0x04003317 RID: 13079
			Calf,
			// Token: 0x04003318 RID: 13080
			Ankle,
			// Token: 0x04003319 RID: 13081
			Shoulder,
			// Token: 0x0400331A RID: 13082
			ArmUp,
			// Token: 0x0400331B RID: 13083
			ArmLow,
			// Token: 0x0400331C RID: 13084
			NipStand
		}

		// Token: 0x020007FC RID: 2044
		public enum FaceShapeIdx
		{
			// Token: 0x0400331E RID: 13086
			FaceBaseW,
			// Token: 0x0400331F RID: 13087
			FaceUpZ,
			// Token: 0x04003320 RID: 13088
			FaceUpY,
			// Token: 0x04003321 RID: 13089
			FaceLowZ,
			// Token: 0x04003322 RID: 13090
			FaceLowW,
			// Token: 0x04003323 RID: 13091
			ChinW,
			// Token: 0x04003324 RID: 13092
			ChinY,
			// Token: 0x04003325 RID: 13093
			ChinZ,
			// Token: 0x04003326 RID: 13094
			ChinRot,
			// Token: 0x04003327 RID: 13095
			ChinLowY,
			// Token: 0x04003328 RID: 13096
			ChinTipW,
			// Token: 0x04003329 RID: 13097
			ChinTipY,
			// Token: 0x0400332A RID: 13098
			ChinTipZ,
			// Token: 0x0400332B RID: 13099
			CheekLowY,
			// Token: 0x0400332C RID: 13100
			CheekLowZ,
			// Token: 0x0400332D RID: 13101
			CheekLowW,
			// Token: 0x0400332E RID: 13102
			CheekUpY,
			// Token: 0x0400332F RID: 13103
			CheekUpZ,
			// Token: 0x04003330 RID: 13104
			CheekUpW,
			// Token: 0x04003331 RID: 13105
			EyeY,
			// Token: 0x04003332 RID: 13106
			EyeX,
			// Token: 0x04003333 RID: 13107
			EyeZ,
			// Token: 0x04003334 RID: 13108
			EyeW,
			// Token: 0x04003335 RID: 13109
			EyeH,
			// Token: 0x04003336 RID: 13110
			EyeRotZ,
			// Token: 0x04003337 RID: 13111
			EyeRotY,
			// Token: 0x04003338 RID: 13112
			EyeInX,
			// Token: 0x04003339 RID: 13113
			EyeOutX,
			// Token: 0x0400333A RID: 13114
			EyeInY,
			// Token: 0x0400333B RID: 13115
			EyeOutY,
			// Token: 0x0400333C RID: 13116
			EyelidForm01,
			// Token: 0x0400333D RID: 13117
			EyelidForm02,
			// Token: 0x0400333E RID: 13118
			NoseAllY,
			// Token: 0x0400333F RID: 13119
			NoseAllZ,
			// Token: 0x04003340 RID: 13120
			NoseAllRotX,
			// Token: 0x04003341 RID: 13121
			NoseAllW,
			// Token: 0x04003342 RID: 13122
			NoseBridgeH,
			// Token: 0x04003343 RID: 13123
			NoseBridgeW,
			// Token: 0x04003344 RID: 13124
			NoseBridgeForm,
			// Token: 0x04003345 RID: 13125
			NoseWingW,
			// Token: 0x04003346 RID: 13126
			NoseWingY,
			// Token: 0x04003347 RID: 13127
			NoseWingZ,
			// Token: 0x04003348 RID: 13128
			NoseWingRotX,
			// Token: 0x04003349 RID: 13129
			NoseWingRotZ,
			// Token: 0x0400334A RID: 13130
			NoseH,
			// Token: 0x0400334B RID: 13131
			NoseRotX,
			// Token: 0x0400334C RID: 13132
			NoseSize,
			// Token: 0x0400334D RID: 13133
			MouthY,
			// Token: 0x0400334E RID: 13134
			MouthW,
			// Token: 0x0400334F RID: 13135
			MouthH,
			// Token: 0x04003350 RID: 13136
			MouthZ,
			// Token: 0x04003351 RID: 13137
			MouthUpForm,
			// Token: 0x04003352 RID: 13138
			MouthLowForm,
			// Token: 0x04003353 RID: 13139
			MouthCornerForm,
			// Token: 0x04003354 RID: 13140
			EarSize,
			// Token: 0x04003355 RID: 13141
			EarRotY,
			// Token: 0x04003356 RID: 13142
			EarRotZ,
			// Token: 0x04003357 RID: 13143
			EarUpForm,
			// Token: 0x04003358 RID: 13144
			EarLowForm
		}

		// Token: 0x020007FD RID: 2045
		public enum HairKind
		{
			// Token: 0x0400335A RID: 13146
			back,
			// Token: 0x0400335B RID: 13147
			front,
			// Token: 0x0400335C RID: 13148
			side,
			// Token: 0x0400335D RID: 13149
			option
		}

		// Token: 0x020007FE RID: 2046
		public enum ClothesKind
		{
			// Token: 0x0400335F RID: 13151
			top,
			// Token: 0x04003360 RID: 13152
			bot,
			// Token: 0x04003361 RID: 13153
			inner_t,
			// Token: 0x04003362 RID: 13154
			inner_b,
			// Token: 0x04003363 RID: 13155
			gloves,
			// Token: 0x04003364 RID: 13156
			panst,
			// Token: 0x04003365 RID: 13157
			socks,
			// Token: 0x04003366 RID: 13158
			shoes
		}

		// Token: 0x020007FF RID: 2047
		public enum SiruParts
		{
			// Token: 0x04003368 RID: 13160
			SiruKao,
			// Token: 0x04003369 RID: 13161
			SiruFrontTop,
			// Token: 0x0400336A RID: 13162
			SiruFrontBot,
			// Token: 0x0400336B RID: 13163
			SiruBackTop,
			// Token: 0x0400336C RID: 13164
			SiruBackBot
		}

		// Token: 0x02000800 RID: 2048
		public enum State
		{
			// Token: 0x0400336E RID: 13166
			Blank,
			// Token: 0x0400336F RID: 13167
			Favor,
			// Token: 0x04003370 RID: 13168
			Enjoyment,
			// Token: 0x04003371 RID: 13169
			Aversion,
			// Token: 0x04003372 RID: 13170
			Slavery,
			// Token: 0x04003373 RID: 13171
			Broken,
			// Token: 0x04003374 RID: 13172
			Dependence
		}

		// Token: 0x02000801 RID: 2049
		public enum TraitKind
		{
			// Token: 0x04003376 RID: 13174
			None,
			// Token: 0x04003377 RID: 13175
			Cleanliness,
			// Token: 0x04003378 RID: 13176
			Lazy,
			// Token: 0x04003379 RID: 13177
			Tire,
			// Token: 0x0400337A RID: 13178
			Tireless,
			// Token: 0x0400337B RID: 13179
			Pollakiuria,
			// Token: 0x0400337C RID: 13180
			HeartOfGlass,
			// Token: 0x0400337D RID: 13181
			Fortitude,
			// Token: 0x0400337E RID: 13182
			Frustration,
			// Token: 0x0400337F RID: 13183
			AsPleases,
			// Token: 0x04003380 RID: 13184
			VerySensitive
		}

		// Token: 0x02000802 RID: 2050
		public enum MindKind
		{
			// Token: 0x04003382 RID: 13186
			None,
			// Token: 0x04003383 RID: 13187
			Interested,
			// Token: 0x04003384 RID: 13188
			Like,
			// Token: 0x04003385 RID: 13189
			LoveAtFirstSight,
			// Token: 0x04003386 RID: 13190
			DifficultToTalkTo,
			// Token: 0x04003387 RID: 13191
			Dislike,
			// Token: 0x04003388 RID: 13192
			Hate,
			// Token: 0x04003389 RID: 13193
			WantToBeDirected,
			// Token: 0x0400338A RID: 13194
			CommandMe,
			// Token: 0x0400338B RID: 13195
			Irresistible,
			// Token: 0x0400338C RID: 13196
			Happy,
			// Token: 0x0400338D RID: 13197
			WantToTease,
			// Token: 0x0400338E RID: 13198
			WantToSubmit,
			// Token: 0x0400338F RID: 13199
			Tententen,
			// Token: 0x04003390 RID: 13200
			Forever
		}

		// Token: 0x02000803 RID: 2051
		public enum HAttributeKind
		{
			// Token: 0x04003392 RID: 13202
			None,
			// Token: 0x04003393 RID: 13203
			HighlySexed,
			// Token: 0x04003394 RID: 13204
			S,
			// Token: 0x04003395 RID: 13205
			M,
			// Token: 0x04003396 RID: 13206
			SensitiveChest,
			// Token: 0x04003397 RID: 13207
			SensitiveAss,
			// Token: 0x04003398 RID: 13208
			SensitiveCrotch,
			// Token: 0x04003399 RID: 13209
			WeakToKiss,
			// Token: 0x0400339A RID: 13210
			ObsessionWithCleanliness,
			// Token: 0x0400339B RID: 13211
			HaveSomeBarriersToH,
			// Token: 0x0400339C RID: 13212
			LonelyPerson,
			// Token: 0x0400339D RID: 13213
			Bisexual
		}

		// Token: 0x02000804 RID: 2052
		public enum Personality
		{
			// Token: 0x0400339F RID: 13215
			Mukuchi,
			// Token: 0x040033A0 RID: 13216
			Oraka,
			// Token: 0x040033A1 RID: 13217
			Cool,
			// Token: 0x040033A2 RID: 13218
			Tsundere,
			// Token: 0x040033A3 RID: 13219
			Monogusa,
			// Token: 0x040033A4 RID: 13220
			Genki,
			// Token: 0x040033A5 RID: 13221
			Kichomen,
			// Token: 0x040033A6 RID: 13222
			Nedeshiko,
			// Token: 0x040033A7 RID: 13223
			Boyish,
			// Token: 0x040033A8 RID: 13224
			Yandere
		}
	}
}

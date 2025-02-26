using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C3 RID: 1987
	[DefaultExecutionOrder(-5)]
	public static class ChaShader
	{
		// Token: 0x04002E9B RID: 11931
		public static readonly int MainTex = Shader.PropertyToID("_MainTex");

		// Token: 0x04002E9C RID: 11932
		public static readonly int Color = Shader.PropertyToID("_Color");

		// Token: 0x04002E9D RID: 11933
		public static readonly int Color2 = Shader.PropertyToID("_Color2");

		// Token: 0x04002E9E RID: 11934
		public static readonly int Color3 = Shader.PropertyToID("_Color3");

		// Token: 0x04002E9F RID: 11935
		public static readonly int Color4 = Shader.PropertyToID("_Color4");

		// Token: 0x04002EA0 RID: 11936
		public static readonly int Specular = Shader.PropertyToID("_Specular");

		// Token: 0x04002EA1 RID: 11937
		public static readonly int Gloss = Shader.PropertyToID("_Gloss");

		// Token: 0x04002EA2 RID: 11938
		public static readonly int Metallic = Shader.PropertyToID("_Metallic");

		// Token: 0x04002EA3 RID: 11939
		public static readonly int Smoothness = Shader.PropertyToID("_Smoothness");

		// Token: 0x04002EA4 RID: 11940
		public static readonly int SkinTex = Shader.PropertyToID("_MainTex");

		// Token: 0x04002EA5 RID: 11941
		public static readonly int SkinCreateDetailTex = Shader.PropertyToID("_DetailMainTex");

		// Token: 0x04002EA6 RID: 11942
		public static readonly int SkinOcclusionMapTex = Shader.PropertyToID("_OcclusionMap");

		// Token: 0x04002EA7 RID: 11943
		public static readonly int SkinNormalMapTex = Shader.PropertyToID("_BumpMap");

		// Token: 0x04002EA8 RID: 11944
		public static readonly int SkinColor = Shader.PropertyToID("_Color");

		// Token: 0x04002EA9 RID: 11945
		public static readonly int SkinDetailTex = Shader.PropertyToID("_BumpMap2");

		// Token: 0x04002EAA RID: 11946
		public static readonly int SkinDetailPower = Shader.PropertyToID("_BumpScale2");

		// Token: 0x04002EAB RID: 11947
		public static readonly int Paint01Tex = Shader.PropertyToID("_Texture5");

		// Token: 0x04002EAC RID: 11948
		public static readonly int Paint01Color = Shader.PropertyToID("_Color5");

		// Token: 0x04002EAD RID: 11949
		public static readonly int Paint01Gloass = Shader.PropertyToID("_Gloss5");

		// Token: 0x04002EAE RID: 11950
		public static readonly int Paint01Metallic = Shader.PropertyToID("_Metallic5");

		// Token: 0x04002EAF RID: 11951
		public static readonly int Paint01Layout = Shader.PropertyToID("_Texture5UV");

		// Token: 0x04002EB0 RID: 11952
		public static readonly int Paint01Rot = Shader.PropertyToID("_Texture5Rotator");

		// Token: 0x04002EB1 RID: 11953
		public static readonly int Paint02Tex = Shader.PropertyToID("_Texture6");

		// Token: 0x04002EB2 RID: 11954
		public static readonly int Paint02Color = Shader.PropertyToID("_Color6");

		// Token: 0x04002EB3 RID: 11955
		public static readonly int Paint02Gloass = Shader.PropertyToID("_Gloss6");

		// Token: 0x04002EB4 RID: 11956
		public static readonly int Paint02Metallic = Shader.PropertyToID("_Metallic6");

		// Token: 0x04002EB5 RID: 11957
		public static readonly int Paint02Layout = Shader.PropertyToID("_Texture6UV");

		// Token: 0x04002EB6 RID: 11958
		public static readonly int Paint02Rot = Shader.PropertyToID("_Texture6Rotator");

		// Token: 0x04002EB7 RID: 11959
		public static readonly int EyeshadowTex = Shader.PropertyToID("_Texture11");

		// Token: 0x04002EB8 RID: 11960
		public static readonly int EyeshadowColor = Shader.PropertyToID("_Color11");

		// Token: 0x04002EB9 RID: 11961
		public static readonly int EyeshadowGloss = Shader.PropertyToID("_Gloss11");

		// Token: 0x04002EBA RID: 11962
		public static readonly int CheekTex = Shader.PropertyToID("_Texture10");

		// Token: 0x04002EBB RID: 11963
		public static readonly int CheekColor = Shader.PropertyToID("_Color10");

		// Token: 0x04002EBC RID: 11964
		public static readonly int CheekGloss = Shader.PropertyToID("_Gloss10");

		// Token: 0x04002EBD RID: 11965
		public static readonly int LipTex = Shader.PropertyToID("_Texture9");

		// Token: 0x04002EBE RID: 11966
		public static readonly int LipColor = Shader.PropertyToID("_Color9");

		// Token: 0x04002EBF RID: 11967
		public static readonly int LipGloss = Shader.PropertyToID("_Gloss9");

		// Token: 0x04002EC0 RID: 11968
		public static readonly int MoleTex = Shader.PropertyToID("_Texture12");

		// Token: 0x04002EC1 RID: 11969
		public static readonly int MoleColor = Shader.PropertyToID("_Color12");

		// Token: 0x04002EC2 RID: 11970
		public static readonly int MoleLayout = Shader.PropertyToID("_Texture12UV");

		// Token: 0x04002EC3 RID: 11971
		public static readonly int EyebrowTex = Shader.PropertyToID("_Texture3");

		// Token: 0x04002EC4 RID: 11972
		public static readonly int EyebrowColor = Shader.PropertyToID("_Color3");

		// Token: 0x04002EC5 RID: 11973
		public static readonly int EyebrowLayout = Shader.PropertyToID("_Texture3UV");

		// Token: 0x04002EC6 RID: 11974
		public static readonly int EyebrowTilt = Shader.PropertyToID("_Texture3Rotator");

		// Token: 0x04002EC7 RID: 11975
		public static readonly int EyesWhiteColor = Shader.PropertyToID("_Color");

		// Token: 0x04002EC8 RID: 11976
		public static readonly int PupilTex = Shader.PropertyToID("_Texture2");

		// Token: 0x04002EC9 RID: 11977
		public static readonly int PupilLayout = Shader.PropertyToID("_texture2uv");

		// Token: 0x04002ECA RID: 11978
		public static readonly int PupilColor = Shader.PropertyToID("_Color2");

		// Token: 0x04002ECB RID: 11979
		public static readonly int PupilEmission = Shader.PropertyToID("_Emission");

		// Token: 0x04002ECC RID: 11980
		public static readonly int PupilBlackTex = Shader.PropertyToID("_Texture3");

		// Token: 0x04002ECD RID: 11981
		public static readonly int PupilBlackColor = Shader.PropertyToID("_Color3");

		// Token: 0x04002ECE RID: 11982
		public static readonly int PupilBlackLayout = Shader.PropertyToID("_texture3uv");

		// Token: 0x04002ECF RID: 11983
		public static readonly int EyesHighlightTex = Shader.PropertyToID("_Texture4");

		// Token: 0x04002ED0 RID: 11984
		public static readonly int EyesHighlightColor = Shader.PropertyToID("_Color4");

		// Token: 0x04002ED1 RID: 11985
		public static readonly int EyesHighlightLayout = Shader.PropertyToID("_Texture4UV");

		// Token: 0x04002ED2 RID: 11986
		public static readonly int EyesHighlightTilt = Shader.PropertyToID("_Texture4Rotator");

		// Token: 0x04002ED3 RID: 11987
		public static readonly int EyesShadowRange = Shader.PropertyToID("_ShadowScale");

		// Token: 0x04002ED4 RID: 11988
		public static readonly int EyelashesTex = Shader.PropertyToID("_MainTex");

		// Token: 0x04002ED5 RID: 11989
		public static readonly int EyelashesColor = Shader.PropertyToID("_Color");

		// Token: 0x04002ED6 RID: 11990
		public static readonly int BeardTex = Shader.PropertyToID("_Texture5");

		// Token: 0x04002ED7 RID: 11991
		public static readonly int BeardColor = Shader.PropertyToID("_Color5");

		// Token: 0x04002ED8 RID: 11992
		public static readonly int EyesHighlightOnOff = Shader.PropertyToID("_Smoothness");

		// Token: 0x04002ED9 RID: 11993
		public static readonly int HohoAka = Shader.PropertyToID("_Texture4Scale");

		// Token: 0x04002EDA RID: 11994
		public static readonly int SunburnTex = Shader.PropertyToID("_Texture7");

		// Token: 0x04002EDB RID: 11995
		public static readonly int SunburnColor = Shader.PropertyToID("_Color7");

		// Token: 0x04002EDC RID: 11996
		public static readonly int NipTex = Shader.PropertyToID("_Texture2");

		// Token: 0x04002EDD RID: 11997
		public static readonly int NipColor = Shader.PropertyToID("_Color2");

		// Token: 0x04002EDE RID: 11998
		public static readonly int NipGloss = Shader.PropertyToID("_NipGloss");

		// Token: 0x04002EDF RID: 11999
		public static readonly int NipScale = Shader.PropertyToID("_NipScale");

		// Token: 0x04002EE0 RID: 12000
		public static readonly int UnderhairTex = Shader.PropertyToID("_Texture3");

		// Token: 0x04002EE1 RID: 12001
		public static readonly int UnderhairColor = Shader.PropertyToID("_Color3");

		// Token: 0x04002EE2 RID: 12002
		public static readonly int NailColor = Shader.PropertyToID("_Color13");

		// Token: 0x04002EE3 RID: 12003
		public static readonly int NailGloss = Shader.PropertyToID("_NailGloss");

		// Token: 0x04002EE4 RID: 12004
		public static readonly int AlphaMask = Shader.PropertyToID("_AlphaMask");

		// Token: 0x04002EE5 RID: 12005
		public static readonly int AlphaMask2 = Shader.PropertyToID("_AlphaMask2");

		// Token: 0x04002EE6 RID: 12006
		public static readonly int alpha_a = Shader.PropertyToID("_alpha_a");

		// Token: 0x04002EE7 RID: 12007
		public static readonly int alpha_b = Shader.PropertyToID("_alpha_b");

		// Token: 0x04002EE8 RID: 12008
		public static readonly int alpha_c = Shader.PropertyToID("_alpha_c");

		// Token: 0x04002EE9 RID: 12009
		public static readonly int alpha_d = Shader.PropertyToID("_alpha_d");

		// Token: 0x04002EEA RID: 12010
		public static readonly int SiriAka = Shader.PropertyToID("_Texture4Scale");

		// Token: 0x04002EEB RID: 12011
		public static readonly int HairMainColor = Shader.PropertyToID("_Color");

		// Token: 0x04002EEC RID: 12012
		public static readonly int HairTopColor = Shader.PropertyToID("_TopColor");

		// Token: 0x04002EED RID: 12013
		public static readonly int HairUnderColor = Shader.PropertyToID("_UnderColor");

		// Token: 0x04002EEE RID: 12014
		public static readonly int HairRingoff = Shader.PropertyToID("_Ringoff");

		// Token: 0x04002EEF RID: 12015
		public static readonly int HairMeshColorMask = Shader.PropertyToID("_MeshColorMask_B");

		// Token: 0x04002EF0 RID: 12016
		public static readonly int HairMeshColor = Shader.PropertyToID("_MeshColor_01");

		// Token: 0x04002EF1 RID: 12017
		public static readonly int DetailMainTex = Shader.PropertyToID("_DetailMainTex");

		// Token: 0x04002EF2 RID: 12018
		public static readonly int ColorMask = Shader.PropertyToID("_ColorMask");

		// Token: 0x04002EF3 RID: 12019
		public static readonly int PatternMask1 = Shader.PropertyToID("_PatternMask1");

		// Token: 0x04002EF4 RID: 12020
		public static readonly int PatternMask2 = Shader.PropertyToID("_PatternMask2");

		// Token: 0x04002EF5 RID: 12021
		public static readonly int PatternMask3 = Shader.PropertyToID("_PatternMask3");

		// Token: 0x04002EF6 RID: 12022
		public static readonly int Color1_2 = Shader.PropertyToID("_Color1_2");

		// Token: 0x04002EF7 RID: 12023
		public static readonly int Color2_2 = Shader.PropertyToID("_Color2_2");

		// Token: 0x04002EF8 RID: 12024
		public static readonly int Color3_2 = Shader.PropertyToID("_Color3_2");

		// Token: 0x04002EF9 RID: 12025
		public static readonly int ClothesGloss1 = Shader.PropertyToID("_Glossiness");

		// Token: 0x04002EFA RID: 12026
		public static readonly int ClothesGloss2 = Shader.PropertyToID("_Glossiness2");

		// Token: 0x04002EFB RID: 12027
		public static readonly int ClothesGloss3 = Shader.PropertyToID("_Glossiness3");

		// Token: 0x04002EFC RID: 12028
		public static readonly int ClothesGloss4 = Shader.PropertyToID("_Glossiness4");

		// Token: 0x04002EFD RID: 12029
		public static readonly int Metallic2 = Shader.PropertyToID("_Metallic2");

		// Token: 0x04002EFE RID: 12030
		public static readonly int Metallic3 = Shader.PropertyToID("_Metallic3");

		// Token: 0x04002EFF RID: 12031
		public static readonly int Metallic4 = Shader.PropertyToID("_Metallic4");

		// Token: 0x04002F00 RID: 12032
		public static readonly int patternuv1 = Shader.PropertyToID("_patternuv1");

		// Token: 0x04002F01 RID: 12033
		public static readonly int patternuv2 = Shader.PropertyToID("_patternuv2");

		// Token: 0x04002F02 RID: 12034
		public static readonly int patternuv3 = Shader.PropertyToID("_patternuv3");

		// Token: 0x04002F03 RID: 12035
		public static readonly int patternuv1Rotator = Shader.PropertyToID("_patternuv1Rotator");

		// Token: 0x04002F04 RID: 12036
		public static readonly int patternuv2Rotator = Shader.PropertyToID("_patternuv2Rotator");

		// Token: 0x04002F05 RID: 12037
		public static readonly int patternuv3Rotator = Shader.PropertyToID("_patternuv3Rotator");

		// Token: 0x04002F06 RID: 12038
		public static readonly int uvScalePattern = Shader.PropertyToID("_UVScalePattern");

		// Token: 0x04002F07 RID: 12039
		public static readonly int ClothesBreak = Shader.PropertyToID("_AlphaEx");

		// Token: 0x04002F08 RID: 12040
		public static readonly int siruFrontTop = Shader.PropertyToID("_WeatheringRange1");

		// Token: 0x04002F09 RID: 12041
		public static readonly int siruFrontBot = Shader.PropertyToID("_WeatheringRange2");

		// Token: 0x04002F0A RID: 12042
		public static readonly int siruBackTop = Shader.PropertyToID("_WeatheringRange3");

		// Token: 0x04002F0B RID: 12043
		public static readonly int siruBackBot = Shader.PropertyToID("_WeatheringRange4");

		// Token: 0x04002F0C RID: 12044
		public static readonly int siruFace = Shader.PropertyToID("_WeatheringRange6");

		// Token: 0x04002F0D RID: 12045
		public static readonly int tearsRate = Shader.PropertyToID("_NamidaScale");

		// Token: 0x04002F0E RID: 12046
		public static readonly int wetRate = Shader.PropertyToID("_ExGloss");
	}
}

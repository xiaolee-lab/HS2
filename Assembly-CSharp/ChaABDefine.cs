using System;

// Token: 0x020007B1 RID: 1969
public static class ChaABDefine
{
	// Token: 0x06002E8F RID: 11919 RVA: 0x001090F1 File Offset: 0x001074F1
	public static string BodyAsset(int sex)
	{
		return (sex != 0) ? "p_cf_body_00" : "p_cm_body_00";
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x00109108 File Offset: 0x00107508
	public static string BodyMaterialAsset(int sex)
	{
		return (sex != 0) ? "cf_m_skin_body_00" : "cm_m_skin_body_00";
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x0010911F File Offset: 0x0010751F
	public static string ShapeHeadListAsset(int sex)
	{
		return (sex != 0) ? "cf_customhead" : "cm_customhead";
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x00109136 File Offset: 0x00107536
	public static string SilhouetteAsset(int sex)
	{
		return (sex != 0) ? "p_cf_body_silhouette" : "p_cm_body_silhouette";
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x0010914D File Offset: 0x0010754D
	public static string PresetAssetBundle(int sex)
	{
		return (sex != 0) ? "custom/00/presets_f_00.unity3d" : "custom/00/presets_m_00.unity3d";
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x00109164 File Offset: 0x00107564
	public static string PresetAsset(int sex)
	{
		return (sex != 0) ? "cf_mannequin" : "cm_mannequin";
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x0010917B File Offset: 0x0010757B
	public static string CustomAnimAssetBundle(int sex)
	{
		return (sex != 0) ? "custom/00/anim_f_00.unity3d" : "custom/00/anim_m_00.unity3d";
	}

	// Token: 0x06002E96 RID: 11926 RVA: 0x00109192 File Offset: 0x00107592
	public static string CustomAnimAsset(int sex)
	{
		return (sex != 0) ? "edit_F" : "edit_M";
	}

	// Token: 0x04002D99 RID: 11673
	public const string MainManifest = "abdata";

	// Token: 0x04002D9A RID: 11674
	public const string RandomNameListAssetBundle = "list/characustom/namelist.unity3d";

	// Token: 0x04002D9B RID: 11675
	public const string RandomNameListAsset = "RandNameList_Name";

	// Token: 0x04002D9C RID: 11676
	public const string EtcAssetBundle = "chara/etc.unity3d";

	// Token: 0x04002D9D RID: 11677
	public const string BaseObjectAssetBundle = "chara/oo_base.unity3d";

	// Token: 0x04002D9E RID: 11678
	public const string BaseMaterialAssetBundle = "chara/mm_base.unity3d";

	// Token: 0x04002D9F RID: 11679
	public const string BodyBoneAsset = "p_cf_anim";

	// Token: 0x04002DA0 RID: 11680
	public const string HeadBoneAsset = "p_cf_head_bone";

	// Token: 0x04002DA1 RID: 11681
	public const string MaleBodyAsset = "p_cm_body_00";

	// Token: 0x04002DA2 RID: 11682
	public const string FemaleBodyAsset = "p_cf_body_00";

	// Token: 0x04002DA3 RID: 11683
	public const string FemaleBodyNormalAsset = "p_cf_body_00_Nml";

	// Token: 0x04002DA4 RID: 11684
	public const string FemaleBodyHitAsset = "p_cf_body_00_hit";

	// Token: 0x04002DA5 RID: 11685
	public const string BlackTex2048Asset = "black2048";

	// Token: 0x04002DA6 RID: 11686
	public const string BlackTex4096Asset = "black4096";

	// Token: 0x04002DA7 RID: 11687
	public const string MaleBodyMaterialAsset = "cm_m_skin_body_00";

	// Token: 0x04002DA8 RID: 11688
	public const string FemaleBodyMaterialAsset = "cf_m_skin_body_00";

	// Token: 0x04002DA9 RID: 11689
	public const string CreateMatFaceBaseAsset = "create_skin_face";

	// Token: 0x04002DAA RID: 11690
	public const string CreateMatFaceGlossAsset = "create_skin detail_face";

	// Token: 0x04002DAB RID: 11691
	public const string CreateMatBodyBaseAsset = "create_skin_body";

	// Token: 0x04002DAC RID: 11692
	public const string CreateMatBodyGlossAsset = "create_skin detail_body";

	// Token: 0x04002DAD RID: 11693
	public const string CreateMatClothesAsset = "create_clothes";

	// Token: 0x04002DAE RID: 11694
	public const string CreateMatClothesGlossAsset = "create_clothes detail";

	// Token: 0x04002DAF RID: 11695
	public const string ShapeListAssetBundle = "list/customshape.unity3d";

	// Token: 0x04002DB0 RID: 11696
	public const string MaleShapeHeadListAsset = "cm_customhead";

	// Token: 0x04002DB1 RID: 11697
	public const string FemaleShapeHeadListAsset = "cf_customhead";

	// Token: 0x04002DB2 RID: 11698
	public const string FemaleShapeBodyListAsset = "cf_custombody";

	// Token: 0x04002DB3 RID: 11699
	public const string FemaleShapeHandListAsset = "cf_customhand";

	// Token: 0x04002DB4 RID: 11700
	public const string FemaleShapeBodyAnimeAsset = "cf_anmShapeBody";

	// Token: 0x04002DB5 RID: 11701
	public const string FemaleShapeHandAnimeAsset = "cf_anmShapeHand";

	// Token: 0x04002DB6 RID: 11702
	public const string MaleSilhouetteAsset = "p_cm_body_silhouette";

	// Token: 0x04002DB7 RID: 11703
	public const string FemaleSilhouetteAsset = "p_cf_body_silhouette";

	// Token: 0x04002DB8 RID: 11704
	public const string MalePresetAssetBundle = "custom/00/presets_m_00.unity3d";

	// Token: 0x04002DB9 RID: 11705
	public const string FemalePresetAssetBundle = "custom/00/presets_f_00.unity3d";

	// Token: 0x04002DBA RID: 11706
	public const string MalePresetAsset = "cm_mannequin";

	// Token: 0x04002DBB RID: 11707
	public const string FemalePresetAsset = "cf_mannequin";

	// Token: 0x04002DBC RID: 11708
	public const string MaleCustomAnimAssetBundle = "custom/00/anim_m_00.unity3d";

	// Token: 0x04002DBD RID: 11709
	public const string FemaleCustomAnimAssetBundle = "custom/00/anim_f_00.unity3d";

	// Token: 0x04002DBE RID: 11710
	public const string MaleCustomAnimAsset = "edit_M";

	// Token: 0x04002DBF RID: 11711
	public const string FemaleCustomAnimAsset = "edit_F";

	// Token: 0x04002DC0 RID: 11712
	public const string HairShaderMatAssetBundle = "chara/hair_shader_mat.unity3d";

	// Token: 0x04002DC1 RID: 11713
	public const string HairShaderMatDitheringAsset = "hair_dithering";

	// Token: 0x04002DC2 RID: 11714
	public const string HairShaderMatCutoutAsset = "hair_cutout";

	// Token: 0x04002DC3 RID: 11715
	public const string ExpressionListAssetBundle = "list/expression.unity3d";

	// Token: 0x04002DC4 RID: 11716
	public const string MaleExpressionListAsset = "cm_expression";

	// Token: 0x04002DC5 RID: 11717
	public const string FemaleExpressionListAsset = "cf_expression";
}

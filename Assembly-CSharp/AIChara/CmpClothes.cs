using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007CF RID: 1999
	[DisallowMultipleComponent]
	public class CmpClothes : CmpBase
	{
		// Token: 0x06003171 RID: 12657 RVA: 0x0012587C File Offset: 0x00123C7C
		public CmpClothes() : base(true)
		{
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x0012595C File Offset: 0x00123D5C
		public void SetDefault()
		{
			Material material = null;
			if (this.rendNormal01 != null && this.rendNormal01.Length != 0)
			{
				material = this.rendNormal01[0].sharedMaterial;
			}
			if (null != material)
			{
				if (this.useColorN01 || this.useColorA01)
				{
					if (material.HasProperty("_Color"))
					{
						this.defMainColor01 = material.GetColor("_Color");
					}
					if (material.HasProperty("_Color1_2"))
					{
						this.defPatternColor01 = material.GetColor("_Color1_2");
					}
					if (material.HasProperty("_Glossiness"))
					{
						this.defGloss01 = material.GetFloat("_Glossiness");
					}
					if (material.HasProperty("_Metallic"))
					{
						this.defMetallic01 = material.GetFloat("_Metallic");
					}
					if (material.HasProperty("_patternuv1"))
					{
						this.defLayout01 = material.GetVector("_patternuv1");
					}
					if (material.HasProperty("_patternuv1Rotator"))
					{
						this.defRotation01 = material.GetFloat("_patternuv1Rotator");
					}
				}
				if (this.useColorN02 || this.useColorA02)
				{
					if (material.HasProperty("_Color2"))
					{
						this.defMainColor02 = material.GetColor("_Color2");
					}
					if (material.HasProperty("_Color2_2"))
					{
						this.defPatternColor01 = material.GetColor("_Color2_2");
					}
					if (material.HasProperty("_Glossiness2"))
					{
						this.defGloss02 = material.GetFloat("_Glossiness2");
					}
					if (material.HasProperty("_Metallic2"))
					{
						this.defMetallic02 = material.GetFloat("_Metallic2");
					}
					if (material.HasProperty("_patternuv2"))
					{
						this.defLayout02 = material.GetVector("_patternuv2");
					}
					if (material.HasProperty("_patternuv2Rotator"))
					{
						this.defRotation02 = material.GetFloat("_patternuv2Rotator");
					}
				}
				if (this.useColorN03 || this.useColorA03)
				{
					if (material.HasProperty("_Color3"))
					{
						this.defMainColor03 = material.GetColor("_Color3");
					}
					if (material.HasProperty("_Color3_2"))
					{
						this.defPatternColor01 = material.GetColor("_Color3_2");
					}
					if (material.HasProperty("_Glossiness3"))
					{
						this.defGloss03 = material.GetFloat("_Glossiness3");
					}
					if (material.HasProperty("_Metallic3"))
					{
						this.defMetallic03 = material.GetFloat("_Metallic3");
					}
					if (material.HasProperty("_patternuv3"))
					{
						this.defLayout03 = material.GetVector("_patternuv3");
					}
					if (material.HasProperty("_patternuv3Rotator"))
					{
						this.defRotation03 = material.GetFloat("_patternuv3Rotator");
					}
				}
				if (material.HasProperty("_UVScalePattern"))
				{
					this.uvScalePattern = material.GetVector("_UVScalePattern");
				}
				if (material.HasProperty("_Color4"))
				{
					this.defMainColor04 = material.GetColor("_Color4");
				}
				if (material.HasProperty("_Glossiness4"))
				{
					this.defGloss04 = material.GetFloat("_Glossiness4");
				}
				if (material.HasProperty("_Metallic4"))
				{
					this.defMetallic04 = material.GetFloat("_Metallic4");
				}
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00125CB8 File Offset: 0x001240B8
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.objTopDef = findAssist.GetObjectFromName("n_top_a");
			this.objTopHalf = findAssist.GetObjectFromName("n_top_b");
			this.objBotDef = findAssist.GetObjectFromName("n_bot_a");
			this.objBotHalf = findAssist.GetObjectFromName("n_bot_b");
			this.objOpt01 = (from x in findAssist.dictObjName
			where x.Key.StartsWith("op1")
			select x.Value).ToArray<GameObject>();
			this.objOpt02 = (from x in findAssist.dictObjName
			where x.Key.StartsWith("op2")
			select x.Value).ToArray<GameObject>();
		}

		// Token: 0x04002F80 RID: 12160
		[Header("破れフラグ")]
		public bool useBreak;

		// Token: 0x04002F81 RID: 12161
		[Header("通常パーツ")]
		public Renderer[] rendNormal01;

		// Token: 0x04002F82 RID: 12162
		public Renderer[] rendNormal02;

		// Token: 0x04002F83 RID: 12163
		public Renderer[] rendNormal03;

		// Token: 0x04002F84 RID: 12164
		public bool useColorN01;

		// Token: 0x04002F85 RID: 12165
		public bool useColorN02;

		// Token: 0x04002F86 RID: 12166
		public bool useColorN03;

		// Token: 0x04002F87 RID: 12167
		public bool useColorA01;

		// Token: 0x04002F88 RID: 12168
		public bool useColorA02;

		// Token: 0x04002F89 RID: 12169
		public bool useColorA03;

		// Token: 0x04002F8A RID: 12170
		[Header("着衣・半脱のまとめ")]
		public GameObject objTopDef;

		// Token: 0x04002F8B RID: 12171
		public GameObject objTopHalf;

		// Token: 0x04002F8C RID: 12172
		public GameObject objBotDef;

		// Token: 0x04002F8D RID: 12173
		public GameObject objBotHalf;

		// Token: 0x04002F8E RID: 12174
		[Header("オプションパーツ")]
		public GameObject[] objOpt01;

		// Token: 0x04002F8F RID: 12175
		public GameObject[] objOpt02;

		// Token: 0x04002F90 RID: 12176
		[Header("柄サイズ調整(固定)")]
		public Vector4 uvScalePattern = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04002F91 RID: 12177
		[Header("基本初期設定")]
		public Color defMainColor01 = Color.white;

		// Token: 0x04002F92 RID: 12178
		public Color defMainColor02 = Color.white;

		// Token: 0x04002F93 RID: 12179
		public Color defMainColor03 = Color.white;

		// Token: 0x04002F94 RID: 12180
		public int defPtnIndex01;

		// Token: 0x04002F95 RID: 12181
		public int defPtnIndex02;

		// Token: 0x04002F96 RID: 12182
		public int defPtnIndex03;

		// Token: 0x04002F97 RID: 12183
		public Color defPatternColor01 = Color.white;

		// Token: 0x04002F98 RID: 12184
		public Color defPatternColor02 = Color.white;

		// Token: 0x04002F99 RID: 12185
		public Color defPatternColor03 = Color.white;

		// Token: 0x04002F9A RID: 12186
		[Range(0f, 1f)]
		public float defGloss01;

		// Token: 0x04002F9B RID: 12187
		[Range(0f, 1f)]
		public float defGloss02;

		// Token: 0x04002F9C RID: 12188
		[Range(0f, 1f)]
		public float defGloss03;

		// Token: 0x04002F9D RID: 12189
		[Range(0f, 1f)]
		public float defMetallic01;

		// Token: 0x04002F9E RID: 12190
		[Range(0f, 1f)]
		public float defMetallic02;

		// Token: 0x04002F9F RID: 12191
		[Range(0f, 1f)]
		public float defMetallic03;

		// Token: 0x04002FA0 RID: 12192
		public Vector4 defLayout01 = new Vector4(10f, 10f, 0f, 0f);

		// Token: 0x04002FA1 RID: 12193
		public Vector4 defLayout02 = new Vector4(10f, 10f, 0f, 0f);

		// Token: 0x04002FA2 RID: 12194
		public Vector4 defLayout03 = new Vector4(10f, 10f, 0f, 0f);

		// Token: 0x04002FA3 RID: 12195
		[Range(-1f, 1f)]
		public float defRotation01;

		// Token: 0x04002FA4 RID: 12196
		[Range(-1f, 1f)]
		public float defRotation02;

		// Token: 0x04002FA5 RID: 12197
		[Range(-1f, 1f)]
		public float defRotation03;

		// Token: 0x04002FA6 RID: 12198
		[Space]
		[Header("４色目(固定)")]
		public Color defMainColor04 = Color.white;

		// Token: 0x04002FA7 RID: 12199
		[Range(0f, 1f)]
		public float defGloss04;

		// Token: 0x04002FA8 RID: 12200
		[Range(0f, 1f)]
		public float defMetallic04;

		// Token: 0x04002FA9 RID: 12201
		[Space]
		[Button("SetDefault", "初期色を設定", new object[]
		{

		})]
		public int setdefault;
	}
}

using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C4 RID: 1988
	[DisallowMultipleComponent]
	public class CmpAccessory : CmpBase
	{
		// Token: 0x06003154 RID: 12628 RVA: 0x00124B24 File Offset: 0x00122F24
		public CmpAccessory() : base(true)
		{
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00124BF4 File Offset: 0x00122FF4
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.trfMove01 = findAssist.GetTransformFromName("N_move");
			this.trfMove02 = findAssist.GetTransformFromName("N_move2");
			this.SetColor();
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00124C3C File Offset: 0x0012303C
		public void SetColor()
		{
			if (this.rendNormal != null && this.rendNormal.Length != 0)
			{
				Material sharedMaterial = this.rendNormal[0].sharedMaterial;
				if (null != sharedMaterial)
				{
					if (sharedMaterial.HasProperty("_Color"))
					{
						this.defColor01 = sharedMaterial.GetColor("_Color");
					}
					if (sharedMaterial.HasProperty("_Glossiness"))
					{
						this.defGlossPower01 = sharedMaterial.GetFloat("_Glossiness");
					}
					if (sharedMaterial.HasProperty("_Metallic"))
					{
						this.defMetallicPower01 = sharedMaterial.GetFloat("_Metallic");
					}
					if (sharedMaterial.HasProperty("_Color2"))
					{
						this.defColor02 = sharedMaterial.GetColor("_Color2");
					}
					if (sharedMaterial.HasProperty("_Glossiness2"))
					{
						this.defGlossPower02 = sharedMaterial.GetFloat("_Glossiness2");
					}
					if (sharedMaterial.HasProperty("_Metallic2"))
					{
						this.defMetallicPower02 = sharedMaterial.GetFloat("_Metallic2");
					}
					if (sharedMaterial.HasProperty("_Color3"))
					{
						this.defColor03 = sharedMaterial.GetColor("_Color3");
					}
					if (sharedMaterial.HasProperty("_Glossiness3"))
					{
						this.defGlossPower03 = sharedMaterial.GetFloat("_Glossiness3");
					}
					if (sharedMaterial.HasProperty("_Metallic3"))
					{
						this.defMetallicPower03 = sharedMaterial.GetFloat("_Metallic3");
					}
				}
			}
			if (this.rendAlpha != null && this.rendAlpha.Length != 0)
			{
				Material sharedMaterial2 = this.rendAlpha[0].sharedMaterial;
				if (null != sharedMaterial2)
				{
					if (sharedMaterial2.HasProperty("_Color"))
					{
						this.defColor04 = sharedMaterial2.GetColor("_Color");
					}
					if (sharedMaterial2.HasProperty("_Glossiness4"))
					{
						this.defGlossPower04 = sharedMaterial2.GetFloat("_Glossiness4");
					}
					if (sharedMaterial2.HasProperty("_Metallic4"))
					{
						this.defMetallicPower04 = sharedMaterial2.GetFloat("_Metallic4");
					}
				}
			}
		}

		// Token: 0x04002F0F RID: 12047
		[Header("< 髪タイプ >-------------------------")]
		public bool typeHair;

		// Token: 0x04002F10 RID: 12048
		[Header("< 通常パーツ >-----------------------")]
		public Renderer[] rendNormal;

		// Token: 0x04002F11 RID: 12049
		[Header("01 or BaseColor")]
		public bool useColor01;

		// Token: 0x04002F12 RID: 12050
		public bool useGloss01 = true;

		// Token: 0x04002F13 RID: 12051
		public bool useMetallic01 = true;

		// Token: 0x04002F14 RID: 12052
		public Color defColor01 = Color.white;

		// Token: 0x04002F15 RID: 12053
		public float defGlossPower01 = 0.5f;

		// Token: 0x04002F16 RID: 12054
		public float defMetallicPower01 = 0.5f;

		// Token: 0x04002F17 RID: 12055
		[Header("02 or TopColor")]
		public bool useColor02;

		// Token: 0x04002F18 RID: 12056
		public bool useGloss02 = true;

		// Token: 0x04002F19 RID: 12057
		public bool useMetallic02 = true;

		// Token: 0x04002F1A RID: 12058
		public Color defColor02 = Color.white;

		// Token: 0x04002F1B RID: 12059
		public float defGlossPower02 = 0.5f;

		// Token: 0x04002F1C RID: 12060
		public float defMetallicPower02 = 0.5f;

		// Token: 0x04002F1D RID: 12061
		[Header("03 or UnderColor")]
		public bool useColor03;

		// Token: 0x04002F1E RID: 12062
		public bool useGloss03 = true;

		// Token: 0x04002F1F RID: 12063
		public bool useMetallic03 = true;

		// Token: 0x04002F20 RID: 12064
		public Color defColor03 = Color.white;

		// Token: 0x04002F21 RID: 12065
		public float defGlossPower03 = 0.5f;

		// Token: 0x04002F22 RID: 12066
		public float defMetallicPower03 = 0.5f;

		// Token: 0x04002F23 RID: 12067
		[Header("< 半透明パーツ >---------------------")]
		public Renderer[] rendAlpha;

		// Token: 0x04002F24 RID: 12068
		public bool useGloss04 = true;

		// Token: 0x04002F25 RID: 12069
		public bool useMetallic04 = true;

		// Token: 0x04002F26 RID: 12070
		public Color defColor04 = Color.white;

		// Token: 0x04002F27 RID: 12071
		public float defGlossPower04 = 0.5f;

		// Token: 0x04002F28 RID: 12072
		public float defMetallicPower04 = 0.5f;

		// Token: 0x04002F29 RID: 12073
		[Header("< 調整NULL >-------------------------")]
		public Transform trfMove01;

		// Token: 0x04002F2A RID: 12074
		public Transform trfMove02;

		// Token: 0x04002F2B RID: 12075
		[Space]
		[Button("SetColor", "初期色を設定", new object[]
		{

		})]
		public int setcolor;
	}
}

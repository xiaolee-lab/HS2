using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069C RID: 1692
	[Serializable]
	public class CTSTerrainTextureDetails
	{
		// Token: 0x06002808 RID: 10248 RVA: 0x000EDEE4 File Offset: 0x000EC2E4
		public CTSTerrainTextureDetails()
		{
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000EDFDC File Offset: 0x000EC3DC
		public CTSTerrainTextureDetails(CTSTerrainTextureDetails src)
		{
			this.m_isOpenInEditor = src.m_isOpenInEditor;
			this.m_textureIdx = src.m_textureIdx;
			this.m_name = src.m_name;
			this.m_detailPower = src.m_detailPower;
			this.m_snowReductionPower = src.m_snowReductionPower;
			this.m_geologicalPower = src.m_geologicalPower;
			this.m_triplanar = src.m_triplanar;
			this.m_tint = src.m_tint;
			this.m_tintBrightness = src.m_tintBrightness;
			this.m_smoothness = src.m_smoothness;
			this.m_albedoIdx = src.m_albedoIdx;
			this.m_albedoTilingClose = src.m_albedoTilingClose;
			this.m_albedoTilingFar = src.m_albedoTilingFar;
			this.m_albedoWasChanged = src.m_albedoWasChanged;
			this.m_albedoTexture = src.m_albedoTexture;
			this.m_normalIdx = src.m_normalIdx;
			this.m_normalStrength = src.m_normalStrength;
			this.m_normalWasChanged = src.m_normalWasChanged;
			this.m_normalTexture = src.m_normalTexture;
			this.m_heightIdx = src.m_heightIdx;
			this.m_heightDepth = src.m_heightDepth;
			this.m_heightTesselationDepth = src.m_heightTesselationDepth;
			this.m_heightContrast = src.m_heightContrast;
			this.m_heightBlendClose = src.m_heightBlendClose;
			this.m_heightBlendFar = src.m_heightBlendFar;
			this.m_heightWasChanged = src.m_heightWasChanged;
			this.m_heightTexture = src.m_heightTexture;
			this.m_aoIdx = src.m_aoIdx;
			this.m_aoPower = src.m_aoPower;
			this.m_aoWasChanged = src.m_aoWasChanged;
			this.m_aoTexture = src.m_aoTexture;
			this.m_emissionIdx = src.m_emissionIdx;
			this.m_emissionStrength = src.m_emissionStrength;
			this.m_emissionWasChanged = src.m_emissionWasChanged;
			this.m_emissionTexture = src.m_emissionTexture;
			this.m_smoothness = src.m_smoothness;
			this.m_roughnessTexture = src.m_roughnessTexture;
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x000EE28D File Offset: 0x000EC68D
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x000EE298 File Offset: 0x000EC698
		public Texture2D Albedo
		{
			get
			{
				return this.m_albedoTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_albedoTexture, value))
				{
					this.m_albedoTexture = value;
					this.m_albedoWasChanged = true;
					if (this.m_albedoTexture != null)
					{
						this.m_name = this.m_albedoTexture.name;
					}
					else
					{
						this.m_name = "Missing Albedo";
					}
				}
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x000EE2F6 File Offset: 0x000EC6F6
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x000EE2FE File Offset: 0x000EC6FE
		public Texture2D Smoothness
		{
			get
			{
				return this.m_smoothnessTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_smoothnessTexture, value))
				{
					this.m_smoothnessTexture = value;
					this.m_smoothnessWasChanged = true;
				}
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000EE31F File Offset: 0x000EC71F
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x000EE327 File Offset: 0x000EC727
		public Texture2D Roughness
		{
			get
			{
				return this.m_roughnessTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_roughnessTexture, value))
				{
					this.m_roughnessTexture = value;
					this.m_roughnessWasChanged = true;
				}
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000EE348 File Offset: 0x000EC748
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x000EE350 File Offset: 0x000EC750
		public Texture2D Normal
		{
			get
			{
				return this.m_normalTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_normalTexture, value))
				{
					this.m_normalTexture = value;
					this.m_normalWasChanged = true;
				}
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000EE371 File Offset: 0x000EC771
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x000EE379 File Offset: 0x000EC779
		public Texture2D Height
		{
			get
			{
				return this.m_heightTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_heightTexture, value))
				{
					this.m_heightTexture = value;
					this.m_heightWasChanged = true;
				}
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000EE39A File Offset: 0x000EC79A
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x000EE3A2 File Offset: 0x000EC7A2
		public Texture2D AmbientOcclusion
		{
			get
			{
				return this.m_aoTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_aoTexture, value))
				{
					this.m_aoTexture = value;
					this.m_aoWasChanged = true;
				}
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000EE3C3 File Offset: 0x000EC7C3
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x000EE3CB File Offset: 0x000EC7CB
		public Texture2D Emission
		{
			get
			{
				return this.m_emissionTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_emissionTexture, value))
				{
					this.m_emissionTexture = value;
					this.m_emissionWasChanged = true;
				}
			}
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000EE3EC File Offset: 0x000EC7EC
		public void ResetChangedFlags()
		{
			this.m_albedoWasChanged = false;
			this.m_normalWasChanged = false;
			this.m_heightWasChanged = false;
			this.m_aoWasChanged = false;
			this.m_emissionWasChanged = false;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000EE414 File Offset: 0x000EC814
		public bool TextureHasChanged()
		{
			return this.m_albedoWasChanged || this.m_normalWasChanged || this.m_heightWasChanged || this.m_aoWasChanged || this.m_emissionWasChanged;
		}

		// Token: 0x040028FF RID: 10495
		public bool m_isOpenInEditor;

		// Token: 0x04002900 RID: 10496
		public int m_textureIdx;

		// Token: 0x04002901 RID: 10497
		public string m_name = "Texture";

		// Token: 0x04002902 RID: 10498
		public float m_detailPower = 1f;

		// Token: 0x04002903 RID: 10499
		public float m_snowReductionPower;

		// Token: 0x04002904 RID: 10500
		public float m_geologicalPower = 1f;

		// Token: 0x04002905 RID: 10501
		public bool m_triplanar;

		// Token: 0x04002906 RID: 10502
		public Color m_tint = new Color(1f, 1f, 1f);

		// Token: 0x04002907 RID: 10503
		public float m_tintBrightness = 1f;

		// Token: 0x04002908 RID: 10504
		public float m_smoothness = 1f;

		// Token: 0x04002909 RID: 10505
		public int m_albedoIdx = -1;

		// Token: 0x0400290A RID: 10506
		public float m_albedoTilingClose = 15f;

		// Token: 0x0400290B RID: 10507
		public float m_albedoTilingFar = 3f;

		// Token: 0x0400290C RID: 10508
		[NonSerialized]
		public bool m_albedoWasChanged;

		// Token: 0x0400290D RID: 10509
		public Vector4 m_albedoAverage;

		// Token: 0x0400290E RID: 10510
		[SerializeField]
		private Texture2D m_albedoTexture;

		// Token: 0x0400290F RID: 10511
		[NonSerialized]
		public bool m_smoothnessWasChanged;

		// Token: 0x04002910 RID: 10512
		[SerializeField]
		private Texture2D m_smoothnessTexture;

		// Token: 0x04002911 RID: 10513
		[NonSerialized]
		public bool m_roughnessWasChanged;

		// Token: 0x04002912 RID: 10514
		[SerializeField]
		private Texture2D m_roughnessTexture;

		// Token: 0x04002913 RID: 10515
		public int m_normalIdx = -1;

		// Token: 0x04002914 RID: 10516
		public float m_normalStrength = 1f;

		// Token: 0x04002915 RID: 10517
		[NonSerialized]
		public bool m_normalWasChanged;

		// Token: 0x04002916 RID: 10518
		[SerializeField]
		private Texture2D m_normalTexture;

		// Token: 0x04002917 RID: 10519
		public int m_heightIdx = -1;

		// Token: 0x04002918 RID: 10520
		public float m_heightDepth = 8f;

		// Token: 0x04002919 RID: 10521
		public float m_heightContrast = 1f;

		// Token: 0x0400291A RID: 10522
		public float m_heightBlendClose = 1f;

		// Token: 0x0400291B RID: 10523
		public float m_heightBlendFar = 1f;

		// Token: 0x0400291C RID: 10524
		public float m_heightTesselationDepth;

		// Token: 0x0400291D RID: 10525
		public float m_heightMin;

		// Token: 0x0400291E RID: 10526
		public float m_heightMax = 1f;

		// Token: 0x0400291F RID: 10527
		[NonSerialized]
		public bool m_heightWasChanged;

		// Token: 0x04002920 RID: 10528
		[SerializeField]
		private Texture2D m_heightTexture;

		// Token: 0x04002921 RID: 10529
		public int m_aoIdx = -1;

		// Token: 0x04002922 RID: 10530
		public float m_aoPower = 1f;

		// Token: 0x04002923 RID: 10531
		[NonSerialized]
		public bool m_aoWasChanged;

		// Token: 0x04002924 RID: 10532
		[SerializeField]
		private Texture2D m_aoTexture;

		// Token: 0x04002925 RID: 10533
		public int m_emissionIdx = -1;

		// Token: 0x04002926 RID: 10534
		public float m_emissionStrength = 1f;

		// Token: 0x04002927 RID: 10535
		[NonSerialized]
		public bool m_emissionWasChanged;

		// Token: 0x04002928 RID: 10536
		[SerializeField]
		private Texture2D m_emissionTexture;
	}
}

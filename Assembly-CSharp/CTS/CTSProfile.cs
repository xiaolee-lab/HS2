using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000695 RID: 1685
	[Serializable]
	public class CTSProfile : ScriptableObject
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000EB613 File Offset: 0x000E9A13
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000EB61B File Offset: 0x000E9A1B
		public int MajorVersion
		{
			get
			{
				return this.m_ctsMajorVersion;
			}
			set
			{
				if (!this.m_ctsMajorVersion.Equals(value))
				{
					this.m_ctsMajorVersion = value;
					this.m_needsAlbedosArrayUpdate = true;
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000EB643 File Offset: 0x000E9A43
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x000EB64B File Offset: 0x000E9A4B
		public int MinorVersion
		{
			get
			{
				return this.m_ctsMinorVersion;
			}
			set
			{
				if (!this.m_ctsMinorVersion.Equals(value))
				{
					this.m_ctsMinorVersion = value;
					this.m_needsAlbedosArrayUpdate = true;
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000EB673 File Offset: 0x000E9A73
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x000EB67B File Offset: 0x000E9A7B
		public CTSConstants.ShaderType ShaderType
		{
			get
			{
				return this.m_shaderType;
			}
			set
			{
				if (this.m_shaderType != value)
				{
					this.m_shaderType = value;
				}
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000EB690 File Offset: 0x000E9A90
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x000EB698 File Offset: 0x000E9A98
		public CTSConstants.TextureSize AlbedoTextureSize
		{
			get
			{
				return this.m_albedoTextureSize;
			}
			set
			{
				if (this.m_albedoTextureSize != value)
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_albedoTextureSize = value;
					this.m_albedoTextureSizePx = CTSConstants.GetTextureSize(this.m_albedoTextureSize);
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000EB6CD File Offset: 0x000E9ACD
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x000EB6D5 File Offset: 0x000E9AD5
		public bool AlbedoCompressionEnabled
		{
			get
			{
				return this.m_albedoCompress;
			}
			set
			{
				if (this.m_albedoCompress != value)
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_albedoCompress = value;
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000EB6F9 File Offset: 0x000E9AF9
		// (set) Token: 0x060027BC RID: 10172 RVA: 0x000EB701 File Offset: 0x000E9B01
		public CTSConstants.TextureSize NormalTextureSize
		{
			get
			{
				return this.m_normalTextureSize;
			}
			set
			{
				if (this.m_normalTextureSize != value)
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_normalTextureSize = value;
					this.m_normalTextureSizePx = CTSConstants.GetTextureSize(this.m_normalTextureSize);
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000EB736 File Offset: 0x000E9B36
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x000EB73E File Offset: 0x000E9B3E
		public bool NormalCompressionEnabled
		{
			get
			{
				return this.m_normalCompress;
			}
			set
			{
				if (this.m_normalCompress != value)
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_normalCompress = value;
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x000EB762 File Offset: 0x000E9B62
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x000EB76A File Offset: 0x000E9B6A
		public Texture2D GlobalDetailNormalMap
		{
			get
			{
				return this.m_globalDetailNormalMap;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_globalDetailNormalMap, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_globalDetailNormalMap = value;
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000EB793 File Offset: 0x000E9B93
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x000EB79B File Offset: 0x000E9B9B
		public Texture2D SnowAlbedo
		{
			get
			{
				return this.m_snowAlbedoTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowAlbedoTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowAlbedoTexture = value;
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000EB7C4 File Offset: 0x000E9BC4
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x000EB7CC File Offset: 0x000E9BCC
		public Texture2D SnowNormal
		{
			get
			{
				return this.m_snowNormalTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowNormalTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowNormalTexture = value;
					this.m_needsNormalsArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000EB7F5 File Offset: 0x000E9BF5
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x000EB7FD File Offset: 0x000E9BFD
		public Texture2D SnowHeight
		{
			get
			{
				return this.m_snowHeightTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowHeightTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowHeightTexture = value;
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000EB826 File Offset: 0x000E9C26
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x000EB82E File Offset: 0x000E9C2E
		public Texture2D SnowAmbientOcclusion
		{
			get
			{
				return this.m_snowAOTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowAOTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowAOTexture = value;
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000EB857 File Offset: 0x000E9C57
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x000EB85F File Offset: 0x000E9C5F
		public Texture2D SnowEmission
		{
			get
			{
				return this.m_snowEmissionTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowEmissionTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowEmissionTexture = value;
					this.m_needsAlbedosArrayUpdate = true;
				}
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000EB888 File Offset: 0x000E9C88
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x000EB890 File Offset: 0x000E9C90
		public Texture2D SnowGlitter
		{
			get
			{
				return this.m_snowGlitterTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_snowGlitterTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_snowGlitterTexture = value;
				}
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000EB8B2 File Offset: 0x000E9CB2
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000EB8BA File Offset: 0x000E9CBA
		public Texture2D GeoAlbedo
		{
			get
			{
				return this.m_geoAlbedoTexture;
			}
			set
			{
				if (CTSProfile.IsDifferentTexture(this.m_geoAlbedoTexture, value))
				{
					CompleteTerrainShader.SetDirty(this, false, true);
					this.m_geoAlbedoTexture = value;
				}
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000EB8DC File Offset: 0x000E9CDC
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000EB8E4 File Offset: 0x000E9CE4
		public List<CTSTerrainTextureDetails> TerrainTextures
		{
			get
			{
				return this.m_terrainTextures;
			}
			set
			{
				this.m_terrainTextures = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000EB8ED File Offset: 0x000E9CED
		public List<Texture2D> ReplacementTerrainAlbedos
		{
			get
			{
				return this.m_replacementTerrainAlbedos;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000EB8F5 File Offset: 0x000E9CF5
		public List<Texture2D> ReplacementTerrainNormals
		{
			get
			{
				return this.m_replacementTerrainNormals;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000EB8FD File Offset: 0x000E9CFD
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x000EB905 File Offset: 0x000E9D05
		public Texture2DArray AlbedosTextureArray
		{
			get
			{
				return this.m_albedosTextureArray;
			}
			set
			{
				CompleteTerrainShader.SetDirty(this, false, false);
				this.m_albedosTextureArray = value;
				this.m_needsAlbedosArrayUpdate = false;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000EB91D File Offset: 0x000E9D1D
		// (set) Token: 0x060027D6 RID: 10198 RVA: 0x000EB925 File Offset: 0x000E9D25
		public Texture2DArray NormalsTextureArray
		{
			get
			{
				return this.m_normalsTextureArray;
			}
			set
			{
				CompleteTerrainShader.SetDirty(this, false, false);
				this.m_normalsTextureArray = value;
				this.m_needsNormalsArrayUpdate = false;
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000EB940 File Offset: 0x000E9D40
		public bool NeedsArrayUpdate()
		{
			if (this.m_needsAlbedosArrayUpdate)
			{
				return true;
			}
			if (this.m_needsNormalsArrayUpdate)
			{
				return true;
			}
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
				if (this.m_terrainTextures[i].TextureHasChanged())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000EB99C File Offset: 0x000E9D9C
		public void RegenerateArraysIfNecessary()
		{
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
			}
			if (this.m_needsAlbedosArrayUpdate)
			{
				this.ConstructAlbedosTextureArray();
			}
			if (this.m_needsNormalsArrayUpdate)
			{
				this.ConstructNormalsTextureArray();
			}
			for (int j = 0; j < this.m_terrainTextures.Count; j++)
			{
				this.m_terrainTextures[j].ResetChangedFlags();
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000EBA14 File Offset: 0x000E9E14
		private void ConstructAlbedosTextureArray()
		{
			this.m_needsAlbedosArrayUpdate = false;
			List<Texture2D> list = new List<Texture2D>();
			int num = 0;
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_terrainTextures[i];
				if (ctsterrainTextureDetails.Albedo != null)
				{
					Texture2D texture2D;
					if (ctsterrainTextureDetails.Smoothness == null && ctsterrainTextureDetails.Roughness == null)
					{
						if (this.m_albedoCompress)
						{
							texture2D = CTSProfile.ResizeTexture(ctsterrainTextureDetails.Albedo, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, true, false, true);
						}
						else
						{
							texture2D = CTSProfile.ResizeTexture(ctsterrainTextureDetails.Albedo, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, true, false, false);
						}
					}
					else
					{
						texture2D = this.BakeAlbedo(ctsterrainTextureDetails.Albedo, ctsterrainTextureDetails.Smoothness, ctsterrainTextureDetails.Roughness);
					}
					list.Add(texture2D);
					Color[] pixels = texture2D.GetPixels(texture2D.mipmapCount - 1);
					Color linear = pixels[0].linear;
					ctsterrainTextureDetails.m_albedoAverage = new Vector4(linear.r, linear.g, linear.b, linear.a);
					ctsterrainTextureDetails.m_albedoIdx = num++;
					if ((this.m_shaderType == CTSConstants.ShaderType.Advanced || this.m_shaderType == CTSConstants.ShaderType.Tesselation) && (ctsterrainTextureDetails.Height != null || ctsterrainTextureDetails.AmbientOcclusion != null))
					{
						byte b;
						byte b2;
						list.Add(this.BakeHAOTexture(ctsterrainTextureDetails.m_name, ctsterrainTextureDetails.Height, ctsterrainTextureDetails.AmbientOcclusion, out b, out b2));
						if (ctsterrainTextureDetails.Height != null)
						{
							ctsterrainTextureDetails.m_heightIdx = num;
							ctsterrainTextureDetails.m_heightMin = (float)b / 255f;
							ctsterrainTextureDetails.m_heightMax = (float)b2 / 255f;
						}
						else
						{
							ctsterrainTextureDetails.m_heightIdx = -1;
						}
						if (ctsterrainTextureDetails.AmbientOcclusion != null)
						{
							ctsterrainTextureDetails.m_aoIdx = num;
						}
						else
						{
							ctsterrainTextureDetails.m_aoIdx = -1;
						}
						num++;
					}
					else
					{
						ctsterrainTextureDetails.m_aoIdx = -1;
						ctsterrainTextureDetails.m_heightIdx = -1;
					}
				}
				else
				{
					ctsterrainTextureDetails.m_albedoIdx = -1;
				}
				ctsterrainTextureDetails.m_albedoWasChanged = false;
			}
			if (this.m_snowAlbedoTexture != null)
			{
				Texture2D texture2D2;
				if (this.m_albedoCompress)
				{
					texture2D2 = CTSProfile.ResizeTexture(this.m_snowAlbedoTexture, this.m_albedoFormat, this.m_normalAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, true, false, true);
				}
				else
				{
					texture2D2 = CTSProfile.ResizeTexture(this.m_snowAlbedoTexture, this.m_albedoFormat, this.m_normalAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, true, false, false);
				}
				Color[] pixels2 = texture2D2.GetPixels(texture2D2.mipmapCount - 1);
				Color linear2 = pixels2[0].linear;
				this.m_snowAverage = new Vector4(linear2.r, linear2.g, linear2.b, linear2.a);
				list.Add(texture2D2);
				this.m_snowAlbedoTextureIdx = num++;
			}
			else
			{
				this.m_snowAlbedoTextureIdx = -1;
			}
			if (this.m_snowHeightTexture != null || this.m_snowAOTexture != null)
			{
				byte b;
				byte b2;
				list.Add(this.BakeHAOTexture("CTS_SnowHAO", this.m_snowHeightTexture, this.m_snowAOTexture, out b, out b2));
				if (this.m_snowHeightTexture != null)
				{
					this.m_snowHeightTextureIdx = num;
					this.m_snowHeightmapMinValue = (float)b / 255f;
					this.m_snowHeightmapMaxValue = (float)b2 / 255f;
				}
				else
				{
					this.m_snowHeightTextureIdx = -1;
					this.m_snowHeightmapMinValue = 0f;
					this.m_snowHeightmapMaxValue = 1f;
				}
				if (this.m_snowAOTexture != null)
				{
					this.m_snowAOTextureIdx = num;
				}
				else
				{
					this.m_snowAOTextureIdx = -1;
				}
				num++;
			}
			else
			{
				this.m_snowAOTextureIdx = -1;
				this.m_snowHeightTextureIdx = -1;
			}
			Texture2DArray textureArray = this.GetTextureArray(list, CTSConstants.TextureType.Albedo, this.m_albedoAniso);
			this.AlbedosTextureArray = textureArray;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000EBE20 File Offset: 0x000EA220
		private void ConstructNormalsTextureArray()
		{
			this.m_needsNormalsArrayUpdate = false;
			List<Texture2D> list = new List<Texture2D>();
			int num = 0;
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_terrainTextures[i];
				if (ctsterrainTextureDetails.Normal != null)
				{
					list.Add(this.BakeNormal(ctsterrainTextureDetails.Normal));
					ctsterrainTextureDetails.m_normalIdx = num++;
				}
				else
				{
					ctsterrainTextureDetails.m_normalIdx = -1;
				}
				ctsterrainTextureDetails.m_normalWasChanged = false;
			}
			if (this.m_snowNormalTexture != null)
			{
				list.Add(this.BakeNormal(this.m_snowNormalTexture));
				this.m_snowNormalTextureIdx = num++;
			}
			else
			{
				this.m_snowNormalTextureIdx = -1;
			}
			if (this.m_globalDetailNormalMap)
			{
				list.Add(this.BakeNormal(this.m_globalDetailNormalMap));
				this.m_globalDetailNormalMapIdx = num++;
			}
			else
			{
				this.m_globalDetailNormalMapIdx = -1;
			}
			Texture2DArray textureArray = this.GetTextureArray(list, CTSConstants.TextureType.Normal, this.m_normalAniso);
			this.NormalsTextureArray = textureArray;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000EBF34 File Offset: 0x000EA334
		public void UpdateSettingsFromTerrain(Terrain terrain, bool forceUpdate)
		{
			if (terrain == null || terrain.terrainData == null)
			{
				return;
			}
			if (forceUpdate)
			{
				this.m_needsAlbedosArrayUpdate = true;
				this.m_needsNormalsArrayUpdate = true;
			}
			while (this.m_terrainTextures.Count > terrain.terrainData.splatPrototypes.Length)
			{
				this.m_terrainTextures.RemoveAt(this.m_terrainTextures.Count - 1);
				this.m_needsAlbedosArrayUpdate = true;
				this.m_needsNormalsArrayUpdate = true;
			}
			SplatPrototype[] splatPrototypes = terrain.terrainData.splatPrototypes;
			for (int i = 0; i < splatPrototypes.Length; i++)
			{
				SplatPrototype splatPrototype = splatPrototypes[i];
				if (i < this.m_terrainTextures.Count)
				{
					CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_terrainTextures[i];
					ctsterrainTextureDetails.Albedo = splatPrototype.texture;
					ctsterrainTextureDetails.m_albedoTilingClose = terrain.terrainData.splatPrototypes[i].tileSize.x;
					ctsterrainTextureDetails.Normal = splatPrototype.normalMap;
				}
				else
				{
					CTSTerrainTextureDetails ctsterrainTextureDetails = new CTSTerrainTextureDetails();
					ctsterrainTextureDetails.m_textureIdx = i;
					ctsterrainTextureDetails.Albedo = terrain.terrainData.splatPrototypes[i].texture;
					ctsterrainTextureDetails.m_albedoTilingClose = terrain.terrainData.splatPrototypes[i].tileSize.x;
					ctsterrainTextureDetails.Normal = terrain.terrainData.splatPrototypes[i].normalMap;
					this.m_terrainTextures.Add(ctsterrainTextureDetails);
				}
			}
			this.RegenerateArraysIfNecessary();
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000EC0AD File Offset: 0x000EA4AD
		private void ImportTexture(Texture2D texture, int textureSize, bool asNormal = false)
		{
			if (texture == null)
			{
				return;
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000EC0BC File Offset: 0x000EA4BC
		private Color32[] GetTextureColors(Texture2D source, TextureFormat format, int dimensions)
		{
			Texture2D texture2D = CTSProfile.ResizeTexture(source, format, this.m_albedoAniso, dimensions, dimensions, false, false, false);
			Color32[] pixels = texture2D.GetPixels32();
			if (!Application.isPlaying)
			{
				UnityEngine.Object.DestroyImmediate(texture2D);
			}
			else
			{
				UnityEngine.Object.Destroy(texture2D);
			}
			return pixels;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000EC100 File Offset: 0x000EA500
		public Texture2D BakeHAOTexture(string name, Texture2D hTexture, Texture2D aoTexture, out byte minHeight, out byte maxHeight)
		{
			minHeight = 0;
			maxHeight = byte.MaxValue;
			int num = this.m_albedoTextureSizePx * this.m_albedoTextureSizePx;
			if (num == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, this.m_albedoFormat, true, false);
			texture2D.name = "CTS_" + name + "_HAO";
			texture2D.anisoLevel = this.m_albedoAniso;
			texture2D.filterMode = this.m_albedoFilterMode;
			Color32[] pixels = texture2D.GetPixels32();
			if (hTexture != null)
			{
				minHeight = byte.MaxValue;
				maxHeight = 0;
				Texture2D texture2D2 = CTSProfile.ResizeTexture(hTexture, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, false, false, false);
				Color32[] pixels2 = texture2D2.GetPixels32();
				for (int i = 0; i < num; i++)
				{
					byte g = pixels2[i].g;
					if (g < minHeight)
					{
						minHeight = g;
					}
					if (g > maxHeight)
					{
						maxHeight = g;
					}
					pixels[i].r = (pixels[i].g = (pixels[i].b = g));
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(texture2D2);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D2);
				}
			}
			if (aoTexture != null)
			{
				Texture2D texture2D3 = CTSProfile.ResizeTexture(aoTexture, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, false, false, false);
				Color32[] pixels3 = texture2D3.GetPixels32();
				for (int j = 0; j < num; j++)
				{
					pixels[j].a = pixels3[j].g;
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(texture2D3);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D3);
				}
			}
			else
			{
				for (int k = 0; k < num; k++)
				{
					pixels[k].a = byte.MaxValue;
				}
			}
			texture2D.SetPixels32(pixels);
			texture2D.Apply(true);
			if (this.m_albedoCompress)
			{
				texture2D.Compress(true);
				texture2D.Apply(true);
			}
			return texture2D;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000EC32C File Offset: 0x000EA72C
		private Texture2D BakeNormal(Texture2D normalTexture)
		{
			int num = this.m_normalTextureSizePx * this.m_normalTextureSizePx;
			if (num == 0 || normalTexture == null)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(this.m_normalTextureSizePx, this.m_normalTextureSizePx, this.m_normalFormat, true, true);
			texture2D.name = "CTS_" + base.name + "_Normal";
			texture2D.anisoLevel = this.m_normalAniso;
			texture2D.filterMode = this.m_normalFilterMode;
			Color32[] pixels = texture2D.GetPixels32();
			Texture2D texture2D2 = CTSProfile.ResizeTexture(normalTexture, this.m_normalFormat, this.m_normalAniso, this.m_normalTextureSizePx, this.m_normalTextureSizePx, false, true, false);
			Color32[] pixels2 = texture2D2.GetPixels32();
			for (int i = 0; i < num; i++)
			{
				pixels[i].r = 128;
				pixels[i].g = pixels2[i].g;
				pixels[i].b = 128;
				pixels[i].a = pixels2[i].a;
			}
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(texture2D2);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(texture2D2);
			}
			texture2D.SetPixels32(pixels);
			texture2D.Apply(true);
			if (this.m_normalCompress)
			{
				texture2D.Compress(true);
				texture2D.Apply(true);
			}
			return texture2D;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000EC48C File Offset: 0x000EA88C
		private Texture2D BakeAlbedo(Texture2D albedoTexture, Texture2D smoothnessTexture, Texture2D roughnessTexture)
		{
			int num = this.m_normalTextureSizePx * this.m_normalTextureSizePx;
			if (num == 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, this.m_albedoFormat, true, false);
			texture2D.name = "CTS_" + base.name + "_ASm";
			texture2D.anisoLevel = this.m_albedoAniso;
			texture2D.filterMode = this.m_albedoFilterMode;
			Color32[] pixels = texture2D.GetPixels32();
			if (albedoTexture != null)
			{
				Texture2D texture2D2 = CTSProfile.ResizeTexture(albedoTexture, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, false, false, false);
				Color32[] pixels2 = texture2D2.GetPixels32();
				for (int i = 0; i < num; i++)
				{
					pixels[i].r = pixels2[i].r;
					pixels[i].g = pixels2[i].g;
					pixels[i].b = pixels2[i].b;
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(texture2D2);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D2);
				}
			}
			if (roughnessTexture != null && smoothnessTexture == null)
			{
				Texture2D texture2D3 = CTSProfile.ResizeTexture(roughnessTexture, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, false, false, false);
				Color32[] pixels3 = texture2D3.GetPixels32();
				for (int j = 0; j < num; j++)
				{
					pixels[j].a = byte.MaxValue - pixels3[j].g;
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(texture2D3);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D3);
				}
			}
			if (smoothnessTexture != null)
			{
				Texture2D texture2D4 = CTSProfile.ResizeTexture(smoothnessTexture, this.m_albedoFormat, this.m_albedoAniso, this.m_albedoTextureSizePx, this.m_albedoTextureSizePx, false, false, false);
				Color32[] pixels4 = texture2D4.GetPixels32();
				for (int k = 0; k < num; k++)
				{
					pixels[k].a = pixels4[k].g;
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(texture2D4);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D4);
				}
			}
			texture2D.SetPixels32(pixels);
			texture2D.Apply(true);
			if (this.m_albedoCompress)
			{
				texture2D.Compress(true);
				texture2D.Apply(true);
			}
			return texture2D;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000EC702 File Offset: 0x000EAB02
		private void DebugTextureColorData(string name, Color32 color)
		{
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000EC704 File Offset: 0x000EAB04
		private void SaveTexture(string path, Texture2D texture)
		{
			byte[] bytes = texture.EncodeToPNG();
			File.WriteAllBytes(path + ".png", bytes);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000EC72C File Offset: 0x000EAB2C
		public static Texture2D ResizeTexture(Texture2D texture, TextureFormat format, int aniso, int width, int height, bool mipmap, bool linear, bool compress)
		{
			RenderTexture temporary;
			if (linear)
			{
				temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
			}
			else
			{
				temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
			}
			bool sRGBWrite = GL.sRGBWrite;
			if (linear)
			{
				GL.sRGBWrite = false;
			}
			else
			{
				GL.sRGBWrite = true;
			}
			Graphics.Blit(texture, temporary);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = temporary;
			Texture2D texture2D = new Texture2D(width, height, format, mipmap, linear);
			texture2D.name = texture.name + " X";
			texture2D.anisoLevel = aniso;
			texture2D.filterMode = texture.filterMode;
			texture2D.wrapMode = texture.wrapMode;
			texture2D.mipMapBias = texture.mipMapBias;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)temporary.width, (float)temporary.height), 0, 0);
			texture2D.Apply(true);
			if (compress)
			{
				texture2D.Compress(true);
				texture2D.Apply(true);
			}
			RenderTexture.active = active;
			RenderTexture.ReleaseTemporary(temporary);
			GL.sRGBWrite = sRGBWrite;
			return texture2D;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000EC834 File Offset: 0x000EAC34
		private Texture2DArray GetTextureArray(List<Texture2D> sourceTextures, CTSConstants.TextureType textureType, int aniso)
		{
			if (sourceTextures == null)
			{
				return null;
			}
			if (sourceTextures.Count == 0)
			{
				return null;
			}
			Texture2D texture2D = sourceTextures[0];
			TextureFormat format = texture2D.format;
			int width = texture2D.width;
			int height = texture2D.height;
			for (int i = 1; i < sourceTextures.Count; i++)
			{
				if (sourceTextures[i].width != width || sourceTextures[i].height != height)
				{
					return null;
				}
			}
			Texture2DArray texture2DArray;
			switch (textureType)
			{
			case CTSConstants.TextureType.Albedo:
			case CTSConstants.TextureType.AmbientOcclusion:
			case CTSConstants.TextureType.Height:
				texture2DArray = new Texture2DArray(width, height, sourceTextures.Count, format, true, false);
				break;
			case CTSConstants.TextureType.Normal:
				texture2DArray = new Texture2DArray(width, height, sourceTextures.Count, format, true, true);
				break;
			default:
				throw new ArgumentOutOfRangeException("textureType", textureType, null);
			}
			texture2DArray.filterMode = texture2D.filterMode;
			texture2DArray.wrapMode = texture2D.wrapMode;
			texture2DArray.anisoLevel = aniso;
			texture2DArray.mipMapBias = texture2D.mipMapBias;
			for (int j = 0; j < sourceTextures.Count; j++)
			{
				if (sourceTextures[j] != null)
				{
					texture2D = sourceTextures[j];
					for (int k = 0; k < texture2D.mipmapCount; k++)
					{
						Graphics.CopyTexture(texture2D, 0, k, texture2DArray, j, k);
					}
				}
			}
			texture2DArray.Apply(false);
			return texture2DArray;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000EC9AC File Offset: 0x000EADAC
		public static bool IsDifferentTexture(Texture2D src, Texture2D target)
		{
			if (src == null)
			{
				return target != null;
			}
			return target == null || src.GetInstanceID() != target.GetInstanceID() || src.width != target.width || src.height != target.height;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000ECA20 File Offset: 0x000EAE20
		public void ConstructTerrainReplacementAlbedos()
		{
			if (Application.isPlaying)
			{
				return;
			}
			while (this.m_replacementTerrainAlbedos.Count > this.m_terrainTextures.Count)
			{
				this.m_replacementTerrainAlbedos.RemoveAt(this.m_replacementTerrainAlbedos.Count - 1);
			}
			while (this.m_replacementTerrainAlbedos.Count < this.m_terrainTextures.Count)
			{
				this.m_replacementTerrainAlbedos.Add(null);
			}
			string text = this.m_ctsDirectory + "Terrains/ReplacementTextures/";
			Directory.CreateDirectory(text);
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_terrainTextures[i];
				if (ctsterrainTextureDetails.Albedo != null)
				{
					string path = text + ctsterrainTextureDetails.Albedo.name + "_cts.png";
					if (!File.Exists(path))
					{
						Texture2D texture2D = CTSProfile.ResizeTexture(ctsterrainTextureDetails.Albedo, this.m_albedoFormat, this.m_albedoAniso, 64, 64, false, true, false);
						texture2D.name = ctsterrainTextureDetails.Albedo.name + "_cts";
						this.m_replacementTerrainAlbedos[i] = texture2D;
						byte[] bytes = this.m_replacementTerrainAlbedos[i].EncodeToPNG();
						File.WriteAllBytes(path, bytes);
					}
				}
				else
				{
					this.m_replacementTerrainAlbedos[i] = null;
				}
			}
			CompleteTerrainShader.SetDirty(this, false, true);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000ECB98 File Offset: 0x000EAF98
		public void ConstructTerrainReplacementNormals()
		{
			if (Application.isPlaying)
			{
				return;
			}
			while (this.m_replacementTerrainNormals.Count > this.m_terrainTextures.Count)
			{
				this.m_replacementTerrainNormals.RemoveAt(this.m_replacementTerrainNormals.Count - 1);
			}
			while (this.m_replacementTerrainNormals.Count < this.m_terrainTextures.Count)
			{
				this.m_replacementTerrainNormals.Add(null);
			}
			string text = this.m_ctsDirectory + "Terrains/ReplacementTextures/";
			Directory.CreateDirectory(text);
			for (int i = 0; i < this.m_terrainTextures.Count; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_terrainTextures[i];
				if (ctsterrainTextureDetails.Normal != null)
				{
					string path = text + ctsterrainTextureDetails.Normal.name + "_nrm_cts.png";
					if (!File.Exists(path))
					{
						Texture2D texture2D = CTSProfile.ResizeTexture(ctsterrainTextureDetails.Normal, this.m_normalFormat, this.m_normalAniso, 64, 64, false, true, false);
						texture2D.name = ctsterrainTextureDetails.Normal.name + "_nrm_cts";
						this.m_replacementTerrainNormals[i] = texture2D;
						byte[] bytes = this.m_replacementTerrainNormals[i].EncodeToPNG();
						File.WriteAllBytes(path, bytes);
					}
				}
				else
				{
					this.m_replacementTerrainNormals[i] = null;
				}
			}
			CompleteTerrainShader.SetDirty(this, false, true);
		}

		// Token: 0x04002831 RID: 10289
		[SerializeField]
		private int m_ctsMajorVersion = CTSConstants.MajorVersion;

		// Token: 0x04002832 RID: 10290
		[SerializeField]
		private int m_ctsMinorVersion = CTSConstants.MinorVersion;

		// Token: 0x04002833 RID: 10291
		public bool m_persistMaterials;

		// Token: 0x04002834 RID: 10292
		public bool m_useMaterialControlBlock;

		// Token: 0x04002835 RID: 10293
		public bool m_showGlobalSettings = true;

		// Token: 0x04002836 RID: 10294
		public bool m_showSnowSettings;

		// Token: 0x04002837 RID: 10295
		public bool m_showTextureSettings;

		// Token: 0x04002838 RID: 10296
		public bool m_showGeoSettings;

		// Token: 0x04002839 RID: 10297
		public bool m_showDetailSettings;

		// Token: 0x0400283A RID: 10298
		public bool m_showColorMapSettings;

		// Token: 0x0400283B RID: 10299
		public bool m_showOptimisationSettings;

		// Token: 0x0400283C RID: 10300
		public string m_ctsDirectory = "Assets/CTS/";

		// Token: 0x0400283D RID: 10301
		[SerializeField]
		private CTSConstants.ShaderType m_shaderType = CTSConstants.ShaderType.Basic;

		// Token: 0x0400283E RID: 10302
		public float m_globalUvMixPower = 3f;

		// Token: 0x0400283F RID: 10303
		public float m_globalUvMixStartDistance = 400f;

		// Token: 0x04002840 RID: 10304
		public float m_globalNormalPower = 0.1f;

		// Token: 0x04002841 RID: 10305
		public float m_globalDetailNormalClosePower;

		// Token: 0x04002842 RID: 10306
		public float m_globalDetailNormalCloseTiling = 60f;

		// Token: 0x04002843 RID: 10307
		public float m_globalDetailNormalFarPower;

		// Token: 0x04002844 RID: 10308
		public float m_globalDetailNormalFarTiling = 300f;

		// Token: 0x04002845 RID: 10309
		public float m_globalTerrainSmoothness = 1f;

		// Token: 0x04002846 RID: 10310
		public float m_globalTerrainSpecular = 1f;

		// Token: 0x04002847 RID: 10311
		public float m_globalTesselationPower = 7f;

		// Token: 0x04002848 RID: 10312
		public float m_globalTesselationMinDistance;

		// Token: 0x04002849 RID: 10313
		public float m_globalTesselationMaxDistance = 50f;

		// Token: 0x0400284A RID: 10314
		public float m_globalTesselationPhongStrength = 1f;

		// Token: 0x0400284B RID: 10315
		public CTSConstants.AOType m_globalAOType = CTSConstants.AOType.NormalMapBased;

		// Token: 0x0400284C RID: 10316
		public float m_globalAOPower = 1f;

		// Token: 0x0400284D RID: 10317
		public float m_globalBasemapDistance = 1000f;

		// Token: 0x0400284E RID: 10318
		public bool m_globalStripTexturesAtRuntime = true;

		// Token: 0x0400284F RID: 10319
		public bool m_globalDisconnectProfileAtRuntime = true;

		// Token: 0x04002850 RID: 10320
		public float m_colorMapClosePower;

		// Token: 0x04002851 RID: 10321
		public float m_colorMapFarPower;

		// Token: 0x04002852 RID: 10322
		public float m_colorMapOpacity = 1f;

		// Token: 0x04002853 RID: 10323
		public float m_geoMapCloseOffset;

		// Token: 0x04002854 RID: 10324
		public float m_geoMapClosePower;

		// Token: 0x04002855 RID: 10325
		public float m_geoMapTilingClose = 100f;

		// Token: 0x04002856 RID: 10326
		public float m_geoMapFarOffset;

		// Token: 0x04002857 RID: 10327
		public float m_geoMapFarPower;

		// Token: 0x04002858 RID: 10328
		public float m_geoMapTilingFar = 100f;

		// Token: 0x04002859 RID: 10329
		public float m_snowAmount;

		// Token: 0x0400285A RID: 10330
		public float m_snowMaxAngle = 40f;

		// Token: 0x0400285B RID: 10331
		public float m_snowMaxAngleHardness = 1f;

		// Token: 0x0400285C RID: 10332
		public float m_snowMinHeight = -1000f;

		// Token: 0x0400285D RID: 10333
		public float m_snowMinHeightBlending = 57f;

		// Token: 0x0400285E RID: 10334
		public float m_snowNoisePower = 0.8f;

		// Token: 0x0400285F RID: 10335
		public float m_snowNoiseTiling = 0.02f;

		// Token: 0x04002860 RID: 10336
		public float m_snowNormalScale = 1f;

		// Token: 0x04002861 RID: 10337
		public float m_snowDetailPower = 1f;

		// Token: 0x04002862 RID: 10338
		public float m_snowTilingClose = 6.9f;

		// Token: 0x04002863 RID: 10339
		public float m_snowTilingFar = 3f;

		// Token: 0x04002864 RID: 10340
		public float m_snowBrightness = 1f;

		// Token: 0x04002865 RID: 10341
		public float m_snowBlendNormal = 0.9f;

		// Token: 0x04002866 RID: 10342
		public float m_snowSmoothness = 1f;

		// Token: 0x04002867 RID: 10343
		public Color m_snowTint = new Color(1f, 1f, 1f);

		// Token: 0x04002868 RID: 10344
		public float m_snowSpecular = 1f;

		// Token: 0x04002869 RID: 10345
		public float m_snowHeightmapBlendClose = 1f;

		// Token: 0x0400286A RID: 10346
		public float m_snowHeightmapBlendFar = 1f;

		// Token: 0x0400286B RID: 10347
		public float m_snowHeightmapDepth = 8f;

		// Token: 0x0400286C RID: 10348
		public float m_snowHeightmapContrast = 1f;

		// Token: 0x0400286D RID: 10349
		public float m_snowHeightmapMinValue;

		// Token: 0x0400286E RID: 10350
		public float m_snowHeightmapMaxValue = 1f;

		// Token: 0x0400286F RID: 10351
		public float m_snowTesselationDepth;

		// Token: 0x04002870 RID: 10352
		public float m_snowAOStrength = 1f;

		// Token: 0x04002871 RID: 10353
		public Vector4 m_snowAverage;

		// Token: 0x04002872 RID: 10354
		public TextureFormat m_albedoFormat = TextureFormat.RGBA32;

		// Token: 0x04002873 RID: 10355
		public int m_albedoAniso = 8;

		// Token: 0x04002874 RID: 10356
		public FilterMode m_albedoFilterMode = FilterMode.Bilinear;

		// Token: 0x04002875 RID: 10357
		[SerializeField]
		private CTSConstants.TextureSize m_albedoTextureSize = CTSConstants.TextureSize.Texture_1024;

		// Token: 0x04002876 RID: 10358
		public int m_albedoTextureSizePx = 1024;

		// Token: 0x04002877 RID: 10359
		[SerializeField]
		private bool m_albedoCompress = true;

		// Token: 0x04002878 RID: 10360
		public TextureFormat m_normalFormat = TextureFormat.RGBA32;

		// Token: 0x04002879 RID: 10361
		public int m_normalAniso = 8;

		// Token: 0x0400287A RID: 10362
		public FilterMode m_normalFilterMode = FilterMode.Bilinear;

		// Token: 0x0400287B RID: 10363
		[SerializeField]
		private CTSConstants.TextureSize m_normalTextureSize = CTSConstants.TextureSize.Texture_1024;

		// Token: 0x0400287C RID: 10364
		public int m_normalTextureSizePx = 1024;

		// Token: 0x0400287D RID: 10365
		[SerializeField]
		private bool m_normalCompress = true;

		// Token: 0x0400287E RID: 10366
		public int m_globalDetailNormalMapIdx = -1;

		// Token: 0x0400287F RID: 10367
		[SerializeField]
		private Texture2D m_globalDetailNormalMap;

		// Token: 0x04002880 RID: 10368
		public int m_snowAlbedoTextureIdx = -1;

		// Token: 0x04002881 RID: 10369
		[SerializeField]
		private Texture2D m_snowAlbedoTexture;

		// Token: 0x04002882 RID: 10370
		public int m_snowNormalTextureIdx = -1;

		// Token: 0x04002883 RID: 10371
		[SerializeField]
		private Texture2D m_snowNormalTexture;

		// Token: 0x04002884 RID: 10372
		public int m_snowHeightTextureIdx = -1;

		// Token: 0x04002885 RID: 10373
		[SerializeField]
		private Texture2D m_snowHeightTexture;

		// Token: 0x04002886 RID: 10374
		public int m_snowAOTextureIdx = -1;

		// Token: 0x04002887 RID: 10375
		[SerializeField]
		private Texture2D m_snowAOTexture;

		// Token: 0x04002888 RID: 10376
		public int m_snowEmissionTextureIdx = -1;

		// Token: 0x04002889 RID: 10377
		[SerializeField]
		private Texture2D m_snowEmissionTexture;

		// Token: 0x0400288A RID: 10378
		[SerializeField]
		private Texture2D m_snowGlitterTexture;

		// Token: 0x0400288B RID: 10379
		public float m_snowGlitterColorPower = 0.2f;

		// Token: 0x0400288C RID: 10380
		public float m_snowGlitterNoiseThreshold = 0.991f;

		// Token: 0x0400288D RID: 10381
		public float m_snowGlitterSpecularPower = 0.2f;

		// Token: 0x0400288E RID: 10382
		public float m_snowGlitterSmoothness = 0.9f;

		// Token: 0x0400288F RID: 10383
		public float m_snowGlitterRefreshSpeed = 4f;

		// Token: 0x04002890 RID: 10384
		public float m_snowGlitterTiling = 2.5f;

		// Token: 0x04002891 RID: 10385
		[SerializeField]
		private Texture2D m_geoAlbedoTexture;

		// Token: 0x04002892 RID: 10386
		[SerializeField]
		private List<CTSTerrainTextureDetails> m_terrainTextures = new List<CTSTerrainTextureDetails>();

		// Token: 0x04002893 RID: 10387
		[SerializeField]
		private List<Texture2D> m_replacementTerrainAlbedos = new List<Texture2D>();

		// Token: 0x04002894 RID: 10388
		[SerializeField]
		private List<Texture2D> m_replacementTerrainNormals = new List<Texture2D>();

		// Token: 0x04002895 RID: 10389
		[SerializeField]
		private Texture2DArray m_albedosTextureArray;

		// Token: 0x04002896 RID: 10390
		public bool m_needsAlbedosArrayUpdate;

		// Token: 0x04002897 RID: 10391
		[SerializeField]
		private Texture2DArray m_normalsTextureArray;

		// Token: 0x04002898 RID: 10392
		public bool m_needsNormalsArrayUpdate;
	}
}

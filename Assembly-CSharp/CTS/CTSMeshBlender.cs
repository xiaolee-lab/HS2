using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000693 RID: 1683
	[ExecuteInEditMode]
	[Serializable]
	public class CTSMeshBlender : MonoBehaviour
	{
		// Token: 0x060027A0 RID: 10144 RVA: 0x000E9DA4 File Offset: 0x000E81A4
		private void Awake()
		{
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000E9DA8 File Offset: 0x000E81A8
		public void ClearBlend()
		{
			if (this.m_filters != null)
			{
				for (int i = 0; i < this.m_filters.Length; i++)
				{
					this.m_filters[i].sharedMesh = this.m_originalMeshes[i];
				}
			}
			if (this.m_renderers != null)
			{
				for (int j = 0; j < this.m_renderers.Length; j++)
				{
					MeshRenderer meshRenderer = this.m_renderers[j];
					List<Material> list = new List<Material>(meshRenderer.sharedMaterials);
					int k = 0;
					while (k < list.Count)
					{
						Material material = list[k];
						if (material == null || material.name == "CTS Model Blend Shader")
						{
							list.RemoveAt(k);
							this.m_sharedMaterial = material;
						}
						else
						{
							k++;
						}
					}
					meshRenderer.sharedMaterials = list.ToArray();
				}
			}
			if (this.m_sharedMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_sharedMaterial);
				this.m_sharedMaterial = null;
			}
			this.m_renderers = null;
			this.m_filters = null;
			this.m_originalMeshes = null;
			this.m_textureList.Clear();
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000E9ED4 File Offset: 0x000E82D4
		public void CreateBlend()
		{
			this.ClearBlend();
			this.m_renderers = base.gameObject.GetComponentsInChildren<MeshRenderer>();
			this.m_filters = base.gameObject.GetComponentsInChildren<MeshFilter>();
			this.m_originalMeshes = new Mesh[this.m_filters.Length];
			for (int i = 0; i < this.m_filters.Length; i++)
			{
				if (this.m_filters[i].sharedMesh == null)
				{
					this.m_originalMeshes[i] = null;
				}
				else
				{
					this.m_originalMeshes[i] = this.m_filters[i].sharedMesh;
					this.m_filters[i].sharedMesh = UnityEngine.Object.Instantiate<Mesh>(this.m_originalMeshes[i]);
				}
			}
			this.GetTexturesAndSettingsAtCurrentLocation();
			Vector3 position = base.transform.position;
			Vector3 localScale = base.transform.localScale;
			Vector3 eulerAngles = base.transform.eulerAngles;
			for (int j = 0; j < this.m_filters.Length; j++)
			{
				Mesh sharedMesh = this.m_filters[j].sharedMesh;
				if (sharedMesh != null)
				{
					int num = sharedMesh.vertices.Length;
					Vector3[] vertices = sharedMesh.vertices;
					Vector3[] normals = sharedMesh.normals;
					Color[] array = sharedMesh.colors;
					if (array.Length == 0)
					{
						array = new Color[num];
					}
					for (int k = 0; k < num; k++)
					{
						Vector3 vector = position + Quaternion.Euler(eulerAngles) * Vector3.Scale(vertices[k], localScale);
						Terrain terrain = this.GetTerrain(vector);
						if (terrain != null)
						{
							Vector3 localPosition = this.GetLocalPosition(terrain, vector);
							float num2 = terrain.SampleHeight(vector);
							float num3 = num2 + this.m_textureBlendOffset;
							float num4 = num3 + this.m_textureBlendStart;
							float num5 = num4 + this.m_textureBlendHeight;
							float num6 = num2 + this.m_normalBlendOffset;
							float num7 = num6 + this.m_normalBlendStart;
							float num8 = num7 + this.m_normalBlendHeight;
							if (vector.y < num3)
							{
								array[k].a = 0f;
							}
							else if (vector.y <= num5)
							{
								Color color = default(Color);
								float a = 1f;
								if (vector.y > num4)
								{
									a = Mathf.Lerp(1f, 0f, (vector.y - num4) / this.m_textureBlendHeight);
								}
								color.a = a;
								float[,,] texturesAtLocation = this.GetTexturesAtLocation(terrain, localPosition);
								if (this.m_textureList.Count >= 1)
								{
									color.r = texturesAtLocation[0, 0, this.m_textureList[0].m_terrainIdx];
								}
								if (this.m_textureList.Count >= 2)
								{
									color.g = texturesAtLocation[0, 0, this.m_textureList[1].m_terrainIdx];
								}
								if (this.m_textureList.Count >= 3)
								{
									color.b = texturesAtLocation[0, 0, this.m_textureList[2].m_terrainIdx];
								}
								array[k] = color;
							}
							else
							{
								array[k].a = 0f;
							}
							if (vector.y >= num6)
							{
								if (vector.y < num7)
								{
									normals[k] = this.GetNormalsAtLocation(terrain, localPosition);
								}
								else if (vector.y <= num8)
								{
									normals[k] = Vector3.Lerp(this.GetNormalsAtLocation(terrain, localPosition), normals[k], (num8 - vector.y) / this.m_normalBlendHeight);
								}
							}
						}
						else
						{
							array[k].a = 0f;
						}
					}
					sharedMesh.colors = array;
					sharedMesh.normals = normals;
				}
			}
			this.InitializeMaterials();
			this.UpdateShader();
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000EA2D4 File Offset: 0x000E86D4
		public Vector3 GetNearestVertice(Vector3 sourcePosition, GameObject targetObject)
		{
			float num = float.MaxValue;
			Vector3 vector = targetObject.transform.position;
			Vector3 a = vector;
			Vector3 localScale = targetObject.transform.localScale;
			Vector3 eulerAngles = targetObject.transform.eulerAngles;
			MeshFilter[] componentsInChildren = targetObject.GetComponentsInChildren<MeshFilter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Mesh sharedMesh = componentsInChildren[i].sharedMesh;
				if (sharedMesh != null)
				{
					int num2 = sharedMesh.vertices.Length;
					Vector3[] vertices = sharedMesh.vertices;
					for (int j = 0; j < num2; j++)
					{
						Vector3 vector2 = a + Quaternion.Euler(eulerAngles) * Vector3.Scale(vertices[j], localScale);
						float num3 = Vector3.Distance(sourcePosition, vector2);
						if (num3 < num)
						{
							num = num3;
							vector = vector2;
						}
					}
				}
			}
			return vector;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000EA3B4 File Offset: 0x000E87B4
		private void InitializeShaderConstants()
		{
			if (!CTSMeshBlender._ShadersIDsAreInitialized)
			{
				CTSMeshBlender._ShadersIDsAreInitialized = true;
				CTSMeshBlender._Use_AO = Shader.PropertyToID("_Use_AO");
				CTSMeshBlender._Use_AO_Texture = Shader.PropertyToID("_Use_AO_Texture");
				CTSMeshBlender._Terrain_Specular = Shader.PropertyToID("_Terrain_Specular");
				CTSMeshBlender._Terrain_Smoothness = Shader.PropertyToID("_Terrain_Smoothness");
				CTSMeshBlender._Texture_Geological_Map = Shader.PropertyToID("_Texture_Geological_Map");
				CTSMeshBlender._Geological_Tiling_Close = Shader.PropertyToID("_Geological_Tiling_Close");
				CTSMeshBlender._Geological_Map_Offset_Close = Shader.PropertyToID("_Geological_Map_Offset_Close");
				CTSMeshBlender._Geological_Map_Close_Power = Shader.PropertyToID("_Geological_Map_Close_Power");
				CTSMeshBlender._Texture_Albedo_Sm_1 = Shader.PropertyToID("_Texture_Albedo_Sm_1");
				CTSMeshBlender._Texture_Color_1 = Shader.PropertyToID("_Texture_Color_1");
				CTSMeshBlender._Texture_Tiling_1 = Shader.PropertyToID("_Texture_Tiling_1");
				CTSMeshBlender._Texture_Normal_1 = Shader.PropertyToID("_Texture_Normal_1");
				CTSMeshBlender._Texture_1_Normal_Power = Shader.PropertyToID("_Texture_1_Normal_Power");
				CTSMeshBlender._Texture_GHeightAAO_1 = Shader.PropertyToID("_Texture_GHeightAAO_1");
				CTSMeshBlender._Texture_1_AO_Power = Shader.PropertyToID("_Texture_1_AO_Power");
				CTSMeshBlender._Texture_1_Geological_Power = Shader.PropertyToID("_Texture_1_Geological_Power");
				CTSMeshBlender._Texture_1_Height_Contrast = Shader.PropertyToID("_Texture_1_Height_Contrast");
				CTSMeshBlender._Texture_1_Heightmap_Depth = Shader.PropertyToID("_Texture_1_Heightmap_Depth");
				CTSMeshBlender._Texture_1_Heightblend_Close = Shader.PropertyToID("_Texture_1_Heightblend_Close");
				CTSMeshBlender._Texture_Albedo_Sm_2 = Shader.PropertyToID("_Texture_Albedo_Sm_2");
				CTSMeshBlender._Texture_Color_2 = Shader.PropertyToID("_Texture_Color_2");
				CTSMeshBlender._Texture_Tiling_2 = Shader.PropertyToID("_Texture_Tiling_2");
				CTSMeshBlender._Texture_Normal_2 = Shader.PropertyToID("_Texture_Normal_2");
				CTSMeshBlender._Texture_2_Normal_Power = Shader.PropertyToID("_Texture_2_Normal_Power");
				CTSMeshBlender._Texture_GHeightAAO_2 = Shader.PropertyToID("_Texture_GHeightAAO_2");
				CTSMeshBlender._Texture_2_AO_Power = Shader.PropertyToID("_Texture_2_AO_Power");
				CTSMeshBlender._Texture_2_Geological_Power = Shader.PropertyToID("_Texture_2_Geological_Power");
				CTSMeshBlender._Texture_2_Height_Contrast = Shader.PropertyToID("_Texture_2_Height_Contrast");
				CTSMeshBlender._Texture_2_Heightmap_Depth = Shader.PropertyToID("_Texture_2_Heightmap_Depth");
				CTSMeshBlender._Texture_2_Heightblend_Close = Shader.PropertyToID("_Texture_2_Heightblend_Close");
				CTSMeshBlender._Texture_Albedo_Sm_3 = Shader.PropertyToID("_Texture_Albedo_Sm_3");
				CTSMeshBlender._Texture_Color_3 = Shader.PropertyToID("_Texture_Color_3");
				CTSMeshBlender._Texture_Tiling_3 = Shader.PropertyToID("_Texture_Tiling_3");
				CTSMeshBlender._Texture_Normal_3 = Shader.PropertyToID("_Texture_Normal_3");
				CTSMeshBlender._Texture_3_Normal_Power = Shader.PropertyToID("_Texture_3_Normal_Power");
				CTSMeshBlender._Texture_GHeightAAO_3 = Shader.PropertyToID("_Texture_GHeightAAO_3");
				CTSMeshBlender._Texture_3_AO_Power = Shader.PropertyToID("_Texture_3_AO_Power");
				CTSMeshBlender._Texture_3_Geological_Power = Shader.PropertyToID("_Texture_3_Geological_Power");
				CTSMeshBlender._Texture_3_Height_Contrast = Shader.PropertyToID("_Texture_3_Height_Contrast");
				CTSMeshBlender._Texture_3_Heightmap_Depth = Shader.PropertyToID("_Texture_3_Heightmap_Depth");
				CTSMeshBlender._Texture_3_Heightblend_Close = Shader.PropertyToID("_Texture_3_Heightblend_Close");
			}
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000EA638 File Offset: 0x000E8A38
		private void InitializeMaterials()
		{
			for (int i = 0; i < this.m_renderers.Length; i++)
			{
				MeshRenderer meshRenderer = this.m_renderers[i];
				if (meshRenderer != null)
				{
					bool flag = false;
					List<Material> list = new List<Material>(this.m_renderers[i].sharedMaterials);
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].name == "CTS Model Blend Shader")
						{
							flag = true;
							this.m_sharedMaterial = list[j];
							break;
						}
					}
					if (!flag)
					{
						if (this.m_sharedMaterial == null)
						{
							this.m_sharedMaterial = new Material(Shader.Find("CTS/CTS_Model_Blend"))
							{
								name = "CTS Model Blend Shader"
							};
						}
						list.Add(this.m_sharedMaterial);
						meshRenderer.sharedMaterials = list.ToArray();
					}
				}
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000EA730 File Offset: 0x000E8B30
		private void UpdateShader()
		{
			this.InitializeShaderConstants();
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_renderers == null || this.m_renderers.Length == 0)
			{
				return;
			}
			if (this.m_textureList.Count == 0)
			{
				return;
			}
			if (this.m_materialProperties == null)
			{
				this.m_materialProperties = new MaterialPropertyBlock();
			}
			for (int i = 0; i < this.m_renderers.Length; i++)
			{
				this.m_sharedMaterial.SetInt(CTSMeshBlender._Use_AO, (!this.m_useAO) ? 0 : 1);
				this.m_sharedMaterial.SetInt(CTSMeshBlender._Use_AO_Texture, (!this.m_useAOTexture) ? 0 : 1);
				this.m_sharedMaterial.SetFloat(CTSMeshBlender._Terrain_Specular, this.m_specular);
				this.m_sharedMaterial.SetFloat(CTSMeshBlender._Terrain_Smoothness, this.m_smoothness);
				this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Geological_Map, this.m_geoMap);
				this.m_sharedMaterial.SetFloat(CTSMeshBlender._Geological_Tiling_Close, this.m_geoTilingClose);
				this.m_sharedMaterial.SetFloat(CTSMeshBlender._Geological_Map_Offset_Close, this.m_geoMapOffsetClose);
				this.m_sharedMaterial.SetFloat(CTSMeshBlender._Geological_Map_Close_Power, this.m_geoMapClosePower);
				if (this.m_textureList.Count >= 1)
				{
					CTSMeshBlender.TextureData textureData = this.m_textureList[0];
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Albedo_Sm_1, textureData.m_albedo);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Normal_1, textureData.m_normal);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_GHeightAAO_1, textureData.m_hao_in_GA);
					this.m_sharedMaterial.SetVector(CTSMeshBlender._Texture_Color_1, textureData.m_color);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_Tiling_1, textureData.m_tiling);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_Normal_Power, textureData.m_normalPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_AO_Power, textureData.m_aoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_Geological_Power, textureData.m_geoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_Height_Contrast, textureData.m_heightContrast);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_Heightmap_Depth, textureData.m_heightDepth);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_1_Heightblend_Close, textureData.m_heightBlendClose);
				}
				if (this.m_textureList.Count >= 2)
				{
					CTSMeshBlender.TextureData textureData2 = this.m_textureList[1];
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Albedo_Sm_2, textureData2.m_albedo);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Normal_2, textureData2.m_normal);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_GHeightAAO_2, textureData2.m_hao_in_GA);
					this.m_sharedMaterial.SetVector(CTSMeshBlender._Texture_Color_2, textureData2.m_color);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_Tiling_2, textureData2.m_tiling);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_Normal_Power, textureData2.m_normalPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_AO_Power, textureData2.m_aoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_Geological_Power, textureData2.m_geoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_Height_Contrast, textureData2.m_heightContrast);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_Heightmap_Depth, textureData2.m_heightDepth);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_2_Heightblend_Close, textureData2.m_heightBlendClose);
				}
				if (this.m_textureList.Count >= 3)
				{
					CTSMeshBlender.TextureData textureData3 = this.m_textureList[2];
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Albedo_Sm_3, textureData3.m_albedo);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_Normal_3, textureData3.m_normal);
					this.m_sharedMaterial.SetTexture(CTSMeshBlender._Texture_GHeightAAO_3, textureData3.m_hao_in_GA);
					this.m_sharedMaterial.SetVector(CTSMeshBlender._Texture_Color_3, textureData3.m_color);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_Tiling_3, textureData3.m_tiling);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_Normal_Power, textureData3.m_normalPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_AO_Power, textureData3.m_aoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_Geological_Power, textureData3.m_geoPower);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_Height_Contrast, textureData3.m_heightContrast);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_Heightmap_Depth, textureData3.m_heightDepth);
					this.m_sharedMaterial.SetFloat(CTSMeshBlender._Texture_3_Heightblend_Close, textureData3.m_heightBlendClose);
				}
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000EABA8 File Offset: 0x000E8FA8
		private void GetTexturesAndSettingsAtCurrentLocation()
		{
			this.m_textureList.Clear();
			CTSProfile ctsprofile = null;
			SplatPrototype[] array = new SplatPrototype[0];
			Vector3 position = base.transform.position;
			Vector3 localScale = base.transform.localScale;
			Vector3 eulerAngles = base.transform.eulerAngles;
			for (int i = this.m_filters.Length - 1; i >= 0; i--)
			{
				Mesh sharedMesh = this.m_filters[i].sharedMesh;
				if (sharedMesh != null)
				{
					Vector3[] vertices = sharedMesh.vertices;
					for (int j = vertices.Length - 1; j >= 0; j--)
					{
						Vector3 locationWU = position + Quaternion.Euler(eulerAngles) * Vector3.Scale(vertices[j], localScale);
						Terrain terrain = this.GetTerrain(locationWU);
						if (terrain != null)
						{
							if (ctsprofile == null)
							{
								CompleteTerrainShader component = terrain.gameObject.GetComponent<CompleteTerrainShader>();
								if (component != null)
								{
									ctsprofile = component.Profile;
								}
							}
							if (array.Length == 0)
							{
								array = terrain.terrainData.splatPrototypes;
							}
							Vector3 localPosition = this.GetLocalPosition(terrain, locationWU);
							float[,,] texturesAtLocation = this.GetTexturesAtLocation(terrain, localPosition);
							for (int k = 0; k < texturesAtLocation.GetLength(2); k++)
							{
								if (k == this.m_textureList.Count)
								{
									CTSMeshBlender.TextureData textureData = new CTSMeshBlender.TextureData();
									textureData.m_terrainIdx = k;
									textureData.m_terrainTextureStrength = texturesAtLocation[0, 0, k];
									this.m_textureList.Add(textureData);
								}
								else
								{
									this.m_textureList[k].m_terrainTextureStrength += texturesAtLocation[0, 0, k];
								}
							}
						}
					}
				}
			}
			List<CTSMeshBlender.TextureData> list = (from x in this.m_textureList
			orderby x.m_terrainTextureStrength descending
			select x).ToList<CTSMeshBlender.TextureData>();
			while (list.Count > 3)
			{
				list.RemoveAt(list.Count - 1);
			}
			this.m_textureList = (from x in list
			orderby x.m_terrainIdx
			select x).ToList<CTSMeshBlender.TextureData>();
			if (ctsprofile != null)
			{
				this.m_geoMap = ctsprofile.GeoAlbedo;
				this.m_geoMapClosePower = ctsprofile.m_geoMapClosePower;
				this.m_geoMapOffsetClose = ctsprofile.m_geoMapCloseOffset;
				this.m_geoTilingClose = ctsprofile.m_geoMapTilingClose;
				this.m_smoothness = ctsprofile.m_globalTerrainSmoothness;
				this.m_specular = ctsprofile.m_globalTerrainSpecular;
				CTSConstants.AOType globalAOType = ctsprofile.m_globalAOType;
				if (globalAOType != CTSConstants.AOType.None)
				{
					if (globalAOType != CTSConstants.AOType.NormalMapBased)
					{
						if (globalAOType == CTSConstants.AOType.TextureBased)
						{
							this.m_useAO = true;
							this.m_useAOTexture = true;
						}
					}
					else
					{
						this.m_useAO = true;
						this.m_useAOTexture = false;
					}
				}
				else
				{
					this.m_useAO = false;
					this.m_useAOTexture = false;
				}
			}
			else
			{
				this.m_geoMap = null;
				this.m_geoMapClosePower = 0f;
				this.m_geoMapOffsetClose = 0f;
				this.m_geoTilingClose = 0f;
				this.m_smoothness = 1f;
				this.m_specular = 1f;
				this.m_useAO = true;
				this.m_useAOTexture = false;
			}
			byte b = 0;
			byte b2 = 0;
			for (int l = 0; l < this.m_textureList.Count; l++)
			{
				CTSMeshBlender.TextureData textureData2 = this.m_textureList[l];
				if (ctsprofile != null && textureData2.m_terrainIdx < ctsprofile.TerrainTextures.Count)
				{
					CTSTerrainTextureDetails ctsterrainTextureDetails = ctsprofile.TerrainTextures[textureData2.m_terrainIdx];
					textureData2.m_albedo = ctsterrainTextureDetails.Albedo;
					textureData2.m_normal = ctsterrainTextureDetails.Normal;
					textureData2.m_hao_in_GA = ctsprofile.BakeHAOTexture(ctsterrainTextureDetails.Albedo.name, ctsterrainTextureDetails.Height, ctsterrainTextureDetails.AmbientOcclusion, out b, out b2);
					textureData2.m_aoPower = ctsterrainTextureDetails.m_aoPower;
					textureData2.m_color = new Vector4(ctsterrainTextureDetails.m_tint.r * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_tint.g * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_tint.b * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_smoothness);
					textureData2.m_geoPower = ctsterrainTextureDetails.m_geologicalPower;
					textureData2.m_normalPower = ctsterrainTextureDetails.m_normalStrength;
					textureData2.m_tiling = ctsterrainTextureDetails.m_albedoTilingClose;
					textureData2.m_heightContrast = ctsterrainTextureDetails.m_heightContrast;
					textureData2.m_heightDepth = ctsterrainTextureDetails.m_heightDepth;
					textureData2.m_heightBlendClose = ctsterrainTextureDetails.m_heightBlendClose;
				}
				else if (textureData2.m_terrainIdx < array.Length)
				{
					SplatPrototype splatPrototype = array[textureData2.m_terrainIdx];
					textureData2.m_albedo = splatPrototype.texture;
					textureData2.m_normal = splatPrototype.normalMap;
					textureData2.m_hao_in_GA = null;
					textureData2.m_aoPower = 0f;
					textureData2.m_color = Vector4.one;
					textureData2.m_geoPower = 0f;
					textureData2.m_normalPower = 1f;
					textureData2.m_tiling = splatPrototype.tileSize.x;
					textureData2.m_heightContrast = 1f;
					textureData2.m_heightDepth = 1f;
					textureData2.m_heightBlendClose = 1f;
				}
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000EB104 File Offset: 0x000E9504
		private Terrain GetTerrain(Vector3 locationWU)
		{
			Terrain terrain = Terrain.activeTerrain;
			if (terrain != null)
			{
				Vector3 position = terrain.GetPosition();
				Vector3 vector = position + terrain.terrainData.size;
				if (locationWU.x >= position.x && locationWU.x <= vector.x && locationWU.z >= position.z && locationWU.z <= vector.z)
				{
					return terrain;
				}
			}
			for (int i = 0; i < Terrain.activeTerrains.Length; i++)
			{
				terrain = Terrain.activeTerrains[i];
				Vector3 position2 = terrain.GetPosition();
				Vector3 vector2 = position2 + terrain.terrainData.size;
				if (locationWU.x >= position2.x && locationWU.x <= vector2.x && locationWU.z >= position2.z && locationWU.z <= vector2.z)
				{
					return terrain;
				}
			}
			return null;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000EB218 File Offset: 0x000E9618
		private Vector3 GetLocalPosition(Terrain terrain, Vector3 locationWU)
		{
			Vector3 vector = terrain.transform.InverseTransformPoint(locationWU);
			return new Vector3(Mathf.InverseLerp(0f, terrain.terrainData.size.x, vector.x), Mathf.InverseLerp(0f, terrain.terrainData.size.y, vector.y), Mathf.InverseLerp(0f, terrain.terrainData.size.z, vector.z));
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000EB2A3 File Offset: 0x000E96A3
		private float[,,] GetTexturesAtLocation(Terrain terrain, Vector3 locationTU)
		{
			return terrain.terrainData.GetAlphamaps((int)(locationTU.x * (float)(terrain.terrainData.alphamapWidth - 1)), (int)(locationTU.z * (float)(terrain.terrainData.alphamapHeight - 1)), 1, 1);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000EB2E0 File Offset: 0x000E96E0
		private Vector3 GetNormalsAtLocation(Terrain terrain, Vector3 locationTU)
		{
			return terrain.terrainData.GetInterpolatedNormal(locationTU.x, locationTU.z);
		}

		// Token: 0x040027E4 RID: 10212
		public float m_textureBlendOffset = -2f;

		// Token: 0x040027E5 RID: 10213
		public float m_textureBlendStart = 2f;

		// Token: 0x040027E6 RID: 10214
		public float m_textureBlendHeight = 1f;

		// Token: 0x040027E7 RID: 10215
		public float m_normalBlendOffset = -1f;

		// Token: 0x040027E8 RID: 10216
		public float m_normalBlendStart;

		// Token: 0x040027E9 RID: 10217
		public float m_normalBlendHeight = 2f;

		// Token: 0x040027EA RID: 10218
		public float m_specular = 1f;

		// Token: 0x040027EB RID: 10219
		public float m_smoothness = 1f;

		// Token: 0x040027EC RID: 10220
		public bool m_useAO = true;

		// Token: 0x040027ED RID: 10221
		public bool m_useAOTexture;

		// Token: 0x040027EE RID: 10222
		public float m_geoMapClosePower;

		// Token: 0x040027EF RID: 10223
		public float m_geoTilingClose = 1f;

		// Token: 0x040027F0 RID: 10224
		public float m_geoMapOffsetClose = 86f;

		// Token: 0x040027F1 RID: 10225
		public Texture2D m_geoMap;

		// Token: 0x040027F2 RID: 10226
		public Material m_sharedMaterial;

		// Token: 0x040027F3 RID: 10227
		public List<CTSMeshBlender.TextureData> m_textureList = new List<CTSMeshBlender.TextureData>();

		// Token: 0x040027F4 RID: 10228
		private MaterialPropertyBlock m_materialProperties;

		// Token: 0x040027F5 RID: 10229
		[SerializeField]
		private MeshRenderer[] m_renderers;

		// Token: 0x040027F6 RID: 10230
		[SerializeField]
		private MeshFilter[] m_filters;

		// Token: 0x040027F7 RID: 10231
		[SerializeField]
		private Mesh[] m_originalMeshes;

		// Token: 0x040027F8 RID: 10232
		private static bool _ShadersIDsAreInitialized;

		// Token: 0x040027F9 RID: 10233
		private static int _Use_AO;

		// Token: 0x040027FA RID: 10234
		private static int _Use_AO_Texture;

		// Token: 0x040027FB RID: 10235
		private static int _Terrain_Specular;

		// Token: 0x040027FC RID: 10236
		private static int _Terrain_Smoothness;

		// Token: 0x040027FD RID: 10237
		private static int _Texture_Geological_Map;

		// Token: 0x040027FE RID: 10238
		private static int _Geological_Tiling_Close;

		// Token: 0x040027FF RID: 10239
		private static int _Geological_Map_Offset_Close;

		// Token: 0x04002800 RID: 10240
		private static int _Geological_Map_Close_Power;

		// Token: 0x04002801 RID: 10241
		private static int _Texture_Albedo_Sm_1;

		// Token: 0x04002802 RID: 10242
		private static int _Texture_Color_1;

		// Token: 0x04002803 RID: 10243
		private static int _Texture_Tiling_1;

		// Token: 0x04002804 RID: 10244
		private static int _Texture_Normal_1;

		// Token: 0x04002805 RID: 10245
		private static int _Texture_1_Normal_Power;

		// Token: 0x04002806 RID: 10246
		private static int _Texture_GHeightAAO_1;

		// Token: 0x04002807 RID: 10247
		private static int _Texture_1_AO_Power;

		// Token: 0x04002808 RID: 10248
		private static int _Texture_1_Geological_Power;

		// Token: 0x04002809 RID: 10249
		private static int _Texture_1_Height_Contrast;

		// Token: 0x0400280A RID: 10250
		private static int _Texture_1_Heightmap_Depth;

		// Token: 0x0400280B RID: 10251
		private static int _Texture_1_Heightblend_Close;

		// Token: 0x0400280C RID: 10252
		private static int _Texture_Albedo_Sm_2;

		// Token: 0x0400280D RID: 10253
		private static int _Texture_Color_2;

		// Token: 0x0400280E RID: 10254
		private static int _Texture_Tiling_2;

		// Token: 0x0400280F RID: 10255
		private static int _Texture_Normal_2;

		// Token: 0x04002810 RID: 10256
		private static int _Texture_2_Normal_Power;

		// Token: 0x04002811 RID: 10257
		private static int _Texture_GHeightAAO_2;

		// Token: 0x04002812 RID: 10258
		private static int _Texture_2_AO_Power;

		// Token: 0x04002813 RID: 10259
		private static int _Texture_2_Geological_Power;

		// Token: 0x04002814 RID: 10260
		private static int _Texture_2_Height_Contrast;

		// Token: 0x04002815 RID: 10261
		private static int _Texture_2_Heightmap_Depth;

		// Token: 0x04002816 RID: 10262
		private static int _Texture_2_Heightblend_Close;

		// Token: 0x04002817 RID: 10263
		private static int _Texture_Albedo_Sm_3;

		// Token: 0x04002818 RID: 10264
		private static int _Texture_Color_3;

		// Token: 0x04002819 RID: 10265
		private static int _Texture_Tiling_3;

		// Token: 0x0400281A RID: 10266
		private static int _Texture_Normal_3;

		// Token: 0x0400281B RID: 10267
		private static int _Texture_3_Normal_Power;

		// Token: 0x0400281C RID: 10268
		private static int _Texture_GHeightAAO_3;

		// Token: 0x0400281D RID: 10269
		private static int _Texture_3_AO_Power;

		// Token: 0x0400281E RID: 10270
		private static int _Texture_3_Geological_Power;

		// Token: 0x0400281F RID: 10271
		private static int _Texture_3_Height_Contrast;

		// Token: 0x04002820 RID: 10272
		private static int _Texture_3_Heightmap_Depth;

		// Token: 0x04002821 RID: 10273
		private static int _Texture_3_Heightblend_Close;

		// Token: 0x02000694 RID: 1684
		[Serializable]
		public class TextureData
		{
			// Token: 0x04002824 RID: 10276
			public int m_terrainIdx;

			// Token: 0x04002825 RID: 10277
			public float m_terrainTextureStrength;

			// Token: 0x04002826 RID: 10278
			public Texture2D m_albedo;

			// Token: 0x04002827 RID: 10279
			public Texture2D m_normal;

			// Token: 0x04002828 RID: 10280
			public Texture2D m_hao_in_GA;

			// Token: 0x04002829 RID: 10281
			public float m_tiling;

			// Token: 0x0400282A RID: 10282
			public Vector4 m_color;

			// Token: 0x0400282B RID: 10283
			public float m_normalPower;

			// Token: 0x0400282C RID: 10284
			public float m_aoPower;

			// Token: 0x0400282D RID: 10285
			public float m_geoPower;

			// Token: 0x0400282E RID: 10286
			public float m_heightContrast = 1f;

			// Token: 0x0400282F RID: 10287
			public float m_heightDepth = 1f;

			// Token: 0x04002830 RID: 10288
			public float m_heightBlendClose = 1f;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000682 RID: 1666
	[RequireComponent(typeof(Terrain))]
	[AddComponentMenu("CTS/Add CTS To Terrain")]
	[ExecuteInEditMode]
	[Serializable]
	public class CompleteTerrainShader : MonoBehaviour
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x000E3225 File Offset: 0x000E1625
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x000E3230 File Offset: 0x000E1630
		public CTSProfile Profile
		{
			get
			{
				return this.m_profile;
			}
			set
			{
				if (this.m_terrain == null)
				{
					this.m_terrain = base.transform.GetComponent<Terrain>();
				}
				if (this.m_profile == null)
				{
					this.m_profile = value;
					if (this.m_profile != null)
					{
						if (this.m_profile.TerrainTextures.Count == 0)
						{
							this.UpdateProfileFromTerrainForced();
						}
						else if (this.TerrainNeedsTextureUpdate())
						{
							this.ReplaceTerrainTexturesFromProfile(false);
						}
					}
				}
				else if (value == null)
				{
					this.m_profile = value;
				}
				else
				{
					if (this.m_profile.name != value.name)
					{
						this.m_profile = value;
					}
					if (this.m_profile.TerrainTextures.Count == 0)
					{
						this.UpdateProfileFromTerrainForced();
					}
					else if (this.TerrainNeedsTextureUpdate())
					{
						this.ReplaceTerrainTexturesFromProfile(false);
					}
				}
				if (this.m_profile != null)
				{
					this.ApplyMaterialAndUpdateShader();
				}
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000E3342 File Offset: 0x000E1742
		// (set) Token: 0x06002702 RID: 9986 RVA: 0x000E334C File Offset: 0x000E174C
		public Texture2D NormalMap
		{
			get
			{
				return this.m_normalMap;
			}
			set
			{
				if (value == null)
				{
					if (this.m_normalMap != null)
					{
						this.m_normalMap = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_normalMap == null || this.m_normalMap.GetInstanceID() != value.GetInstanceID())
				{
					this.m_normalMap = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000E33C0 File Offset: 0x000E17C0
		// (set) Token: 0x06002704 RID: 9988 RVA: 0x000E33C8 File Offset: 0x000E17C8
		public bool AutoBakeNormalMap
		{
			get
			{
				return this.m_bakeNormalMap;
			}
			set
			{
				this.m_bakeNormalMap = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000E33D1 File Offset: 0x000E17D1
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x000E33DC File Offset: 0x000E17DC
		public Texture2D ColorMap
		{
			get
			{
				return this.m_colorMap;
			}
			set
			{
				if (value == null)
				{
					if (this.m_colorMap != null)
					{
						this.m_colorMap = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_colorMap == null || this.m_colorMap.GetInstanceID() != value.GetInstanceID())
				{
					this.m_colorMap = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000E3450 File Offset: 0x000E1850
		// (set) Token: 0x06002708 RID: 9992 RVA: 0x000E3458 File Offset: 0x000E1858
		public bool AutoBakeColorMap
		{
			get
			{
				return this.m_bakeColorMap;
			}
			set
			{
				this.m_bakeColorMap = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000E3461 File Offset: 0x000E1861
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x000E3469 File Offset: 0x000E1869
		public bool AutoBakeGrassIntoColorMap
		{
			get
			{
				return this.m_bakeGrassTextures;
			}
			set
			{
				this.m_bakeGrassTextures = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000E3472 File Offset: 0x000E1872
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x000E347A File Offset: 0x000E187A
		public float AutoBakeGrassMixStrength
		{
			get
			{
				return this.m_bakeGrassMixStrength;
			}
			set
			{
				this.m_bakeGrassMixStrength = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000E3483 File Offset: 0x000E1883
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000E348B File Offset: 0x000E188B
		public float AutoBakeGrassDarkenAmount
		{
			get
			{
				return this.m_bakeGrassDarkenAmount;
			}
			set
			{
				this.m_bakeGrassDarkenAmount = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000E3494 File Offset: 0x000E1894
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x000E349C File Offset: 0x000E189C
		public bool UseCutout
		{
			get
			{
				return this.m_useCutout;
			}
			set
			{
				if (this.m_useCutout != value)
				{
					this.m_useCutout = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000E34B9 File Offset: 0x000E18B9
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x000E34C4 File Offset: 0x000E18C4
		public Texture2D CutoutMask
		{
			get
			{
				return this.m_cutoutMask;
			}
			set
			{
				if (value == null)
				{
					if (this.m_cutoutMask != null)
					{
						this.m_cutoutMask = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_cutoutMask == null || this.m_cutoutMask.GetInstanceID() != value.GetInstanceID())
				{
					this.m_cutoutMask = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000E3538 File Offset: 0x000E1938
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x000E3540 File Offset: 0x000E1940
		public float CutoutHeight
		{
			get
			{
				return this.m_cutoutHeight;
			}
			set
			{
				if (this.m_cutoutHeight != value)
				{
					this.m_cutoutHeight = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000E355D File Offset: 0x000E195D
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x000E3568 File Offset: 0x000E1968
		public Texture2D Splat1
		{
			get
			{
				return this.m_splat1;
			}
			set
			{
				if (value == null)
				{
					if (this.m_splat1 != null)
					{
						this.m_splat1 = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_splat1 == null || this.m_splat1.GetInstanceID() != value.GetInstanceID())
				{
					this.m_splat1 = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000E35DC File Offset: 0x000E19DC
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000E35E4 File Offset: 0x000E19E4
		public Texture2D Splat2
		{
			get
			{
				return this.m_splat2;
			}
			set
			{
				if (value == null)
				{
					if (this.m_splat2 != null)
					{
						this.m_splat2 = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_splat2 == null || this.m_splat2.GetInstanceID() != value.GetInstanceID())
				{
					this.m_splat2 = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000E3658 File Offset: 0x000E1A58
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x000E3660 File Offset: 0x000E1A60
		public Texture2D Splat3
		{
			get
			{
				return this.m_splat3;
			}
			set
			{
				if (value == null)
				{
					if (this.m_splat3 != null)
					{
						this.m_splat3 = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_splat3 == null || this.m_splat3.GetInstanceID() != value.GetInstanceID())
				{
					this.m_splat3 = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000E36D4 File Offset: 0x000E1AD4
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x000E36DC File Offset: 0x000E1ADC
		public Texture2D Splat4
		{
			get
			{
				return this.m_splat4;
			}
			set
			{
				if (value == null)
				{
					if (this.m_splat4 != null)
					{
						this.m_splat4 = value;
						CompleteTerrainShader.SetDirty(this, false, false);
					}
				}
				else if (this.m_splat4 == null || this.m_splat4.GetInstanceID() != value.GetInstanceID())
				{
					this.m_splat4 = value;
					CompleteTerrainShader.SetDirty(this, false, false);
				}
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000E3750 File Offset: 0x000E1B50
		private void Awake()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogWarning("CTS needs a terrain to work!");
				}
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000E3790 File Offset: 0x000E1B90
		private void Start()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogWarning("CTS needs a terrain, exiting!");
					return;
				}
			}
			this.ApplyMaterialAndUpdateShader();
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000E37E1 File Offset: 0x000E1BE1
		private void OnEnable()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			CTSSingleton<CTSTerrainManager>.Instance.RegisterShader(this);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000E3810 File Offset: 0x000E1C10
		private void OnDisable()
		{
			CTSSingleton<CTSTerrainManager>.Instance.UnregisterShader(this);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000E381D File Offset: 0x000E1C1D
		public static UnityEngine.Object GetAsset(string fileNameOrPath, Type assetType)
		{
			return null;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000E3820 File Offset: 0x000E1C20
		public static string GetAssetPath(string fileName)
		{
			return string.Empty;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000E3828 File Offset: 0x000E1C28
		public static Type GetType(string TypeName)
		{
			Type type = Type.GetType(TypeName);
			if (type != null)
			{
				return type;
			}
			if (TypeName.Contains("."))
			{
				string assemblyString = TypeName.Substring(0, TypeName.IndexOf('.'));
				try
				{
					Assembly assembly = Assembly.Load(assemblyString);
					if (assembly == null)
					{
						return null;
					}
					type = assembly.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
				catch (Exception)
				{
				}
			}
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly != null)
			{
				type = callingAssembly.GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.GetLength(0); i++)
			{
				type = assemblies[i].GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			AssemblyName[] referencedAssemblies = callingAssembly.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				Assembly assembly2 = Assembly.Load(assemblyRef);
				if (assembly2 != null)
				{
					type = assembly2.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
			}
			return null;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000E3988 File Offset: 0x000E1D88
		private void ApplyUnityShader()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogError("Unable to locate Terrain, apply unity shader cancelled.");
					return;
				}
			}
			if (Application.isPlaying && this.m_profile != null && this.m_splatBackupArray != null && this.m_splatBackupArray.GetLength(0) > 0)
			{
				this.ReplaceTerrainTexturesFromProfile(true);
				this.m_terrain.terrainData.SetAlphamaps(0, 0, this.m_splatBackupArray);
				this.m_terrain.Flush();
			}
			this.m_terrain.basemapDistance = 5000f;
			this.m_activeShaderType = CTSConstants.ShaderType.Unity;
			this.m_terrain.materialType = Terrain.MaterialType.BuiltInStandard;
			this.m_terrain.materialTemplate = null;
			this.m_material = null;
			this.m_materialPropertyBlock = null;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000E3A74 File Offset: 0x000E1E74
		private void ApplyMaterial()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogWarning("CTS needs terrain to function - exiting!");
					return;
				}
			}
			if (this.m_profile == null)
			{
				UnityEngine.Debug.LogWarning("CTS needs a profile to function - applying unity shader and exiting!");
				this.ApplyUnityShader();
				return;
			}
			if (this.m_profile.AlbedosTextureArray == null)
			{
				UnityEngine.Debug.LogWarning("CTS profile needs albedos texture array to function - applying unity shader and exiting!");
				this.m_profile.m_needsAlbedosArrayUpdate = true;
				this.ApplyUnityShader();
				return;
			}
			if (this.m_profile.NormalsTextureArray == null)
			{
				UnityEngine.Debug.LogWarning("CTS profile needs normals texture array to function - applying unity shader and exiting!");
				this.m_profile.m_needsNormalsArrayUpdate = true;
				this.ApplyUnityShader();
				return;
			}
			if (this.m_splat1 == null && this.m_terrain.terrainData.alphamapTextures.Length > 0)
			{
				this.m_splat1 = this.m_terrain.terrainData.alphamapTextures[0];
			}
			if (this.m_splat2 == null && this.m_terrain.terrainData.alphamapTextures.Length > 1)
			{
				this.m_splat2 = this.m_terrain.terrainData.alphamapTextures[1];
			}
			if (this.m_splat3 == null && this.m_terrain.terrainData.alphamapTextures.Length > 2)
			{
				this.m_splat3 = this.m_terrain.terrainData.alphamapTextures[2];
			}
			if (this.m_splat4 == null && this.m_terrain.terrainData.alphamapTextures.Length > 3)
			{
				this.m_splat4 = this.m_terrain.terrainData.alphamapTextures[3];
			}
			this.m_materialPropertyBlock = null;
			this.m_activeShaderType = this.m_profile.ShaderType;
			switch (this.m_activeShaderType)
			{
			case CTSConstants.ShaderType.Unity:
				this.ApplyUnityShader();
				return;
			case CTSConstants.ShaderType.Basic:
				if (!this.m_useCutout)
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Basic", this.m_profile);
				}
				else
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Basic CutOut", this.m_profile);
				}
				break;
			case CTSConstants.ShaderType.Advanced:
				if (!this.m_useCutout)
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Advanced", this.m_profile);
				}
				else
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Advanced CutOut", this.m_profile);
				}
				break;
			case CTSConstants.ShaderType.Tesselation:
				if (!this.m_useCutout)
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Advanced Tess", this.m_profile);
				}
				else
				{
					this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Advanced Tess CutOut", this.m_profile);
				}
				break;
			case CTSConstants.ShaderType.Lite:
				this.m_material = CTSMaterials.GetMaterial("CTS/CTS Terrain Shader Lite", this.m_profile);
				break;
			}
			if (this.m_material == null)
			{
				UnityEngine.Debug.LogErrorFormat("CTS could not locate shader {0} - exiting!", new object[]
				{
					this.m_activeShaderType
				});
				return;
			}
			this.m_terrain.materialType = Terrain.MaterialType.Custom;
			this.m_terrain.materialTemplate = this.m_material;
			this.UpdateTerrainSplatsAtRuntime();
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000E3DC4 File Offset: 0x000E21C4
		public void ApplyMaterialAndUpdateShader()
		{
			if (this.m_profile == null)
			{
				this.ApplyMaterial();
			}
			else if (this.m_activeShaderType != this.m_profile.ShaderType)
			{
				this.ApplyMaterial();
			}
			if (this.m_activeShaderType != CTSConstants.ShaderType.Unity)
			{
				this.UpdateShader();
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000E3E1C File Offset: 0x000E221C
		public void UpdateShader()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogWarning("CTS missing terrain, cannot operate without terrain!");
					return;
				}
			}
			if (this.m_activeShaderType == CTSConstants.ShaderType.Unity)
			{
				this.m_terrain.basemapDistance = 5000f;
				return;
			}
			if (this.m_profile == null)
			{
				UnityEngine.Debug.LogWarning("Missing CTS profile!");
				return;
			}
			if (this.m_profile.AlbedosTextureArray == null)
			{
				UnityEngine.Debug.LogError("Missing CTS texture array - rebake textures");
				return;
			}
			if (this.m_profile.NormalsTextureArray == null)
			{
				UnityEngine.Debug.LogError("Missing CTS texture array - rebake textures");
				return;
			}
			if (this.m_splat1 == null)
			{
				UnityEngine.Debug.LogError("Missing splat textures - add some textures to your terrain");
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (this.m_activeShaderType != this.m_profile.ShaderType || this.m_material == null)
			{
				this.ApplyMaterial();
			}
			if (this.m_stripTexturesAtRuntime != this.m_profile.m_globalStripTexturesAtRuntime)
			{
				this.m_stripTexturesAtRuntime = this.m_profile.m_globalStripTexturesAtRuntime;
				CompleteTerrainShader.SetDirty(this, false, false);
			}
			if (this.m_terrain.basemapDistance != this.m_profile.m_globalBasemapDistance)
			{
				this.m_terrain.basemapDistance = this.m_profile.m_globalBasemapDistance;
			}
			this.m_material.SetTexture(CTSShaderID.Texture_Array_Albedo, this.m_profile.AlbedosTextureArray);
			this.m_material.SetTexture(CTSShaderID.Texture_Array_Normal, this.m_profile.NormalsTextureArray);
			this.m_material.SetFloat(CTSShaderID.UV_Mix_Power, this.m_profile.m_globalUvMixPower);
			this.m_material.SetFloat(CTSShaderID.UV_Mix_Start_Distance, this.m_profile.m_globalUvMixStartDistance + UnityEngine.Random.Range(0.001f, 0.003f));
			this.m_material.SetFloat(CTSShaderID.Perlin_Normal_Tiling_Close, this.m_profile.m_globalDetailNormalCloseTiling);
			this.m_material.SetFloat(CTSShaderID.Perlin_Normal_Tiling_Far, this.m_profile.m_globalDetailNormalFarTiling);
			this.m_material.SetFloat(CTSShaderID.Perlin_Normal_Power, this.m_profile.m_globalDetailNormalFarPower);
			this.m_material.SetFloat(CTSShaderID.Perlin_Normal_Power_Close, this.m_profile.m_globalDetailNormalClosePower);
			this.m_material.SetFloat(CTSShaderID.Terrain_Smoothness, this.m_profile.m_globalTerrainSmoothness);
			this.m_material.SetFloat(CTSShaderID.Terrain_Specular, this.m_profile.m_globalTerrainSpecular);
			this.m_material.SetFloat(CTSShaderID.TessValue, this.m_profile.m_globalTesselationPower);
			this.m_material.SetFloat(CTSShaderID.TessMin, this.m_profile.m_globalTesselationMinDistance);
			this.m_material.SetFloat(CTSShaderID.TessMax, this.m_profile.m_globalTesselationMaxDistance);
			this.m_material.SetFloat(CTSShaderID.TessPhongStrength, this.m_profile.m_globalTesselationPhongStrength);
			this.m_material.SetFloat(CTSShaderID.TessDistance, this.m_profile.m_globalTesselationMaxDistance);
			this.m_material.SetInt(CTSShaderID.Ambient_Occlusion_Type, (int)this.m_profile.m_globalAOType);
			if (this.m_profile.m_globalAOType == CTSConstants.AOType.None)
			{
				this.m_material.DisableKeyword("_Use_AO_ON");
				this.m_material.DisableKeyword("_USE_AO_TEXTURE_ON");
				this.m_material.SetInt(CTSShaderID.Use_AO, 0);
				this.m_material.SetInt(CTSShaderID.Use_AO_Texture, 0);
				this.m_material.SetFloat(CTSShaderID.Ambient_Occlusion_Power, 0f);
			}
			else if (this.m_profile.m_globalAOType == CTSConstants.AOType.NormalMapBased)
			{
				this.m_material.DisableKeyword("_USE_AO_TEXTURE_ON");
				this.m_material.SetInt(CTSShaderID.Use_AO_Texture, 0);
				if (this.m_profile.m_globalAOPower > 0f)
				{
					this.m_material.EnableKeyword("_USE_AO_ON");
					this.m_material.SetInt(CTSShaderID.Use_AO, 1);
					this.m_material.SetFloat(CTSShaderID.Ambient_Occlusion_Power, this.m_profile.m_globalAOPower);
				}
				else
				{
					this.m_material.DisableKeyword("_USE_AO_ON");
					this.m_material.SetInt(CTSShaderID.Use_AO, 0);
					this.m_material.SetFloat(CTSShaderID.Ambient_Occlusion_Power, 0f);
				}
			}
			else if (this.m_profile.m_globalAOPower > 0f)
			{
				this.m_material.EnableKeyword("_USE_AO_ON");
				this.m_material.EnableKeyword("_USE_AO_TEXTURE_ON");
				this.m_material.SetInt(CTSShaderID.Use_AO, 1);
				this.m_material.SetInt(CTSShaderID.Use_AO_Texture, 1);
				this.m_material.SetFloat(CTSShaderID.Ambient_Occlusion_Power, this.m_profile.m_globalAOPower);
			}
			else
			{
				this.m_material.DisableKeyword("_USE_AO_ON");
				this.m_material.DisableKeyword("_USE_AO_TEXTURE_ON");
				this.m_material.SetInt(CTSShaderID.Use_AO, 0);
				this.m_material.SetInt(CTSShaderID.Use_AO_Texture, 0);
				this.m_material.SetFloat(CTSShaderID.Ambient_Occlusion_Power, 0f);
			}
			if (this.m_profile.m_globalDetailNormalClosePower > 0f || this.m_profile.m_globalDetailNormalFarPower > 0f)
			{
				this.m_material.SetInt(CTSShaderID.Texture_Perlin_Normal_Index, this.m_profile.m_globalDetailNormalMapIdx);
			}
			else
			{
				this.m_material.SetInt(CTSShaderID.Texture_Perlin_Normal_Index, -1);
			}
			if (this.m_profile.GeoAlbedo != null)
			{
				if (this.m_profile.m_geoMapClosePower > 0f || this.m_profile.m_geoMapFarPower > 0f)
				{
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Offset_Close, this.m_profile.m_geoMapCloseOffset);
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Close_Power, this.m_profile.m_geoMapClosePower);
					this.m_material.SetFloat(CTSShaderID.Geological_Tiling_Close, this.m_profile.m_geoMapTilingClose);
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Offset_Far, this.m_profile.m_geoMapFarOffset);
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Far_Power, this.m_profile.m_geoMapFarPower);
					this.m_material.SetFloat(CTSShaderID.Geological_Tiling_Far, this.m_profile.m_geoMapTilingFar);
					this.m_material.SetTexture(CTSShaderID.Texture_Geological_Map, this.m_profile.GeoAlbedo);
				}
				else
				{
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Close_Power, 0f);
					this.m_material.SetFloat(CTSShaderID.Geological_Map_Far_Power, 0f);
					this.m_material.SetTexture(CTSShaderID.Texture_Geological_Map, null);
				}
			}
			else
			{
				this.m_material.SetFloat(CTSShaderID.Geological_Map_Close_Power, 0f);
				this.m_material.SetFloat(CTSShaderID.Geological_Map_Far_Power, 0f);
				this.m_material.SetTexture(CTSShaderID.Texture_Geological_Map, null);
			}
			this.m_material.SetFloat(CTSShaderID.Snow_Amount, this.m_profile.m_snowAmount);
			this.m_material.SetInt(CTSShaderID.Texture_Snow_Index, this.m_profile.m_snowAlbedoTextureIdx);
			this.m_material.SetInt(CTSShaderID.Texture_Snow_Normal_Index, this.m_profile.m_snowNormalTextureIdx);
			this.m_material.SetInt(CTSShaderID.Texture_Snow_H_AO_Index, (this.m_profile.m_snowHeightTextureIdx == -1) ? this.m_profile.m_snowAOTextureIdx : this.m_profile.m_snowHeightTextureIdx);
			this.m_material.SetTexture(CTSShaderID.Texture_Glitter, this.m_profile.SnowGlitter);
			this.m_material.SetFloat(CTSShaderID.Snow_Maximum_Angle, this.m_profile.m_snowMaxAngle);
			this.m_material.SetFloat(CTSShaderID.Snow_Maximum_Angle_Hardness, this.m_profile.m_snowMaxAngleHardness);
			this.m_material.SetFloat(CTSShaderID.Snow_Min_Height, this.m_profile.m_snowMinHeight);
			this.m_material.SetFloat(CTSShaderID.Snow_Min_Height_Blending, this.m_profile.m_snowMinHeightBlending);
			this.m_material.SetFloat(CTSShaderID.Snow_Noise_Power, this.m_profile.m_snowNoisePower);
			this.m_material.SetFloat(CTSShaderID.Snow_Noise_Tiling, this.m_profile.m_snowNoiseTiling);
			this.m_material.SetFloat(CTSShaderID.Snow_Normal_Scale, this.m_profile.m_snowNormalScale);
			this.m_material.SetFloat(CTSShaderID.Snow_Perlin_Power, this.m_profile.m_snowDetailPower);
			this.m_material.SetFloat(CTSShaderID.Snow_Tiling, this.m_profile.m_snowTilingClose);
			this.m_material.SetFloat(CTSShaderID.Snow_Tiling_Far_Multiplier, this.m_profile.m_snowTilingFar);
			this.m_material.SetFloat(CTSShaderID.Snow_Brightness, this.m_profile.m_snowBrightness);
			this.m_material.SetFloat(CTSShaderID.Snow_Blend_Normal, this.m_profile.m_snowBlendNormal);
			this.m_material.SetFloat(CTSShaderID.Snow_Smoothness, this.m_profile.m_snowSmoothness);
			this.m_material.SetFloat(CTSShaderID.Snow_Specular, this.m_profile.m_snowSpecular);
			this.m_material.SetFloat(CTSShaderID.Snow_Heightblend_Close, this.m_profile.m_snowHeightmapBlendClose);
			this.m_material.SetFloat(CTSShaderID.Snow_Heightblend_Far, this.m_profile.m_snowHeightmapBlendFar);
			this.m_material.SetFloat(CTSShaderID.Snow_Height_Contrast, this.m_profile.m_snowHeightmapContrast);
			this.m_material.SetFloat(CTSShaderID.Snow_Heightmap_Depth, this.m_profile.m_snowHeightmapDepth);
			this.m_material.SetFloat(CTSShaderID.Snow_Heightmap_MinHeight, this.m_profile.m_snowHeightmapMinValue);
			this.m_material.SetFloat(CTSShaderID.Snow_Heightmap_MaxHeight, this.m_profile.m_snowHeightmapMaxValue);
			this.m_material.SetFloat(CTSShaderID.Snow_Ambient_Occlusion_Power, this.m_profile.m_snowAOStrength);
			this.m_material.SetFloat(CTSShaderID.Snow_Tesselation_Depth, this.m_profile.m_snowTesselationDepth);
			this.m_material.SetVector(CTSShaderID.Snow_Color, new Vector4(this.m_profile.m_snowTint.r * this.m_profile.m_snowBrightness, this.m_profile.m_snowTint.g * this.m_profile.m_snowBrightness, this.m_profile.m_snowTint.b * this.m_profile.m_snowBrightness, this.m_profile.m_snowSmoothness));
			this.m_material.SetVector(CTSShaderID.Texture_Snow_Average, this.m_profile.m_snowAverage);
			this.m_material.SetFloat(CTSShaderID.Glitter_Color_Power, this.m_profile.m_snowGlitterColorPower);
			this.m_material.SetFloat(CTSShaderID.Glitter_Noise_Threshold, this.m_profile.m_snowGlitterNoiseThreshold);
			this.m_material.SetFloat(CTSShaderID.Glitter_Specular, this.m_profile.m_snowGlitterSpecularPower);
			this.m_material.SetFloat(CTSShaderID.Glitter_Smoothness, this.m_profile.m_snowGlitterSmoothness);
			this.m_material.SetFloat(CTSShaderID.Glitter_Refreshing_Speed, this.m_profile.m_snowGlitterRefreshSpeed);
			this.m_material.SetFloat(CTSShaderID.Glitter_Tiling, this.m_profile.m_snowGlitterTiling);
			for (int i = 0; i < this.m_profile.TerrainTextures.Count; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_profile.TerrainTextures[i];
				this.m_material.SetInt(CTSShaderID.Texture_X_Albedo_Index[i], ctsterrainTextureDetails.m_albedoIdx);
				this.m_material.SetInt(CTSShaderID.Texture_X_Normal_Index[i], ctsterrainTextureDetails.m_normalIdx);
				this.m_material.SetInt(CTSShaderID.Texture_X_H_AO_Index[i], (ctsterrainTextureDetails.m_heightIdx == -1) ? ctsterrainTextureDetails.m_aoIdx : ctsterrainTextureDetails.m_heightIdx);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Tiling[i], ctsterrainTextureDetails.m_albedoTilingClose);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Far_Multiplier[i], ctsterrainTextureDetails.m_albedoTilingFar);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Perlin_Power[i], ctsterrainTextureDetails.m_detailPower);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Snow_Reduction[i], ctsterrainTextureDetails.m_snowReductionPower);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Geological_Power[i], ctsterrainTextureDetails.m_geologicalPower);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Heightmap_Depth[i], ctsterrainTextureDetails.m_heightDepth);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Height_Contrast[i], ctsterrainTextureDetails.m_heightContrast);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Heightblend_Close[i], ctsterrainTextureDetails.m_heightBlendClose);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Heightblend_Far[i], ctsterrainTextureDetails.m_heightBlendFar);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Tesselation_Depth[i], ctsterrainTextureDetails.m_heightTesselationDepth);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Heightmap_MinHeight[i], ctsterrainTextureDetails.m_heightMin);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Heightmap_MaxHeight[i], ctsterrainTextureDetails.m_heightMax);
				this.m_material.SetFloat(CTSShaderID.Texture_X_AO_Power[i], ctsterrainTextureDetails.m_aoPower);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Normal_Power[i], ctsterrainTextureDetails.m_normalStrength);
				this.m_material.SetFloat(CTSShaderID.Texture_X_Triplanar[i], (!ctsterrainTextureDetails.m_triplanar) ? 0f : 1f);
				this.m_material.SetVector(CTSShaderID.Texture_X_Average[i], ctsterrainTextureDetails.m_albedoAverage);
				this.m_material.SetVector(CTSShaderID.Texture_X_Color[i], new Vector4(ctsterrainTextureDetails.m_tint.r * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_tint.g * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_tint.b * ctsterrainTextureDetails.m_tintBrightness, ctsterrainTextureDetails.m_smoothness));
			}
			for (int j = this.m_profile.TerrainTextures.Count; j < 16; j++)
			{
				this.m_material.SetInt(CTSShaderID.Texture_X_Albedo_Index[j], -1);
				this.m_material.SetInt(CTSShaderID.Texture_X_Normal_Index[j], -1);
				this.m_material.SetInt(CTSShaderID.Texture_X_H_AO_Index[j], -1);
			}
			if (this.m_profile.m_useMaterialControlBlock)
			{
				if (this.m_materialPropertyBlock == null)
				{
					this.m_materialPropertyBlock = new MaterialPropertyBlock();
				}
				this.m_terrain.GetSplatMaterialPropertyBlock(this.m_materialPropertyBlock);
				this.m_materialPropertyBlock.SetTexture(CTSShaderID.Texture_Splat_1, this.m_splat1);
				if (this.m_splat2 != null)
				{
					this.m_materialPropertyBlock.SetTexture(CTSShaderID.Texture_Splat_2, this.m_splat2);
				}
				if (this.m_splat3 != null)
				{
					this.m_materialPropertyBlock.SetTexture(CTSShaderID.Texture_Splat_3, this.m_splat3);
				}
				if (this.m_splat4 != null)
				{
					this.m_materialPropertyBlock.SetTexture(CTSShaderID.Texture_Splat_4, this.m_splat4);
				}
				this.m_materialPropertyBlock.SetFloat(CTSShaderID.Remove_Vert_Height, this.m_cutoutHeight);
				if (this.m_cutoutMask != null)
				{
					this.m_materialPropertyBlock.SetTexture(CTSShaderID.Texture_Additional_Masks, this.m_cutoutMask);
				}
				if (this.NormalMap != null)
				{
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Normalmap_Power, this.m_profile.m_globalNormalPower);
					if (this.m_profile.m_globalNormalPower > 0f && this.NormalMap != null)
					{
						this.m_materialPropertyBlock.SetTexture(CTSShaderID.Global_Normal_Map, this.NormalMap);
					}
				}
				else
				{
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Normalmap_Power, 0f);
				}
				if (this.ColorMap != null)
				{
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Map_Far_Power, this.m_profile.m_colorMapFarPower);
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Map_Close_Power, this.m_profile.m_colorMapClosePower);
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Opacity_Power, this.m_profile.m_colorMapOpacity);
					if (this.m_profile.m_colorMapFarPower > 0f || this.m_profile.m_colorMapClosePower > 0f)
					{
						this.m_materialPropertyBlock.SetTexture(CTSShaderID.Global_Color_Map, this.ColorMap);
					}
				}
				else
				{
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Map_Far_Power, 0f);
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Map_Close_Power, 0f);
					this.m_materialPropertyBlock.SetFloat(CTSShaderID.Global_Color_Opacity_Power, 0f);
				}
				this.m_terrain.SetSplatMaterialPropertyBlock(this.m_materialPropertyBlock);
			}
			else
			{
				this.m_material.SetTexture(CTSShaderID.Texture_Splat_1, this.m_splat1);
				if (this.m_splat2 != null)
				{
					this.m_material.SetTexture(CTSShaderID.Texture_Splat_2, this.m_splat2);
				}
				if (this.m_splat3 != null)
				{
					this.m_material.SetTexture(CTSShaderID.Texture_Splat_3, this.m_splat3);
				}
				if (this.m_splat4 != null)
				{
					this.m_material.SetTexture(CTSShaderID.Texture_Splat_4, this.m_splat4);
				}
				this.m_material.SetFloat(CTSShaderID.Remove_Vert_Height, this.m_cutoutHeight);
				if (this.m_cutoutMask != null)
				{
					this.m_material.SetTexture(CTSShaderID.Texture_Additional_Masks, this.m_cutoutMask);
				}
				if (this.NormalMap != null)
				{
					this.m_material.SetFloat(CTSShaderID.Global_Normalmap_Power, this.m_profile.m_globalNormalPower);
					if (this.m_profile.m_globalNormalPower > 0f && this.NormalMap != null)
					{
						this.m_material.SetTexture(CTSShaderID.Global_Normal_Map, this.NormalMap);
					}
				}
				else
				{
					this.m_material.SetFloat(CTSShaderID.Global_Normalmap_Power, 0f);
				}
				if (this.ColorMap != null)
				{
					this.m_material.SetFloat(CTSShaderID.Global_Color_Map_Far_Power, this.m_profile.m_colorMapFarPower);
					this.m_material.SetFloat(CTSShaderID.Global_Color_Map_Close_Power, this.m_profile.m_colorMapClosePower);
					this.m_material.SetFloat(CTSShaderID.Global_Color_Opacity_Power, this.m_profile.m_colorMapOpacity);
					if (this.m_profile.m_colorMapFarPower > 0f || this.m_profile.m_colorMapClosePower > 0f)
					{
						this.m_material.SetTexture(CTSShaderID.Global_Color_Map, this.ColorMap);
					}
				}
				else
				{
					this.m_material.SetFloat(CTSShaderID.Global_Color_Map_Far_Power, 0f);
					this.m_material.SetFloat(CTSShaderID.Global_Color_Map_Close_Power, 0f);
					this.m_material.SetFloat(CTSShaderID.Global_Color_Opacity_Power, 0f);
				}
			}
			if (stopwatch.ElapsedMilliseconds > 5L)
			{
			}
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000E50E0 File Offset: 0x000E34E0
		public void UpdateProfileFromTerrainForced()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					UnityEngine.Debug.LogError("CTS is missing terrain, cannot update.");
					return;
				}
			}
			this.m_profile.UpdateSettingsFromTerrain(this.m_terrain, true);
			this.ApplyMaterialAndUpdateShader();
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000E5144 File Offset: 0x000E3544
		private bool ProfileNeedsTextureUpdate()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain == null)
			{
				UnityEngine.Debug.LogWarning("No terrain , unable to check if needs texture update");
				return false;
			}
			if (this.m_profile == null)
			{
				UnityEngine.Debug.LogWarning("No profile, unable to check if needs texture update");
				return false;
			}
			if (this.m_profile.TerrainTextures.Count == 0)
			{
				return false;
			}
			SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
			if (this.m_profile.TerrainTextures.Count != splatPrototypes.Length)
			{
				return true;
			}
			for (int i = 0; i < splatPrototypes.Length; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_profile.TerrainTextures[i];
				SplatPrototype splatPrototype = splatPrototypes[i];
				if (ctsterrainTextureDetails.Albedo == null)
				{
					if (splatPrototype.texture != null)
					{
						return true;
					}
				}
				else
				{
					if (splatPrototype.texture == null)
					{
						return true;
					}
					if (ctsterrainTextureDetails.Albedo.name != splatPrototype.texture.name)
					{
						return true;
					}
				}
				if (ctsterrainTextureDetails.Normal == null)
				{
					if (splatPrototype.normalMap != null)
					{
						return true;
					}
				}
				else
				{
					if (splatPrototype.normalMap == null)
					{
						return true;
					}
					if (ctsterrainTextureDetails.Normal.name != splatPrototype.normalMap.name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000E52D8 File Offset: 0x000E36D8
		private bool TerrainNeedsTextureUpdate()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain == null)
			{
				UnityEngine.Debug.LogWarning("No terrain , unable to check if needs texture update");
				return false;
			}
			if (this.m_profile == null)
			{
				UnityEngine.Debug.LogWarning("No profile, unable to check if needs texture update");
				return false;
			}
			if (this.m_profile.TerrainTextures.Count == 0)
			{
				return false;
			}
			SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
			if (this.m_profile.TerrainTextures.Count != splatPrototypes.Length)
			{
				return true;
			}
			for (int i = 0; i < splatPrototypes.Length; i++)
			{
				CTSTerrainTextureDetails ctsterrainTextureDetails = this.m_profile.TerrainTextures[i];
				SplatPrototype splatPrototype = splatPrototypes[i];
				if (ctsterrainTextureDetails.Albedo == null)
				{
					if (splatPrototype.texture != null)
					{
						return true;
					}
				}
				else
				{
					if (splatPrototype.texture == null)
					{
						return true;
					}
					if (ctsterrainTextureDetails.Albedo.name != splatPrototype.texture.name)
					{
						return true;
					}
					if (ctsterrainTextureDetails.m_albedoTilingClose != splatPrototype.tileSize.x)
					{
						return true;
					}
				}
				if (ctsterrainTextureDetails.Normal == null)
				{
					if (splatPrototype.normalMap != null)
					{
						return true;
					}
				}
				else
				{
					if (splatPrototype.normalMap == null)
					{
						return true;
					}
					if (ctsterrainTextureDetails.Normal.name != splatPrototype.normalMap.name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000E5488 File Offset: 0x000E3888
		public void ReplaceAlbedoInTerrain(Texture2D texture, int textureIdx, float tiling)
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain != null)
			{
				SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
				if (textureIdx >= 0 && textureIdx < splatPrototypes.Length)
				{
					splatPrototypes[textureIdx].texture = texture;
					splatPrototypes[textureIdx].tileSize = new Vector2(tiling, tiling);
					this.m_terrain.terrainData.splatPrototypes = splatPrototypes;
					this.m_terrain.Flush();
					CompleteTerrainShader.SetDirty(this.m_terrain, false, false);
				}
				else
				{
					UnityEngine.Debug.LogWarning("Invalid texture index in replace albedo");
				}
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000E553C File Offset: 0x000E393C
		public void ReplaceNormalInTerrain(Texture2D texture, int textureIdx, float tiling)
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain != null)
			{
				SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
				if (textureIdx >= 0 && textureIdx < splatPrototypes.Length)
				{
					splatPrototypes[textureIdx].normalMap = texture;
					splatPrototypes[textureIdx].tileSize = new Vector2(tiling, tiling);
					this.m_terrain.terrainData.splatPrototypes = splatPrototypes;
					this.m_terrain.Flush();
					CompleteTerrainShader.SetDirty(this.m_terrain, false, false);
				}
				else
				{
					UnityEngine.Debug.LogWarning("Invalid texture index in replace normal!");
				}
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000E55F0 File Offset: 0x000E39F0
		public void BakeTerrainNormals()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain == null)
			{
				UnityEngine.Debug.LogWarning("Could not make terrain normal, as terrain object not set.");
				return;
			}
			Texture2D texture2D = this.CalculateNormals(this.m_terrain);
			texture2D.name = this.m_terrain.name + " Nrm";
			this.NormalMap = texture2D;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000E566C File Offset: 0x000E3A6C
		public Texture2D CalculateNormals(Terrain terrain)
		{
			int heightmapWidth = terrain.terrainData.heightmapWidth;
			int heightmapHeight = terrain.terrainData.heightmapHeight;
			float num = 1f / ((float)heightmapWidth - 1f);
			float num2 = 1f / ((float)heightmapHeight - 1f);
			float num3 = (float)heightmapWidth / 2f;
			float num4 = num3 / (float)heightmapWidth;
			float num5 = num3 / (float)heightmapHeight;
			float[] array = new float[heightmapWidth * heightmapHeight];
			Buffer.BlockCopy(terrain.terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight), 0, array, 0, array.Length * 4);
			Texture2D texture2D = new Texture2D(heightmapWidth, heightmapHeight, TextureFormat.RGBAFloat, false, true);
			for (int i = 0; i < heightmapHeight; i++)
			{
				for (int j = 0; j < heightmapWidth; j++)
				{
					int num6 = (j != heightmapWidth - 1) ? (j + 1) : j;
					int num7 = (j != 0) ? (j - 1) : j;
					int num8 = (i != heightmapHeight - 1) ? (i + 1) : i;
					int num9 = (i != 0) ? (i - 1) : i;
					float num10 = array[num7 + i * heightmapWidth] * num4;
					float num11 = array[num6 + i * heightmapWidth] * num4;
					float num12 = array[j + num9 * heightmapWidth] * num5;
					float num13 = array[j + num8 * heightmapWidth] * num5;
					float num14 = (num11 - num10) / (2f * num);
					float num15 = (num13 - num12) / (2f * num2);
					Vector3 vector;
					vector.x = -num14;
					vector.y = -num15;
					vector.z = 1f;
					vector.Normalize();
					Color color;
					color.r = vector.x * 0.5f + 0.5f;
					color.g = vector.y * 0.5f + 0.5f;
					color.b = vector.z;
					color.a = 1f;
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000E5864 File Offset: 0x000E3C64
		public void BakeTerrainBaseMap()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain == null)
			{
				UnityEngine.Debug.LogWarning("Could not make terrain base map, as terrain object not set.");
				return;
			}
			int num = 2048;
			int num2 = 2048;
			Texture2D[] alphamapTextures = this.m_terrain.terrainData.alphamapTextures;
			SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
			if (alphamapTextures.Length > 0)
			{
				num = alphamapTextures[0].width;
				num2 = alphamapTextures[0].height;
			}
			float num3 = (float)(num * num2);
			Color[] array = new Color[splatPrototypes.Length];
			for (int i = 0; i < splatPrototypes.Length; i++)
			{
				SplatPrototype splatPrototype = splatPrototypes[i];
				Texture2D texture2D = CTSProfile.ResizeTexture(splatPrototype.texture, TextureFormat.ARGB32, 8, num, num2, true, false, false);
				Color[] pixels = texture2D.GetPixels(texture2D.mipmapCount - 1);
				array[i] = new Color(pixels[0].r, pixels[0].g, pixels[0].b, pixels[0].a);
			}
			Texture2D texture2D2 = new Texture2D(num, num2, TextureFormat.RGBA32, false);
			texture2D2.name = this.m_terrain.name + "_BaseMap";
			texture2D2.wrapMode = TextureWrapMode.Repeat;
			texture2D2.filterMode = FilterMode.Bilinear;
			texture2D2.anisoLevel = 8;
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num2; k++)
				{
					int num4 = 0;
					Color color = Color.black;
					foreach (Texture2D texture2D3 in alphamapTextures)
					{
						Color pixel = texture2D3.GetPixel(j, k);
						if (num4 < array.Length)
						{
							color = Color.Lerp(color, array[num4++], pixel.r);
						}
						if (num4 < array.Length)
						{
							color = Color.Lerp(color, array[num4++], pixel.g);
						}
						if (num4 < array.Length)
						{
							color = Color.Lerp(color, array[num4++], pixel.b);
						}
						if (num4 < array.Length)
						{
							color = Color.Lerp(color, array[num4++], pixel.a);
						}
						color.a = 1f;
					}
					texture2D2.SetPixel(j, k, color);
				}
			}
			texture2D2.Apply();
			this.ColorMap = texture2D2;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000E5B18 File Offset: 0x000E3F18
		public void BakeTerrainBaseMapWithGrass()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
			}
			if (this.m_terrain == null)
			{
				UnityEngine.Debug.LogWarning("Could not make terrain base map, as terrain object not set.");
				return;
			}
			int num = 2048;
			int num2 = 2048;
			Texture2D[] alphamapTextures = this.m_terrain.terrainData.alphamapTextures;
			SplatPrototype[] splatPrototypes = this.m_terrain.terrainData.splatPrototypes;
			if (alphamapTextures.Length > 0)
			{
				num = alphamapTextures[0].width;
				num2 = alphamapTextures[0].height;
			}
			float num3 = (float)(num * num2);
			Color[] array = new Color[splatPrototypes.Length];
			for (int i = 0; i < splatPrototypes.Length; i++)
			{
				SplatPrototype splatPrototype = splatPrototypes[i];
				Texture2D texture2D = CTSProfile.ResizeTexture(splatPrototype.texture, TextureFormat.ARGB32, 8, num, num2, true, false, false);
				Color[] pixels = texture2D.GetPixels(texture2D.mipmapCount - 1);
				array[i] = new Color(pixels[0].r, pixels[0].g, pixels[0].b, pixels[0].a);
			}
			List<Color> list = new List<Color>();
			DetailPrototype[] detailPrototypes = this.m_terrain.terrainData.detailPrototypes;
			List<CTSHeightMap> list2 = new List<CTSHeightMap>();
			int detailWidth = this.m_terrain.terrainData.detailWidth;
			int detailHeight = this.m_terrain.terrainData.detailHeight;
			for (int j = 0; j < detailPrototypes.Length; j++)
			{
				DetailPrototype detailPrototype = detailPrototypes[j];
				if (!detailPrototype.usePrototypeMesh && detailPrototype.prototypeTexture != null)
				{
					list2.Add(new CTSHeightMap(this.m_terrain.terrainData.GetDetailLayer(0, 0, detailWidth, detailHeight, j)));
					Texture2D texture2D2 = CTSProfile.ResizeTexture(detailPrototype.prototypeTexture, TextureFormat.ARGB32, 8, detailWidth, detailHeight, true, false, false);
					Color[] pixels2 = texture2D2.GetPixels(texture2D2.mipmapCount - 1);
					Color color = new Color(pixels2[0].r, pixels2[0].g, pixels2[0].b, 1f);
					Color b = Color.Lerp(detailPrototype.healthyColor, detailPrototype.dryColor, 0.2f);
					color = Color.Lerp(color, b, 0.3f);
					list.Add(color);
				}
			}
			Texture2D texture2D3 = new Texture2D(num, num2, TextureFormat.RGBA32, false);
			texture2D3.name = this.m_terrain.name + "_BaseMap";
			texture2D3.wrapMode = TextureWrapMode.Repeat;
			texture2D3.filterMode = FilterMode.Bilinear;
			texture2D3.anisoLevel = 8;
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < num2; l++)
				{
					int num4 = 0;
					Color color2 = Color.black;
					foreach (Texture2D texture2D4 in alphamapTextures)
					{
						Color pixel = texture2D4.GetPixel(k, l);
						if (num4 < array.Length)
						{
							color2 = Color.Lerp(color2, array[num4++], pixel.r);
						}
						if (num4 < array.Length)
						{
							color2 = Color.Lerp(color2, array[num4++], pixel.g);
						}
						if (num4 < array.Length)
						{
							color2 = Color.Lerp(color2, array[num4++], pixel.b);
						}
						if (num4 < array.Length)
						{
							color2 = Color.Lerp(color2, array[num4++], pixel.a);
						}
					}
					for (int n = 0; n < list.Count; n++)
					{
						CTSHeightMap ctsheightMap = list2[n];
						float t = ctsheightMap[(float)l / (float)num2, (float)k / (float)num] * this.m_bakeGrassMixStrength;
						color2 = Color.Lerp(color2, Color.Lerp(list[n], Color.black, this.m_bakeGrassDarkenAmount), t);
					}
					color2.a = 1f;
					texture2D3.SetPixel(k, l, color2);
				}
			}
			texture2D3.Apply();
			this.ColorMap = texture2D3;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000E5F68 File Offset: 0x000E4368
		private void UpdateTerrainSplatsAtRuntime()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.m_terrain == null)
			{
				return;
			}
			if (this.m_profile != null)
			{
				this.m_stripTexturesAtRuntime = this.m_profile.m_globalStripTexturesAtRuntime;
			}
			if (!this.m_stripTexturesAtRuntime)
			{
				return;
			}
			if (this.m_splatBackupArray == null || this.m_splatBackupArray.GetLength(0) == 0)
			{
				this.m_splatBackupArray = this.m_terrain.terrainData.GetAlphamaps(0, 0, this.m_terrain.terrainData.alphamapWidth, this.m_terrain.terrainData.alphamapHeight);
			}
			if (this.m_terrain.materialType != Terrain.MaterialType.Custom)
			{
				return;
			}
			if (!this.m_terrain.terrainData.name.EndsWith("_copy"))
			{
				TerrainData terrainData = new TerrainData();
				terrainData.name = this.m_terrain.terrainData.name + "_copy";
				terrainData.thickness = this.m_terrain.terrainData.thickness;
				terrainData.alphamapResolution = this.m_terrain.terrainData.alphamapResolution;
				terrainData.baseMapResolution = this.m_terrain.terrainData.baseMapResolution;
				terrainData.SetDetailResolution(this.m_terrain.terrainData.detailResolution, 64);
				terrainData.detailPrototypes = this.m_terrain.terrainData.detailPrototypes;
				for (int i = 0; i < terrainData.detailPrototypes.Length; i++)
				{
					terrainData.SetDetailLayer(0, 0, i, this.m_terrain.terrainData.GetDetailLayer(0, 0, terrainData.detailResolution, terrainData.detailResolution, i));
				}
				terrainData.wavingGrassAmount = this.m_terrain.terrainData.wavingGrassAmount;
				terrainData.wavingGrassSpeed = this.m_terrain.terrainData.wavingGrassSpeed;
				terrainData.wavingGrassStrength = this.m_terrain.terrainData.wavingGrassStrength;
				terrainData.wavingGrassTint = this.m_terrain.terrainData.wavingGrassTint;
				terrainData.treePrototypes = this.m_terrain.terrainData.treePrototypes;
				terrainData.treeInstances = this.m_terrain.terrainData.treeInstances;
				terrainData.heightmapResolution = this.m_terrain.terrainData.heightmapResolution;
				terrainData.SetHeights(0, 0, this.m_terrain.terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution));
				terrainData.size = this.m_terrain.terrainData.size;
				this.m_terrain.terrainData = terrainData;
				this.m_terrain.Flush();
				TerrainCollider component = this.m_terrain.gameObject.GetComponent<TerrainCollider>();
				if (component != null)
				{
					component.terrainData = terrainData;
				}
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000E6230 File Offset: 0x000E4630
		private void ReplaceTerrainTexturesFromProfile(bool ignoreStripTextures)
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.transform.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					return;
				}
			}
			if (this.m_profile == null)
			{
				UnityEngine.Debug.LogWarning("No profile, unable to replace terrain textures");
				return;
			}
			if (this.m_profile.TerrainTextures.Count == 0)
			{
				UnityEngine.Debug.LogWarning("No profile textures, unable to replace terrain textures");
				return;
			}
			this.m_stripTexturesAtRuntime = this.m_profile.m_globalStripTexturesAtRuntime;
			if (Application.isPlaying && !ignoreStripTextures && this.m_stripTexturesAtRuntime)
			{
				return;
			}
			SplatPrototype[] array = new SplatPrototype[this.m_profile.TerrainTextures.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SplatPrototype();
				array[i].texture = this.m_profile.TerrainTextures[i].Albedo;
				array[i].normalMap = this.m_profile.TerrainTextures[i].Normal;
				array[i].tileSize = new Vector2(this.m_profile.TerrainTextures[i].m_albedoTilingClose, this.m_profile.TerrainTextures[i].m_albedoTilingClose);
			}
			this.m_terrain.terrainData.splatPrototypes = array;
			this.m_terrain.Flush();
			CompleteTerrainShader.SetDirty(this.m_terrain, false, false);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000E63AA File Offset: 0x000E47AA
		public static string GetCTSDirectory()
		{
			if (string.IsNullOrEmpty(CompleteTerrainShader.s_ctsDirectory))
			{
				CompleteTerrainShader.s_ctsDirectory = "Assets/CTS/";
			}
			return CompleteTerrainShader.s_ctsDirectory;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000E63CA File Offset: 0x000E47CA
		public static void SetDirty(UnityEngine.Object obj, bool recordUndo, bool isPlayingAllowed)
		{
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000E63CC File Offset: 0x000E47CC
		public void RemoveWorldSeams()
		{
			Terrain[] activeTerrains = Terrain.activeTerrains;
			if (activeTerrains.Length == 0)
			{
				return;
			}
			float x = activeTerrains[0].terrainData.size.x;
			float z = activeTerrains[0].terrainData.size.z;
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float num3 = float.MaxValue;
			float num4 = -2.1474836E+09f;
			foreach (Terrain terrain in activeTerrains)
			{
				Vector3 position = terrain.GetPosition();
				if (position.x < num)
				{
					num = position.x;
				}
				if (position.z < num3)
				{
					num3 = position.z;
				}
				if (position.x > num2)
				{
					num2 = position.x;
				}
				if (position.z > num4)
				{
					num4 = position.z;
				}
			}
			int num5 = (int)((num2 - num) / x) + 1;
			int num6 = (int)((num4 - num3) / z) + 1;
			Terrain[,] array2 = new Terrain[num5, num6];
			foreach (Terrain terrain2 in activeTerrains)
			{
				Vector3 position2 = terrain2.GetPosition();
				int num7 = num5 - (int)((num2 - position2.x) / x) - 1;
				int num8 = num6 - (int)((num4 - position2.z) / z) - 1;
				array2[num7, num8] = terrain2;
			}
			for (int k = 0; k < num5; k++)
			{
				for (int l = 0; l < num6; l++)
				{
					Terrain right = null;
					Terrain left = null;
					Terrain bottom = null;
					Terrain top = null;
					if (k > 0)
					{
						left = array2[k - 1, l];
					}
					if (k < num5 - 1)
					{
						right = array2[k + 1, l];
					}
					if (l > 0)
					{
						bottom = array2[k, l - 1];
					}
					if (l < num6 - 1)
					{
						top = array2[k, l + 1];
					}
					array2[k, l].SetNeighbors(left, top, right, bottom);
				}
			}
		}

		// Token: 0x04002746 RID: 10054
		[SerializeField]
		private CTSProfile m_profile;

		// Token: 0x04002747 RID: 10055
		[SerializeField]
		private Texture2D m_normalMap;

		// Token: 0x04002748 RID: 10056
		[SerializeField]
		private bool m_bakeNormalMap = true;

		// Token: 0x04002749 RID: 10057
		[SerializeField]
		private Texture2D m_colorMap;

		// Token: 0x0400274A RID: 10058
		[SerializeField]
		private bool m_bakeColorMap;

		// Token: 0x0400274B RID: 10059
		[SerializeField]
		private bool m_bakeGrassTextures;

		// Token: 0x0400274C RID: 10060
		[SerializeField]
		private float m_bakeGrassMixStrength = 0.2f;

		// Token: 0x0400274D RID: 10061
		[SerializeField]
		private float m_bakeGrassDarkenAmount = 0.2f;

		// Token: 0x0400274E RID: 10062
		[SerializeField]
		private bool m_useCutout;

		// Token: 0x0400274F RID: 10063
		[SerializeField]
		private Texture2D m_cutoutMask;

		// Token: 0x04002750 RID: 10064
		[SerializeField]
		private float m_cutoutHeight = 50f;

		// Token: 0x04002751 RID: 10065
		[SerializeField]
		private Texture2D m_splat1;

		// Token: 0x04002752 RID: 10066
		[SerializeField]
		private Texture2D m_splat2;

		// Token: 0x04002753 RID: 10067
		[SerializeField]
		private Texture2D m_splat3;

		// Token: 0x04002754 RID: 10068
		[SerializeField]
		private Texture2D m_splat4;

		// Token: 0x04002755 RID: 10069
		[SerializeField]
		public bool m_stripTexturesAtRuntime = true;

		// Token: 0x04002756 RID: 10070
		[NonSerialized]
		private float[,,] m_splatBackupArray;

		// Token: 0x04002757 RID: 10071
		[SerializeField]
		private CTSConstants.ShaderType m_activeShaderType;

		// Token: 0x04002758 RID: 10072
		[NonSerialized]
		private Terrain m_terrain;

		// Token: 0x04002759 RID: 10073
		[NonSerialized]
		private Material m_material;

		// Token: 0x0400275A RID: 10074
		[NonSerialized]
		private MaterialPropertyBlock m_materialPropertyBlock;

		// Token: 0x0400275B RID: 10075
		private static string s_ctsDirectory;

		// Token: 0x02000683 RID: 1667
		[Flags]
		internal enum TerrainChangedFlags
		{
			// Token: 0x0400275D RID: 10077
			NoChange = 0,
			// Token: 0x0400275E RID: 10078
			Heightmap = 1,
			// Token: 0x0400275F RID: 10079
			TreeInstances = 2,
			// Token: 0x04002760 RID: 10080
			DelayedHeightmapUpdate = 4,
			// Token: 0x04002761 RID: 10081
			FlushEverythingImmediately = 8,
			// Token: 0x04002762 RID: 10082
			RemoveDirtyDetailsImmediately = 16,
			// Token: 0x04002763 RID: 10083
			WillBeDestroyed = 256
		}
	}
}

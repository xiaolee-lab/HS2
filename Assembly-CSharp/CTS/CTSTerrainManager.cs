using System;
using System.Collections.Generic;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069A RID: 1690
	public class CTSTerrainManager : CTSSingleton<CTSTerrainManager>
	{
		// Token: 0x060027F5 RID: 10229 RVA: 0x000ED840 File Offset: 0x000EBC40
		protected CTSTerrainManager()
		{
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000ED85E File Offset: 0x000EBC5E
		public void RegisterShader(CompleteTerrainShader shader)
		{
			this.m_shaderSet.Add(shader);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000ED86D File Offset: 0x000EBC6D
		public void UnregisterShader(CompleteTerrainShader shader)
		{
			this.m_shaderSet.Remove(shader);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000ED87C File Offset: 0x000EBC7C
		public void RegisterWeatherController(CTSWeatherController weatherController)
		{
			this.m_controllerSet.Add(weatherController);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000ED88B File Offset: 0x000EBC8B
		public void UnregisterWeatherController(CTSWeatherController weatherController)
		{
			this.m_controllerSet.Remove(weatherController);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000ED89C File Offset: 0x000EBC9C
		public void AddCTSToAllTerrains()
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				CompleteTerrainShader component = terrain.gameObject.GetComponent<CompleteTerrainShader>();
				if (component == null)
				{
					terrain.gameObject.AddComponent<CompleteTerrainShader>();
					CompleteTerrainShader.SetDirty(terrain, false, false);
				}
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000ED8F4 File Offset: 0x000EBCF4
		public void AddCTSToTerrain(Terrain terrain)
		{
			if (terrain == null)
			{
				return;
			}
			CompleteTerrainShader component = terrain.gameObject.GetComponent<CompleteTerrainShader>();
			if (component == null)
			{
				terrain.gameObject.AddComponent<CompleteTerrainShader>();
				CompleteTerrainShader.SetDirty(terrain, false, false);
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000ED93C File Offset: 0x000EBD3C
		public bool ProfileIsActive(CTSProfile profile)
		{
			if (profile == null)
			{
				return false;
			}
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.Profile != null && completeTerrainShader.Profile.GetInstanceID() == profile.GetInstanceID())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000ED9D0 File Offset: 0x000EBDD0
		public void BroadcastProfileSelect(CTSProfile profile)
		{
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				completeTerrainShader.Profile = profile;
			}
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000EDA2C File Offset: 0x000EBE2C
		public void BroadcastProfileSelect(CTSProfile profile, Terrain terrain)
		{
			if (profile == null || terrain == null)
			{
				return;
			}
			CompleteTerrainShader completeTerrainShader = terrain.gameObject.GetComponent<CompleteTerrainShader>();
			if (completeTerrainShader == null)
			{
				completeTerrainShader = terrain.gameObject.AddComponent<CompleteTerrainShader>();
			}
			completeTerrainShader.Profile = profile;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000EDA80 File Offset: 0x000EBE80
		public void BroadcastProfileUpdate(CTSProfile profile)
		{
			if (profile == null)
			{
				return;
			}
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.Profile != null && completeTerrainShader.Profile.name == profile.name)
				{
					completeTerrainShader.UpdateShader();
				}
			}
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000EDB14 File Offset: 0x000EBF14
		public void BroadcastShaderSetup(CTSProfile profile)
		{
			if (Terrain.activeTerrain != null)
			{
				CompleteTerrainShader component = Terrain.activeTerrain.GetComponent<CompleteTerrainShader>();
				if (component != null && component.Profile != null && component.Profile.name == profile.name)
				{
					component.UpdateProfileFromTerrainForced();
					this.BroadcastProfileUpdate(profile);
					return;
				}
			}
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.Profile != null)
				{
					if (profile == null)
					{
						completeTerrainShader.UpdateProfileFromTerrainForced();
					}
					else if (completeTerrainShader.Profile.name == profile.name)
					{
						completeTerrainShader.UpdateProfileFromTerrainForced();
						this.BroadcastProfileUpdate(profile);
						break;
					}
				}
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000EDC20 File Offset: 0x000EC020
		public void BroadcastBakeTerrains()
		{
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.AutoBakeNormalMap)
				{
					completeTerrainShader.BakeTerrainNormals();
				}
				if (completeTerrainShader.AutoBakeColorMap)
				{
					if (!completeTerrainShader.AutoBakeGrassIntoColorMap)
					{
						completeTerrainShader.BakeTerrainBaseMap();
					}
					else
					{
						completeTerrainShader.BakeTerrainBaseMapWithGrass();
					}
				}
			}
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000EDCB0 File Offset: 0x000EC0B0
		public void BroadcastAlbedoTextureSwitch(CTSProfile profile, Texture2D texture, int textureIdx, float tiling)
		{
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.Profile != null)
				{
					if (profile == null)
					{
						completeTerrainShader.ReplaceAlbedoInTerrain(texture, textureIdx, tiling);
					}
					else if (completeTerrainShader.Profile.name == profile.name)
					{
						completeTerrainShader.ReplaceAlbedoInTerrain(texture, textureIdx, tiling);
					}
				}
			}
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000EDD58 File Offset: 0x000EC158
		public void BroadcastNormalTextureSwitch(CTSProfile profile, Texture2D texture, int textureIdx, float tiling)
		{
			foreach (CompleteTerrainShader completeTerrainShader in this.m_shaderSet)
			{
				if (completeTerrainShader.Profile != null)
				{
					if (profile == null)
					{
						completeTerrainShader.ReplaceNormalInTerrain(texture, textureIdx, tiling);
					}
					else if (completeTerrainShader.Profile.name == profile.name)
					{
						completeTerrainShader.ReplaceNormalInTerrain(texture, textureIdx, tiling);
					}
				}
			}
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000EDE00 File Offset: 0x000EC200
		public void BroadcastWeatherUpdate(CTSWeatherManager manager)
		{
			foreach (CTSWeatherController ctsweatherController in this.m_controllerSet)
			{
				ctsweatherController.ProcessWeatherUpdate(manager);
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000EDE5C File Offset: 0x000EC25C
		public void RemoveWorldSeams()
		{
			if (this.m_shaderSet.Count > 0)
			{
				using (HashSet<CompleteTerrainShader>.Enumerator enumerator = this.m_shaderSet.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						CompleteTerrainShader completeTerrainShader = enumerator.Current;
						completeTerrainShader.RemoveWorldSeams();
					}
				}
			}
		}

		// Token: 0x040028FD RID: 10493
		private HashSet<CompleteTerrainShader> m_shaderSet = new HashSet<CompleteTerrainShader>();

		// Token: 0x040028FE RID: 10494
		private HashSet<CTSWeatherController> m_controllerSet = new HashSet<CTSWeatherController>();
	}
}

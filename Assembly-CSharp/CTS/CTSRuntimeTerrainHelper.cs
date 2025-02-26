using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000696 RID: 1686
	public class CTSRuntimeTerrainHelper : MonoBehaviour
	{
		// Token: 0x060027E9 RID: 10217 RVA: 0x000ECD20 File Offset: 0x000EB120
		private void Awake()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.GetComponent<Terrain>();
			}
			if (this.m_autoApplyProfile)
			{
				if (this.m_terrain == null)
				{
					this.ApplyProfileToActiveTerrains();
				}
				else
				{
					this.ApplyProfileToTerrain();
				}
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000ECD78 File Offset: 0x000EB178
		private void Start()
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.GetComponent<Terrain>();
			}
			if (this.m_autoApplyProfile)
			{
				if (this.m_terrain == null)
				{
					this.ApplyProfileToActiveTerrains();
				}
				else
				{
					this.ApplyProfileToTerrain();
				}
			}
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000ECDCF File Offset: 0x000EB1CF
		private void OnGenerateCompleted(Terrain terrain)
		{
			if (terrain != null)
			{
				this.m_terrain = terrain;
				this.ApplyProfileToTerrain();
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000ECDEA File Offset: 0x000EB1EA
		public void ApplyProfileToTerrain()
		{
			if (this.m_terrain != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.AddCTSToTerrain(this.m_terrain);
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_CTSProfile, this.m_terrain);
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000ECE23 File Offset: 0x000EB223
		public void ApplyProfileToTerrain(Terrain terrain)
		{
			CTSSingleton<CTSTerrainManager>.Instance.AddCTSToTerrain(terrain);
			CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_CTSProfile, terrain);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000ECE41 File Offset: 0x000EB241
		public void ApplyProfileToActiveTerrains()
		{
			CTSSingleton<CTSTerrainManager>.Instance.AddCTSToAllTerrains();
			CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_CTSProfile);
		}

		// Token: 0x04002899 RID: 10393
		public CTSProfile m_CTSProfile;

		// Token: 0x0400289A RID: 10394
		public bool m_autoApplyProfile = true;

		// Token: 0x0400289B RID: 10395
		public Terrain m_terrain;
	}
}

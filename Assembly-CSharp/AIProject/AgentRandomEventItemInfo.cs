using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200001C RID: 28
	public class AgentRandomEventItemInfo : ScriptableObject
	{
		// Token: 0x04000048 RID: 72
		public List<AgentRandomEventItemInfo.Param> param = new List<AgentRandomEventItemInfo.Param>();

		// Token: 0x0200001D RID: 29
		[Serializable]
		public class Param
		{
			// Token: 0x0600002D RID: 45 RVA: 0x00005B39 File Offset: 0x00003F39
			public Param()
			{
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00005B4C File Offset: 0x00003F4C
			public Param(AgentRandomEventItemInfo.Param src)
			{
				this.ID = src.ID;
				this.ItemData = src.ItemData.ToList<AgentRandomEventItemInfo.ItemData>();
			}

			// Token: 0x04000049 RID: 73
			public int ID;

			// Token: 0x0400004A RID: 74
			public List<AgentRandomEventItemInfo.ItemData> ItemData = new List<AgentRandomEventItemInfo.ItemData>();
		}

		// Token: 0x0200001E RID: 30
		[Serializable]
		public class ItemData
		{
			// Token: 0x0400004B RID: 75
			public int nameHash = -1;

			// Token: 0x0400004C RID: 76
			public int sum = 1;
		}
	}
}

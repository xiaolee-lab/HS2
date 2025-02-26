using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000015 RID: 21
	public class AgentAdvScroungeInfo : ScriptableObject
	{
		// Token: 0x0400003C RID: 60
		public List<AgentAdvScroungeInfo.Param> param = new List<AgentAdvScroungeInfo.Param>();

		// Token: 0x02000016 RID: 22
		[Serializable]
		public class Param
		{
			// Token: 0x06000025 RID: 37 RVA: 0x00005A8C File Offset: 0x00003E8C
			public Param()
			{
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00005A9F File Offset: 0x00003E9F
			public Param(AgentAdvScroungeInfo.Param src)
			{
				this.ID = src.ID;
				this.ItemData = src.ItemData.ToList<AgentAdvScroungeInfo.ItemData>();
			}

			// Token: 0x0400003D RID: 61
			public int ID;

			// Token: 0x0400003E RID: 62
			public List<AgentAdvScroungeInfo.ItemData> ItemData = new List<AgentAdvScroungeInfo.ItemData>();
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class ItemData
		{
			// Token: 0x0400003F RID: 63
			public int nameHash = -1;

			// Token: 0x04000040 RID: 64
			public int sum = 1;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000012 RID: 18
	public class AgentAdvPresentInfo : ScriptableObject
	{
		// Token: 0x04000037 RID: 55
		public List<AgentAdvPresentInfo.Param> param = new List<AgentAdvPresentInfo.Param>();

		// Token: 0x02000013 RID: 19
		[Serializable]
		public class Param
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00005A1B File Offset: 0x00003E1B
			public Param()
			{
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00005A2E File Offset: 0x00003E2E
			public Param(AgentAdvPresentInfo.Param src)
			{
				this.ID = src.ID;
				this.ItemID = src.ItemID;
				this.ItemData = src.ItemData.ToList<AgentAdvPresentInfo.ItemData>();
			}

			// Token: 0x04000038 RID: 56
			public int ID;

			// Token: 0x04000039 RID: 57
			public int ItemID;

			// Token: 0x0400003A RID: 58
			public List<AgentAdvPresentInfo.ItemData> ItemData = new List<AgentAdvPresentInfo.ItemData>();
		}

		// Token: 0x02000014 RID: 20
		[Serializable]
		public class ItemData
		{
			// Token: 0x0400003B RID: 59
			public int nameHash = -1;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200000F RID: 15
	public class AgentAdvPresentBirthdayInfo : ScriptableObject
	{
		// Token: 0x04000032 RID: 50
		public List<AgentAdvPresentBirthdayInfo.Param> param = new List<AgentAdvPresentBirthdayInfo.Param>();

		// Token: 0x02000010 RID: 16
		[Serializable]
		public class Param
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000059AA File Offset: 0x00003DAA
			public Param()
			{
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000059BD File Offset: 0x00003DBD
			public Param(AgentAdvPresentBirthdayInfo.Param src)
			{
				this.ID = src.ID;
				this.ItemID = src.ItemID;
				this.ItemData = src.ItemData.ToList<AgentAdvPresentBirthdayInfo.ItemData>();
			}

			// Token: 0x04000033 RID: 51
			public int ID;

			// Token: 0x04000034 RID: 52
			public int ItemID;

			// Token: 0x04000035 RID: 53
			public List<AgentAdvPresentBirthdayInfo.ItemData> ItemData = new List<AgentAdvPresentBirthdayInfo.ItemData>();
		}

		// Token: 0x02000011 RID: 17
		[Serializable]
		public class ItemData
		{
			// Token: 0x04000036 RID: 54
			public int nameHash = -1;
		}
	}
}

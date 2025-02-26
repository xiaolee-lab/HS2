using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000023 RID: 35
	public class FoodEventItemData : ScriptableObject
	{
		// Token: 0x04000059 RID: 89
		public List<FoodEventItemData.Param> param = new List<FoodEventItemData.Param>();

		// Token: 0x02000024 RID: 36
		[Serializable]
		public class Param
		{
			// Token: 0x0400005A RID: 90
			public string Name;

			// Token: 0x0400005B RID: 91
			public int CategoryID;

			// Token: 0x0400005C RID: 92
			public int ItemID;

			// Token: 0x0400005D RID: 93
			public int EventItemID;

			// Token: 0x0400005E RID: 94
			public string DateEventItemID;
		}
	}
}

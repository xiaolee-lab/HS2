using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000030 RID: 48
	public class ItemData_System : ScriptableObject
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00005CD8 File Offset: 0x000040D8
		public static StuffItemInfo Convert(int category, ItemData_System.Param param)
		{
			return new StuffItemInfo(category, new ItemData.Param
			{
				ID = param.ID,
				IconID = param.IconID,
				Name = param.Name,
				Grade = param.Grade,
				Rate = param.Rate,
				Explanation = param.Explanation
			}, true);
		}

		// Token: 0x040000BC RID: 188
		public List<ItemData_System.Param> param = new List<ItemData_System.Param>();

		// Token: 0x02000031 RID: 49
		[Serializable]
		public class Param
		{
			// Token: 0x040000BD RID: 189
			public int ID = -1;

			// Token: 0x040000BE RID: 190
			public int IconID = -1;

			// Token: 0x040000BF RID: 191
			public string Name = string.Empty;

			// Token: 0x040000C0 RID: 192
			public int Grade;

			// Token: 0x040000C1 RID: 193
			public int Rate;

			// Token: 0x040000C2 RID: 194
			public string Explanation = string.Empty;
		}
	}
}

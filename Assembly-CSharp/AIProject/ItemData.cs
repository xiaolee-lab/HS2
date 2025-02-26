using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200002E RID: 46
	public class ItemData : ScriptableObject
	{
		// Token: 0x040000B4 RID: 180
		public List<ItemData.Param> param = new List<ItemData.Param>();

		// Token: 0x0200002F RID: 47
		[Serializable]
		public class Param
		{
			// Token: 0x040000B5 RID: 181
			public int ID = -1;

			// Token: 0x040000B6 RID: 182
			public int IconID = -1;

			// Token: 0x040000B7 RID: 183
			public string Name = string.Empty;

			// Token: 0x040000B8 RID: 184
			public int Grade;

			// Token: 0x040000B9 RID: 185
			public int Rate;

			// Token: 0x040000BA RID: 186
			public string Explanation = string.Empty;

			// Token: 0x040000BB RID: 187
			public int nameHash = -1;
		}
	}
}

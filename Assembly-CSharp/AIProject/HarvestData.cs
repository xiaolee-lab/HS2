using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200002B RID: 43
	public class HarvestData : ScriptableObject
	{
		// Token: 0x040000AC RID: 172
		public List<HarvestData.Param> param = new List<HarvestData.Param>();

		// Token: 0x0200002C RID: 44
		[Serializable]
		public class Data
		{
			// Token: 0x040000AD RID: 173
			public int group;

			// Token: 0x040000AE RID: 174
			public int nameHash = -1;

			// Token: 0x040000AF RID: 175
			public int stock = 1;

			// Token: 0x040000B0 RID: 176
			public int percent = 100;
		}

		// Token: 0x0200002D RID: 45
		[Serializable]
		public class Param
		{
			// Token: 0x040000B1 RID: 177
			public int nameHash = -1;

			// Token: 0x040000B2 RID: 178
			public int time;

			// Token: 0x040000B3 RID: 179
			public List<HarvestData.Data> data = new List<HarvestData.Data>();
		}
	}
}

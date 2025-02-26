using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000057 RID: 87
	public class VendData : ScriptableObject
	{
		// Token: 0x04000149 RID: 329
		public List<VendData.Param> param = new List<VendData.Param>();

		// Token: 0x02000058 RID: 88
		[Serializable]
		public class Param
		{
			// Token: 0x0400014A RID: 330
			public int Group;

			// Token: 0x0400014B RID: 331
			public int nameHash = -1;

			// Token: 0x0400014C RID: 332
			public int[] Stocks;

			// Token: 0x0400014D RID: 333
			public int Rate;

			// Token: 0x0400014E RID: 334
			public int Percent;
		}
	}
}

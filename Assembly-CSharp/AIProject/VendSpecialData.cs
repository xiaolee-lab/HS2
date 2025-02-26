using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000059 RID: 89
	public class VendSpecialData : ScriptableObject
	{
		// Token: 0x0400014F RID: 335
		public List<VendSpecialData.Param> param = new List<VendSpecialData.Param>();

		// Token: 0x0200005A RID: 90
		[Serializable]
		public class Param
		{
			// Token: 0x04000150 RID: 336
			public int ID = -1;

			// Token: 0x04000151 RID: 337
			public int nameHash = -1;

			// Token: 0x04000152 RID: 338
			public int Rate = -1;

			// Token: 0x04000153 RID: 339
			public int Stock = 1;
		}
	}
}

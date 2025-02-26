using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000044 RID: 68
	public class PlantIvyFilterData : ScriptableObject
	{
		// Token: 0x0400010F RID: 271
		public List<PlantIvyFilterData.Param> param = new List<PlantIvyFilterData.Param>();

		// Token: 0x02000045 RID: 69
		[Serializable]
		public class Param
		{
			// Token: 0x04000110 RID: 272
			public string Name;

			// Token: 0x04000111 RID: 273
			public int CategoryID;

			// Token: 0x04000112 RID: 274
			public int ItemID;

			// Token: 0x04000113 RID: 275
			public int ObjID;
		}
	}
}

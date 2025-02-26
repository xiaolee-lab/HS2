using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000048 RID: 72
	public class RecipeData : ScriptableObject
	{
		// Token: 0x0400011A RID: 282
		public List<RecipeData.Param> param = new List<RecipeData.Param>();

		// Token: 0x02000049 RID: 73
		[Serializable]
		public class NeedData
		{
			// Token: 0x0400011B RID: 283
			public int Sum = 1;

			// Token: 0x0400011C RID: 284
			public int nameHash = -1;
		}

		// Token: 0x0200004A RID: 74
		[Serializable]
		public class Param
		{
			// Token: 0x0400011D RID: 285
			public int CreateSum = 1;

			// Token: 0x0400011E RID: 286
			public List<RecipeData.NeedData> NeedList = new List<RecipeData.NeedData>();

			// Token: 0x0400011F RID: 287
			public int nameHash = -1;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200004B RID: 75
	public class SkillObtainData : ScriptableObject
	{
		// Token: 0x04000120 RID: 288
		public List<SkillObtainData.Param> param = new List<SkillObtainData.Param>();

		// Token: 0x0200004C RID: 76
		[Serializable]
		public class Param
		{
			// Token: 0x04000121 RID: 289
			public int ID;

			// Token: 0x04000122 RID: 290
			public string Name;

			// Token: 0x04000123 RID: 291
			public int Rate;

			// Token: 0x04000124 RID: 292
			public int Category;

			// Token: 0x04000125 RID: 293
			public int ItemID;
		}
	}
}

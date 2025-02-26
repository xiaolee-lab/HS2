using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000032 RID: 50
	public class LifeStyleData : ScriptableObject
	{
		// Token: 0x040000C3 RID: 195
		public List<LifeStyleData.Param> param = new List<LifeStyleData.Param>();

		// Token: 0x02000033 RID: 51
		[Serializable]
		public class Param
		{
			// Token: 0x040000C4 RID: 196
			public int ID = -1;

			// Token: 0x040000C5 RID: 197
			public string Name = string.Empty;

			// Token: 0x040000C6 RID: 198
			public string Explanation = string.Empty;
		}
	}
}

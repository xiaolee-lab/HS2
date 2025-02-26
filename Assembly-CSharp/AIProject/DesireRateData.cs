using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000021 RID: 33
	public class DesireRateData : ScriptableObject
	{
		// Token: 0x04000052 RID: 82
		public List<DesireRateData.Param> param = new List<DesireRateData.Param>();

		// Token: 0x02000022 RID: 34
		[Serializable]
		public class Param
		{
			// Token: 0x04000053 RID: 83
			public int PersonalID;

			// Token: 0x04000054 RID: 84
			public int ID;

			// Token: 0x04000055 RID: 85
			public string TypeName;

			// Token: 0x04000056 RID: 86
			public float Morning;

			// Token: 0x04000057 RID: 87
			public float Day;

			// Token: 0x04000058 RID: 88
			public float Night;
		}
	}
}

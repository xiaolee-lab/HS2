using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200001F RID: 31
	public class DesireBorderData : ScriptableObject
	{
		// Token: 0x0400004D RID: 77
		public List<DesireBorderData.Param> param = new List<DesireBorderData.Param>();

		// Token: 0x02000020 RID: 32
		[Serializable]
		public class Param
		{
			// Token: 0x0400004E RID: 78
			public int ID;

			// Token: 0x0400004F RID: 79
			public string TypeName;

			// Token: 0x04000050 RID: 80
			public int Border;

			// Token: 0x04000051 RID: 81
			public int Limit;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000004 RID: 4
	public class ActByproductData : ScriptableObject
	{
		// Token: 0x04000006 RID: 6
		public List<ActByproductData.Param> param = new List<ActByproductData.Param>();

		// Token: 0x02000005 RID: 5
		[Serializable]
		public class Param
		{
			// Token: 0x04000007 RID: 7
			public string ActName;

			// Token: 0x04000008 RID: 8
			public string PoseName;

			// Token: 0x04000009 RID: 9
			public int ActionID;

			// Token: 0x0400000A RID: 10
			public int PoseID;

			// Token: 0x0400000B RID: 11
			public List<string> ItemList = new List<string>();
		}
	}
}

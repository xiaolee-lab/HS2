using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000025 RID: 37
	public class GameIconData : ScriptableObject
	{
		// Token: 0x0400005F RID: 95
		public List<GameIconData.Param> param = new List<GameIconData.Param>();

		// Token: 0x02000026 RID: 38
		[Serializable]
		public class Param
		{
			// Token: 0x04000060 RID: 96
			public int ID;

			// Token: 0x04000061 RID: 97
			public string Name;

			// Token: 0x04000062 RID: 98
			public string Bundle;

			// Token: 0x04000063 RID: 99
			public string Asset;

			// Token: 0x04000064 RID: 100
			public string Manifest;
		}
	}
}

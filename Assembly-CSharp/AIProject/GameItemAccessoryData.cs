using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000027 RID: 39
	public class GameItemAccessoryData : ScriptableObject
	{
		// Token: 0x04000065 RID: 101
		public List<GameItemAccessoryData.Param> param = new List<GameItemAccessoryData.Param>();

		// Token: 0x02000028 RID: 40
		[Serializable]
		public class Param
		{
			// Token: 0x04000066 RID: 102
			public int ID;

			// Token: 0x04000067 RID: 103
			public string Name;

			// Token: 0x04000068 RID: 104
			public string Manifest;

			// Token: 0x04000069 RID: 105
			public string AssetBundle;

			// Token: 0x0400006A RID: 106
			public string Asset;

			// Token: 0x0400006B RID: 107
			public string ParentName;

			// Token: 0x0400006C RID: 108
			public int Weight;
		}
	}
}

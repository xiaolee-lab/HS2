using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000046 RID: 70
	public class PointPrefabData : ScriptableObject
	{
		// Token: 0x04000114 RID: 276
		public List<PointPrefabData.Param> param = new List<PointPrefabData.Param>();

		// Token: 0x02000047 RID: 71
		[Serializable]
		public class Param
		{
			// Token: 0x04000115 RID: 277
			public string Name;

			// Token: 0x04000116 RID: 278
			public int MapID;

			// Token: 0x04000117 RID: 279
			public string AssetBundle;

			// Token: 0x04000118 RID: 280
			public string Asset;

			// Token: 0x04000119 RID: 281
			public string Manifest;
		}
	}
}

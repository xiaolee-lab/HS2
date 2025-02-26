using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000002 RID: 2
	public class ABInfoData : ScriptableObject
	{
		// Token: 0x04000001 RID: 1
		public List<ABInfoData.Param> param = new List<ABInfoData.Param>();

		// Token: 0x02000003 RID: 3
		[Serializable]
		public class Param
		{
			// Token: 0x04000002 RID: 2
			public int ID;

			// Token: 0x04000003 RID: 3
			public string AssetBundle;

			// Token: 0x04000004 RID: 4
			public string AssetFile;

			// Token: 0x04000005 RID: 5
			public string Manifest;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200003C RID: 60
	public class MinimapIconIDData : ScriptableObject
	{
		// Token: 0x040000FF RID: 255
		public List<MinimapIconIDData.Param> param = new List<MinimapIconIDData.Param>();

		// Token: 0x0200003D RID: 61
		[Serializable]
		public class Param
		{
			// Token: 0x04000100 RID: 256
			public int ActionID;

			// Token: 0x04000101 RID: 257
			public int ID;

			// Token: 0x04000102 RID: 258
			public string Name;
		}
	}
}

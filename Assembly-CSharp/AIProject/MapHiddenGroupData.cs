using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200003A RID: 58
	public class MapHiddenGroupData : ScriptableObject
	{
		// Token: 0x040000FA RID: 250
		public List<MapHiddenGroupData.Param> param = new List<MapHiddenGroupData.Param>();

		// Token: 0x0200003B RID: 59
		[Serializable]
		public class Param
		{
			// Token: 0x040000FB RID: 251
			public int MapID;

			// Token: 0x040000FC RID: 252
			public int ID;

			// Token: 0x040000FD RID: 253
			public string GroupName;

			// Token: 0x040000FE RID: 254
			public string Summary;
		}
	}
}

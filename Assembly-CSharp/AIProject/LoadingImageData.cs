using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000034 RID: 52
	public class LoadingImageData : ScriptableObject
	{
		// Token: 0x040000C7 RID: 199
		public List<LoadingImageData.Param> param = new List<LoadingImageData.Param>();

		// Token: 0x02000035 RID: 53
		[Serializable]
		public class Param
		{
			// Token: 0x040000C8 RID: 200
			public int ID;

			// Token: 0x040000C9 RID: 201
			public string Name;

			// Token: 0x040000CA RID: 202
			public string Bundle;

			// Token: 0x040000CB RID: 203
			public string Asset;

			// Token: 0x040000CC RID: 204
			public string Manifest;
		}
	}
}

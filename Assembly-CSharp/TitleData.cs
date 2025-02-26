using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class TitleData : ScriptableObject
{
	// Token: 0x0400013C RID: 316
	public List<TitleData.Param> param = new List<TitleData.Param>();

	// Token: 0x02000054 RID: 84
	[Serializable]
	public class Param
	{
		// Token: 0x0400013D RID: 317
		public int id;

		// Token: 0x0400013E RID: 318
		public string comment;

		// Token: 0x0400013F RID: 319
		public string assetPath;

		// Token: 0x04000140 RID: 320
		public string fileName;

		// Token: 0x04000141 RID: 321
		public string manifest;
	}
}

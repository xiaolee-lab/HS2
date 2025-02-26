using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class Sound3DSettingData : ScriptableObject
{
	// Token: 0x04000126 RID: 294
	public List<Sound3DSettingData.Param> param = new List<Sound3DSettingData.Param>();

	// Token: 0x0200004E RID: 78
	[Serializable]
	public class Param
	{
		// Token: 0x04000127 RID: 295
		public int No;

		// Token: 0x04000128 RID: 296
		public float DopplerLevel;

		// Token: 0x04000129 RID: 297
		public float Spread;

		// Token: 0x0400012A RID: 298
		public float MinDistance;

		// Token: 0x0400012B RID: 299
		public float MaxDistance;

		// Token: 0x0400012C RID: 300
		public int AudioRolloffMode;
	}
}

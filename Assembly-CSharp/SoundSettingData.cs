using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class SoundSettingData : ScriptableObject
{
	// Token: 0x0400012D RID: 301
	public List<SoundSettingData.Param> param = new List<SoundSettingData.Param>();

	// Token: 0x02000050 RID: 80
	[Serializable]
	public class Param
	{
		// Token: 0x0400012E RID: 302
		public int No;

		// Token: 0x0400012F RID: 303
		public float Volume;

		// Token: 0x04000130 RID: 304
		public float Pitch;

		// Token: 0x04000131 RID: 305
		public float Pan;

		// Token: 0x04000132 RID: 306
		public float Level3D;

		// Token: 0x04000133 RID: 307
		public int Priority;

		// Token: 0x04000134 RID: 308
		public bool PlayAwake;

		// Token: 0x04000135 RID: 309
		public bool Loop;

		// Token: 0x04000136 RID: 310
		public float DelayTime;

		// Token: 0x04000137 RID: 311
		public int Setting3DNo;
	}
}

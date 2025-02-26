using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class VoiceInfo : ScriptableObject
{
	// Token: 0x04000154 RID: 340
	public List<VoiceInfo.Param> param = new List<VoiceInfo.Param>();

	// Token: 0x0200005C RID: 92
	[Serializable]
	public class Param
	{
		// Token: 0x04000155 RID: 341
		public string Personality;

		// Token: 0x04000156 RID: 342
		public int No;

		// Token: 0x04000157 RID: 343
		public int Sort;
	}
}

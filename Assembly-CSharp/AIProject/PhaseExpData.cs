using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000042 RID: 66
	public class PhaseExpData : ScriptableObject
	{
		// Token: 0x0400010B RID: 267
		public List<PhaseExpData.Param> param = new List<PhaseExpData.Param>();

		// Token: 0x02000043 RID: 67
		[Serializable]
		public class Param
		{
			// Token: 0x0400010C RID: 268
			public int Personality;

			// Token: 0x0400010D RID: 269
			public string Name;

			// Token: 0x0400010E RID: 270
			public List<string> ExpArray = new List<string>();
		}
	}
}

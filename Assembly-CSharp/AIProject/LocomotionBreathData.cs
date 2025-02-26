using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000036 RID: 54
	public class LocomotionBreathData : ScriptableObject
	{
		// Token: 0x040000CD RID: 205
		public List<LocomotionBreathData.Param> param = new List<LocomotionBreathData.Param>();

		// Token: 0x02000037 RID: 55
		[Serializable]
		public class Param
		{
			// Token: 0x040000CE RID: 206
			public string Summary;

			// Token: 0x040000CF RID: 207
			public string State;

			// Token: 0x040000D0 RID: 208
			public int VoiceID;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200001A RID: 26
	public class AgentRandomEventInfo : ScriptableObject
	{
		// Token: 0x04000046 RID: 70
		public List<AgentRandomEventInfo.Param> param = new List<AgentRandomEventInfo.Param>();

		// Token: 0x0200001B RID: 27
		[Serializable]
		public class Param
		{
			// Token: 0x04000047 RID: 71
			public List<string> FileNames = new List<string>();
		}
	}
}

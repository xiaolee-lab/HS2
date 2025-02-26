using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000018 RID: 24
	public class AgentHiddenAreaData : ScriptableObject
	{
		// Token: 0x04000041 RID: 65
		public List<AgentHiddenAreaData.Param> param = new List<AgentHiddenAreaData.Param>();

		// Token: 0x02000019 RID: 25
		[Serializable]
		public class Param
		{
			// Token: 0x04000042 RID: 66
			public string Name;

			// Token: 0x04000043 RID: 67
			public int MapID;

			// Token: 0x04000044 RID: 68
			public int AreaID;

			// Token: 0x04000045 RID: 69
			public string HiddenAreaIDMulti;
		}
	}
}

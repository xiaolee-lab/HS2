using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000006 RID: 6
	public class ActionObstacleData : ScriptableObject
	{
		// Token: 0x0400000C RID: 12
		public List<ActionObstacleData.Param> param = new List<ActionObstacleData.Param>();

		// Token: 0x02000007 RID: 7
		[Serializable]
		public class Param
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000007 RID: 7 RVA: 0x000056EB File Offset: 0x00003AEB
			// (set) Token: 0x06000008 RID: 8 RVA: 0x000056F3 File Offset: 0x00003AF3
			public string ActionName { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000009 RID: 9 RVA: 0x000056FC File Offset: 0x00003AFC
			// (set) Token: 0x0600000A RID: 10 RVA: 0x00005704 File Offset: 0x00003B04
			public string Summary { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000B RID: 11 RVA: 0x0000570D File Offset: 0x00003B0D
			// (set) Token: 0x0600000C RID: 12 RVA: 0x00005715 File Offset: 0x00003B15
			public int ActionID { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000D RID: 13 RVA: 0x0000571E File Offset: 0x00003B1E
			// (set) Token: 0x0600000E RID: 14 RVA: 0x00005726 File Offset: 0x00003B26
			public int PoseID { get; set; }
		}
	}
}

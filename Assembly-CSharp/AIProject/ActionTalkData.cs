using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000008 RID: 8
	public class ActionTalkData : ScriptableObject
	{
		// Token: 0x04000011 RID: 17
		public List<ActionTalkData.Param> param = new List<ActionTalkData.Param>();

		// Token: 0x02000009 RID: 9
		[Serializable]
		public class Param
		{
			// Token: 0x04000012 RID: 18
			public string ActionName;

			// Token: 0x04000013 RID: 19
			public string Summary;

			// Token: 0x04000014 RID: 20
			public int ActionID;

			// Token: 0x04000015 RID: 21
			public int PoseID;

			// Token: 0x04000016 RID: 22
			public float ObstacleRadius;

			// Token: 0x04000017 RID: 23
			public bool useNeckLook;

			// Token: 0x04000018 RID: 24
			public bool CanTalk;

			// Token: 0x04000019 RID: 25
			public int TalkAttitudeID;

			// Token: 0x0400001A RID: 26
			public bool CanHCommand;

			// Token: 0x0400001B RID: 27
			public bool IsBadMood;

			// Token: 0x0400001C RID: 28
			public bool IsSpecial;

			// Token: 0x0400001D RID: 29
			public int HPositionID;

			// Token: 0x0400001E RID: 30
			public int HPositionSubID;
		}
	}
}

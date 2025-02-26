using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200000A RID: 10
	public class AgentAdvEventInfo : ScriptableObject
	{
		// Token: 0x0400001F RID: 31
		public List<AgentAdvEventInfo.Param> param = new List<AgentAdvEventInfo.Param>();

		// Token: 0x0200000B RID: 11
		[Serializable]
		public class Param
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000013 RID: 19 RVA: 0x000057D2 File Offset: 0x00003BD2
			public bool IsStateEmpty
			{
				[CompilerGenerated]
				get
				{
					return this.States.IsNullOrEmpty<AgentAdvEventInfo.State>();
				}
			}

			// Token: 0x06000014 RID: 20 RVA: 0x000057E0 File Offset: 0x00003BE0
			public bool IsState(int id, int actionID, int poseID)
			{
				return this.IsStateEmpty || this.States.Any((AgentAdvEventInfo.State item) => item.Check(id, actionID, poseID));
			}

			// Token: 0x04000020 RID: 32
			public string FileName = string.Empty;

			// Token: 0x04000021 RID: 33
			public int[] PlaceIDs = new int[0];

			// Token: 0x04000022 RID: 34
			public int[] Weathers = new int[0];

			// Token: 0x04000023 RID: 35
			public AgentAdvEventInfo.TimeRound TimeRound = new AgentAdvEventInfo.TimeRound();

			// Token: 0x04000024 RID: 36
			public int[] Phases = new int[0];

			// Token: 0x04000025 RID: 37
			public AgentAdvEventInfo.State[] States = new AgentAdvEventInfo.State[0];

			// Token: 0x04000026 RID: 38
			public int EventType;

			// Token: 0x04000027 RID: 39
			public int ExpansionID = -1;

			// Token: 0x04000028 RID: 40
			public int SortID;

			// Token: 0x04000029 RID: 41
			public bool LookPlayer = true;

			// Token: 0x0400002A RID: 42
			public AgentAdvEventInfo.EventPosition EventPos = new AgentAdvEventInfo.EventPosition();
		}

		// Token: 0x0200000C RID: 12
		[Serializable]
		public class TimeRound
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000016 RID: 22 RVA: 0x00005868 File Offset: 0x00003C68
			public bool isAll
			{
				[CompilerGenerated]
				get
				{
					return this.start < 0 || this.end < 0;
				}
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00005884 File Offset: 0x00003C84
			public bool Check(int now)
			{
				if (this.isAll)
				{
					return true;
				}
				if (this.start <= this.end)
				{
					if (now >= this.start && now <= this.end)
					{
						return true;
					}
				}
				else if (now >= this.start || now <= this.end)
				{
					return true;
				}
				return false;
			}

			// Token: 0x0400002B RID: 43
			public int start = -1;

			// Token: 0x0400002C RID: 44
			public int end = -1;
		}

		// Token: 0x0200000D RID: 13
		[Serializable]
		public class State
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00005908 File Offset: 0x00003D08
			public bool Check(int pointID, int actionID, int poseID)
			{
				return (this.pointID == -1 || this.pointID == pointID) && (this.actionID == -1 || this.actionID == actionID) && (this.poseID == -1 || this.poseID == poseID);
			}

			// Token: 0x0400002D RID: 45
			public int pointID = -1;

			// Token: 0x0400002E RID: 46
			public int actionID = -1;

			// Token: 0x0400002F RID: 47
			public int poseID = -1;
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public class EventPosition
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001B RID: 27 RVA: 0x0000597A File Offset: 0x00003D7A
			public bool isOrder
			{
				[CompilerGenerated]
				get
				{
					return this.mapID != -1 && this.ID != -1;
				}
			}

			// Token: 0x04000030 RID: 48
			public int mapID = -1;

			// Token: 0x04000031 RID: 49
			public int ID = -1;
		}
	}
}

using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C9A RID: 3226
	[TaskCategory("")]
	public class ClothChange : AgentStateAction
	{
		// Token: 0x0600691E RID: 26910 RVA: 0x002CB3F4 File Offset: 0x002C97F4
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.ClothChange;
			List<string> closetCoordinateList = Singleton<Game>.Instance.Environment.ClosetCoordinateList;
			string element = closetCoordinateList.GetElement(UnityEngine.Random.Range(0, closetCoordinateList.Count));
			agent.AgentData.NowCoordinateFileName = element;
			base.OnStart();
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x002CB448 File Offset: 0x002C9848
		protected override void OnCompletedStateTask()
		{
			base.Agent.AgentData.IsOtherCoordinate = true;
			base.Agent.AgentData.AddAppendEventFlagParam(4, 1);
		}
	}
}

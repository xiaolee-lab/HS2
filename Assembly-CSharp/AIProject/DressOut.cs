using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C9E RID: 3230
	[TaskCategory("")]
	public class DressOut : AgentStateAction
	{
		// Token: 0x0600692D RID: 26925 RVA: 0x002CB6AC File Offset: 0x002C9AAC
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.DressOut;
			agent.AgentData.BathCoordinateFileName = null;
			base.OnStart();
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x002CB6DD File Offset: 0x002C9ADD
		protected override void OnCompletedStateTask()
		{
			base.Agent.AgentData.PlayedDressIn = false;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CDF RID: 3295
	[TaskCategory("")]
	public class ReceiveTalk : AgentAction
	{
		// Token: 0x06006A77 RID: 27255 RVA: 0x002D5D2C File Offset: 0x002D412C
		public override void OnStart()
		{
			base.OnStart();
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x002D5D34 File Offset: 0x002D4134
		public override TaskStatus OnUpdate()
		{
			if (!(base.Agent.CommandPartner is AgentActor))
			{
				return TaskStatus.Failure;
			}
			AgentActor agentActor = base.Agent.CommandPartner as AgentActor;
			if (agentActor.LivesTalkSequence)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}
	}
}

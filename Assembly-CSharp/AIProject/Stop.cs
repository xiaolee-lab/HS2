using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CE6 RID: 3302
	[TaskCategory("")]
	public class Stop : AgentAction
	{
		// Token: 0x06006A8B RID: 27275 RVA: 0x002D6958 File Offset: 0x002D4D58
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			return TaskStatus.Success;
		}
	}
}

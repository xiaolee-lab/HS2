using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CF3 RID: 3315
	[TaskCategory("")]
	public class Wait : AgentAction
	{
		// Token: 0x06006AC4 RID: 27332 RVA: 0x002D99D4 File Offset: 0x002D7DD4
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x002D99FA File Offset: 0x002D7DFA
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x002D9A07 File Offset: 0x002D7E07
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}

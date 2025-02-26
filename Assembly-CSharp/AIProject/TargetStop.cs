using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CE9 RID: 3305
	[TaskCategory("")]
	public class TargetStop : AgentAction
	{
		// Token: 0x06006A99 RID: 27289 RVA: 0x002D73A4 File Offset: 0x002D57A4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Actor targetInSightActor = agent.TargetInSightActor;
			if (!(targetInSightActor is AgentActor) && !(targetInSightActor is MerchantActor))
			{
				return TaskStatus.Failure;
			}
			targetInSightActor.StopNavMeshAgent();
			targetInSightActor.ChangeStaticNavMeshAgentAvoidance();
			return TaskStatus.Success;
		}
	}
}

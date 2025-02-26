using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D63 RID: 3427
	[TaskCategory("")]
	public class IsOnOffMeshLink : AgentConditional
	{
		// Token: 0x06006C2B RID: 27691 RVA: 0x002E5BEC File Offset: 0x002E3FEC
		public override TaskStatus OnUpdate()
		{
			NavMeshAgent navMeshAgent = base.Agent.NavMeshAgent;
			if (navMeshAgent.isOnOffMeshLink && navMeshAgent.currentOffMeshLinkData.offMeshLink != null)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

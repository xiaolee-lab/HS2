using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CC8 RID: 3272
	[TaskCategory("")]
	public class GoToNextOffMeshLink : AgentAction
	{
		// Token: 0x060069E9 RID: 27113 RVA: 0x002D16B4 File Offset: 0x002CFAB4
		public override TaskStatus OnUpdate()
		{
			NavMeshAgent navMeshAgent = base.Agent.NavMeshAgent;
			OffMeshLink offMeshLink = navMeshAgent.nextOffMeshLinkData.offMeshLink;
			if (!(offMeshLink != null))
			{
				return TaskStatus.Failure;
			}
			if (offMeshLink.startTransform != null)
			{
				navMeshAgent.SetDestination(offMeshLink.startTransform.position);
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

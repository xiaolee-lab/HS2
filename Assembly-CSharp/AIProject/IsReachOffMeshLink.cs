using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D68 RID: 3432
	public class IsReachOffMeshLink : AgentConditional
	{
		// Token: 0x06006C35 RID: 27701 RVA: 0x002E5CD4 File Offset: 0x002E40D4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			OffMeshLink targetOffMeshLink = agent.TargetOffMeshLink;
			if (targetOffMeshLink == null)
			{
				return TaskStatus.Failure;
			}
			return (!agent.HasArrivedOffMeshLink(targetOffMeshLink)) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D55 RID: 3413
	[TaskCategory("")]
	public class IsDoorForOffMeshLink : AgentConditional
	{
		// Token: 0x06006C0F RID: 27663 RVA: 0x002E56AC File Offset: 0x002E3AAC
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.NavMeshAgent.currentOffMeshLinkData.offMeshLink != null)
			{
				DoorPoint component = base.Agent.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<DoorPoint>();
				if (component != null)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D6B RID: 3435
	[TaskCategory("")]
	public class IsStandbyMove : AgentConditional
	{
		// Token: 0x06006C3B RID: 27707 RVA: 0x002E5E24 File Offset: 0x002E4224
		public override TaskStatus OnUpdate()
		{
			OffMeshLink nearOffMeshLink = base.Agent.NearOffMeshLink;
			if (base.Agent.IsInvalidMoveDestination(nearOffMeshLink))
			{
				base.Agent.TargetOffMeshLink = nearOffMeshLink;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x002E5E5D File Offset: 0x002E425D
		public override void OnBehaviorComplete()
		{
			base.Agent.TargetOffMeshLink = null;
			base.OnBehaviorComplete();
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D16 RID: 3350
	[TaskCategory("")]
	public class SetPartnerFromTargetInSight : AgentAction
	{
		// Token: 0x06006B59 RID: 27481 RVA: 0x002E0358 File Offset: 0x002DE758
		public override TaskStatus OnUpdate()
		{
			base.Agent.CommandPartner = base.Agent.TargetInSightActor;
			return TaskStatus.Success;
		}
	}
}

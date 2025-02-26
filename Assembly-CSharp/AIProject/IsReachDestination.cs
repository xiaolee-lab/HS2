using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D67 RID: 3431
	[TaskCategory("")]
	public class IsReachDestination : AgentConditional
	{
		// Token: 0x06006C33 RID: 27699 RVA: 0x002E5CB4 File Offset: 0x002E40B4
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.HasArrived())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

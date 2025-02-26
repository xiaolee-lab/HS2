using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D6F RID: 3439
	[TaskCategory("")]
	public class IsTargetFree : AgentConditional
	{
		// Token: 0x06006C44 RID: 27716 RVA: 0x002E5F74 File Offset: 0x002E4374
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.TargetInSightActor.IsNeutralCommand)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

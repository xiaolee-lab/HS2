using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D2E RID: 3374
	[TaskCategory("")]
	public class AcceptedLesbianH : AgentConditional
	{
		// Token: 0x06006BAD RID: 27565 RVA: 0x002E2B67 File Offset: 0x002E0F67
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Failure;
		}
	}
}

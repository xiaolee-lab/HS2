using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D76 RID: 3446
	[TaskCategory("")]
	public class ReceiveYes : AgentConditional
	{
		// Token: 0x06006C52 RID: 27730 RVA: 0x002E611D File Offset: 0x002E451D
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Failure;
		}
	}
}

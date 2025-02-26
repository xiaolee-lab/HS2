using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D7E RID: 3454
	[TaskCategory("")]
	public class CanBirthdayGift : AgentConditional
	{
		// Token: 0x06006C62 RID: 27746 RVA: 0x002E630B File Offset: 0x002E470B
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanBirthdayPresent)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D83 RID: 3459
	[TaskCategory("")]
	public class CanGift : AgentConditional
	{
		// Token: 0x06006C6C RID: 27756 RVA: 0x002E640A File Offset: 0x002E480A
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanPresent)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

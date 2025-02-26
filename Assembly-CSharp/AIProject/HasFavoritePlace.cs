using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D41 RID: 3393
	[TaskCategory("")]
	public class HasFavoritePlace : AgentConditional
	{
		// Token: 0x06006BE4 RID: 27620 RVA: 0x002E4D6B File Offset: 0x002E316B
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Failure;
		}
	}
}

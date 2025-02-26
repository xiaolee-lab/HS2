using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D8A RID: 3466
	[TaskCategory("")]
	public class CanScrounge : AgentConditional
	{
		// Token: 0x06006C7A RID: 27770 RVA: 0x002E6526 File Offset: 0x002E4926
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanScrounge)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D7F RID: 3455
	[TaskCategory("")]
	public class CanCauseHurt : AgentConditional
	{
		// Token: 0x06006C64 RID: 27748 RVA: 0x002E6328 File Offset: 0x002E4728
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanCauseHurt)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

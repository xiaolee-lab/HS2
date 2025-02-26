using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D88 RID: 3464
	[TaskCategory("")]
	public class CanMasturbation : AgentConditional
	{
		// Token: 0x06006C76 RID: 27766 RVA: 0x002E64DC File Offset: 0x002E48DC
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanMasturbation)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

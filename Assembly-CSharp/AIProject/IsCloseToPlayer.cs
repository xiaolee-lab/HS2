using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D50 RID: 3408
	[TaskCategory("")]
	public class IsCloseToPlayer : AgentConditional
	{
		// Token: 0x06006C03 RID: 27651 RVA: 0x002E53F1 File Offset: 0x002E37F1
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.IsCloseToPlayer)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

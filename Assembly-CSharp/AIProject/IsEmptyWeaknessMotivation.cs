using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D8D RID: 3469
	[TaskCategory("")]
	public class IsEmptyWeaknessMotivation : AgentConditional
	{
		// Token: 0x06006C80 RID: 27776 RVA: 0x002E6587 File Offset: 0x002E4987
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.IsEmptyWeaknessMotivation)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

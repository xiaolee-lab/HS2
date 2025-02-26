using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D7B RID: 3451
	[TaskCategory("")]
	public class IsHurt : AgentConditional
	{
		// Token: 0x06006C5C RID: 27740 RVA: 0x002E6210 File Offset: 0x002E4610
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.ID == 4)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

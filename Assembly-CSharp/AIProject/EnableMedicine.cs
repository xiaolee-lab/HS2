using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D8C RID: 3468
	[TaskCategory("")]
	public class EnableMedicine : AgentConditional
	{
		// Token: 0x06006C7E RID: 27774 RVA: 0x002E6560 File Offset: 0x002E4960
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.Enabled)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

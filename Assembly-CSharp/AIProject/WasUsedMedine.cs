using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D7D RID: 3453
	[TaskCategory("")]
	public class WasUsedMedine : AgentConditional
	{
		// Token: 0x06006C60 RID: 27744 RVA: 0x002E62E4 File Offset: 0x002E46E4
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.UsedMedicine)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

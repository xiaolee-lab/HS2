using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D04 RID: 3332
	[TaskCategory("")]
	public class HealStomachache : AgentAction
	{
		// Token: 0x06006B04 RID: 27396 RVA: 0x002DBBC3 File Offset: 0x002D9FC3
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.ID == 1)
			{
				base.Agent.AgentData.SickState.ID = -1;
			}
			return TaskStatus.Success;
		}
	}
}

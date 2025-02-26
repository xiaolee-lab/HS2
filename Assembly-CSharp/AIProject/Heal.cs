using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D03 RID: 3331
	[TaskCategory("")]
	public class Heal : AgentAction
	{
		// Token: 0x06006B02 RID: 27394 RVA: 0x002DBB8C File Offset: 0x002D9F8C
		public override TaskStatus OnUpdate()
		{
			base.Agent.AgentData.SickState.ID = -1;
			base.Agent.AgentData.SickState.UsedMedicine = false;
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C94 RID: 3220
	[TaskCategory("")]
	public class EndCarryFood : AgentAction
	{
		// Token: 0x0600690C RID: 26892 RVA: 0x002CA84D File Offset: 0x002C8C4D
		public override TaskStatus OnUpdate()
		{
			base.Agent.AgentData.CarryingItem = null;
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C88 RID: 3208
	[TaskCategory("")]
	public class ChangeReservedMode : AgentAction
	{
		// Token: 0x060068EF RID: 26863 RVA: 0x002CA371 File Offset: 0x002C8771
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeReservedBehavior();
			return TaskStatus.Success;
		}
	}
}

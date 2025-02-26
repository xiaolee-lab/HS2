using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C95 RID: 3221
	[TaskCategory("")]
	public class EndStandby : AgentAction
	{
		// Token: 0x0600690E RID: 26894 RVA: 0x002CA869 File Offset: 0x002C8C69
		public override TaskStatus OnUpdate()
		{
			base.Agent.IsStandby = false;
			return TaskStatus.Success;
		}
	}
}

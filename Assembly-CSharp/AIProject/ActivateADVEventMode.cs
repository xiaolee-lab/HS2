using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C7E RID: 3198
	[TaskCategory("")]
	public class ActivateADVEventMode : AgentAction
	{
		// Token: 0x060068D8 RID: 26840 RVA: 0x002CA01B File Offset: 0x002C841B
		public override TaskStatus OnUpdate()
		{
			base.Agent.IsEvent = true;
			return TaskStatus.Success;
		}
	}
}

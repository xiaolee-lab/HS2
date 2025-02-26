using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C87 RID: 3207
	[TaskCategory("")]
	public class ChangePrevMode : AgentAction
	{
		// Token: 0x060068ED RID: 26861 RVA: 0x002CA350 File Offset: 0x002C8750
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeBehavior(base.Agent.PrevMode);
			return TaskStatus.Success;
		}
	}
}

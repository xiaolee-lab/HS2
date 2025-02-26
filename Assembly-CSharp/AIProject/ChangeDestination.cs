using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C84 RID: 3204
	[TaskCategory("")]
	public class ChangeDestination : AgentAction
	{
		// Token: 0x060068E7 RID: 26855 RVA: 0x002CA2F7 File Offset: 0x002C86F7
		public override TaskStatus OnUpdate()
		{
			base.Agent.TargetInSightActionPoint = base.Agent.NextPoint;
			return TaskStatus.Success;
		}
	}
}

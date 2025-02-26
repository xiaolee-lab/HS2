using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D7A RID: 3450
	[TaskCategory("")]
	public class ElapsedDaysOfSick : AgentConditional
	{
		// Token: 0x06006C5A RID: 27738 RVA: 0x002E61CF File Offset: 0x002E45CF
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.ElapsedTime > base.Agent.AgentData.SickState.Duration)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

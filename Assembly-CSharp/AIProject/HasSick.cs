using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D79 RID: 3449
	[TaskCategory("")]
	public class HasSick : AgentConditional
	{
		// Token: 0x06006C58 RID: 27736 RVA: 0x002E61A7 File Offset: 0x002E45A7
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.SickState.ID == -1)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D80 RID: 3456
	[TaskCategory("")]
	public class CanClothChange : AgentConditional
	{
		// Token: 0x06006C66 RID: 27750 RVA: 0x002E6348 File Offset: 0x002E4748
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.CanClothChange)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

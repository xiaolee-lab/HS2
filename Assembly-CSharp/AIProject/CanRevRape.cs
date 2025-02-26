using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D89 RID: 3465
	public class CanRevRape : AgentConditional
	{
		// Token: 0x06006C78 RID: 27768 RVA: 0x002E64FC File Offset: 0x002E48FC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.CanRevRape())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

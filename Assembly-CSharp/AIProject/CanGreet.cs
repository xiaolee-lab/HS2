using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D85 RID: 3461
	public class CanGreet : AgentConditional
	{
		// Token: 0x06006C70 RID: 27760 RVA: 0x002E6478 File Offset: 0x002E4878
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.CanGreet)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D8B RID: 3467
	[TaskCategory("")]
	public class CanWashFace : AgentConditional
	{
		// Token: 0x06006C7C RID: 27772 RVA: 0x002E6543 File Offset: 0x002E4943
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanWashFace)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

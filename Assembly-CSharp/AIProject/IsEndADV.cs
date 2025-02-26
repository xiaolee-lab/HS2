using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D57 RID: 3415
	[TaskCategory("")]
	public class IsEndADV : AgentConditional
	{
		// Token: 0x06006C13 RID: 27667 RVA: 0x002E5764 File Offset: 0x002E3B64
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.IsEvent)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D4B RID: 3403
	[TaskCategory("")]
	public class IsActiveEventADV : AgentConditional
	{
		// Token: 0x06006BF8 RID: 27640 RVA: 0x002E5259 File Offset: 0x002E3659
		public override TaskStatus OnUpdate()
		{
			if (!base.Agent.IsEvent)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
	}
}

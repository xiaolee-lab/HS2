using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D22 RID: 3362
	[TaskCategory("")]
	public class StartStandby : AgentAction
	{
		// Token: 0x06006B81 RID: 27521 RVA: 0x002E1B18 File Offset: 0x002DFF18
		public override TaskStatus OnUpdate()
		{
			base.Agent.IsStandby = true;
			return TaskStatus.Success;
		}
	}
}

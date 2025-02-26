using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D2A RID: 3370
	[TaskCategory("")]
	public class WaitForEndHScene : AgentAction
	{
		// Token: 0x06006B9B RID: 27547 RVA: 0x002E206B File Offset: 0x002E046B
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}

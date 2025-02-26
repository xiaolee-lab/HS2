using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D26 RID: 3366
	[TaskCategory("")]
	public class TeleportPosition : AgentAction
	{
		// Token: 0x06006B8D RID: 27533 RVA: 0x002E1D74 File Offset: 0x002E0174
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}

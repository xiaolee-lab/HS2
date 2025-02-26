using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D86 RID: 3462
	[TaskCategory("")]
	public class CanInvitation : AgentConditional
	{
		// Token: 0x06006C72 RID: 27762 RVA: 0x002E64A2 File Offset: 0x002E48A2
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanInvitation)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

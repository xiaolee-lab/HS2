using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D66 RID: 3430
	[TaskCategory("")]
	public class IsPrivate : AgentConditional
	{
		// Token: 0x06006C31 RID: 27697 RVA: 0x002E5C96 File Offset: 0x002E4096
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AreaType == MapArea.AreaType.Private)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

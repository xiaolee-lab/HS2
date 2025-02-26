using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D87 RID: 3463
	[TaskCategory("")]
	public class CanLesbian : AgentConditional
	{
		// Token: 0x06006C74 RID: 27764 RVA: 0x002E64BF File Offset: 0x002E48BF
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanLesbian)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

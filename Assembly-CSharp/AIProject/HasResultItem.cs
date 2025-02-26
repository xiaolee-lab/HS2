using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D4A RID: 3402
	[TaskCategory("")]
	public class HasResultItem : AgentConditional
	{
		// Token: 0x06006BF6 RID: 27638 RVA: 0x002E524E File Offset: 0x002E364E
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Failure;
		}
	}
}

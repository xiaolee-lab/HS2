using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D58 RID: 3416
	[TaskCategory("")]
	public class IsEquipable : AgentConditional
	{
		// Token: 0x06006C15 RID: 27669 RVA: 0x002E5781 File Offset: 0x002E3B81
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}

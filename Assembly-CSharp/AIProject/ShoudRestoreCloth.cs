using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D90 RID: 3472
	[TaskCategory("")]
	public class ShoudRestoreCloth : AgentConditional
	{
		// Token: 0x06006C86 RID: 27782 RVA: 0x002E6676 File Offset: 0x002E4A76
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.ShouldRestoreCloth)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

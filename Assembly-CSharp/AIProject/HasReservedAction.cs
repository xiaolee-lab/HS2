using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D49 RID: 3401
	[TaskCategory("")]
	public class HasReservedAction : AgentConditional
	{
		// Token: 0x06006BF4 RID: 27636 RVA: 0x002E520E File Offset: 0x002E360E
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Partner == null)
			{
				return TaskStatus.Failure;
			}
			if (base.Agent.Partner.CurrentPoint == null)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
	}
}

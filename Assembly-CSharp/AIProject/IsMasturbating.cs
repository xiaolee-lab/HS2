using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D5D RID: 3421
	[TaskCategory("")]
	public class IsMasturbating : AgentConditional
	{
		// Token: 0x06006C1F RID: 27679 RVA: 0x002E5943 File Offset: 0x002E3D43
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.IsMasturbating)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

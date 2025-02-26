using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D92 RID: 3474
	public class SucceedFood : AgentConditional
	{
		// Token: 0x06006C8E RID: 27790 RVA: 0x002E68A3 File Offset: 0x002E4CA3
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.SuccessCook)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

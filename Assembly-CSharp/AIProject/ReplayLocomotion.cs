using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CE2 RID: 3298
	public class ReplayLocomotion : AgentAction
	{
		// Token: 0x06006A7E RID: 27262 RVA: 0x002D5E1C File Offset: 0x002D421C
		public override TaskStatus OnUpdate()
		{
			base.Agent.ActivateTransfer(true);
			return TaskStatus.Success;
		}
	}
}

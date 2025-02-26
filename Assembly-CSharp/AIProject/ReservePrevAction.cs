using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D0C RID: 3340
	[TaskCategory("")]
	public class ReservePrevAction : AgentAction
	{
		// Token: 0x06006B1C RID: 27420 RVA: 0x002DC5D3 File Offset: 0x002DA9D3
		public override TaskStatus OnUpdate()
		{
			base.Agent.TargetInSightActionPoint = base.Agent.PrevActionPoint;
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CB1 RID: 3249
	[TaskCategory("")]
	public class ChangeDynamicAvoidancePriority : AgentAction
	{
		// Token: 0x0600697E RID: 27006 RVA: 0x002CEC54 File Offset: 0x002CD054
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
			return TaskStatus.Success;
		}
	}
}

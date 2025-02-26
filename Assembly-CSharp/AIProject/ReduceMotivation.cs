using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D08 RID: 3336
	[TaskCategory("")]
	public class ReduceMotivation : AgentAction
	{
		// Token: 0x06006B0E RID: 27406 RVA: 0x002DBF91 File Offset: 0x002DA391
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.UpdateMotivation = true;
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x002DBFA5 File Offset: 0x002DA3A5
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x002DBFA8 File Offset: 0x002DA3A8
		public override void OnBehaviorComplete()
		{
			base.Agent.UpdateMotivation = false;
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x002DBFB8 File Offset: 0x002DA3B8
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			if (agent != null)
			{
				agent.UpdateMotivation = false;
			}
			base.OnEnd();
		}
	}
}

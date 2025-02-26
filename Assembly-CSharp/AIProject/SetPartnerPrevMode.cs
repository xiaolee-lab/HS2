using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D17 RID: 3351
	[TaskCategory("")]
	public class SetPartnerPrevMode : AgentAction
	{
		// Token: 0x06006B5B RID: 27483 RVA: 0x002E037C File Offset: 0x002DE77C
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Partner == null)
			{
				return TaskStatus.Failure;
			}
			AgentActor agentActor = base.Agent.Partner as AgentActor;
			agentActor.PrevMode = this._mode;
			return TaskStatus.Success;
		}

		// Token: 0x04005A7E RID: 23166
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

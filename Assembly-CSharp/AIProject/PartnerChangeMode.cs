using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C8A RID: 3210
	[TaskCategory("")]
	public class PartnerChangeMode : AgentAction
	{
		// Token: 0x060068F3 RID: 26867 RVA: 0x002CA3A4 File Offset: 0x002C87A4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			AgentActor agentActor = agent.Partner as AgentActor;
			agentActor.BehaviorResources.ChangeMode(this._modeToChange);
			agentActor.Mode = this._modeToChange;
			return TaskStatus.Success;
		}

		// Token: 0x04005980 RID: 22912
		[SerializeField]
		private Desire.ActionType _modeToChange;
	}
}

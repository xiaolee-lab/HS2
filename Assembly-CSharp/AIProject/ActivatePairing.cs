using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CBC RID: 3260
	[TaskCategory("")]
	public class ActivatePairing : AgentAction
	{
		// Token: 0x060069C5 RID: 27077 RVA: 0x002D0854 File Offset: 0x002CEC54
		public override TaskStatus OnUpdate()
		{
			AgentActor agentActor = base.Agent.TargetInSightActor as AgentActor;
			base.Agent.Partner = agentActor;
			agentActor.Partner = base.Agent;
			agentActor.BehaviorResources.ChangeMode(this._modeToChange);
			agentActor.Mode = this._modeToChange;
			return TaskStatus.Success;
		}

		// Token: 0x040059BC RID: 22972
		[SerializeField]
		private Desire.ActionType _modeToChange;
	}
}

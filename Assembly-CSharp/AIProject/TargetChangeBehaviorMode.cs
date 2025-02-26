using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C8B RID: 3211
	[TaskCategory("")]
	public class TargetChangeBehaviorMode : AgentAction
	{
		// Token: 0x060068F5 RID: 26869 RVA: 0x002CA3EC File Offset: 0x002C87EC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActor == null)
			{
				return TaskStatus.Failure;
			}
			if (!(agent.TargetInSightActor is AgentActor))
			{
				return TaskStatus.Failure;
			}
			AgentActor agentActor = agent.TargetInSightActor as AgentActor;
			agentActor.BehaviorResources.ChangeMode(this._mode);
			return TaskStatus.Success;
		}

		// Token: 0x04005981 RID: 22913
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

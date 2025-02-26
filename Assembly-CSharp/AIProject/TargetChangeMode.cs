using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C8D RID: 3213
	[TaskCategory("")]
	public class TargetChangeMode : AgentAction
	{
		// Token: 0x060068F9 RID: 26873 RVA: 0x002CA4AC File Offset: 0x002C88AC
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
			agentActor.ChangeBehavior(this._mode);
			return TaskStatus.Success;
		}

		// Token: 0x04005983 RID: 22915
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CCD RID: 3277
	[TaskCategory("")]
	public class IdleWhileFreeLink : AgentAction
	{
		// Token: 0x06006A00 RID: 27136 RVA: 0x002D22E7 File Offset: 0x002D06E7
		public override void OnStart()
		{
			this._agent = base.Agent;
			this._prevStopped = this._agent.NavMeshAgent.isStopped;
			if (!this._prevStopped)
			{
				this._agent.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x002D2327 File Offset: 0x002D0727
		public override TaskStatus OnUpdate()
		{
			if (this._agent.IsInvalidMoveDestination(this._agent.TargetOffMeshLink))
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x002D2348 File Offset: 0x002D0748
		public override void OnEnd()
		{
			if (this._agent.NavMeshAgent.isStopped != this._prevStopped)
			{
				this._agent.NavMeshAgent.isStopped = this._prevStopped;
			}
			base.Agent.TargetOffMeshLink = null;
			base.OnEnd();
		}

		// Token: 0x040059D3 RID: 22995
		private AgentActor _agent;

		// Token: 0x040059D4 RID: 22996
		private bool _prevStopped;
	}
}

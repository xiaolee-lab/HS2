using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CDE RID: 3294
	[TaskCategory("")]
	public class ReachOffMeshLinkPoint : AgentAction
	{
		// Token: 0x06006A73 RID: 27251 RVA: 0x002D5BFC File Offset: 0x002D3FFC
		public override void OnStart()
		{
			this._agent = base.Agent;
			this._navMeshAgent = this._agent.NavMeshAgent;
			this._prevStopped = this._navMeshAgent.isStopped;
			if (this._agent.TargetOffMeshLink != null && this._prevStopped)
			{
				this._navMeshAgent.isStopped = false;
			}
			base.OnStart();
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x002D5C6C File Offset: 0x002D406C
		public override TaskStatus OnUpdate()
		{
			OffMeshLink targetOffMeshLink = this._agent.TargetOffMeshLink;
			Transform transform = (targetOffMeshLink != null) ? targetOffMeshLink.startTransform : null;
			if (transform == null)
			{
				return TaskStatus.Failure;
			}
			if (this._agent.HasArrived())
			{
				return TaskStatus.Success;
			}
			if (transform != null)
			{
				this._navMeshAgent.SetDestination(transform.position);
			}
			if (!this._navMeshAgent.pathPending && !this._navMeshAgent.hasPath)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x002D5CF5 File Offset: 0x002D40F5
		public override void OnEnd()
		{
			if (this._prevStopped != this._navMeshAgent.isStopped)
			{
				this._navMeshAgent.isStopped = this._prevStopped;
			}
			base.OnEnd();
		}

		// Token: 0x04005A04 RID: 23044
		private AgentActor _agent;

		// Token: 0x04005A05 RID: 23045
		private NavMeshAgent _navMeshAgent;

		// Token: 0x04005A06 RID: 23046
		private bool _prevStopped = true;
	}
}

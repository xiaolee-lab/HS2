using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CCA RID: 3274
	[TaskCategory("")]
	public class EnterHPoint : AgentAction
	{
		// Token: 0x060069F0 RID: 27120 RVA: 0x002D1A60 File Offset: 0x002CFE60
		public override void OnStart()
		{
			base.OnStart();
			this._path = new NavMeshPath();
			AgentActor agent = base.Agent;
			agent.ActivateTransfer(true);
			this.SetDestinationForce(agent.DestPosition.Value);
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x002D1AA4 File Offset: 0x002CFEA4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.DestPosition == null)
			{
				return TaskStatus.Failure;
			}
			if (agent.DestPosition != null)
			{
				this.SetDestination(agent.DestPosition.Value);
			}
			agent.NavMeshAgent.CalculatePath(agent.DestPosition.Value, this._path);
			if (this._path.status != NavMeshPathStatus.PathComplete)
			{
				agent.DestPosition = null;
				return TaskStatus.Failure;
			}
			float num = Vector3.Distance(agent.DestPosition.Value, agent.Position);
			if (num > Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPoint)
			{
				if (Mathf.Approximately(agent.NavMeshAgent.desiredVelocity.magnitude, 0f))
				{
					this._stopCount++;
					if (this._stopCount >= 10 && agent.DestPosition != null)
					{
						this._stopCount = 0;
						this.SetDestinationForce(agent.DestPosition.Value);
					}
				}
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x002D1BDC File Offset: 0x002CFFDC
		private bool SetDestinationForce(Vector3 destination)
		{
			bool result = false;
			AgentActor agent = base.Agent;
			NavMeshAgent navMeshAgent = agent.NavMeshAgent;
			if (!navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			if (this._path == null)
			{
				this._path = new NavMeshPath();
			}
			if (navMeshAgent.CalculatePath(destination, this._path))
			{
				navMeshAgent.ResetPath();
				if (!navMeshAgent.SetPath(this._path) || navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
				{
				}
			}
			return result;
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x002D1C5C File Offset: 0x002D005C
		private bool SetDestination(Vector3 destination)
		{
			bool result = false;
			AgentActor agent = base.Agent;
			NavMeshAgent navMeshAgent = agent.NavMeshAgent;
			if (!navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			if (navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
			{
				if (this._path == null)
				{
					this._path = new NavMeshPath();
				}
				if (!navMeshAgent.CalculatePath(destination, this._path) || navMeshAgent.SetPath(this._path))
				{
				}
			}
			return result;
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x002D1CD5 File Offset: 0x002D00D5
		public override void OnPause(bool paused)
		{
			if (!paused)
			{
				base.Agent.ActivateTransfer(true);
			}
		}

		// Token: 0x060069F5 RID: 27125 RVA: 0x002D1CEC File Offset: 0x002D00EC
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			if (agent.DestPosition != null)
			{
				agent.DestPosition = null;
			}
			this._path = null;
		}

		// Token: 0x040059CF RID: 22991
		private int _stopCount;

		// Token: 0x040059D0 RID: 22992
		private NavMeshPath _path;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D02 RID: 3330
	[TaskCategory("")]
	public class HeadToStoryPoint : AgentAction
	{
		// Token: 0x06006AFC RID: 27388 RVA: 0x002DB874 File Offset: 0x002D9C74
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._navMeshAgent = ((this._agent != null) ? this._agent.NavMeshAgent : null);
			this._point = ((this._agent != null) ? this._agent.TargetStoryPoint : null);
			this._missing = (this._agent == null || this._navMeshAgent == null || this._point == null);
			if (this._missing)
			{
				return;
			}
			Vector3? vector = new Vector3?(this._point.Position);
			this._agent.DestPosition = vector;
			Vector3? vector2 = vector;
			this._agent.ActivateNavMeshAgent();
			this._agent.PlayTutorialDefaultStateAnim();
			this.SetDestinationForce(vector2.Value);
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x002DB958 File Offset: 0x002D9D58
		public override TaskStatus OnUpdate()
		{
			if (this._missing || this._agent.DestPosition == null)
			{
				return TaskStatus.Failure;
			}
			if (!this._navMeshAgent.isActiveAndEnabled || !this._navMeshAgent.isOnNavMesh)
			{
				return TaskStatus.Failure;
			}
			this.SetDestination(this._agent.DestPosition.Value);
			float approachDistanceStoryPoint = Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceStoryPoint;
			if (this._navMeshAgent.hasPath)
			{
				float remainingDistance = this._navMeshAgent.remainingDistance;
				if (remainingDistance <= approachDistanceStoryPoint)
				{
					return TaskStatus.Success;
				}
			}
			else
			{
				float num = Vector3.Distance(this._agent.DestPosition.Value, this._agent.Position);
				if (num <= approachDistanceStoryPoint)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x002DBA34 File Offset: 0x002D9E34
		private bool SetDestinationForce(Vector3 destination)
		{
			bool result = false;
			if (!this._navMeshAgent.isActiveAndEnabled || !this._navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			NavMeshPath path = new NavMeshPath();
			if (!(result = this._navMeshAgent.CalculatePath(destination, path)) || !(result = this._navMeshAgent.SetPath(path)) || !(result = !this._navMeshAgent.path.corners.IsNullOrEmpty<Vector3>()))
			{
			}
			return result;
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x002DBAB4 File Offset: 0x002D9EB4
		private bool SetDestination(Vector3 destination)
		{
			bool result = false;
			if (!this._navMeshAgent.isActiveAndEnabled || !this._navMeshAgent.isOnNavMesh)
			{
				return result;
			}
			if (!(result = !this._navMeshAgent.path.corners.IsNullOrEmpty<Vector3>()))
			{
				NavMeshPath path = new NavMeshPath();
				if (!(result = this._navMeshAgent.CalculatePath(destination, path)) || (result = this._navMeshAgent.SetPath(path)))
				{
				}
			}
			return result;
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x002DBB34 File Offset: 0x002D9F34
		public override void OnEnd()
		{
			if (this._agent != null && this._agent.DestPosition != null)
			{
				this._agent.DestPosition = null;
			}
			base.OnEnd();
		}

		// Token: 0x04005A4D RID: 23117
		private AgentActor _agent;

		// Token: 0x04005A4E RID: 23118
		private NavMeshAgent _navMeshAgent;

		// Token: 0x04005A4F RID: 23119
		private StoryPoint _point;

		// Token: 0x04005A50 RID: 23120
		private bool _missing;
	}
}

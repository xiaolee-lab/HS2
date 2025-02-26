using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CFA RID: 3322
	[TaskCategory("")]
	public class EnterActionPoint : AgentAction
	{
		// Token: 0x06006AE3 RID: 27363 RVA: 0x002DAE38 File Offset: 0x002D9238
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.ActivateTransfer(true);
			ActionPoint targetInSightActionPoint = agent.TargetInSightActionPoint;
			if (targetInSightActionPoint != null)
			{
				Vector3? vector = new Vector3?(targetInSightActionPoint.LocatedPosition);
				agent.DestPosition = vector;
				Vector3? vector2 = vector;
				this.SetDestinationForce(vector2.Value);
			}
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x002DAE90 File Offset: 0x002D9290
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActionPoint == null)
			{
				return TaskStatus.Failure;
			}
			if (this._rejected)
			{
				this._rejected = false;
				if (agent.Mode == Desire.ActionType.TakeSleepPoint || agent.Mode == Desire.ActionType.TakeSleepHPoint || agent.Mode == Desire.ActionType.TakeEatPoint || agent.Mode == Desire.ActionType.TakeBreakPoint)
				{
					return TaskStatus.Success;
				}
				agent.TargetInSightActionPoint = null;
				return TaskStatus.Failure;
			}
			else
			{
				if (!agent.TargetInSightActionPoint.IsNeutralCommand)
				{
					if (agent.TargetInSightActionPoint.Reserver == agent)
					{
						agent.TargetInSightActionPoint.Reserver = null;
					}
					agent.TargetInSightActionPoint = null;
					return TaskStatus.Failure;
				}
				if (agent.Mode != Desire.ActionType.TakeSleepPoint && agent.Mode != Desire.ActionType.TakeSleepHPoint && agent.Mode != Desire.ActionType.TakeEatPoint && agent.Mode != Desire.ActionType.TakeBreakPoint)
				{
					List<ActionPoint> connectedActionPoints = agent.TargetInSightActionPoint.ConnectedActionPoints;
					if (!connectedActionPoints.IsNullOrEmpty<ActionPoint>())
					{
						foreach (ActionPoint actionPoint in connectedActionPoints)
						{
							if (!(actionPoint == null))
							{
								if (!actionPoint.IsNeutralCommand)
								{
									if (agent.TargetInSightActionPoint.Reserver == agent)
									{
										agent.TargetInSightActionPoint.Reserver = null;
									}
									agent.TargetInSightActionPoint = null;
									return TaskStatus.Failure;
								}
							}
						}
					}
				}
				if (agent.TargetInSightActionPoint.Reserver != agent)
				{
					agent.TargetInSightActionPoint = null;
					return TaskStatus.Failure;
				}
				if (agent.TargetInSightActionPoint is WarpPoint)
				{
					WarpPoint warpPoint = agent.TargetInSightActionPoint as WarpPoint;
					if (!(warpPoint != null))
					{
						agent.TargetInSightActionPoint = null;
						return TaskStatus.Failure;
					}
					Dictionary<int, List<WarpPoint>> dictionary;
					List<WarpPoint> list;
					if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(warpPoint.OwnerArea.ChunkID, out dictionary) || !dictionary.TryGetValue(warpPoint.TableID, out list) || list.IsNullOrEmpty<WarpPoint>() || list.Count < 2)
					{
						agent.TargetInSightActionPoint = null;
						return TaskStatus.Failure;
					}
				}
				if (agent.DestPosition == null)
				{
					return TaskStatus.Failure;
				}
				if (agent.DestPosition != null)
				{
					this.SetDestination(agent.DestPosition.Value);
				}
				float num = Vector3.Distance(agent.DestPosition.Value, agent.Position);
				float num2;
				if (this._enterClose)
				{
					num2 = Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPointCloser;
				}
				else
				{
					num2 = Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPoint;
				}
				if (num > num2)
				{
					if (Mathf.Approximately(agent.NavMeshAgent.desiredVelocity.magnitude, 0f) && Time.timeScale > 0f)
					{
						this._stopCount++;
						if (this._stopCount >= 10 && agent.DestPosition != null)
						{
							this._stopCount = 0;
							agent.NavMeshAgent.ResetPath();
							this.SetDestinationForce(agent.DestPosition.Value);
						}
					}
					return TaskStatus.Running;
				}
				return TaskStatus.Success;
			}
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x002DB208 File Offset: 0x002D9608
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
				if (this._path.status != NavMeshPathStatus.PathComplete)
				{
					this._rejected = true;
				}
				if (!navMeshAgent.SetPath(this._path) || navMeshAgent.path.corners.IsNullOrEmpty<Vector3>())
				{
				}
			}
			return result;
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x002DB298 File Offset: 0x002D9698
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
				if (navMeshAgent.CalculatePath(destination, this._path))
				{
					if (this._path.status != NavMeshPathStatus.PathComplete)
					{
						this._rejected = true;
					}
					if (navMeshAgent.SetPath(this._path))
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x002DB328 File Offset: 0x002D9728
		public override void OnPause(bool paused)
		{
			if (!paused)
			{
				AgentActor agent = base.Agent;
				agent.ActivateTransfer(true);
				ActionPoint targetInSightActionPoint = agent.TargetInSightActionPoint;
				if (targetInSightActionPoint != null)
				{
					Vector3? vector = new Vector3?(targetInSightActionPoint.LocatedPosition);
					agent.DestPosition = vector;
					Vector3? vector2 = vector;
					this.SetDestinationForce(vector2.Value);
				}
			}
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x002DB380 File Offset: 0x002D9780
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			if (agent.DestPosition != null)
			{
				agent.DestPosition = null;
			}
			this._path = null;
		}

		// Token: 0x04005A44 RID: 23108
		[SerializeField]
		private bool _enterClose;

		// Token: 0x04005A45 RID: 23109
		private bool _rejected;

		// Token: 0x04005A46 RID: 23110
		private int _stopCount;

		// Token: 0x04005A47 RID: 23111
		private NavMeshPath _path;
	}
}

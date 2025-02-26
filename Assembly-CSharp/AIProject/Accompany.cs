using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CBB RID: 3259
	[TaskCategory("")]
	public class Accompany : AgentMovement
	{
		// Token: 0x060069BA RID: 27066 RVA: 0x002D0544 File Offset: 0x002CE944
		public override void OnStart()
		{
			base.OnStart();
			if (base.Agent.Partner == null)
			{
				return;
			}
			base.Agent.ActivateTransfer(true);
			Vector3 destination = this.DesiredPosition(base.Agent.Partner);
			this.SetDestination(destination);
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x002D0594 File Offset: 0x002CE994
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Partner == null)
			{
				return TaskStatus.Failure;
			}
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			NavMeshAgent navMeshAgent = agent.NavMeshAgent;
			Vector3 vector = this.DesiredPosition(agent.Partner);
			if (Vector3.Distance(vector, agent.Position) >= agentProfile.RestDistance)
			{
				this.SetDestination(vector);
				this._moved = true;
			}
			else
			{
				NavMeshPathStatus pathStatus = navMeshAgent.pathStatus;
				if (pathStatus != NavMeshPathStatus.PathPartial && pathStatus != NavMeshPathStatus.PathInvalid)
				{
					if (!navMeshAgent.pathPending)
					{
						if (navMeshAgent.remainingDistance < agentProfile.RestDistance && agent.IsRunning)
						{
							agent.IsRunning = false;
						}
						if (this._moved && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
						{
							this.Stop();
							this._moved = false;
						}
					}
				}
				else if (Vector3.Distance(agent.Position, agent.Partner.Position) < agentProfile.RestDistance)
				{
					this.Stop();
					if (agent.IsRunning)
					{
						agent.IsRunning = false;
					}
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x002D06CC File Offset: 0x002CEACC
		private Vector3 DesiredPosition(Actor partner)
		{
			float shapeBodyValue = base.Agent.ChaControl.GetShapeBodyValue(0);
			return partner.Position + partner.Rotation * Singleton<Manager.Resources>.Instance.AgentProfile.GetOffsetInParty(shapeBodyValue);
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x002D0711 File Offset: 0x002CEB11
		protected override bool SetDestination(Vector3 destination)
		{
			if (base.Agent.NavMeshAgent.isStopped)
			{
				base.Agent.NavMeshAgent.isStopped = false;
			}
			return base.Agent.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x002D074A File Offset: 0x002CEB4A
		protected override void UpdateRotation(bool update)
		{
			base.Agent.NavMeshAgent.updateRotation = update;
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x002D0760 File Offset: 0x002CEB60
		protected override bool HasPath()
		{
			NavMeshAgent navMeshAgent = base.Agent.NavMeshAgent;
			return navMeshAgent.hasPath && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x002D0795 File Offset: 0x002CEB95
		protected override Vector3 Velocity()
		{
			return base.Agent.NavMeshAgent.velocity;
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x002D07A8 File Offset: 0x002CEBA8
		protected override bool HasArrived()
		{
			if (base.Agent.enabled)
			{
				float num = (!base.Agent.NavMeshAgent.pathPending) ? base.Agent.NavMeshAgent.remainingDistance : float.PositiveInfinity;
				return num <= Singleton<Manager.Resources>.Instance.AgentProfile.RestDistance;
			}
			return false;
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x002D080C File Offset: 0x002CEC0C
		protected override void Stop()
		{
			if (base.Agent.NavMeshAgent.hasPath)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x002D0834 File Offset: 0x002CEC34
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this._moved = false;
				this.Stop();
			}
		}

		// Token: 0x040059B9 RID: 22969
		private bool _moved;

		// Token: 0x040059BA RID: 22970
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x040059BB RID: 22971
		private Vector3 _prevDestination = Vector3.zero;
	}
}

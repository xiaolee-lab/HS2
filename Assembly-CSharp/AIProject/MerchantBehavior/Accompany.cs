using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAB RID: 3499
	[TaskCategory("商人")]
	public class Accompany : MerchantMovement
	{
		// Token: 0x06006D19 RID: 27929 RVA: 0x002E8187 File Offset: 0x002E6587
		public override void OnAwake()
		{
			base.OnAwake();
			this._merchant = base.Merchant;
			this._navMeshAgent = this._merchant.NavMeshAgent;
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x002E81AC File Offset: 0x002E65AC
		public override void OnStart()
		{
			base.OnStart();
			this._partner = this._merchant.Partner;
			if (this._partner == null)
			{
				return;
			}
			this._merchant.ActivateLocomotionMotion();
			this._merchant.ActivateNavMeshAgent();
			Vector3 destination = this.DesiredPosition(this._partner);
			this.SetDestination(destination);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x002E8210 File Offset: 0x002E6610
		public override TaskStatus OnUpdate()
		{
			if (this._partner == null)
			{
				return TaskStatus.Failure;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return TaskStatus.Running;
			}
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			Vector3 vector = this.DesiredPosition(this._partner);
			if (agentProfile.RestDistance <= Vector3.Distance(vector, this._merchant.Position))
			{
				this.SetDestination(vector);
				this._moved = true;
			}
			else
			{
				NavMeshPathStatus pathStatus = this._navMeshAgent.pathStatus;
				if (pathStatus != NavMeshPathStatus.PathPartial && pathStatus != NavMeshPathStatus.PathInvalid)
				{
					if (!this._navMeshAgent.pathPending)
					{
						if (this._navMeshAgent.remainingDistance < agentProfile.RestDistance && this._merchant.IsRunning)
						{
							this._merchant.IsRunning = false;
						}
						if (this._moved && this._navMeshAgent.remainingDistance < this._navMeshAgent.stoppingDistance)
						{
							this.Stop();
							this._moved = false;
						}
					}
				}
				else if (Vector3.Distance(this._merchant.Position, this._partner.Position) < agentProfile.RestDistance)
				{
					this.Stop();
					if (this._merchant.IsRunning)
					{
						this._merchant.IsRunning = false;
					}
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x002E8378 File Offset: 0x002E6778
		private Vector3 DesiredPosition(Actor partner)
		{
			float shapeBodyValue = base.Merchant.ChaControl.GetShapeBodyValue(0);
			return partner.Position + partner.Rotation * Singleton<Manager.Resources>.Instance.AgentProfile.GetOffsetInParty(shapeBodyValue);
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x002E83BD File Offset: 0x002E67BD
		protected override bool SetDestination(Vector3 destination)
		{
			if (this._navMeshAgent.isStopped)
			{
				this._navMeshAgent.isStopped = false;
			}
			return this._navMeshAgent.SetDestination(destination);
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x002E83E7 File Offset: 0x002E67E7
		protected override void UpdateRotation(bool update)
		{
			this._navMeshAgent.updateRotation = update;
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x002E83F5 File Offset: 0x002E67F5
		protected override bool HasPath()
		{
			return this._navMeshAgent.hasPath && this._navMeshAgent.stoppingDistance < this._navMeshAgent.remainingDistance;
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x002E8422 File Offset: 0x002E6822
		protected override Vector3 Velocity()
		{
			return this._navMeshAgent.velocity;
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x002E8430 File Offset: 0x002E6830
		protected override bool HasArrived()
		{
			if (this._merchant.enabled)
			{
				float num = (!this._navMeshAgent.pathPending) ? this._navMeshAgent.remainingDistance : float.PositiveInfinity;
				return num <= Singleton<Manager.Resources>.Instance.AgentProfile.RestDistance;
			}
			return false;
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x002E848A File Offset: 0x002E688A
		protected override void Stop()
		{
			if (this._navMeshAgent.hasPath)
			{
				this._navMeshAgent.isStopped = true;
			}
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x002E84A8 File Offset: 0x002E68A8
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this._moved = false;
				this.Stop();
			}
		}

		// Token: 0x04005B24 RID: 23332
		private bool _moved;

		// Token: 0x04005B25 RID: 23333
		private MerchantActor _merchant;

		// Token: 0x04005B26 RID: 23334
		private NavMeshAgent _navMeshAgent;

		// Token: 0x04005B27 RID: 23335
		private Actor _partner;
	}
}

using System;
using AIProject.Animal;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB3 RID: 3251
	[TaskCategory("")]
	public class ChaseAnimal : AgentMovement
	{
		// Token: 0x0600698D RID: 27021 RVA: 0x002CF08C File Offset: 0x002CD48C
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.StateType = State.Type.Normal;
			base.Agent.ActivateTransfer(true);
			float _speed = base.Agent.NavMeshAgent.speed;
			ObservableEasing.Linear(1f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				this.Agent.NavMeshAgent.speed = Mathf.Lerp(_speed, Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed, x.Value);
			});
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x002CF100 File Offset: 0x002CD500
		public override TaskStatus OnUpdate()
		{
			AnimalBase targetInSightAnimal = base.Agent.TargetInSightAnimal;
			if (targetInSightAnimal == null)
			{
				return TaskStatus.Failure;
			}
			if (!targetInSightAnimal.IsWithAgentFree(base.Agent))
			{
				return TaskStatus.Failure;
			}
			AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
			if (!base.Agent.NavMeshAgent.pathPending)
			{
				Vector3 position = targetInSightAnimal.Position;
				this.SetDestination(position);
			}
			return this.HasArrived() ? TaskStatus.Success : TaskStatus.Running;
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x002CF180 File Offset: 0x002CD580
		public override void OnEnd()
		{
			this.Stop();
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06006990 RID: 27024 RVA: 0x002CF188 File Offset: 0x002CD588
		private float RemainingDistance
		{
			get
			{
				if (base.Agent.enabled)
				{
					return (!base.Agent.NavMeshAgent.pathPending) ? base.Agent.NavMeshAgent.remainingDistance : float.PositiveInfinity;
				}
				return float.PositiveInfinity;
			}
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x002CF1DC File Offset: 0x002CD5DC
		protected override bool HasArrived()
		{
			return this.RemainingDistance <= Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x002CF20C File Offset: 0x002CD60C
		protected override bool HasPath()
		{
			return base.Agent.NavMeshAgent.hasPath && Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance < base.Agent.NavMeshAgent.remainingDistance;
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x002CF25A File Offset: 0x002CD65A
		protected override bool SetDestination(Vector3 destination)
		{
			if (base.Agent.NavMeshAgent.isStopped)
			{
				base.Agent.NavMeshAgent.isStopped = false;
			}
			return base.Agent.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x002CF293 File Offset: 0x002CD693
		protected override void Stop()
		{
			if (base.Agent.NavMeshAgent.hasPath)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x002CF2BB File Offset: 0x002CD6BB
		protected override void UpdateRotation(bool update)
		{
			base.Agent.NavMeshAgent.updateRotation = update;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x002CF2CE File Offset: 0x002CD6CE
		protected override Vector3 Velocity()
		{
			return base.Agent.NavMeshAgent.velocity;
		}
	}
}

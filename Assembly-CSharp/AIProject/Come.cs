using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB8 RID: 3256
	[TaskCategory("")]
	public class Come : AgentMovement
	{
		// Token: 0x060069A4 RID: 27044 RVA: 0x002CFE84 File Offset: 0x002CE284
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Normal;
			agent.ActivateTransfer(true);
			float speed = agent.NavMeshAgent.speed;
			ObservableEasing.Linear(1f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				agent.NavMeshAgent.speed = Mathf.Lerp(speed, Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed, x.Value);
			});
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x002CFEFC File Offset: 0x002CE2FC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActor == null)
			{
				return TaskStatus.Failure;
			}
			Actor targetInSightActor = agent.TargetInSightActor;
			AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
			if (!agent.NavMeshAgent.pathPending)
			{
				Vector3 position = targetInSightActor.Position;
				this.SetDestination(position);
			}
			if (!this.HasArrived())
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x002CFF67 File Offset: 0x002CE367
		public override void OnEnd()
		{
			base.Agent.StopNavMeshAgent();
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x002CFF74 File Offset: 0x002CE374
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopNavMeshAgent();
			}
			else if (!agent.NavMeshAgent.pathPending && agent.TargetInSightActor != null)
			{
				Vector3 position = agent.TargetInSightActor.Position;
				this.SetDestination(position);
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060069A8 RID: 27048 RVA: 0x002CFFD0 File Offset: 0x002CE3D0
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

		// Token: 0x060069A9 RID: 27049 RVA: 0x002D0024 File Offset: 0x002CE424
		protected override bool HasArrived()
		{
			return this.RemainingDistance <= Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x002D0054 File Offset: 0x002CE454
		protected override bool HasPath()
		{
			return base.Agent.NavMeshAgent.hasPath && base.Agent.NavMeshAgent.remainingDistance > Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x002D00A2 File Offset: 0x002CE4A2
		protected override bool SetDestination(Vector3 destination)
		{
			if (base.Agent.NavMeshAgent.isStopped)
			{
				base.Agent.NavMeshAgent.isStopped = false;
			}
			return base.Agent.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x002D00DB File Offset: 0x002CE4DB
		protected override void Stop()
		{
			if (base.Agent.NavMeshAgent.hasPath)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x002D0103 File Offset: 0x002CE503
		protected override void UpdateRotation(bool update)
		{
			base.Agent.NavMeshAgent.updateRotation = update;
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x002D0116 File Offset: 0x002CE516
		protected override Vector3 Velocity()
		{
			return base.Agent.NavMeshAgent.velocity;
		}
	}
}

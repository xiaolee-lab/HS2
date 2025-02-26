using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB2 RID: 3250
	[TaskCategory("")]
	public class Chase : AgentMovement
	{
		// Token: 0x06006980 RID: 27008 RVA: 0x002CEC88 File Offset: 0x002CD088
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			agent.ActivateTransfer(true);
			float speed = agent.NavMeshAgent.speed;
			ObservableEasing.Linear(1f, false).TakeUntilDestroy(agent).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				agent.NavMeshAgent.speed = Mathf.Lerp(speed, Singleton<Manager.Resources>.Instance.LocomotionProfile.AgentSpeed.walkSpeed, x.Value);
			});
			AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
			this._arrivedDistance = rangeSetting.arrivedDistance;
			this._acceptableHeight = rangeSetting.acceptableHeight;
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x002CED34 File Offset: 0x002CD134
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActor == null)
			{
				return TaskStatus.Failure;
			}
			Actor targetInSightActor = agent.TargetInSightActor;
			if (targetInSightActor.Partner != null)
			{
				agent.ClearDesire(Desire.Type.Lonely);
				agent.ClearDesire(Desire.Type.H);
				return TaskStatus.Failure;
			}
			AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
			if (!agent.NavMeshAgent.pathPending)
			{
				Vector3 position = targetInSightActor.Position;
				this.SetDestination(position);
			}
			TaskStatus result;
			if (!this.HasArrived(agent, targetInSightActor))
			{
				result = TaskStatus.Running;
			}
			else
			{
				result = TaskStatus.Success;
			}
			return result;
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x002CEDD4 File Offset: 0x002CD1D4
		public override void OnEnd()
		{
			base.Agent.StopNavMeshAgent();
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x002CEDE4 File Offset: 0x002CD1E4
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopNavMeshAgent();
			}
			else if (!base.Agent.NavMeshAgent.pathPending && agent.TargetInSightActor != null)
			{
				Vector3 position = agent.TargetInSightActor.Position;
				this.SetDestination(position);
			}
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x002CEE44 File Offset: 0x002CD244
		private bool HasArrived(AgentActor agent, Actor target)
		{
			if (target == null)
			{
				return false;
			}
			Vector3 position = agent.Position;
			position.y = 0f;
			Vector3 position2 = target.Position;
			position2.y = 0f;
			float num = Vector3.Distance(position, position2);
			float num2 = Mathf.Abs(agent.Position.y - target.Position.y);
			return target != null && num <= this._arrivedDistance && num2 <= this._acceptableHeight;
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06006985 RID: 27013 RVA: 0x002CEEDC File Offset: 0x002CD2DC
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

		// Token: 0x06006986 RID: 27014 RVA: 0x002CEF30 File Offset: 0x002CD330
		protected override bool HasArrived()
		{
			return this.RemainingDistance <= Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x002CEF60 File Offset: 0x002CD360
		protected override bool HasPath()
		{
			return base.Agent.NavMeshAgent.hasPath && base.Agent.NavMeshAgent.remainingDistance > Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x002CEFAE File Offset: 0x002CD3AE
		protected override bool SetDestination(Vector3 destination)
		{
			if (base.Agent.NavMeshAgent.isStopped)
			{
				base.Agent.NavMeshAgent.isStopped = false;
			}
			return base.Agent.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x002CEFE7 File Offset: 0x002CD3E7
		protected override void Stop()
		{
			if (base.Agent.NavMeshAgent.hasPath)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x002CF00F File Offset: 0x002CD40F
		protected override void UpdateRotation(bool update)
		{
			base.Agent.NavMeshAgent.updateRotation = update;
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x002CF022 File Offset: 0x002CD422
		protected override Vector3 Velocity()
		{
			return base.Agent.NavMeshAgent.velocity;
		}

		// Token: 0x040059AF RID: 22959
		private float _arrivedDistance;

		// Token: 0x040059B0 RID: 22960
		private float _acceptableHeight;
	}
}

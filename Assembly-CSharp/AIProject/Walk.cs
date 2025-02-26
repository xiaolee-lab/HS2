using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CD4 RID: 3284
	public class Walk : AgentMovement
	{
		// Token: 0x06006A2E RID: 27182 RVA: 0x002D2A24 File Offset: 0x002D0E24
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x002D2A4C File Offset: 0x002D0E4C
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartPatrol();
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x002D2A64 File Offset: 0x002D0E64
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> walkRoute = agent.WalkRoute;
			if ((walkRoute.Count == 0 && base.Agent.DestWaypoint == null) || !agent.LivesPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x002D2ABB File Offset: 0x002D0EBB
		public override void OnEnd()
		{
			base.Agent.StopPatrol();
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x002D2AC8 File Offset: 0x002D0EC8
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopPatrol();
			}
			else
			{
				agent.ResetLocomotionAnimation(true);
				agent.SetOriginalDestination();
				agent.ResumePatrol();
			}
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x002D2B00 File Offset: 0x002D0F00
		public override void OnBehaviorRestart()
		{
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x002D2B02 File Offset: 0x002D0F02
		protected override bool SetDestination(Vector3 destination)
		{
			base.Agent.NavMeshAgent.isStopped = false;
			return base.Agent.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x002D2B26 File Offset: 0x002D0F26
		protected override void UpdateRotation(bool update)
		{
			base.Agent.NavMeshAgent.updateRotation = update;
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x002D2B3C File Offset: 0x002D0F3C
		protected override bool HasPath()
		{
			return base.Agent.NavMeshAgent.hasPath && base.Agent.NavMeshAgent.remainingDistance > Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.arrivedDistance;
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x002D2B8A File Offset: 0x002D0F8A
		protected override Vector3 Velocity()
		{
			return base.Agent.NavMeshAgent.velocity;
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x002D2B9C File Offset: 0x002D0F9C
		protected override bool HasArrived()
		{
			if (base.Agent.NavMeshAgent.enabled)
			{
				float num = (!base.Agent.NavMeshAgent.pathPending) ? base.Agent.NavMeshAgent.remainingDistance : float.PositiveInfinity;
				return num <= Singleton<Manager.Resources>.Instance.AgentProfile.WalkSetting.arrivedDistance;
			}
			return false;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x002D2C0D File Offset: 0x002D100D
		protected override void Stop()
		{
			if (base.Agent.NavMeshAgent.hasPath)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
		}
	}
}

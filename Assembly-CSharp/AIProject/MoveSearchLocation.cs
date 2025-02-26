using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CD2 RID: 3282
	public class MoveSearchLocation : AgentAction
	{
		// Token: 0x06006A20 RID: 27168 RVA: 0x002D2854 File Offset: 0x002D0C54
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x002D287C File Offset: 0x002D0C7C
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartLocationPatrol();
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x002D2894 File Offset: 0x002D0C94
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesActionCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchActionRoute = agent.SearchActionRoute;
			if ((searchActionRoute.Count == 0 && agent.DestWaypoint == null) || !agent.LivesLocationPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x002D28E8 File Offset: 0x002D0CE8
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.StopLocationPatrol();
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x002D2904 File Offset: 0x002D0D04
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopLocationPatrol();
			}
			else
			{
				this.Replay(agent);
			}
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x002D2930 File Offset: 0x002D0D30
		public override void OnBehaviorRestart()
		{
		}
	}
}

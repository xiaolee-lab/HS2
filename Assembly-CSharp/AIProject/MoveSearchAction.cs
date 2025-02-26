using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CCE RID: 3278
	public class MoveSearchAction : AgentAction
	{
		// Token: 0x06006A04 RID: 27140 RVA: 0x002D23A0 File Offset: 0x002D07A0
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x002D23C8 File Offset: 0x002D07C8
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartActionPatrol();
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x002D23E0 File Offset: 0x002D07E0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesActionCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchActionRoute = agent.SearchActionRoute;
			if ((searchActionRoute.Count == 0 && agent.DestWaypoint == null) || !agent.LivesActionPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x002D2434 File Offset: 0x002D0834
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.StopActionPatrol();
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x002D2450 File Offset: 0x002D0850
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopActionPatrol();
			}
			else
			{
				this.Replay(agent);
			}
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x002D247C File Offset: 0x002D087C
		public override void OnBehaviorRestart()
		{
		}
	}
}

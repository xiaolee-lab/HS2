using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CD3 RID: 3283
	public class MoveSearchPlayer : AgentAction
	{
		// Token: 0x06006A27 RID: 27175 RVA: 0x002D293C File Offset: 0x002D0D3C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x002D2964 File Offset: 0x002D0D64
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartPlayerPatrol();
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x002D297C File Offset: 0x002D0D7C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesPlayerCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchPlayerRoute = agent.SearchPlayerRoute;
			if ((searchPlayerRoute.Count == 0 && agent.DestWaypoint == null) || !agent.LivesPlayerPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x002D29D0 File Offset: 0x002D0DD0
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.StopPlayerPatrol();
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x002D29EC File Offset: 0x002D0DEC
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopPlayerPatrol();
			}
			else
			{
				this.Replay(agent);
			}
		}

		// Token: 0x06006A2C RID: 27180 RVA: 0x002D2A18 File Offset: 0x002D0E18
		public override void OnBehaviorRestart()
		{
		}
	}
}

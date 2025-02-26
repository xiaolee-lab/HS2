using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CCF RID: 3279
	public class MoveSearchActor : AgentAction
	{
		// Token: 0x06006A0B RID: 27147 RVA: 0x002D2488 File Offset: 0x002D0888
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x002D24B0 File Offset: 0x002D08B0
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartActorPatrol();
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x002D24C8 File Offset: 0x002D08C8
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesActorCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchActorRoute = agent.SearchActorRoute;
			if ((searchActorRoute.Count == 0 && agent.DestWaypoint == null) || !agent.LivesActorPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x002D251C File Offset: 0x002D091C
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.StopActorPatrol();
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x002D2538 File Offset: 0x002D0938
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopActorPatrol();
			}
			else
			{
				this.Replay(agent);
			}
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x002D2564 File Offset: 0x002D0964
		public override void OnBehaviorRestart()
		{
		}
	}
}

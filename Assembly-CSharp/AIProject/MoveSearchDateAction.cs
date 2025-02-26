using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CD1 RID: 3281
	public class MoveSearchDateAction : AgentAction
	{
		// Token: 0x06006A19 RID: 27161 RVA: 0x002D276C File Offset: 0x002D0B6C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			base.OnStart();
			agent.StateType = State.Type.Normal;
			this.Replay(agent);
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x002D2794 File Offset: 0x002D0B94
		private void Replay(AgentActor agent)
		{
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartDateActionPatrol();
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x002D27AC File Offset: 0x002D0BAC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.LivesDateActionCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchDateActionRoute = agent.SearchDateActionRoute;
			if ((searchDateActionRoute.Count == 0 && agent.DestWaypoint == null) || !agent.LivesActionPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x002D2800 File Offset: 0x002D0C00
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.StopActionPatrol();
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x002D281C File Offset: 0x002D0C1C
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				agent.StopDateActionPatrol();
			}
			else
			{
				this.Replay(agent);
			}
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x002D2848 File Offset: 0x002D0C48
		public override void OnBehaviorRestart()
		{
		}
	}
}

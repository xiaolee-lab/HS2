using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CD0 RID: 3280
	public class MoveSearchAnimal : AgentAction
	{
		// Token: 0x06006A12 RID: 27154 RVA: 0x002D256E File Offset: 0x002D096E
		public override void OnStart()
		{
			this._agent = base.Agent;
			base.OnStart();
			this._agent.StateType = State.Type.Normal;
			this.Replay(this._agent);
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x002D259A File Offset: 0x002D099A
		private void Replay(AgentActor agent)
		{
			agent.SearchAnimalEmpty = false;
			agent.ResetLocomotionAnimation(true);
			agent.SetOriginalDestination();
			agent.StartAnimalPatrol();
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x002D25B8 File Offset: 0x002D09B8
		public override TaskStatus OnUpdate()
		{
			PointManager pointManager = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.PointAgent;
			Waypoint[] source = (!(pointManager != null)) ? null : pointManager.Waypoints;
			if (source.IsNullOrEmpty<Waypoint>())
			{
				bool flag = false;
				Dictionary<int, List<Waypoint>> dictionary = (!(pointManager != null)) ? null : pointManager.HousingWaypointTable;
				if (!dictionary.IsNullOrEmpty<int, List<Waypoint>>())
				{
					foreach (KeyValuePair<int, List<Waypoint>> keyValuePair in dictionary)
					{
						List<Waypoint> value = keyValuePair.Value;
						if (flag = !value.IsNullOrEmpty<Waypoint>())
						{
							break;
						}
					}
				}
				if (!flag)
				{
					return TaskStatus.Running;
				}
			}
			if (this._agent.SearchAnimalEmpty)
			{
				return TaskStatus.Failure;
			}
			if (this._agent.LivesAnimalCalc)
			{
				return TaskStatus.Running;
			}
			Queue<Waypoint> searchAnimalRoute = this._agent.SearchAnimalRoute;
			if ((searchAnimalRoute.Count == 0 && this._agent.DestWaypoint == null) || !this._agent.LivesAnimalPatrol)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x002D26FC File Offset: 0x002D0AFC
		public override void OnEnd()
		{
			this._agent.StopAnimalPatrol();
			base.OnEnd();
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x002D270F File Offset: 0x002D0B0F
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this._agent.StopAnimalPatrol();
			}
			else
			{
				this.Replay(this._agent);
			}
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x002D2733 File Offset: 0x002D0B33
		public override void OnBehaviorRestart()
		{
			if (this._agent != null)
			{
				this._agent.StopForcedAnimalPatrol();
				this._agent.ClearAnimalRoutePoints();
			}
			base.OnBehaviorComplete();
		}

		// Token: 0x040059D5 RID: 22997
		private AgentActor _agent;
	}
}

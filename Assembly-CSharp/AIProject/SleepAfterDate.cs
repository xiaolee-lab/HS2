using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace AIProject
{
	// Token: 0x02000CA9 RID: 3241
	[TaskCategory("")]
	public class SleepAfterDate : AgentAction
	{
		// Token: 0x0600695F RID: 26975 RVA: 0x002CDAA4 File Offset: 0x002CBEA4
		public override void OnStart()
		{
			base.OnStart();
			if (base.Agent.CurrentPoint == null)
			{
				base.Agent.CurrentPoint = base.Agent.TargetInSightActionPoint;
			}
			this._missing = (base.Agent.CurrentPoint == null);
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x002CDAFC File Offset: 0x002CBEFC
		public override TaskStatus OnUpdate()
		{
			if (this._missing)
			{
				this.Complete();
				return TaskStatus.Success;
			}
			ActorAnimation animation = base.Agent.Animation;
			if (animation.PlayingActAnimation)
			{
				return TaskStatus.Running;
			}
			if (base.Agent.Schedule.enabled)
			{
				return TaskStatus.Running;
			}
			if (this._endAction != null)
			{
				this._endAction.OnNext(Unit.Default);
			}
			if (animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x002CDB80 File Offset: 0x002CBF80
		public override void OnEnd()
		{
			base.OnEnd();
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x002CDB88 File Offset: 0x002CBF88
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			int desireKey = Desire.GetDesireKey(Desire.Type.Sleep);
			agent.SetDesire(desireKey, 0f);
			agent.SetStatus(0, 50f);
			agent.HealSickBySleep();
			base.Agent.ActivateNavMeshAgent();
			base.Agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
			agent.AgentData.AddAppendEventFlagParam(6, 1);
		}

		// Token: 0x040059A3 RID: 22947
		private Subject<Unit> _endAction = new Subject<Unit>();

		// Token: 0x040059A4 RID: 22948
		private bool _missing;
	}
}

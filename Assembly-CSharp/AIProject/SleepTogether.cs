using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace AIProject
{
	// Token: 0x02000CAA RID: 3242
	[TaskCategory("")]
	public class SleepTogether : AgentAction
	{
		// Token: 0x06006964 RID: 26980 RVA: 0x002CDC6C File Offset: 0x002CC06C
		public override void OnStart()
		{
			base.OnStart();
			if (base.Agent.CurrentPoint == null)
			{
				base.Agent.CurrentPoint = base.Agent.TargetInSightActionPoint;
			}
			this._missing = (base.Agent.CurrentPoint == null);
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x002CDCC4 File Offset: 0x002CC0C4
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

		// Token: 0x06006966 RID: 26982 RVA: 0x002CDD48 File Offset: 0x002CC148
		public override void OnEnd()
		{
			base.OnEnd();
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x002CDD50 File Offset: 0x002CC150
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
			agent.SetDefaultStateHousingItem();
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

		// Token: 0x040059A5 RID: 22949
		private Subject<Unit> _endAction = new Subject<Unit>();

		// Token: 0x040059A6 RID: 22950
		private bool _missing;
	}
}

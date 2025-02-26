using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CA8 RID: 3240
	[TaskCategory("")]
	public class Sleep : AgentAction
	{
		// Token: 0x06006959 RID: 26969 RVA: 0x002CD408 File Offset: 0x002CB808
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Sleep;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			if (agent.CurrentPoint == null)
			{
				return;
			}
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			ActionPointInfo actionPointInfo = agent.TargetInSightActionPoint.GetActionPointInfo(agent);
			agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			int num = actionPointInfo2.eventID;
			agent.ActionID = num;
			int num2 = num;
			num = actionPointInfo2.poseID;
			agent.PoseID = num;
			int num3 = num;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num3];
			UnityEx.ValueTuple<int, string> valueTuple;
			if (AIProject.Definitions.Action.NameTable.TryGetValue(agent.EventKey, out valueTuple))
			{
				int item = valueTuple.Item1;
				if (item != agent.ActionID)
				{
					return;
				}
			}
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num2, num3, playState);
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
			this._onActionPlayDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			});
			if (animInfo.hasAction)
			{
				this._onActionPlayDisposable = agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
				{
					agent.Animation.PlayActionAnimation(animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo2.actionName, animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, true);
				if (agent.AgentData.ScheduleEnabled)
				{
					Actor.BehaviorSchedule schedule = agent.Schedule;
					schedule.enabled = agent.AgentData.ScheduleEnabled;
					schedule.elapsedTime = agent.AgentData.ScheduleElapsedTime;
					schedule.duration = agent.AgentData.ScheduleDuration;
					agent.Schedule = schedule;
					agent.AgentData.ScheduleEnabled = false;
				}
			}
			MapUIContainer.AddSystemLog(string.Format("{0}が寝ました", MapUIContainer.CharaNameColor(agent)), true);
		}

		// Token: 0x0600695A RID: 26970 RVA: 0x002CD828 File Offset: 0x002CBC28
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			UnityEx.ValueTuple<int, string> valueTuple;
			if (AIProject.Definitions.Action.NameTable.TryGetValue(agent.EventKey, out valueTuple))
			{
				int item = valueTuple.Item1;
				if (item != agent.ActionID)
				{
					agent.ChangeBehavior(Desire.ActionType.Normal);
					return TaskStatus.Failure;
				}
			}
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x002CD880 File Offset: 0x002CBC80
		public override void OnEnd()
		{
			base.OnEnd();
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			if (this._onActionPlayDisposable != null)
			{
				this._onActionPlayDisposable.Dispose();
			}
			if (this._onCompleteActionDisposable != null)
			{
				this._onCompleteActionDisposable.Dispose();
			}
			AgentActor agent = base.Agent;
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x002CD8F0 File Offset: 0x002CBCF0
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			int desireKey = Desire.GetDesireKey(Desire.Type.Sleep);
			agent.SetDesire(desireKey, 0f);
			this.OnCompletedStateTask();
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 50)
			{
				agent.ChangeBehavior(Desire.ActionType.EndTaskSecondSleep);
			}
			else
			{
				agent.ActivateNavMeshAgent();
				agent.SetActiveOnEquipedItem(true);
				agent.ClearItems();
				agent.ClearParticles();
				agent.Animation.EndStates();
				agent.ResetActionFlag();
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
				agent.AgentData.YobaiTrigger = false;
			}
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x002CD9D4 File Offset: 0x002CBDD4
		protected void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			agent.SetStatus(0, 50f);
			agent.SetDefaultImmoral();
			agent.HealSickBySleep();
			agent.AgentData.AddAppendEventFlagParam(0, 1);
		}

		// Token: 0x040059A0 RID: 22944
		private IDisposable _onActionPlayDisposable;

		// Token: 0x040059A1 RID: 22945
		private IDisposable _onEndActionDisposable;

		// Token: 0x040059A2 RID: 22946
		private IDisposable _onCompleteActionDisposable;
	}
}

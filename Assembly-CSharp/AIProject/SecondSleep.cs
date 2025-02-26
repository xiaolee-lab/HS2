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
	// Token: 0x02000CA6 RID: 3238
	[TaskCategory("")]
	public class SecondSleep : AgentAction
	{
		// Token: 0x0600694D RID: 26957 RVA: 0x002CC818 File Offset: 0x002CAC18
		public override void OnStart()
		{
			base.OnStart();
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
				ThresholdInt secondSleepDurationMinMax = Singleton<Manager.Resources>.Instance.AgentProfile.SecondSleepDurationMinMax;
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo2.actionName, secondSleepDurationMinMax.min, secondSleepDurationMinMax.max, animInfo.hasAction, true);
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
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x002CCC2C File Offset: 0x002CB02C
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
			return agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x002CCC80 File Offset: 0x002CB080
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

		// Token: 0x06006950 RID: 26960 RVA: 0x002CCCF0 File Offset: 0x002CB0F0
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.ApplySituationResultParameter(34);
			this.OnCompletedStateTask();
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			agent.CurrentPoint.SetActiveMapItemObjs(true);
			agent.CurrentPoint.ReleaseSlot(agent);
			agent.CurrentPoint = null;
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x002CCD7C File Offset: 0x002CB17C
		protected void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			agent.SetStatus(0, 50f);
			agent.SetDefaultImmoral();
			agent.HealSickBySleep();
		}

		// Token: 0x0400599A RID: 22938
		private IDisposable _onActionPlayDisposable;

		// Token: 0x0400599B RID: 22939
		private IDisposable _onEndActionDisposable;

		// Token: 0x0400599C RID: 22940
		private IDisposable _onCompleteActionDisposable;
	}
}

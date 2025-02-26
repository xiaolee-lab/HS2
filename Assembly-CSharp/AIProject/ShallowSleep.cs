using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CA7 RID: 3239
	[TaskCategory("")]
	public class ShallowSleep : AgentAction
	{
		// Token: 0x06006953 RID: 26963 RVA: 0x002CCE30 File Offset: 0x002CB230
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Sleep;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
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
			agent.CurrentPoint.DestroyByproduct(num2, num3);
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num2, num3, playState);
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, animInfo.inFadeOutTime, animInfo.layer);
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
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo2.actionName, 100, 200, animInfo.hasAction, true);
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

		// Token: 0x06006954 RID: 26964 RVA: 0x002CD205 File Offset: 0x002CB605
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x002CD218 File Offset: 0x002CB618
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

		// Token: 0x06006956 RID: 26966 RVA: 0x002CD288 File Offset: 0x002CB688
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			int desireKey = Desire.GetDesireKey(Desire.Type.Sleep);
			agent.SetDesire(desireKey, 0f);
			this.OnCompletedStateTask();
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
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
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x002CD344 File Offset: 0x002CB744
		protected void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			agent.SetStatus(0, 50f);
			agent.SetDefaultImmoral();
			agent.HealSickBySleep();
			agent.AgentData.AddAppendEventFlagParam(0, 1);
		}

		// Token: 0x0400599D RID: 22941
		private IDisposable _onActionPlayDisposable;

		// Token: 0x0400599E RID: 22942
		private IDisposable _onEndActionDisposable;

		// Token: 0x0400599F RID: 22943
		private IDisposable _onCompleteActionDisposable;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CF7 RID: 3319
	[TaskCategory("")]
	public class WashHand : AgentAction
	{
		// Token: 0x06006AD3 RID: 27347 RVA: 0x002DA120 File Offset: 0x002D8520
		public override void OnStart()
		{
			WashHand.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey = new WashHand.<OnStart>c__AnonStorey0();
			<OnStart>c__AnonStorey.$this = this;
			<OnStart>c__AnonStorey.agent = base.Agent;
			<OnStart>c__AnonStorey.agent.EventKey = EventType.Wash;
			<OnStart>c__AnonStorey.agent.CurrentPoint = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint;
			<OnStart>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
			<OnStart>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
			<OnStart>c__AnonStorey.agent.ElectNextPoint();
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			PoseKeyPair handWash = instance.AgentProfile.PoseIDTable.HandWash;
			ActionPointInfo actionPointInfo = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint.GetActionPointInfo(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnStart>c__AnonStorey.agent.CurrentPoint.transform;
			GameObject gameObject2 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			<OnStart>c__AnonStorey.agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			<OnStart>c__AnonStorey.agent.ActionID = handWash.postureID;
			<OnStart>c__AnonStorey.agent.PoseID = handWash.poseID;
			PlayState playState = instance.Animation.AgentActionAnimTable[<OnStart>c__AnonStorey.agent.ActionID][<OnStart>c__AnonStorey.agent.PoseID];
			<OnStart>c__AnonStorey.agent.Animation.LoadEventKeyTable(<OnStart>c__AnonStorey.agent.ActionID, <OnStart>c__AnonStorey.agent.PoseID);
			<OnStart>c__AnonStorey.agent.LoadEventItems(playState);
			<OnStart>c__AnonStorey.agent.LoadEventParticles(<OnStart>c__AnonStorey.agent.ActionID, <OnStart>c__AnonStorey.agent.PoseID);
			<OnStart>c__AnonStorey.agent.Animation.InitializeStates(playState);
			WashHand.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey2 = <OnStart>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				inFadeOutTime = playState.MainStateInfo.FadeOutTime,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate,
				isLoop = playState.MainStateInfo.IsLoop,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction,
				layer = playState.MainStateInfo.InStateInfo.StateInfos[0].layer
			};
			<OnStart>c__AnonStorey.agent.Animation.AnimInfo = animInfo;
			<OnStart>c__AnonStorey2.animInfo = animInfo;
			<OnStart>c__AnonStorey.agent.LoadActionFlag(<OnStart>c__AnonStorey.agent.ActionID, <OnStart>c__AnonStorey.agent.PoseID);
			<OnStart>c__AnonStorey.agent.DeactivateNavMeshAgent();
			<OnStart>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
			<OnStart>c__AnonStorey.agent.Animation.PlayInAnimation(<OnStart>c__AnonStorey.animInfo.inEnableBlend, <OnStart>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnStart>c__AnonStorey.animInfo.layer);
			this._onEndActionDisposable = <OnStart>c__AnonStorey.agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				<OnStart>c__AnonStorey.agent.Animation.PlayOutAnimation(<OnStart>c__AnonStorey.animInfo.outEnableBlend, <OnStart>c__AnonStorey.animInfo.outBlendSec, <OnStart>c__AnonStorey.animInfo.layer);
			});
			if (<OnStart>c__AnonStorey.animInfo.hasAction)
			{
				this._onActionPlayDisposable = <OnStart>c__AnonStorey.agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
				{
					<OnStart>c__AnonStorey.agent.Animation.PlayActionAnimation(<OnStart>c__AnonStorey.animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = <OnStart>c__AnonStorey.agent.AnimationAgent.OnCompleteActionAsObservable().Subscribe(delegate(Unit _)
			{
				<OnStart>c__AnonStorey.$this.Complete();
			});
			<OnStart>c__AnonStorey.agent.CurrentPoint.SetSlot(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (<OnStart>c__AnonStorey.animInfo.isLoop)
			{
				<OnStart>c__AnonStorey.agent.SetCurrentSchedule(<OnStart>c__AnonStorey.animInfo.isLoop, actionPointInfo2.actionName, <OnStart>c__AnonStorey.animInfo.loopMinTime, <OnStart>c__AnonStorey.animInfo.loopMaxTime, <OnStart>c__AnonStorey.animInfo.hasAction, false);
			}
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x002DA5F1 File Offset: 0x002D89F1
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x002DA604 File Offset: 0x002D8A04
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
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x002DA668 File Offset: 0x002D8A68
		protected virtual void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
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
				agent.CurrentPoint.CreateByproduct(agent.ActionID, agent.PoseID);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x04005A40 RID: 23104
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A41 RID: 23105
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A42 RID: 23106
		private IDisposable _onCompleteActionDisposable;
	}
}

using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CF6 RID: 3318
	[TaskCategory("")]
	public class WashFace : AgentAction
	{
		// Token: 0x06006ACE RID: 27342 RVA: 0x002D9AB4 File Offset: 0x002D7EB4
		public override void OnStart()
		{
			WashFace.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey = new WashFace.<OnStart>c__AnonStorey0();
			<OnStart>c__AnonStorey.$this = this;
			<OnStart>c__AnonStorey.agent = base.Agent;
			<OnStart>c__AnonStorey.agent.EventKey = EventType.Wash;
			<OnStart>c__AnonStorey.agent.CurrentPoint = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint;
			<OnStart>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
			<OnStart>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
			<OnStart>c__AnonStorey.agent.ElectNextPoint();
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			PoseKeyPair faceWash = instance.AgentProfile.PoseIDTable.FaceWash;
			ActionPointInfo actionPointInfo = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint.GetActionPointInfo(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnStart>c__AnonStorey.agent.CurrentPoint.transform;
			GameObject gameObject2 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			<OnStart>c__AnonStorey.agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			<OnStart>c__AnonStorey.agent.ActionID = faceWash.postureID;
			<OnStart>c__AnonStorey.agent.PoseID = faceWash.poseID;
			PlayState playState = instance.Animation.AgentActionAnimTable[<OnStart>c__AnonStorey.agent.ActionID][<OnStart>c__AnonStorey.agent.PoseID];
			<OnStart>c__AnonStorey.agent.Animation.LoadEventKeyTable(<OnStart>c__AnonStorey.agent.ActionID, <OnStart>c__AnonStorey.agent.PoseID);
			<OnStart>c__AnonStorey.agent.LoadEventItems(playState);
			<OnStart>c__AnonStorey.agent.LoadEventParticles(<OnStart>c__AnonStorey.agent.ActionID, <OnStart>c__AnonStorey.agent.PoseID);
			<OnStart>c__AnonStorey.agent.Animation.InitializeStates(playState);
			WashFace.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey2 = <OnStart>c__AnonStorey;
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

		// Token: 0x06006ACF RID: 27343 RVA: 0x002D9F85 File Offset: 0x002D8385
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x002D9F98 File Offset: 0x002D8398
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
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x002D9FF4 File Offset: 0x002D83F4
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

		// Token: 0x04005A3C RID: 23100
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A3D RID: 23101
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A3E RID: 23102
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A3F RID: 23103
		private IDisposable _onCompleteActionDisposable;
	}
}

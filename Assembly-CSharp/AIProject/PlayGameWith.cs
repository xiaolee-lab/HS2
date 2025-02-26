using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CDB RID: 3291
	[TaskCategory("")]
	public class PlayGameWith : AgentAction
	{
		// Token: 0x06006A67 RID: 27239 RVA: 0x002D4BAC File Offset: 0x002D2FAC
		public override void OnStart()
		{
			PlayGameWith.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey = new PlayGameWith.<OnStart>c__AnonStorey0();
			<OnStart>c__AnonStorey.agent = base.Agent;
			<OnStart>c__AnonStorey.agent.EventKey = EventType.Play;
			<OnStart>c__AnonStorey.agent.CurrentPoint = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint;
			<OnStart>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
			<OnStart>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
			<OnStart>c__AnonStorey.agent.ElectNextPoint();
			<OnStart>c__AnonStorey.agent.CurrentPoint.SetActiveMapItemObjs(false);
			DateActionPointInfo dateActionPointInfo;
			<OnStart>c__AnonStorey.agent.CurrentPoint.TryGetAgentDateActionPointInfo(EventType.Play, out dateActionPointInfo);
			GameObject gameObject = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnStart>c__AnonStorey.agent.CurrentPoint.transform;
			GameObject gameObject2 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? <OnStart>c__AnonStorey.agent.CurrentPoint.transform;
			GameObject gameObject3 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			<OnStart>c__AnonStorey.agent.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameB);
			<OnStart>c__AnonStorey.agent.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			int num = dateActionPointInfo.eventID;
			<OnStart>c__AnonStorey.agent.ActionID = num;
			int num2 = num;
			num = dateActionPointInfo.poseIDA;
			<OnStart>c__AnonStorey.agent.PoseID = num;
			int num3 = num;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num3];
			<OnStart>c__AnonStorey.agent.Animation.LoadEventKeyTable(num2, num3);
			<OnStart>c__AnonStorey.agent.LoadEventItems(playState);
			<OnStart>c__AnonStorey.agent.LoadEventParticles(num2, num3);
			<OnStart>c__AnonStorey.agent.Animation.InitializeStates(playState);
			<OnStart>c__AnonStorey.partner = (<OnStart>c__AnonStorey.agent.Partner as AgentActor);
			<OnStart>c__AnonStorey.partner.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			<OnStart>c__AnonStorey.partner.Mode = Desire.ActionType.Idle;
			<OnStart>c__AnonStorey.partner.ActionID = num2;
			num = dateActionPointInfo.poseIDB;
			<OnStart>c__AnonStorey.partner.PoseID = num;
			int num4 = num;
			<OnStart>c__AnonStorey.partner.Animation.LoadEventKeyTable(num2, num4);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num4];
			<OnStart>c__AnonStorey.partner.LoadEventItems(playState2);
			<OnStart>c__AnonStorey.partner.LoadEventParticles(num2, num4);
			<OnStart>c__AnonStorey.partner.Animation.InitializeStates(playState2);
			PlayGameWith.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey2 = <OnStart>c__AnonStorey;
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
				loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName,
				randomCount = playState.ActionInfo.randomCount,
				oldNormalizedTime = 0f,
				layer = playState.MainStateInfo.InStateInfo.StateInfos[0].layer
			};
			<OnStart>c__AnonStorey.agent.Animation.AnimInfo = animInfo;
			<OnStart>c__AnonStorey2.animInfo = animInfo;
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			<OnStart>c__AnonStorey.partner.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			<OnStart>c__AnonStorey.agent.DisableActionFlag();
			<OnStart>c__AnonStorey.partner.DisableActionFlag();
			<OnStart>c__AnonStorey.agent.DeactivateNavMeshAgent();
			<OnStart>c__AnonStorey.agent.IsKinematic = true;
			<OnStart>c__AnonStorey.partner.SetActiveOnEquipedItem(false);
			<OnStart>c__AnonStorey.partner.ChaControl.setAllLayerWeight(0f);
			<OnStart>c__AnonStorey.partner.DeactivateNavMeshAgent();
			<OnStart>c__AnonStorey.partner.IsKinematic = true;
			<OnStart>c__AnonStorey.agent.CurrentPoint.SetSlot(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
			<OnStart>c__AnonStorey.agent.Animation.PlayInAnimation(<OnStart>c__AnonStorey.animInfo.inEnableBlend, <OnStart>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnStart>c__AnonStorey.animInfo.layer);
			<OnStart>c__AnonStorey.agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, <OnStart>c__AnonStorey.animInfo.layer);
			<OnStart>c__AnonStorey.partner.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			<OnStart>c__AnonStorey.partner.SetStand(t2, playState2.MainStateInfo.InStateInfo.EnableFade, playState2.MainStateInfo.InStateInfo.FadeSecond, actorAnimInfo2.layer);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnStart>c__AnonStorey.agent.Animation.PlayOutAnimation(<OnStart>c__AnonStorey.animInfo.outEnableBlend, <OnStart>c__AnonStorey.animInfo.outBlendSec, <OnStart>c__AnonStorey.animInfo.layer);
				<OnStart>c__AnonStorey.partner.Animation.PlayOutAnimation(<OnStart>c__AnonStorey.animInfo.outEnableBlend, <OnStart>c__AnonStorey.animInfo.outBlendSec, <OnStart>c__AnonStorey.animInfo.layer);
			});
			if (<OnStart>c__AnonStorey.animInfo.hasAction)
			{
				this._onActionPlay.Subscribe(delegate(Unit _)
				{
					<OnStart>c__AnonStorey.agent.Animation.PlayActionAnimation(<OnStart>c__AnonStorey.animInfo.layer);
					<OnStart>c__AnonStorey.partner.Animation.PlayActionAnimation(<OnStart>c__AnonStorey.animInfo.layer);
				});
			}
			if (<OnStart>c__AnonStorey.animInfo.isLoop)
			{
				<OnStart>c__AnonStorey.agent.SetCurrentSchedule(<OnStart>c__AnonStorey.animInfo.isLoop, dateActionPointInfo.actionName, <OnStart>c__AnonStorey.animInfo.loopMinTime, <OnStart>c__AnonStorey.animInfo.loopMaxTime, <OnStart>c__AnonStorey.animInfo.hasAction, false);
			}
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x002D52FC File Offset: 0x002D36FC
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			ActorAnimInfo animInfo = base.Agent.Animation.AnimInfo;
			if (animInfo.isLoop)
			{
				if (base.Agent.Animation.PlayingActAnimation)
				{
					return TaskStatus.Running;
				}
				if (base.Agent.Schedule.enabled)
				{
					AnimatorStateInfo currentAnimatorStateInfo = base.Agent.Animation.Animator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.IsName(animInfo.loopStateName))
					{
						float num = currentAnimatorStateInfo.normalizedTime - animInfo.oldNormalizedTime;
						if (num > 1f)
						{
							animInfo.oldNormalizedTime = currentAnimatorStateInfo.normalizedTime;
							if (UnityEngine.Random.Range(0, animInfo.randomCount) == 0)
							{
								if (this._onActionPlay != null)
								{
									this._onActionPlay.OnNext(Unit.Default);
								}
								animInfo.oldNormalizedTime = 0f;
							}
						}
					}
					return TaskStatus.Running;
				}
				if (this._onEndAction != null)
				{
					this._onEndAction.OnNext(Unit.Default);
				}
				if (base.Agent.Animation.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				this.Complete();
				return TaskStatus.Success;
			}
			else
			{
				if (this._onEndAction != null)
				{
					this._onEndAction.OnNext(Unit.Default);
				}
				if (base.Agent.Animation.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				this.Complete();
				return TaskStatus.Success;
			}
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x002D547C File Offset: 0x002D387C
		public override void OnEnd()
		{
			base.OnEnd();
			base.Agent.ActivateNavMeshAgent();
			base.Agent.SetActiveOnEquipedItem(true);
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x002D549C File Offset: 0x002D389C
		private void Complete()
		{
			AgentActor agent = base.Agent;
			AgentActor agentActor = agent.Partner as AgentActor;
			ActorAnimInfo animInfo = agent.Animation.AnimInfo;
			if (!animInfo.outEnableBlend)
			{
				agent.Animation.CrossFadeScreen(-1f);
			}
			agent.SetStand(agent.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			agent.Animation.RefsActAnimInfo = true;
			agentActor.SetStand(agentActor.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			agentActor.Animation.RefsActAnimInfo = true;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agentActor.UpdateStatus(agentActor.ActionID, agentActor.PoseID);
			agent.CauseSick();
			agent.ApplySituationResultParameter(28);
			agentActor.ApplySituationResultParameter(28);
			int desireKey = Desire.GetDesireKey(Desire.Type.Game);
			agent.SetDesire(desireKey, 0f);
			agentActor.SetDesire(desireKey, 0f);
			desireKey = Desire.GetDesireKey(Desire.Type.Lonely);
			agent.SetDesire(desireKey, 0f);
			agentActor.SetDesire(desireKey, 0f);
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agentActor.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			agent.CurrentPoint.SetActiveMapItemObjs(true);
			agent.Partner = null;
			agentActor.Partner = null;
			agentActor.ActivateNavMeshAgent();
			agentActor.SetActiveOnEquipedItem(true);
			ActorAnimInfo animInfo2 = agentActor.Animation.AnimInfo;
			agentActor.BehaviorResources.ChangeMode(Desire.ActionType.Normal);
			agentActor.Mode = Desire.ActionType.Normal;
			agent.TargetInSightActor = null;
			agent.CurrentPoint.ReleaseSlot(base.Agent);
			agent.CurrentPoint = null;
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = base.Agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
			agent.AgentData.AddAppendEventFlagParam(3, 1);
			agentActor.AgentData.AddAppendEventFlagParam(3, 1);
		}

		// Token: 0x040059FD RID: 23037
		protected Subject<Unit> _onActionPlay = new Subject<Unit>();

		// Token: 0x040059FE RID: 23038
		protected Subject<Unit> _onEndAction = new Subject<Unit>();
	}
}

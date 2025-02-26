using System;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CF9 RID: 3321
	public class WokenUp : AgentAction
	{
		// Token: 0x06006ADF RID: 27359 RVA: 0x002DA9A4 File Offset: 0x002D8DA4
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Sleep;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			agent.DeactivateNavMeshAgent();
			agent.DisableActionFlag();
			ActionPointInfo actionPointInfo = agent.TargetInSightActionPoint.GetActionPointInfo(agent);
			agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			PoseKeyPair wakenUpID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.WakenUpID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[wakenUpID.postureID][wakenUpID.poseID];
			agent.Animation.LoadEventKeyTable(wakenUpID.postureID, wakenUpID.poseID);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
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
				oldNormalizedTime = 0f
			};
			agent.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			if (!playState.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in playState.MainStateInfo.InStateInfo.StateInfos)
				{
					agent.Animation.InStates.Enqueue(item);
				}
			}
			agent.Animation.OutStates.Clear();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
			agent.SleepTrigger = false;
			agent.AgentData.YobaiTrigger = false;
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x002DAD5C File Offset: 0x002D915C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x002DAD8C File Offset: 0x002D918C
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.ResetActionFlag();
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			if (agent.TargetInSightActionPoint != null)
			{
				agent.TargetInSightActionPoint.Reserver = null;
			}
			agent.TargetInSightActionPoint = null;
			agent.ApplySituationResultParameter(32);
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB6 RID: 3254
	[TaskCategory("")]
	public class Coma : AgentAction
	{
		// Token: 0x0600699C RID: 27036 RVA: 0x002CF730 File Offset: 0x002CDB30
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.CurrentPoint == null)
			{
				agent.CurrentPoint = agent.TargetInSightActionPoint;
			}
			agent.DeactivateNavMeshAgent();
			PoseKeyPair comaID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.ComaID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[comaID.postureID][comaID.poseID];
			agent.Animation.LoadEventKeyTable(comaID.postureID, comaID.poseID);
			agent.Animation.InitializeStates(playState);
			agent.LoadActionFlag(comaID.postureID, comaID.poseID);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
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
			agent.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
			ActionPointInfo actionPointInfo;
			agent.CurrentPoint.TryGetAgentActionPointInfo(EventType.Sleep, out actionPointInfo);
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x002CFA1A File Offset: 0x002CDE1A
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x002CFA34 File Offset: 0x002CDE34
		public override void OnEnd()
		{
			base.OnEnd();
		}
	}
}

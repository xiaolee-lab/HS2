using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D01 RID: 3329
	[TaskCategory("")]
	public class FallWeakness : AgentAction
	{
		// Token: 0x06006AF8 RID: 27384 RVA: 0x002DB644 File Offset: 0x002D9A44
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Collapse;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair weaknessID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.WeaknessID;
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[weaknessID.postureID][weaknessID.poseID];
			agent.Animation.LoadEventKeyTable(weaknessID.postureID, weaknessID.poseID);
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
			agent.Animation.InitializeStates(playState);
			agent.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x002DB84E File Offset: 0x002D9C4E
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x002DB868 File Offset: 0x002D9C68
		public override void OnEnd()
		{
		}
	}
}

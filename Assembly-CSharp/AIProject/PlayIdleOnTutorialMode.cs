using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CF0 RID: 3312
	public class PlayIdleOnTutorialMode : AgentAction
	{
		// Token: 0x06006ABB RID: 27323 RVA: 0x002D8E40 File Offset: 0x002D7240
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._animation = this._agent.Animation;
			this._agent.DeactivateNavMeshAgent();
			if (this._notNeet = (this._animation == null))
			{
				return;
			}
			this._loopStateName = string.Empty;
			PoseKeyPair poseKeyPair;
			if (Singleton<Resources>.IsInstance() && this.TryGetPoseID(this._agent.ChaControl.fileParam.personality, out poseKeyPair))
			{
				PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
				this._loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				if (this._notNeet = this._animation.Animator.GetCurrentAnimatorStateInfo(0).IsName(this._loopStateName))
				{
					return;
				}
				this._animation.InitializeStates(playState);
				this._animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
				ActorAnimInfo actorAnimInfo = new ActorAnimInfo
				{
					layer = playState.Layer,
					inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
					inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
					outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
					outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
					directionType = playState.DirectionType,
					isLoop = playState.MainStateInfo.IsLoop
				};
				this._animation.AnimInfo = actorAnimInfo;
				ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
				this._animation.StopAllAnimCoroutine();
				this._animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
			}
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x002D9057 File Offset: 0x002D7457
		public override TaskStatus OnUpdate()
		{
			if (this._notNeet)
			{
				return TaskStatus.Success;
			}
			if (this._animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x002D907C File Offset: 0x002D747C
		private bool TryGetPoseID(int personalID, out PoseKeyPair pair)
		{
			Dictionary<int, PoseKeyPair> tutorialIdlePoseTable = Singleton<Resources>.Instance.AgentProfile.TutorialIdlePoseTable;
			if (tutorialIdlePoseTable.IsNullOrEmpty<int, PoseKeyPair>())
			{
				pair = default(PoseKeyPair);
				return false;
			}
			if (tutorialIdlePoseTable.TryGetValue(personalID, out pair))
			{
				return true;
			}
			if (tutorialIdlePoseTable.TryGetValue(0, out pair))
			{
				return true;
			}
			pair = default(PoseKeyPair);
			return false;
		}

		// Token: 0x04005A36 RID: 23094
		private bool _notNeet;

		// Token: 0x04005A37 RID: 23095
		private AgentActor _agent;

		// Token: 0x04005A38 RID: 23096
		private ActorAnimation _animation;

		// Token: 0x04005A39 RID: 23097
		private string _loopStateName = string.Empty;
	}
}

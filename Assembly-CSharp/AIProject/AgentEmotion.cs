using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;

namespace AIProject
{
	// Token: 0x02000D98 RID: 3480
	public abstract class AgentEmotion : AgentAction
	{
		// Token: 0x06006C9A RID: 27802 RVA: 0x002D0F30 File Offset: 0x002CF330
		protected void PlayAnimation(int postureID, int poseID)
		{
			AgentEmotion.<PlayAnimation>c__AnonStorey0 <PlayAnimation>c__AnonStorey = new AgentEmotion.<PlayAnimation>c__AnonStorey0();
			<PlayAnimation>c__AnonStorey.agent = base.Agent;
			<PlayAnimation>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
			<PlayAnimation>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[postureID][poseID];
			<PlayAnimation>c__AnonStorey.agent.Animation.LoadEventKeyTable(postureID, poseID);
			AgentEmotion.<PlayAnimation>c__AnonStorey0 <PlayAnimation>c__AnonStorey2 = <PlayAnimation>c__AnonStorey;
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
				oldNormalizedTime = 0f
			};
			<PlayAnimation>c__AnonStorey.agent.Animation.AnimInfo = animInfo;
			<PlayAnimation>c__AnonStorey2.animInfo = animInfo;
			<PlayAnimation>c__AnonStorey.agent.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
			this._onEndAction = new Subject<Unit>();
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<PlayAnimation>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
				<PlayAnimation>c__AnonStorey.agent.Animation.PlayOutAnimation(<PlayAnimation>c__AnonStorey.animInfo.outEnableBlend, <PlayAnimation>c__AnonStorey.animInfo.outBlendSec, <PlayAnimation>c__AnonStorey.animInfo.layer);
			});
			<PlayAnimation>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
			<PlayAnimation>c__AnonStorey.agent.Animation.PlayInAnimation(<PlayAnimation>c__AnonStorey.animInfo.inEnableBlend, <PlayAnimation>c__AnonStorey.animInfo.inBlendSec, <PlayAnimation>c__AnonStorey.animInfo.inFadeOutTime, <PlayAnimation>c__AnonStorey.animInfo.layer);
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x002D11B0 File Offset: 0x002CF5B0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Animation.PlayingInAnimation)
			{
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
			this.OnCompletedEmoteTask();
			return TaskStatus.Success;
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x002D1211 File Offset: 0x002CF611
		protected virtual void OnCompletedEmoteTask()
		{
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x002D1213 File Offset: 0x002CF613
		public override void OnEnd()
		{
			base.Agent.SetActiveOnEquipedItem(true);
		}

		// Token: 0x04005AEF RID: 23279
		protected Subject<Unit> _onEndAction;
	}
}

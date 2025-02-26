using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;

namespace AIProject
{
	// Token: 0x02000CBE RID: 3262
	[TaskCategory("")]
	public class Ovation : AgentAction
	{
		// Token: 0x060069C9 RID: 27081 RVA: 0x002D0944 File Offset: 0x002CED44
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			PoseKeyPair ovationID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.OvationID;
			agent.ActionID = ovationID.postureID;
			agent.PoseID = ovationID.poseID;
			PlayState info = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[ovationID.postureID][ovationID.poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(ovationID.postureID, ovationID.poseID, info);
			agent.LoadActionFlag(ovationID.postureID, ovationID.poseID);
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			agent.Animation.StopAllAnimCoroutine();
			PlayerActor pPlayer = agent.Partner as PlayerActor;
			agent.Animation.PlayTurnAnimation(pPlayer.Position, 1f, null, false);
			this._onEndTurnDisposable = (this._onEndTurn = new Subject<Unit>()).Take(1).Subscribe(delegate(Unit _)
			{
				if (pPlayer != null)
				{
					agent.ChaControl.ChangeLookEyesTarget(1, pPlayer.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 2f);
					agent.ChaControl.ChangeLookEyesPtn(1);
					agent.ChaControl.ChangeLookNeckTarget(1, pPlayer.CameraControl.CameraComponent.transform, 0.5f, 0f, 1f, 0.8f);
					agent.ChaControl.ChangeLookNeckPtn(1, 1f);
				}
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, info.MainStateInfo.FadeOutTime, animInfo.layer);
			});
			this._onEndActionDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			});
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, "拍手", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x002D0B6C File Offset: 0x002CEF6C
		public override void OnEnd()
		{
			base.OnEnd();
			if (this._onEndTurnDisposable != null)
			{
				this._onEndTurnDisposable.Dispose();
			}
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			if (this._onCompleteActionDisposable != null)
			{
				this._onCompleteActionDisposable.Dispose();
			}
			AgentActor agent = base.Agent;
			agent.ChangeDynamicNavMeshAgentAvoidance();
			agent.SetActiveOnEquipedItem(true);
			agent.ChaControl.ChangeLookEyesPtn(0);
			agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			agent.ChaControl.ChangeLookNeckPtn(3, 1f);
			agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			PlayerActor playerActor = agent.Partner as PlayerActor;
			if (playerActor != null && playerActor.OldEnabledHoldingHand)
			{
				playerActor.HandsHolder.enabled = true;
				playerActor.OldEnabledHoldingHand = false;
			}
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x002D0C70 File Offset: 0x002CF070
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Animation.PlayingTurnAnimation)
			{
				return TaskStatus.Running;
			}
			this._onEndTurn.OnNext(Unit.Default);
			return agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x002D0CB4 File Offset: 0x002CF0B4
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
		}

		// Token: 0x040059BE RID: 22974
		private Subject<Unit> _onEndTurn;

		// Token: 0x040059BF RID: 22975
		private IDisposable _onEndTurnDisposable;

		// Token: 0x040059C0 RID: 22976
		private IDisposable _onEndActionDisposable;

		// Token: 0x040059C1 RID: 22977
		private IDisposable _onCompleteActionDisposable;
	}
}

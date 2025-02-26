using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CEC RID: 3308
	[TaskCategory("")]
	public class Pee : AgentAction
	{
		// Token: 0x06006AA5 RID: 27301 RVA: 0x002D7C84 File Offset: 0x002D6084
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			PoseKeyPair peeID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.PeeID;
			agent.ActionID = peeID.postureID;
			agent.PoseID = peeID.poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[peeID.postureID][peeID.poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(peeID.postureID, peeID.poseID, playState);
			agent.LoadActionFlag(peeID.postureID, peeID.poseID);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
			this._onEndActionDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			});
			if (animInfo.hasAction)
			{
				this._onActionPlayDisposable = agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
				{
					this.Agent.Animation.PlayActionAnimation(animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, "おもらし", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x002D7EB1 File Offset: 0x002D62B1
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x002D7EC4 File Offset: 0x002D62C4
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
			base.Agent.ActivateNavMeshAgent();
			base.Agent.SetActiveOnEquipedItem(true);
			base.Agent.ClearItems();
			base.Agent.ClearParticles();
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x002D7F4C File Offset: 0x002D634C
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
			agent.SetDesire(desireKey, 0f);
			agent.ApplySituationResultParameter(29);
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
		}

		// Token: 0x04005A21 RID: 23073
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A22 RID: 23074
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A23 RID: 23075
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A24 RID: 23076
		private IDisposable _onCompleteActionDisposable;
	}
}

using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D9B RID: 3483
	public abstract class AgentTutorialMoveAction : AgentAction
	{
		// Token: 0x06006CAC RID: 27820 RVA: 0x002D9118 File Offset: 0x002D7518
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			ActionPointInfo actionPointInfo = agent.TargetInSightActionPoint.GetActionPointInfo(agent);
			agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			int num;
			int poseID;
			if (this._actionMotion.postureID < 0 || this._actionMotion.poseID < 0)
			{
				num = actionPointInfo2.eventID;
				poseID = actionPointInfo2.poseID;
			}
			else
			{
				num = this._actionMotion.postureID;
				poseID = this._actionMotion.poseID;
			}
			agent.ActionID = num;
			agent.PoseID = poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num][poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num, poseID, playState);
			agent.LoadActionFlag(num, poseID);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
			this._onEndActionDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			});
			if (animInfo.hasAction)
			{
				this._onActionPlayDisposable = agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
				{
					agent.Animation.PlayActionAnimation(animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo2.actionName, animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x002D9482 File Offset: 0x002D7882
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x002D9494 File Offset: 0x002D7894
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
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x002D9510 File Offset: 0x002D7910
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.Animation.EndStates();
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
			this.OnCompletedStateTask();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.CurrentPoint.SetActiveMapItemObjs(true);
			agent.CurrentPoint.ReleaseSlot(agent);
			agent.CurrentPoint = null;
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x002D9598 File Offset: 0x002D7998
		protected virtual void OnCompletedStateTask()
		{
		}

		// Token: 0x04005AF7 RID: 23287
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005AF8 RID: 23288
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005AF9 RID: 23289
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005AFA RID: 23290
		private IDisposable _onCompleteActionDisposable;

		// Token: 0x04005AFB RID: 23291
		protected PoseKeyPair _actionMotion = new PoseKeyPair
		{
			postureID = -1,
			poseID = -1
		};
	}
}

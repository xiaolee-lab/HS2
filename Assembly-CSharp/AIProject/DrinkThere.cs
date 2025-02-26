using System;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CEA RID: 3306
	[TaskCategory("")]
	public class DrinkThere : AgentAction
	{
		// Token: 0x06006A9B RID: 27291 RVA: 0x002D73EC File Offset: 0x002D57EC
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Drink;
			this._targetItem = agent.SelectDrinkItem();
			base.OnStart();
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			PoseKeyPair drinkStandID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.DrinkStandID;
			agent.ActionID = drinkStandID.postureID;
			agent.PoseID = drinkStandID.poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[drinkStandID.postureID][drinkStandID.poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(drinkStandID.postureID, drinkStandID.poseID, playState);
			agent.LoadActionFlag(drinkStandID.postureID, drinkStandID.poseID);
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
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
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, "その場で飲む", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x002D763F File Offset: 0x002D5A3F
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x002D7654 File Offset: 0x002D5A54
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
			agent.ChangeDynamicNavMeshAgentAvoidance();
			agent.SetActiveOnEquipedItem(true);
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x002D76D0 File Offset: 0x002D5AD0
		private void Complete()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Drink);
			agent.SetDesire(desireKey, 0f);
			agent.Animation.EndStates();
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
			agent.ApplyDrinkParameter(this._targetItem);
			this._targetItem = null;
		}

		// Token: 0x04005A18 RID: 23064
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A19 RID: 23065
		private StuffItem _targetItem;

		// Token: 0x04005A1A RID: 23066
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A1B RID: 23067
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A1C RID: 23068
		private IDisposable _onCompleteActionDisposable;
	}
}

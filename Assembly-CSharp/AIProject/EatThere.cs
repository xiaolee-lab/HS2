using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CEB RID: 3307
	[TaskCategory("")]
	public class EatThere : AgentAction
	{
		// Token: 0x06006AA0 RID: 27296 RVA: 0x002D77B8 File Offset: 0x002D5BB8
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Play;
			base.OnStart();
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			PoseKeyPair eatStandID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.EatStandID;
			agent.ActionID = eatStandID.postureID;
			agent.PoseID = eatStandID.poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[eatStandID.postureID][eatStandID.poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(eatStandID.postureID, eatStandID.poseID, playState);
			StuffItem carryingItem = agent.AgentData.CarryingItem;
			Dictionary<int, int> dictionary;
			int num;
			ActionItemInfo eventItemInfo;
			if (Singleton<Manager.Resources>.Instance.Map.FoodEventItemList.TryGetValue(carryingItem.CategoryID, out dictionary) && dictionary.TryGetValue(carryingItem.ID, out num) && Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(num, out eventItemInfo))
			{
				string rightHandParentName = Singleton<Manager.Resources>.Instance.LocomotionProfile.RightHandParentName;
				GameObject gameObject = agent.LoadEventItem(num, rightHandParentName, false, eventItemInfo);
				if (gameObject != null)
				{
					Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer in componentsInChildren)
					{
						renderer.enabled = true;
					}
				}
			}
			agent.LoadActionFlag(eatStandID.postureID, eatStandID.poseID);
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
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
					agent.Animation.PlayActionAnimation(animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, "その場で食べる", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x002D7AC2 File Offset: 0x002D5EC2
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x002D7AD4 File Offset: 0x002D5ED4
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

		// Token: 0x06006AA3 RID: 27299 RVA: 0x002D7B50 File Offset: 0x002D5F50
		private void Complete()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Eat);
			agent.SetDesire(desireKey, 0f);
			if (!agent.Animation.AnimInfo.outEnableBlend)
			{
				agent.Animation.CrossFadeScreen(-1f);
			}
			agent.Animation.RefsActAnimInfo = true;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
			agent.ApplyFoodParameter(agent.AgentData.CarryingItem);
			agent.AgentData.CarryingItem = null;
		}

		// Token: 0x04005A1D RID: 23069
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A1E RID: 23070
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A1F RID: 23071
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A20 RID: 23072
		private IDisposable _onCompleteActionDisposable;
	}
}

using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CA0 RID: 3232
	[TaskCategory("")]
	public class Eat : AgentAction
	{
		// Token: 0x06006933 RID: 26931 RVA: 0x002CB7C4 File Offset: 0x002C9BC4
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Eat;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			StuffItem carryingItem = agent.AgentData.CarryingItem;
			ItemIDKeyPair[] canStandEatItems = Singleton<Manager.Resources>.Instance.AgentProfile.CanStandEatItems;
			bool flag = false;
			foreach (ItemIDKeyPair itemIDKeyPair in canStandEatItems)
			{
				if (carryingItem.CategoryID == itemIDKeyPair.categoryID && carryingItem.ID == itemIDKeyPair.itemID)
				{
					flag = true;
					break;
				}
			}
			ActionPointInfo actionPointInfo;
			if (flag)
			{
				PoseKeyPair eatDeskID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.EatDeskID;
				PoseKeyPair eatChairID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.EatChairID;
				if (agent.TargetInSightActionPoint.FindAgentActionPointInfo(EventType.Eat, eatDeskID.poseID, out actionPointInfo))
				{
					agent.Animation.ActionPointInfo = actionPointInfo;
				}
				else if (agent.TargetInSightActionPoint.FindAgentActionPointInfo(EventType.Eat, eatChairID.poseID, out actionPointInfo))
				{
					agent.Animation.ActionPointInfo = actionPointInfo;
				}
			}
			else
			{
				PoseKeyPair eatDishID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.EatDishID;
				if (agent.TargetInSightActionPoint.FindAgentActionPointInfo(EventType.Eat, eatDishID.poseID, out actionPointInfo))
				{
					agent.Animation.ActionPointInfo = actionPointInfo;
				}
			}
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			int num = actionPointInfo.eventID;
			agent.ActionID = num;
			int num2 = num;
			num = actionPointInfo.poseID;
			agent.PoseID = num;
			int num3 = num;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num3];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num2, num3, playState);
			Dictionary<int, int> dictionary;
			int num4;
			ActionItemInfo eventItemInfo;
			if (Singleton<Manager.Resources>.Instance.Map.FoodEventItemList.TryGetValue(carryingItem.CategoryID, out dictionary) && dictionary.TryGetValue(carryingItem.ID, out num4) && Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(num4, out eventItemInfo))
			{
				LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				string parentName = (!flag) ? locomotionProfile.RootParentName : locomotionProfile.RightHandParentName;
				GameObject gameObject3 = agent.LoadEventItem(num4, parentName, false, eventItemInfo);
				if (gameObject3 != null)
				{
					Renderer[] componentsInChildren = gameObject3.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer in componentsInChildren)
					{
						renderer.enabled = true;
					}
				}
			}
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
			this._onActionPlayDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
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
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo.actionName, animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, true);
			}
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x002CBCFE File Offset: 0x002CA0FE
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x002CBD10 File Offset: 0x002CA110
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
			int desireKey = Desire.GetDesireKey(Desire.Type.Eat);
			agent.SetDesire(desireKey, 0f);
			agent.ApplyFoodParameter(agent.AgentData.CarryingItem);
			agent.AgentData.CarryingItem = null;
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x002CBDE8 File Offset: 0x002CA1E8
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
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x04005991 RID: 22929
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005992 RID: 22930
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005993 RID: 22931
		private IDisposable _onCompleteActionDisposable;
	}
}

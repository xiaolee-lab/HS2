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
	// Token: 0x02000CEE RID: 3310
	[TaskCategory("")]
	public class Steal : AgentAction
	{
		// Token: 0x06006AAF RID: 27311 RVA: 0x002D84C8 File Offset: 0x002D68C8
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Cook;
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> foodParameterTable = instance.GameInfo.FoodParameterTable;
			List<StuffItem> itemListInPantry = Singleton<Game>.Instance.Environment.ItemListInPantry;
			List<StuffItem> list = ListPool<StuffItem>.Get();
			foreach (StuffItem stuffItem in itemListInPantry)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				if (foodParameterTable.TryGetValue(stuffItem.CategoryID, out dictionary))
				{
					Dictionary<int, FoodParameterPacket> dictionary2;
					if (dictionary.TryGetValue(stuffItem.ID, out dictionary2))
					{
						this._existsFoods = true;
						list.Add(stuffItem);
					}
				}
			}
			if (!this._existsFoods)
			{
				ListPool<StuffItem>.Release(list);
				return;
			}
			StuffItem element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			ListPool<StuffItem>.Release(list);
			if (element == null)
			{
				this._existsFoods = false;
				return;
			}
			agent.AgentData.CarryingItem = new StuffItem(element.CategoryID, element.ID, 1);
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			PoseKeyPair snitchFoodID = instance.AgentProfile.PoseIDTable.SnitchFoodID;
			agent.ActionID = snitchFoodID.postureID;
			agent.PoseID = snitchFoodID.poseID;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(instance.DefinePack.MapDefines.StealPivotName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			agent.Animation.RecoveryPoint = null;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[snitchFoodID.postureID][snitchFoodID.poseID];
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(snitchFoodID.postureID, snitchFoodID.poseID, playState);
			agent.LoadActionFlag(snitchFoodID.postureID, snitchFoodID.poseID);
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
				agent.SetCurrentSchedule(animInfo.isLoop, "盗み食い", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x002D88E4 File Offset: 0x002D6CE4
		public override TaskStatus OnUpdate()
		{
			if (!this._existsFoods)
			{
				return TaskStatus.Success;
			}
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x002D8904 File Offset: 0x002D6D04
		public override void OnEnd()
		{
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
			agent.SetActiveOnEquipedItem(true);
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x002D8974 File Offset: 0x002D6D74
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
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
				agent.CurrentPoint.CreateByproduct(agent.ActionID, agent.PoseID);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x04005A2A RID: 23082
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A2B RID: 23083
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A2C RID: 23084
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A2D RID: 23085
		private IDisposable _onCompleteActionDisposable;

		// Token: 0x04005A2E RID: 23086
		private bool _existsFoods;
	}
}

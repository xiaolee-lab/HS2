using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000D2F RID: 3375
	[TaskCategory("")]
	public class EnterActionPointInSight : AgentConditional
	{
		// Token: 0x06006BAF RID: 27567 RVA: 0x002E2B74 File Offset: 0x002E0F74
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Dictionary<int, CollisionState> actionPointCollisionStateTable = agent.ActionPointCollisionStateTable;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			foreach (ActionPoint actionPoint in agent.SearchTargets)
			{
				CollisionState collisionState;
				if (actionPointCollisionStateTable.TryGetValue(actionPoint.InstanceID, out collisionState) && collisionState == CollisionState.Enter)
				{
					list.Add(actionPoint);
				}
			}
			if (list.Count > 0)
			{
				List<ActionPoint> list2 = ListPool<ActionPoint>.Get();
				foreach (ActionPoint actionPoint2 in list)
				{
					if (actionPoint2.IsNeutralCommand)
					{
						list2.Add(actionPoint2);
					}
				}
				Desire.Type requestedDesire = agent.RequestedDesire;
				EventType eventType = (EventType)0;
				foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
				{
					if (valueTuple.Item2 == requestedDesire)
					{
						eventType = valueTuple.Item1;
						break;
					}
				}
				ActionPoint actionPoint3 = null;
				foreach (ActionPoint actionPoint4 in list2)
				{
					if (agent.Partner != null)
					{
						if (actionPoint4.AgentDateEventType.Contains(eventType))
						{
							actionPoint3 = actionPoint4;
						}
					}
					else if (actionPoint4.AgentEventType.Contains(eventType))
					{
						actionPoint3 = actionPoint4;
					}
				}
				if (actionPoint3 == null)
				{
					actionPoint3 = list2.GetElement(UnityEngine.Random.Range(0, list2.Count));
					if (actionPoint3 == null)
					{
						ListPool<ActionPoint>.Release(list2);
						ListPool<ActionPoint>.Release(list);
						return TaskStatus.Failure;
					}
				}
				ListPool<ActionPoint>.Release(list2);
				if (!global::Debug.isDebugBuild || agent.Partner != null)
				{
				}
				if (requestedDesire == Desire.Type.Bath && eventType == EventType.DressIn && (float)agent.ChaControl.fileGameInfo.flavorState[2] < Singleton<Manager.Resources>.Instance.StatusProfile.CanDressBorder)
				{
					eventType = EventType.Bath;
				}
				if (eventType == EventType.Eat)
				{
					StuffItem carryingItem = agent.AgentData.CarryingItem;
					AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
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
						PoseKeyPair eatDeskID = agentProfile.PoseIDTable.EatDeskID;
						PoseKeyPair eatChairID = agentProfile.PoseIDTable.EatChairID;
						if (!actionPoint3.FindAgentActionPointInfo(EventType.Eat, eatDeskID.poseID, out actionPointInfo) && !actionPoint3.FindAgentActionPointInfo(EventType.Eat, eatChairID.poseID, out actionPointInfo))
						{
							return TaskStatus.Failure;
						}
					}
					else if (!actionPoint3.FindAgentActionPointInfo(EventType.Eat, agentProfile.PoseIDTable.EatDishID.poseID, out actionPointInfo))
					{
						return TaskStatus.Failure;
					}
				}
				AgentController.PermissionStatus permission = agent.AgentController.GetPermission(actionPoint3);
				if (permission != AgentController.PermissionStatus.Prohibition)
				{
					if (permission == AgentController.PermissionStatus.Permission)
					{
						if (eventType == (EventType)0)
						{
						}
						agent.EventKey = eventType;
						agent.TargetInSightActionPoint = actionPoint3;
						agent.RuntimeDesire = agent.RequestedDesire;
					}
				}
			}
			ListPool<ActionPoint>.Release(list);
			if (agent.TargetInSightActionPoint != null)
			{
				agent.ClearReservedNearActionWaypoints();
				agent.AbortActionPatrol();
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006BB0 RID: 27568 RVA: 0x002E2F58 File Offset: 0x002E1358
		public override void OnBehaviorComplete()
		{
			MovementUtility.ClearCache();
		}
	}
}

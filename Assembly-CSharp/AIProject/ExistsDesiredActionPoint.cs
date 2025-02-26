using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D3A RID: 3386
	[TaskCategory("")]
	public class ExistsDesiredActionPoint : AgentConditional
	{
		// Token: 0x06006BCF RID: 27599 RVA: 0x002E3FDC File Offset: 0x002E23DC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			ExistsDesiredActionPoint.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType);
			bool flag = !list.IsNullOrEmpty<ActionPoint>();
			if (!flag)
			{
				ExistsDesiredActionPoint.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType);
				flag = !list.IsNullOrEmpty<ActionPoint>();
			}
			ListPool<ActionPoint>.Release(list);
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006BD0 RID: 27600 RVA: 0x002E4074 File Offset: 0x002E2474
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow)
		{
			int searchCount = Singleton<Map>.Instance.EnvironmentProfile.SearchCount;
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
			foreach (ActionPoint actionPoint in source)
			{
				if (!(actionPoint == null) && !(actionPoint.OwnerArea == null))
				{
					if (actionPoint.IsNeutralCommand)
					{
						if (!actionPoint.IsReserved(agent))
						{
							List<ActionPoint> connectedActionPoints = actionPoint.ConnectedActionPoints;
							if (!connectedActionPoints.IsNullOrEmpty<ActionPoint>())
							{
								bool flag = false;
								foreach (ActionPoint actionPoint2 in connectedActionPoints)
								{
									if (!(actionPoint2 == null))
									{
										if (!actionPoint2.IsNeutralCommand || actionPoint2.IsReserved(agent))
										{
											flag = true;
											break;
										}
									}
								}
								if (flag)
								{
									continue;
								}
							}
							MapArea ownerArea = actionPoint.OwnerArea;
							bool flag2;
							if (!dictionary.TryGetValue(ownerArea.AreaID, out flag2))
							{
								flag2 = (dictionary[ownerArea.AreaID] = Singleton<Map>.Instance.CheckAvailableMapArea(ownerArea.AreaID));
							}
							if (flag2)
							{
								EventType source2;
								if (isFollow)
								{
									source2 = actionPoint.AgentDateEventType;
								}
								else
								{
									source2 = actionPoint.AgentEventType;
								}
								if (source2.Contains(eventType))
								{
									if (eventType != EventType.Eat)
									{
										if (eventType == EventType.Search)
										{
											SearchActionPoint searchActionPoint = actionPoint as SearchActionPoint;
											if (searchActionPoint != null)
											{
												int registerID = searchActionPoint.RegisterID;
												Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = agent.AgentData.SearchActionLockTable;
												AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
												if (!searchActionLockTable.TryGetValue(registerID, out searchActionInfo))
												{
													AIProject.SaveData.Environment.SearchActionInfo searchActionInfo2 = new AIProject.SaveData.Environment.SearchActionInfo();
													searchActionLockTable[registerID] = searchActionInfo2;
													searchActionInfo = searchActionInfo2;
												}
												if (searchActionInfo.Count >= searchCount)
												{
													continue;
												}
												int tableID = searchActionPoint.TableID;
												StuffItem itemInfo = agent.AgentData.EquipedSearchItem(tableID);
												int searchAreaID = agent.SearchAreaID;
												if (searchAreaID != 0)
												{
													if (agent.SearchAreaID != searchActionPoint.TableID)
													{
														continue;
													}
													if (!searchActionPoint.CanSearch(EventType.Search, itemInfo))
													{
														continue;
													}
												}
												else
												{
													if (tableID != 0 && tableID != 1 && tableID != 2)
													{
														continue;
													}
													if (!searchActionPoint.CanSearch(EventType.Search, itemInfo))
													{
														continue;
													}
												}
											}
										}
									}
									else
									{
										StuffItem carryingItem = agent.AgentData.CarryingItem;
										AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
										ItemIDKeyPair[] canStandEatItems = Singleton<Manager.Resources>.Instance.AgentProfile.CanStandEatItems;
										bool flag3 = false;
										foreach (ItemIDKeyPair itemIDKeyPair in canStandEatItems)
										{
											if (carryingItem.CategoryID == itemIDKeyPair.categoryID && carryingItem.ID == itemIDKeyPair.itemID)
											{
												flag3 = true;
												break;
											}
										}
										ActionPointInfo actionPointInfo;
										if (flag3)
										{
											PoseKeyPair eatDeskID = agentProfile.PoseIDTable.EatDeskID;
											PoseKeyPair eatDeskID2 = agentProfile.PoseIDTable.EatDeskID;
											if (!actionPoint.FindAgentActionPointInfo(EventType.Eat, eatDeskID.poseID, out actionPointInfo) && !actionPoint.FindAgentActionPointInfo(EventType.Eat, eatDeskID2.poseID, out actionPointInfo))
											{
												continue;
											}
										}
										else if (!actionPoint.FindAgentActionPointInfo(EventType.Eat, agentProfile.PoseIDTable.EatDishID.poseID, out actionPointInfo))
										{
											continue;
										}
									}
									if (ExistsDesiredActionPoint._navMeshPath == null)
									{
										ExistsDesiredActionPoint._navMeshPath = new NavMeshPath();
									}
									if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, ExistsDesiredActionPoint._navMeshPath))
									{
										if (ExistsDesiredActionPoint._navMeshPath.status == NavMeshPathStatus.PathComplete)
										{
											destination.Add(actionPoint);
										}
									}
								}
							}
						}
					}
				}
			}
			DictionaryPool<int, bool>.Release(dictionary);
		}

		// Token: 0x04005AB7 RID: 23223
		private static NavMeshPath _navMeshPath;

		// Token: 0x04005AB8 RID: 23224
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005AB9 RID: 23225
		[SerializeField]
		private EventType _eventType;
	}
}

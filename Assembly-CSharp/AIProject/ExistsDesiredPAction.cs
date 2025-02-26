using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D3B RID: 3387
	[TaskCategory("")]
	public class ExistsDesiredPAction : AgentConditional
	{
		// Token: 0x06006BD3 RID: 27603 RVA: 0x002E44BC File Offset: 0x002E28BC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			ExistsDesiredPAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkDateType);
			bool flag = !list.IsNullOrEmpty<ActionPoint>();
			if (!flag)
			{
				ExistsDesiredPAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkDateType);
				flag = !list.IsNullOrEmpty<ActionPoint>();
			}
			ListPool<ActionPoint>.Release(list);
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006BD4 RID: 27604 RVA: 0x002E4554 File Offset: 0x002E2954
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isDate)
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
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
								if (isDate)
								{
									source2 = actionPoint.PlayerDateEventType[(int)player.ChaControl.sex];
								}
								else
								{
									source2 = actionPoint.PlayerEventType;
								}
								if (source2.Contains(eventType))
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
									if (ExistsDesiredPAction._navMeshPath == null)
									{
										ExistsDesiredPAction._navMeshPath = new NavMeshPath();
									}
									if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, ExistsDesiredPAction._navMeshPath))
									{
										if (ExistsDesiredPAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x04005ABA RID: 23226
		private static NavMeshPath _navMeshPath;

		// Token: 0x04005ABB RID: 23227
		[SerializeField]
		private bool _checkDateType;

		// Token: 0x04005ABC RID: 23228
		[SerializeField]
		private EventType _eventType;
	}
}

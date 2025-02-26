using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D0E RID: 3342
	[TaskCategory("")]
	public class SetDesiredAction : AgentAction
	{
		// Token: 0x06006B20 RID: 27424 RVA: 0x002DC658 File Offset: 0x002DAA58
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Manager.Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			if ((weather == Weather.Rain || weather == Weather.Storm) && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(16))
			{
				SetDesiredAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetDesiredAction.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredAction.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x002DC81C File Offset: 0x002DAC1C
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			int searchCount = Singleton<Manager.Map>.Instance.EnvironmentProfile.SearchCount;
			float actionPointNavMeshSampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.ActionPointNavMeshSampleDistance;
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
							if (!isRain || actionPoint.AreaType == MapArea.AreaType.Indoor)
							{
								MapArea ownerArea = actionPoint.OwnerArea;
								bool flag2;
								if (!dictionary.TryGetValue(ownerArea.AreaID, out flag2))
								{
									flag2 = (dictionary[ownerArea.AreaID] = Singleton<Manager.Map>.Instance.CheckAvailableMapArea(ownerArea.AreaID));
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
											if (eventType != EventType.Search)
											{
												if (eventType == EventType.Warp)
												{
													WarpPoint warpPoint = actionPoint as WarpPoint;
													if (!(warpPoint != null))
													{
														continue;
													}
													Dictionary<int, List<WarpPoint>> dictionary2;
													List<WarpPoint> list;
													if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(ownerArea.ChunkID, out dictionary2) || !dictionary2.TryGetValue(warpPoint.TableID, out list))
													{
														continue;
													}
													if (list.Count < 2)
													{
														continue;
													}
												}
											}
											else
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
										if (SetDesiredAction._navMeshPath == null)
										{
											SetDesiredAction._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredAction._navMeshPath))
										{
											if (SetDesiredAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
											{
												NavMeshHit navMeshHit;
												if (NavMesh.SamplePosition(actionPoint.LocatedPosition, out navMeshHit, actionPointNavMeshSampleDistance, agent.NavMeshAgent.areaMask))
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
				}
			}
			DictionaryPool<int, bool>.Release(dictionary);
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x002DCD34 File Offset: 0x002DB134
		private static void NearestPoint(Vector3 position, List<ActionPoint> actionPoints, out ActionPoint destination)
		{
			destination = null;
			float? num = null;
			foreach (ActionPoint actionPoint in actionPoints)
			{
				float num2 = Vector3.Distance(position, actionPoint.Position);
				if (num == null)
				{
					num = new float?(num2);
					destination = actionPoint;
				}
				else if (num == null || !(num <= num2))
				{
					num = new float?(num2);
					destination = actionPoint;
				}
			}
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x002DCDFC File Offset: 0x002DB1FC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActionPoint != null)
			{
				if (this._checkFollowType)
				{
					if (agent.TargetInSightActionPoint.AgentDateEventType.Contains(this._eventType))
					{
						return TaskStatus.Success;
					}
				}
				else if (agent.TargetInSightActionPoint.AgentEventType.Contains(this._eventType))
				{
					return TaskStatus.Success;
				}
			}
			if (this._destination == null)
			{
				if (this._desireIfNotFound != Desire.Type.None)
				{
					int desireKey = Desire.GetDesireKey(this._desireIfNotFound);
					agent.SetDesire(desireKey, 0f);
					agent.ChangeBehavior(Desire.ActionType.Normal);
				}
				return TaskStatus.Failure;
			}
			agent.TargetInSightActionPoint = this._destination;
			agent.EventKey = this._eventType;
			this._destination.Reserver = agent;
			return TaskStatus.Success;
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x002DCECE File Offset: 0x002DB2CE
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A5A RID: 23130
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005A5B RID: 23131
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A5C RID: 23132
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A5D RID: 23133
		private ActionPoint _destination;

		// Token: 0x04005A5E RID: 23134
		private static NavMeshPath _navMeshPath;
	}
}

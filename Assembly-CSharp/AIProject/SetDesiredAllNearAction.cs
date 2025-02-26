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
	// Token: 0x02000D11 RID: 3345
	[TaskCategory("")]
	public class SetDesiredAllNearAction : AgentAction
	{
		// Token: 0x06006B36 RID: 27446 RVA: 0x002DDF1C File Offset: 0x002DC31C
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
				SetDesiredAllNearAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredAllNearAction.NearestPoint(position, list, out this._destination);
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredAllNearAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetDesiredAllNearAction.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredAllNearAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredAllNearAction.NearestPoint(position, list, out this._destination);
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredAllNearAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredAllNearAction.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x002DE0C8 File Offset: 0x002DC4C8
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
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
										if (SetDesiredAllNearAction._navMeshPath == null)
										{
											SetDesiredAllNearAction._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredAllNearAction._navMeshPath))
										{
											if (SetDesiredAllNearAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B38 RID: 27448 RVA: 0x002DE5C8 File Offset: 0x002DC9C8
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

		// Token: 0x06006B39 RID: 27449 RVA: 0x002DE690 File Offset: 0x002DCA90
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

		// Token: 0x06006B3A RID: 27450 RVA: 0x002DE762 File Offset: 0x002DCB62
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A6A RID: 23146
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005A6B RID: 23147
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A6C RID: 23148
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A6D RID: 23149
		private ActionPoint _destination;

		// Token: 0x04005A6E RID: 23150
		private static NavMeshPath _navMeshPath;
	}
}

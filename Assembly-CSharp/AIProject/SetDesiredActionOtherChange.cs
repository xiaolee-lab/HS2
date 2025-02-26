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
	// Token: 0x02000D0F RID: 3343
	[TaskCategory("")]
	public class SetDesiredActionOtherChange : AgentAction
	{
		// Token: 0x06006B27 RID: 27431 RVA: 0x002DCEE8 File Offset: 0x002DB2E8
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
				SetDesiredActionOtherChange.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredActionOtherChange.NearestPoint(position, list, out this._destination);
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredActionOtherChange.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetDesiredActionOtherChange.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredActionOtherChange.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredActionOtherChange.NearestPoint(position, list, out this._destination);
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredActionOtherChange.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredActionOtherChange.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x002DD094 File Offset: 0x002DB494
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
			int searchCount = Singleton<Manager.Map>.Instance.EnvironmentProfile.SearchCount;
			float actionPointNavMeshSampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.ActionPointNavMeshSampleDistance;
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
												Dictionary<int, List<WarpPoint>> dictionary;
												List<WarpPoint> list;
												if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(warpPoint.OwnerArea.ChunkID, out dictionary) || !dictionary.TryGetValue(warpPoint.TableID, out list))
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
										bool flag2 = false;
										foreach (ItemIDKeyPair itemIDKeyPair in canStandEatItems)
										{
											if (carryingItem.CategoryID == itemIDKeyPair.categoryID && carryingItem.ID == itemIDKeyPair.itemID)
											{
												flag2 = true;
												break;
											}
										}
										ActionPointInfo actionPointInfo;
										if (flag2)
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
									if (SetDesiredActionOtherChange._navMeshPath == null)
									{
										SetDesiredActionOtherChange._navMeshPath = new NavMeshPath();
									}
									if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredActionOtherChange._navMeshPath))
									{
										if (SetDesiredActionOtherChange._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B29 RID: 27433 RVA: 0x002DD544 File Offset: 0x002DB944
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

		// Token: 0x06006B2A RID: 27434 RVA: 0x002DD60C File Offset: 0x002DBA0C
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
					agent.ChangeBehavior((Desire.ActionType)this._modeIDIfNotFound);
				}
				return TaskStatus.Failure;
			}
			agent.TargetInSightActionPoint = this._destination;
			agent.EventKey = this._eventType;
			this._destination.Reserver = agent;
			return TaskStatus.Success;
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x002DD6E3 File Offset: 0x002DBAE3
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A5F RID: 23135
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005A60 RID: 23136
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A61 RID: 23137
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A62 RID: 23138
		[SerializeField]
		private int _modeIDIfNotFound;

		// Token: 0x04005A63 RID: 23139
		private ActionPoint _destination;

		// Token: 0x04005A64 RID: 23140
		private static NavMeshPath _navMeshPath;
	}
}

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
	// Token: 0x02000D14 RID: 3348
	[TaskCategory("")]
	public class SetDesiredRandomAction : AgentAction
	{
		// Token: 0x06006B4B RID: 27467 RVA: 0x002DF4A4 File Offset: 0x002DD8A4
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Manager.Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			this._changeWarp = false;
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			if ((weather == Weather.Rain || weather == Weather.Storm) && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(16))
			{
				SetDesiredRandomAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, true);
				this._destination = SetDesiredRandomAction.RandomDestination(agent, this._eventType, chunk.AppendActionPoints, list, this._checkFollowType, true, out this._changeWarp);
				if (this._destination == null && !this._changeWarp)
				{
					list.Clear();
					SetDesiredRandomAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, true);
					this._destination = SetDesiredRandomAction.RandomDestination(agent, this._eventType, chunk.ActionPoints, list, this._checkFollowType, true, out this._changeWarp);
				}
			}
			if (this._destination == null && !this._changeWarp && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredRandomAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, this._checkFollowType, false);
				this._destination = SetDesiredRandomAction.RandomDestination(agent, this._eventType, chunk.AppendActionPoints, list, this._checkFollowType, false, out this._changeWarp);
			}
			if (this._destination == null && !this._changeWarp)
			{
				list.Clear();
				SetDesiredRandomAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, this._checkFollowType, false);
				this._destination = SetDesiredRandomAction.RandomDestination(agent, this._eventType, chunk.ActionPoints, list, this._checkFollowType, false, out this._changeWarp);
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x002DF6B0 File Offset: 0x002DDAB0
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
			int chunkID = agent.ChunkID;
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
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
										if (SetDesiredRandomAction._navMeshPath == null)
										{
											SetDesiredRandomAction._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredRandomAction._navMeshPath))
										{
											if (SetDesiredRandomAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B4D RID: 27469 RVA: 0x002DFBC8 File Offset: 0x002DDFC8
		private static ActionPoint RandomDestination(AgentActor agent, EventType eventType, List<ActionPoint> sourceList, List<ActionPoint> list, bool isFollow, bool isRain, out bool changeWarp)
		{
			changeWarp = false;
			if (eventType == EventType.Play && !isFollow)
			{
				List<ActionPoint> list2 = ListPool<ActionPoint>.Get();
				SetDesiredRandomAction.CreateList(agent, sourceList, list2, EventType.Warp, false, isRain);
				if (list.IsNullOrEmpty<ActionPoint>() && list2.IsNullOrEmpty<ActionPoint>())
				{
					return null;
				}
				int num = UnityEngine.Random.Range(0, list.Count + list2.Count);
				ListPool<ActionPoint>.Release(list2);
				changeWarp = (num >= list.Count);
				if (!changeWarp)
				{
					return list.GetElement(num);
				}
				return null;
			}
			else
			{
				if (list.IsNullOrEmpty<ActionPoint>())
				{
					return null;
				}
				return list.GetElement(UnityEngine.Random.Range(0, list.Count));
			}
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x002DFC70 File Offset: 0x002DE070
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
			if (!(this._destination == null))
			{
				agent.TargetInSightActionPoint = this._destination;
				agent.EventKey = this._eventType;
				this._destination.Reserver = agent;
				return TaskStatus.Success;
			}
			if (this._changeWarp)
			{
				agent.ChangeBehavior(Desire.ActionType.SearchWarpPoint);
				return TaskStatus.Success;
			}
			Actor partner = agent.Partner;
			if (partner != null)
			{
				agent.Partner = null;
				partner.Partner = null;
				if (partner is AgentActor)
				{
					(partner as AgentActor).BehaviorResources.ChangeMode(Desire.ActionType.Normal);
				}
				else if (partner is MerchantActor)
				{
					MerchantActor merchantActor = partner as MerchantActor;
					merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				}
			}
			if (this._desireIfNotFound != Desire.Type.None)
			{
				int desireKey = Desire.GetDesireKey(this._desireIfNotFound);
				agent.SetDesire(desireKey, 0f);
				agent.ChangeBehavior(Desire.ActionType.Normal);
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x002DFDBA File Offset: 0x002DE1BA
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A76 RID: 23158
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005A77 RID: 23159
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A78 RID: 23160
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A79 RID: 23161
		private ActionPoint _destination;

		// Token: 0x04005A7A RID: 23162
		private static NavMeshPath _navMeshPath;

		// Token: 0x04005A7B RID: 23163
		private bool _changeWarp;
	}
}

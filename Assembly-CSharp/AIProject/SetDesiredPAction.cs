using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D12 RID: 3346
	[TaskCategory("")]
	public class SetDesiredPAction : AgentAction
	{
		// Token: 0x06006B3D RID: 27453 RVA: 0x002DE77C File Offset: 0x002DCB7C
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			Weather weather = Singleton<Map>.Instance.Simulator.Weather;
			if ((weather == Weather.Rain || weather == Weather.Storm) && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(16))
			{
				SetDesiredPAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredPAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetDesiredPAction.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredPAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredPAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredPAction.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x002DE928 File Offset: 0x002DCD28
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isRain)
		{
			int searchCount = Singleton<Map>.Instance.EnvironmentProfile.SearchCount;
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
							if (connectedActionPoints.IsNullOrEmpty<ActionPoint>())
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
									flag2 = (dictionary[ownerArea.AreaID] = Singleton<Map>.Instance.CheckAvailableMapArea(ownerArea.AreaID));
								}
								if (flag2)
								{
									EventType playerEventType = actionPoint.PlayerEventType;
									if (playerEventType.Contains(eventType))
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
										if (SetDesiredPAction._navMeshPath == null)
										{
											SetDesiredPAction._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredPAction._navMeshPath))
										{
											if (SetDesiredPAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B3F RID: 27455 RVA: 0x002DEC88 File Offset: 0x002DD088
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

		// Token: 0x06006B40 RID: 27456 RVA: 0x002DED50 File Offset: 0x002DD150
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActionPoint != null && agent.TargetInSightActionPoint.PlayerEventType.Contains(this._eventType))
			{
				return TaskStatus.Success;
			}
			if (this._destination == null)
			{
				return TaskStatus.Failure;
			}
			agent.TargetInSightActionPoint = this._destination;
			agent.EventKey = this._eventType;
			this._destination.Reserver = agent;
			return TaskStatus.Success;
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x002DEDCA File Offset: 0x002DD1CA
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A6F RID: 23151
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A70 RID: 23152
		private ActionPoint _destination;

		// Token: 0x04005A71 RID: 23153
		private static NavMeshPath _navMeshPath;
	}
}

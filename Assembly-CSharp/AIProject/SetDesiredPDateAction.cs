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
	// Token: 0x02000D13 RID: 3347
	[TaskCategory("")]
	public class SetDesiredPDateAction : AgentAction
	{
		// Token: 0x06006B44 RID: 27460 RVA: 0x002DEDE4 File Offset: 0x002DD1E4
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
				SetDesiredPDateAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredPDateAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetDesiredPDateAction.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredPDateAction.CreateList(agent, chunk.AppendActionPoints, list, this._eventType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredPDateAction.CreateList(agent, chunk.ActionPoints, list, this._eventType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetDesiredPDateAction.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x002DEF90 File Offset: 0x002DD390
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isRain)
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
									EventType source2 = actionPoint.PlayerDateEventType[(int)Singleton<Manager.Map>.Instance.Player.ChaControl.sex];
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
										if (SetDesiredPDateAction._navMeshPath == null)
										{
											SetDesiredPDateAction._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetDesiredPDateAction._navMeshPath))
										{
											if (SetDesiredPDateAction._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B46 RID: 27462 RVA: 0x002DF308 File Offset: 0x002DD708
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

		// Token: 0x06006B47 RID: 27463 RVA: 0x002DF3D0 File Offset: 0x002DD7D0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActionPoint != null && agent.TargetInSightActionPoint.PlayerDateEventType[(int)Singleton<Manager.Map>.Instance.Player.ChaControl.sex].Contains(this._eventType))
			{
				return TaskStatus.Success;
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

		// Token: 0x06006B48 RID: 27464 RVA: 0x002DF48A File Offset: 0x002DD88A
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A72 RID: 23154
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A73 RID: 23155
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A74 RID: 23156
		private ActionPoint _destination;

		// Token: 0x04005A75 RID: 23157
		private static NavMeshPath _navMeshPath;
	}
}

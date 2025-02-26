using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D15 RID: 3349
	[TaskCategory("")]
	public class SetHizamkuraPoint : AgentAction
	{
		// Token: 0x06006B52 RID: 27474 RVA: 0x002DFDD4 File Offset: 0x002DE1D4
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
				SetHizamkuraPoint.CreateList(agent, chunk.AppendActionPoints, list, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetHizamkuraPoint.NearestPoint(position, list, out this._destination);
				}
				if (this._destination == null)
				{
					list.Clear();
					SetHizamkuraPoint.CreateList(agent, chunk.ActionPoints, list, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						SetHizamkuraPoint.NearestPoint(position, list, out this._destination);
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetHizamkuraPoint.CreateList(agent, chunk.AppendActionPoints, list, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetHizamkuraPoint.NearestPoint(position, list, out this._destination);
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetHizamkuraPoint.CreateList(agent, chunk.ActionPoints, list, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					SetHizamkuraPoint.NearestPoint(position, list, out this._destination);
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x002DFF50 File Offset: 0x002DE350
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, bool isRain)
		{
			int searchCount = Singleton<Map>.Instance.EnvironmentProfile.SearchCount;
			float actionPointNavMeshSampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.ActionPointNavMeshSampleDistance;
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
			int hizamakuraID = Singleton<Manager.Resources>.Instance.PlayerProfile.HizamakuraPTID;
			foreach (ActionPoint actionPoint in source)
			{
				if (!(actionPoint == null) && !(actionPoint.OwnerArea == null))
				{
					if (actionPoint.IsNeutralCommand)
					{
						if (!actionPoint.IsReserved(agent))
						{
							if (!isRain || actionPoint.AreaType == MapArea.AreaType.Indoor)
							{
								MapArea ownerArea = actionPoint.OwnerArea;
								bool flag;
								if (!dictionary.TryGetValue(ownerArea.AreaID, out flag))
								{
									flag = (dictionary[ownerArea.AreaID] = Singleton<Map>.Instance.CheckAvailableMapArea(ownerArea.AreaID));
								}
								if (flag)
								{
									EventType eventType = actionPoint.PlayerDateEventType[(int)Singleton<Map>.Instance.Player.ChaControl.sex];
									if ((!actionPoint.IDList.IsNullOrEmpty<int>() && actionPoint.IDList.Any((int x) => x == hizamakuraID)) || (actionPoint.IDList.IsNullOrEmpty<int>() && actionPoint.ID == hizamakuraID))
									{
										if (SetHizamkuraPoint._navMeshPath == null)
										{
											SetHizamkuraPoint._navMeshPath = new NavMeshPath();
										}
										if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, SetHizamkuraPoint._navMeshPath))
										{
											if (SetHizamkuraPoint._navMeshPath.status == NavMeshPathStatus.PathComplete)
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

		// Token: 0x06006B54 RID: 27476 RVA: 0x002E0188 File Offset: 0x002DE588
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

		// Token: 0x06006B55 RID: 27477 RVA: 0x002E0250 File Offset: 0x002DE650
		public override TaskStatus OnUpdate()
		{
			int hizamakuraID = Singleton<Manager.Resources>.Instance.PlayerProfile.HizamakuraPTID;
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActionPoint != null)
			{
				ActionPoint targetInSightActionPoint = agent.TargetInSightActionPoint;
				if ((!targetInSightActionPoint.IDList.IsNullOrEmpty<int>() && targetInSightActionPoint.IDList.Any((int x) => x == hizamakuraID)) || (targetInSightActionPoint.IDList.IsNullOrEmpty<int>() && targetInSightActionPoint.ID == hizamakuraID))
				{
					return TaskStatus.Success;
				}
			}
			if (this._destination == null)
			{
				return TaskStatus.Failure;
			}
			agent.TargetInSightActionPoint = this._destination;
			agent.EventKey = EventType.Break;
			this._destination.Reserver = agent;
			return TaskStatus.Success;
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x002E0319 File Offset: 0x002DE719
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A7C RID: 23164
		private ActionPoint _destination;

		// Token: 0x04005A7D RID: 23165
		private static NavMeshPath _navMeshPath;
	}
}

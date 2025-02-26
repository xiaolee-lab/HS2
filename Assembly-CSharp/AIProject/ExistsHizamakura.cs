using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D3C RID: 3388
	[TaskCategory("")]
	public class ExistsHizamakura : AgentConditional
	{
		// Token: 0x06006BD7 RID: 27607 RVA: 0x002E48A0 File Offset: 0x002E2CA0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Chunk chunk;
			Singleton<Map>.Instance.ChunkTable.TryGetValue(agent.ChunkID, out chunk);
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			ExistsHizamakura.CreateList(agent, chunk.AppendActionPoints, list);
			bool flag = !list.IsNullOrEmpty<ActionPoint>();
			if (!flag)
			{
				ExistsHizamakura.CreateList(agent, chunk.ActionPoints, list);
				flag = !list.IsNullOrEmpty<ActionPoint>();
			}
			ListPool<ActionPoint>.Release(list);
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x002E4920 File Offset: 0x002E2D20
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination)
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
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
									if (ExistsHizamakura._navMeshPath == null)
									{
										ExistsHizamakura._navMeshPath = new NavMeshPath();
									}
									if (agent.NavMeshAgent.CalculatePath(actionPoint.LocatedPosition, ExistsHizamakura._navMeshPath))
									{
										if (ExistsHizamakura._navMeshPath.status == NavMeshPathStatus.PathComplete)
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
			DictionaryPool<int, bool>.Release(dictionary);
		}

		// Token: 0x04005ABD RID: 23229
		private static NavMeshPath _navMeshPath;
	}
}

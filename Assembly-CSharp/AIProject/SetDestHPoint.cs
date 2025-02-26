using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CCB RID: 3275
	[TaskCategory("")]
	public class SetDestHPoint : AgentAction
	{
		// Token: 0x060069F7 RID: 27127 RVA: 0x002D1D34 File Offset: 0x002D0134
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			Vector3 position = agent.Position;
			List<HPoint> list = ListPool<HPoint>.Get();
			HPointList hpointList;
			Singleton<Manager.Resources>.Instance.HSceneTable.hPointLists.TryGetValue(Singleton<Map>.Instance.MapID, out hpointList);
			list.Clear();
			foreach (KeyValuePair<int, List<HPoint>> keyValuePair in hpointList.lst)
			{
				if (agent.FromFemale)
				{
					SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 0);
					SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 3);
					SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 5);
					SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 6);
					SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 13);
				}
				else
				{
					PlayerActor player = Singleton<Map>.Instance.Player;
					if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
					{
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 0);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 1);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 2);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 3);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 5);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 6);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 13);
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, 4);
					}
					else
					{
						SetDestHPoint.CreateList(agent, keyValuePair.Value, list, -1);
					}
				}
			}
			foreach (KeyValuePair<int, Chunk> keyValuePair2 in Singleton<Map>.Instance.ChunkTable)
			{
				foreach (MapArea mapArea in keyValuePair2.Value.MapAreas)
				{
					if (mapArea.AreaID == agent.AreaID)
					{
						SetDestHPoint.CreateList(agent, mapArea.AppendHPoints, list, 0);
					}
				}
			}
			if (!list.IsNullOrEmpty<HPoint>())
			{
				SetDestHPoint.NearestPoint(position, list, out this._destination);
			}
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x002D1FB8 File Offset: 0x002D03B8
		private static void CreateList(AgentActor agent, List<HPoint> source, List<HPoint> destination, int placeID = -1)
		{
			float hsampleDistance = Singleton<Manager.Resources>.Instance.AgentProfile.HSampleDistance;
			foreach (HPoint hpoint in source)
			{
				if (!(hpoint == null))
				{
					if (placeID == -1 || hpoint._nPlace.Exists((KeyValuePair<int, UnityEx.ValueTuple<int, int>> kvp) => kvp.Value.Item1 == placeID))
					{
						if (SetDestHPoint._navMeshPath == null)
						{
							SetDestHPoint._navMeshPath = new NavMeshPath();
						}
						if (agent.NavMeshAgent.CalculatePath(hpoint.transform.position, SetDestHPoint._navMeshPath))
						{
							if (SetDestHPoint._navMeshPath.status == NavMeshPathStatus.PathComplete)
							{
								destination.Add(hpoint);
							}
						}
					}
				}
			}
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x002D20B8 File Offset: 0x002D04B8
		private static void NearestPoint(Vector3 position, List<HPoint> hPoints, out HPoint destination)
		{
			destination = null;
			float? num = null;
			foreach (HPoint hpoint in hPoints)
			{
				float num2 = Vector3.Distance(position, hpoint.transform.position);
				if (num == null)
				{
					num = new float?(num2);
					destination = hpoint;
				}
				else if (num == null || !(num <= num2))
				{
					num = new float?(num2);
					destination = hpoint;
				}
			}
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x002D2188 File Offset: 0x002D0588
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.DestPosition != null)
			{
				return TaskStatus.Success;
			}
			float hsampleDistance = Singleton<Manager.Resources>.Instance.AgentProfile.HSampleDistance;
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(this._destination.transform.position, out navMeshHit, hsampleDistance, -1))
			{
				agent.DestPosition = new Vector3?(navMeshHit.position);
			}
			else
			{
				agent.DestPosition = new Vector3?(this._destination.transform.position);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x002D2212 File Offset: 0x002D0612
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x040059D1 RID: 22993
		private HPoint _destination;

		// Token: 0x040059D2 RID: 22994
		private static NavMeshPath _navMeshPath;
	}
}

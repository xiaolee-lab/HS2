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
	// Token: 0x02000D10 RID: 3344
	[TaskCategory("")]
	public class SetDesiredActionOtherChunk : AgentAction
	{
		// Token: 0x06006B2E RID: 27438 RVA: 0x002DD6FC File Offset: 0x002DBAFC
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			Vector3 position = agent.Position;
			List<ActionPoint> list = ListPool<ActionPoint>.Get();
			PointManager pointAgent = Singleton<Manager.Map>.Instance.PointAgent;
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			if ((weather == Weather.Rain || weather == Weather.Storm) && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(16))
			{
				SetDesiredActionOtherChunk.CreateList(agent, pointAgent.AppendActionPoints, list, this._eventType, this._checkFollowType, true);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
				if (this._destination == null)
				{
					list.Clear();
					SetDesiredActionOtherChunk.CreateList(agent, pointAgent.ActionPoints, list, this._eventType, this._checkFollowType, true);
					if (!list.IsNullOrEmpty<ActionPoint>())
					{
						this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
					}
				}
			}
			if (this._destination == null && !agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(17))
			{
				list.Clear();
				SetDesiredActionOtherChunk.CreateList(agent, pointAgent.AppendActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			if (this._destination == null)
			{
				list.Clear();
				SetDesiredActionOtherChunk.CreateList(agent, pointAgent.ActionPoints, list, this._eventType, this._checkFollowType, false);
				if (!list.IsNullOrEmpty<ActionPoint>())
				{
					this._destination = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			ListPool<ActionPoint>.Release(list);
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x002DD8C8 File Offset: 0x002DBCC8
		private static void CreateList(AgentActor agent, List<ActionPoint> source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
			int chunkID = agent.ChunkID;
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
			int searchCount = Singleton<Manager.Map>.Instance.EnvironmentProfile.SearchCount;
			float actionPointNavMeshSampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.ActionPointNavMeshSampleDistance;
			foreach (ActionPoint actionPoint in source)
			{
				if (SetDesiredActionOtherChunk.CheckNeutral(agent, actionPoint, dictionary, searchCount, chunkID, eventType, isFollow, isRain, actionPointNavMeshSampleDistance))
				{
					destination.Add(actionPoint);
				}
			}
			DictionaryPool<int, bool>.Release(dictionary);
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x002DD96C File Offset: 0x002DBD6C
		private static void CreateList(AgentActor agent, ActionPoint[] source, List<ActionPoint> destination, EventType eventType, bool isFollow, bool isRain)
		{
			int chunkID = agent.ChunkID;
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
			int searchCount = Singleton<Manager.Map>.Instance.EnvironmentProfile.SearchCount;
			float actionPointNavMeshSampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.ActionPointNavMeshSampleDistance;
			foreach (ActionPoint actionPoint in source)
			{
				if (SetDesiredActionOtherChunk.CheckNeutral(agent, actionPoint, dictionary, searchCount, chunkID, eventType, isFollow, isRain, actionPointNavMeshSampleDistance))
				{
					destination.Add(actionPoint);
				}
			}
			DictionaryPool<int, bool>.Release(dictionary);
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x002DD9F0 File Offset: 0x002DBDF0
		private static bool CheckNeutral(AgentActor agent, ActionPoint pt, Dictionary<int, bool> availableArea, int searchCount, int chunkID, EventType eventType, bool isFollow, bool isRain, float sampleDistance)
		{
			if (pt == null || pt.OwnerArea == null)
			{
				return false;
			}
			if (!pt.IsNeutralCommand)
			{
				return false;
			}
			if (pt.IsReserved(agent))
			{
				return false;
			}
			List<ActionPoint> connectedActionPoints = pt.ConnectedActionPoints;
			if (!connectedActionPoints.IsNullOrEmpty<ActionPoint>())
			{
				foreach (ActionPoint actionPoint in connectedActionPoints)
				{
					if (!(actionPoint == null))
					{
						if (!actionPoint.IsNeutralCommand || actionPoint.IsReserved(agent))
						{
							return false;
						}
					}
				}
			}
			if (isRain && pt.AreaType != MapArea.AreaType.Indoor)
			{
				return false;
			}
			MapArea ownerArea = pt.OwnerArea;
			if (ownerArea.ChunkID == chunkID)
			{
				return false;
			}
			bool flag;
			if (!availableArea.TryGetValue(ownerArea.AreaID, out flag))
			{
				flag = (availableArea[ownerArea.AreaID] = Singleton<Manager.Map>.Instance.CheckAvailableMapArea(ownerArea.AreaID));
			}
			if (!flag)
			{
				return false;
			}
			EventType source;
			if (isFollow)
			{
				source = pt.AgentDateEventType;
			}
			else
			{
				source = pt.AgentEventType;
			}
			if (source.Contains(eventType))
			{
				if (eventType != EventType.Eat)
				{
					if (eventType != EventType.Search)
					{
						if (eventType == EventType.Warp)
						{
							WarpPoint warpPoint = pt as WarpPoint;
							if (!(warpPoint != null))
							{
								return false;
							}
							Dictionary<int, List<WarpPoint>> dictionary;
							List<WarpPoint> list;
							if (!Singleton<Manager.Map>.Instance.WarpPointDic.TryGetValue(ownerArea.ChunkID, out dictionary) || !dictionary.TryGetValue(warpPoint.TableID, out list))
							{
								return false;
							}
							if (list.Count < 2)
							{
								return false;
							}
						}
					}
					else
					{
						SearchActionPoint searchActionPoint = pt as SearchActionPoint;
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
								return false;
							}
							int tableID = searchActionPoint.TableID;
							StuffItem itemInfo = agent.AgentData.EquipedSearchItem(tableID);
							int searchAreaID = agent.SearchAreaID;
							if (searchAreaID != 0)
							{
								if (agent.SearchAreaID != searchActionPoint.TableID)
								{
									return false;
								}
								if (!searchActionPoint.CanSearch(EventType.Search, itemInfo))
								{
									return false;
								}
							}
							else
							{
								if (tableID != 0 && tableID != 1 && tableID != 2)
								{
									return false;
								}
								if (!searchActionPoint.CanSearch(EventType.Search, itemInfo))
								{
									return false;
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
						if (!pt.FindAgentActionPointInfo(EventType.Eat, eatDeskID.poseID, out actionPointInfo) && !pt.FindAgentActionPointInfo(EventType.Eat, eatDeskID2.poseID, out actionPointInfo))
						{
							return false;
						}
					}
					else if (!pt.FindAgentActionPointInfo(EventType.Eat, agentProfile.PoseIDTable.EatDishID.poseID, out actionPointInfo))
					{
						return false;
					}
				}
				if (SetDesiredActionOtherChunk._navMeshPath == null)
				{
					SetDesiredActionOtherChunk._navMeshPath = new NavMeshPath();
				}
				NavMeshHit navMeshHit;
				return agent.NavMeshAgent.CalculatePath(pt.LocatedPosition, SetDesiredActionOtherChunk._navMeshPath) && SetDesiredActionOtherChunk._navMeshPath.status == NavMeshPathStatus.PathComplete && NavMesh.SamplePosition(pt.LocatedPosition, out navMeshHit, sampleDistance, agent.NavMeshAgent.areaMask);
			}
			return false;
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x002DDE30 File Offset: 0x002DC230
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

		// Token: 0x06006B33 RID: 27443 RVA: 0x002DDF02 File Offset: 0x002DC302
		public override void OnEnd()
		{
			base.OnEnd();
			this._destination = null;
		}

		// Token: 0x04005A65 RID: 23141
		[SerializeField]
		private bool _checkFollowType;

		// Token: 0x04005A66 RID: 23142
		[SerializeField]
		private EventType _eventType;

		// Token: 0x04005A67 RID: 23143
		[SerializeField]
		private Desire.Type _desireIfNotFound;

		// Token: 0x04005A68 RID: 23144
		private ActionPoint _destination;

		// Token: 0x04005A69 RID: 23145
		private static NavMeshPath _navMeshPath;
	}
}

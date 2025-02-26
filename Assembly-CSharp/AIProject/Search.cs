using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CA5 RID: 3237
	[TaskCategory("")]
	public class Search : AgentStateAction
	{
		// Token: 0x0600694A RID: 26954 RVA: 0x002CC60C File Offset: 0x002CAA0C
		public override void OnStart()
		{
			base.Agent.EventKey = EventType.Search;
			base.OnStart();
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x002CC624 File Offset: 0x002CAA24
		protected override void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Hunt);
			agent.SetDesire(desireKey, 0f);
			Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = agent.AgentData.SearchActionLockTable;
			AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
			if (!searchActionLockTable.TryGetValue(agent.CurrentPoint.RegisterID, out searchActionInfo))
			{
				searchActionInfo = new AIProject.SaveData.Environment.SearchActionInfo();
			}
			searchActionInfo.Count++;
			searchActionLockTable[agent.CurrentPoint.RegisterID] = searchActionInfo;
			ActionPoint currentPoint = base.Agent.CurrentPoint;
			Resources instance = Singleton<Resources>.Instance;
			int pointID;
			if (!currentPoint.IDList.IsNullOrEmpty<int>())
			{
				pointID = currentPoint.IDList.GetElement(0);
			}
			else
			{
				pointID = currentPoint.ID;
			}
			Dictionary<int, ItemTableElement> itemTableInArea = instance.GameInfo.GetItemTableInArea(pointID);
			if (itemTableInArea == null)
			{
			}
			Actor.SearchInfo searchInfo = agent.RandomAddItem(itemTableInArea, false);
			if (searchInfo.IsSuccess)
			{
				foreach (Actor.ItemSearchInfo itemSearchInfo in searchInfo.ItemList)
				{
					StuffItem item = new StuffItem(itemSearchInfo.categoryID, itemSearchInfo.id, itemSearchInfo.count);
					agent.AgentData.ItemList.AddItem(item);
					StuffItemInfo item2 = Singleton<Resources>.Instance.GameInfo.GetItem(itemSearchInfo.categoryID, itemSearchInfo.id);
					MapUIContainer.AddItemLog(agent, item2, itemSearchInfo.count, false);
				}
			}
			SearchActionPoint searchActionPoint = currentPoint as SearchActionPoint;
			if (searchActionPoint != null)
			{
				if (searchActionPoint.TableID == 5)
				{
					agent.AgentData.SetAppendEventFlagCheck(8, true);
				}
				else if (searchActionPoint.TableID == 6)
				{
					agent.AgentData.SetAppendEventFlagCheck(6, true);
				}
				agent.AgentData.AddAppendEventFlagParam(5, 1);
			}
		}
	}
}

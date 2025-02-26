using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D45 RID: 3397
	[TaskCategory("")]
	public class HasItemToEat : AgentConditional
	{
		// Token: 0x06006BEC RID: 27628 RVA: 0x002E4F5C File Offset: 0x002E335C
		public override TaskStatus OnUpdate()
		{
			List<StuffItem> itemList = base.Agent.AgentData.ItemList;
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> foodParameterTable = Singleton<Resources>.Instance.GameInfo.FoodParameterTable;
			foreach (StuffItem stuffItem in itemList)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				if (foodParameterTable.TryGetValue(stuffItem.CategoryID, out dictionary))
				{
					Dictionary<int, FoodParameterPacket> dictionary2;
					if (dictionary.TryGetValue(stuffItem.ID, out dictionary2))
					{
						return TaskStatus.Success;
					}
				}
			}
			return TaskStatus.Failure;
		}
	}
}

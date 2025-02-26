using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D44 RID: 3396
	[TaskCategory("")]
	public class HasItemToDrink : AgentConditional
	{
		// Token: 0x06006BEA RID: 27626 RVA: 0x002E4EA0 File Offset: 0x002E32A0
		public override TaskStatus OnUpdate()
		{
			List<StuffItem> itemList = base.Agent.AgentData.ItemList;
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> drinkParameterTable = Singleton<Resources>.Instance.GameInfo.DrinkParameterTable;
			foreach (StuffItem stuffItem in itemList)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				if (drinkParameterTable.TryGetValue(stuffItem.CategoryID, out dictionary))
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

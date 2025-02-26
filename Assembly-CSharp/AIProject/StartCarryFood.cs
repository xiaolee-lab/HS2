using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1F RID: 3359
	[TaskCategory("")]
	public class StartCarryFood : AgentAction
	{
		// Token: 0x06006B6B RID: 27499 RVA: 0x002E0744 File Offset: 0x002DEB44
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			AgentData agentData = agent.AgentData;
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> foodParameterTable = Singleton<Manager.Resources>.Instance.GameInfo.FoodParameterTable;
			List<StuffItem> list = ListPool<StuffItem>.Get();
			ItemIDKeyPair[] canStandEatItems = Singleton<Manager.Resources>.Instance.AgentProfile.CanStandEatItems;
			foreach (StuffItem stuffItem in agentData.ItemList)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				if (foodParameterTable.TryGetValue(stuffItem.CategoryID, out dictionary))
				{
					Dictionary<int, FoodParameterPacket> dictionary2;
					if (dictionary.TryGetValue(stuffItem.ID, out dictionary2))
					{
						if (this._selectCanStandFood)
						{
							bool flag = false;
							foreach (ItemIDKeyPair itemIDKeyPair in canStandEatItems)
							{
								if (itemIDKeyPair.categoryID == stuffItem.CategoryID && itemIDKeyPair.itemID == stuffItem.ID)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								continue;
							}
						}
						list.Add(stuffItem);
					}
				}
			}
			StuffItem element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			if (element == null)
			{
				return TaskStatus.Failure;
			}
			agentData.CarryingItem = new StuffItem(element.CategoryID, element.ID, 1);
			ListPool<StuffItem>.Release(list);
			if (agentData.CarryingItem == null)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005A84 RID: 23172
		[SerializeField]
		private bool _selectCanStandFood;
	}
}

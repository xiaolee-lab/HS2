using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB4 RID: 3252
	[TaskCategory("")]
	public class CheckPantry : AgentAction
	{
		// Token: 0x06006998 RID: 27032 RVA: 0x002CF33C File Offset: 0x002CD73C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			base.OnStart();
			agent.DisableActionFlag();
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x002CF368 File Offset: 0x002CD768
		public override TaskStatus OnUpdate()
		{
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x002CF374 File Offset: 0x002CD774
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.ResetActionFlag();
			AgentData agentData = agent.AgentData;
			List<StuffItem> itemListInPantry = Singleton<Game>.Instance.WorldData.Environment.ItemListInPantry;
			List<StuffItem> list = ListPool<StuffItem>.Get();
			Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> dictionary;
			if (this._checkType == CheckPantry.CheckType.Eat)
			{
				dictionary = Singleton<Manager.Resources>.Instance.GameInfo.FoodParameterTable;
			}
			else
			{
				dictionary = Singleton<Manager.Resources>.Instance.GameInfo.DrinkParameterTable;
			}
			foreach (StuffItem stuffItem3 in itemListInPantry)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary2;
				if (dictionary.TryGetValue(stuffItem3.CategoryID, out dictionary2))
				{
					Dictionary<int, FoodParameterPacket> dictionary3;
					if (dictionary2.TryGetValue(stuffItem3.ID, out dictionary3))
					{
						list.Add(stuffItem3);
					}
				}
			}
			StuffItem stuffItem2 = null;
			if (this._checkType == CheckPantry.CheckType.Eat)
			{
				stuffItem2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			}
			else
			{
				AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
				float num = agentData.StatsTable[0];
				if (num <= agentProfile.ColdTempBorder)
				{
					List<StuffItem> list2 = ListPool<StuffItem>.Get();
					using (List<StuffItem>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							StuffItem stuffItem = enumerator2.Current;
							if (agentProfile.LowerTempDrinkItems.Exists((ItemIDKeyPair pair) => pair.categoryID == stuffItem.CategoryID && pair.itemID == stuffItem.ID))
							{
								list2.Add(stuffItem);
							}
						}
					}
					if (!list2.IsNullOrEmpty<StuffItem>())
					{
						stuffItem2 = list2.GetElement(UnityEngine.Random.Range(0, list2.Count));
					}
					ListPool<StuffItem>.Release(list2);
				}
				else if (num >= agentProfile.HotTempBorder)
				{
					List<StuffItem> list3 = ListPool<StuffItem>.Get();
					using (List<StuffItem>.Enumerator enumerator3 = list.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							StuffItem stuffItem = enumerator3.Current;
							if (agentProfile.RaiseTempDrinkItems.Exists((ItemIDKeyPair pair) => pair.categoryID == stuffItem.CategoryID && pair.itemID == stuffItem.ID))
							{
								list3.Add(stuffItem);
							}
						}
					}
					if (!list3.IsNullOrEmpty<StuffItem>())
					{
						stuffItem2 = list3.GetElement(UnityEngine.Random.Range(0, list3.Count));
					}
					ListPool<StuffItem>.Release(list3);
				}
				if (stuffItem2 == null)
				{
					stuffItem2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				}
			}
			if (stuffItem2 != null)
			{
				StuffItem item = new StuffItem(stuffItem2.CategoryID, stuffItem2.ID, 1);
				agentData.ItemList.Add(item);
				itemListInPantry.RemoveItem(item);
			}
			ListPool<StuffItem>.Release(list);
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x040059B1 RID: 22961
		[SerializeField]
		private CheckPantry.CheckType _checkType;

		// Token: 0x02000CB5 RID: 3253
		private enum CheckType
		{
			// Token: 0x040059B3 RID: 22963
			Eat,
			// Token: 0x040059B4 RID: 22964
			Drink
		}
	}
}

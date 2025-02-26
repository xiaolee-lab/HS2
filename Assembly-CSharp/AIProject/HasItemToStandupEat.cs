using System;
using System.Collections.Generic;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D47 RID: 3399
	[TaskCategory("")]
	public class HasItemToStandupEat : AgentConditional
	{
		// Token: 0x06006BF0 RID: 27632 RVA: 0x002E50D4 File Offset: 0x002E34D4
		public override TaskStatus OnUpdate()
		{
			AgentProfile agentProfile = Singleton<Resources>.Instance.AgentProfile;
			using (List<StuffItem>.Enumerator enumerator = base.Agent.AgentData.ItemList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StuffItem item = enumerator.Current;
					if (agentProfile.CanStandEatItems.Exists((ItemIDKeyPair pair) => pair.categoryID == item.CategoryID && pair.itemID == item.ID))
					{
						return TaskStatus.Success;
					}
				}
			}
			return TaskStatus.Failure;
		}
	}
}

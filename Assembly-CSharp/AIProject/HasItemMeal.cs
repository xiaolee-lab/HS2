using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D43 RID: 3395
	[TaskCategory("")]
	public class HasItemMeal : AgentConditional
	{
		// Token: 0x06006BE8 RID: 27624 RVA: 0x002E4DFC File Offset: 0x002E31FC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			AgentProfile agentProfile = Singleton<Resources>.Instance.AgentProfile;
			StuffItem carryingItem = agent.AgentData.CarryingItem;
			if (carryingItem == null)
			{
				return TaskStatus.Failure;
			}
			if (!agentProfile.CanStandEatItems.Exists((ItemIDKeyPair pair) => pair.categoryID == carryingItem.CategoryID && pair.itemID == carryingItem.ID))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

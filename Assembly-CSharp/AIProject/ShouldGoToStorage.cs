using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D78 RID: 3448
	[TaskCategory("")]
	public class ShouldGoToStorage : AgentConditional
	{
		// Token: 0x06006C56 RID: 27734 RVA: 0x002E6171 File Offset: 0x002E4571
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AgentData.ItemList.Count >= Singleton<Resources>.Instance.AgentProfile.ItemSlotCountToItemBox)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

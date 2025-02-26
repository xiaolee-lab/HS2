using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D39 RID: 3385
	[TaskCategory("")]
	public class IsEquipedUmbrella : AgentConditional
	{
		// Token: 0x06006BCD RID: 27597 RVA: 0x002E3F58 File Offset: 0x002E2358
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.EquipedItem == null)
			{
				return TaskStatus.Failure;
			}
			ItemIDKeyPair umbrellaID = Singleton<Resources>.Instance.CommonDefine.ItemIDDefine.UmbrellaID;
			EquipEventItemInfo equipEventItemInfo = Singleton<Resources>.Instance.GameInfo.CommonEquipEventItemTable[umbrellaID.categoryID][umbrellaID.itemID];
			if (equipEventItemInfo.EventItemID == base.Agent.EquipedItem.EventItemID)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

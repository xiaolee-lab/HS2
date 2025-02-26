using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D46 RID: 3398
	[TaskCategory("")]
	public class HasItemToEquip : AgentConditional
	{
		// Token: 0x06006BEE RID: 27630 RVA: 0x002E5018 File Offset: 0x002E3418
		public override TaskStatus OnUpdate()
		{
			foreach (StuffItem stuffItem in base.Agent.AgentData.ItemList)
			{
				StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID);
				if (item != null)
				{
					if (item.EquipableState >= ItemEquipableState.Heroine)
					{
						if (this._itemID == item.ID)
						{
							return TaskStatus.Success;
						}
					}
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC1 RID: 23233
		[SerializeField]
		private int _itemID;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D40 RID: 3392
	[TaskCategory("")]
	public class HasEquipedItem : AgentConditional
	{
		// Token: 0x06006BE2 RID: 27618 RVA: 0x002E4D31 File Offset: 0x002E3131
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.EquipedItem == null)
			{
				return TaskStatus.Failure;
			}
			if (this._eventItemID == base.Agent.EquipedItem.EventItemID)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ABF RID: 23231
		[SerializeField]
		private int _eventItemID;
	}
}

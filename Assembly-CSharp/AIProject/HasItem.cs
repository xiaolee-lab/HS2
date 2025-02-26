using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D42 RID: 3394
	[TaskCategory("")]
	public class HasItem : AgentConditional
	{
		// Token: 0x06006BE6 RID: 27622 RVA: 0x002E4D78 File Offset: 0x002E3178
		public override TaskStatus OnUpdate()
		{
			foreach (StuffItem stuffItem in base.Agent.AgentData.ItemList)
			{
				if (stuffItem.CategoryID == this._targetCategory)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC0 RID: 23232
		[SerializeField]
		private int _targetCategory;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D38 RID: 3384
	[TaskCategory("")]
	public class EqualsSelectedActionID : AgentConditional
	{
		// Token: 0x06006BCB RID: 27595 RVA: 0x002E3F35 File Offset: 0x002E2335
		public override TaskStatus OnUpdate()
		{
			if (this._assignedID == base.Agent.SelectedActionID)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AB6 RID: 23222
		[SerializeField]
		private int _assignedID;
	}
}

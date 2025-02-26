using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D0B RID: 3339
	[TaskCategory("")]
	public class ReserveMode : AgentAction
	{
		// Token: 0x06006B1A RID: 27418 RVA: 0x002DC5B7 File Offset: 0x002DA9B7
		public override TaskStatus OnUpdate()
		{
			base.Agent.ReservedMode = this._actionType;
			return TaskStatus.Success;
		}

		// Token: 0x04005A59 RID: 23129
		[SerializeField]
		private Desire.ActionType _actionType;
	}
}

using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D19 RID: 3353
	[TaskCategory("")]
	public class SetPrevMode : AgentAction
	{
		// Token: 0x06006B5F RID: 27487 RVA: 0x002E03E7 File Offset: 0x002DE7E7
		public override TaskStatus OnUpdate()
		{
			base.Agent.PrevMode = this._mode;
			return TaskStatus.Success;
		}

		// Token: 0x04005A7F RID: 23167
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

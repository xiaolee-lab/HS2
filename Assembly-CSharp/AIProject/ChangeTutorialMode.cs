using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C82 RID: 3202
	[TaskCategory("")]
	public class ChangeTutorialMode : AgentAction
	{
		// Token: 0x060068E3 RID: 26851 RVA: 0x002CA2BA File Offset: 0x002C86BA
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeTutorialBehavior(this._changeType);
			return TaskStatus.Success;
		}

		// Token: 0x0400597B RID: 22907
		[SerializeField]
		private Tutorial.ActionType _changeType;
	}
}

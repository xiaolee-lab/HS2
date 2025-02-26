using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C85 RID: 3205
	[TaskCategory("")]
	public class ChangeMode : AgentAction
	{
		// Token: 0x060068E9 RID: 26857 RVA: 0x002CA318 File Offset: 0x002C8718
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeBehavior(this._modeType);
			return TaskStatus.Success;
		}

		// Token: 0x0400597D RID: 22909
		[SerializeField]
		private Desire.ActionType _modeType;
	}
}

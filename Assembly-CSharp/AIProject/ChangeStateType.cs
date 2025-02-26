using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C89 RID: 3209
	[TaskCategory("")]
	public class ChangeStateType : AgentAction
	{
		// Token: 0x060068F1 RID: 26865 RVA: 0x002CA387 File Offset: 0x002C8787
		public override TaskStatus OnUpdate()
		{
			base.Agent.StateType = this._type;
			return TaskStatus.Success;
		}

		// Token: 0x0400597F RID: 22911
		[SerializeField]
		private State.Type _type;
	}
}

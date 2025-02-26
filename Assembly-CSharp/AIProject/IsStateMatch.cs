using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D6C RID: 3436
	[TaskCategory("")]
	public class IsStateMatch : AgentConditional
	{
		// Token: 0x06006C3E RID: 27710 RVA: 0x002E5E79 File Offset: 0x002E4279
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.StateType != this._targetState)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005ADF RID: 23263
		[SerializeField]
		private State.Type _targetState;
	}
}

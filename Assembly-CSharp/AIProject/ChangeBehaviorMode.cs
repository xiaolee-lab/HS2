using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C83 RID: 3203
	[TaskCategory("")]
	public class ChangeBehaviorMode : AgentAction
	{
		// Token: 0x060068E5 RID: 26853 RVA: 0x002CA2D6 File Offset: 0x002C86D6
		public override TaskStatus OnUpdate()
		{
			base.Agent.BehaviorResources.ChangeMode(this._type);
			return TaskStatus.Success;
		}

		// Token: 0x0400597C RID: 22908
		[SerializeField]
		private Desire.ActionType _type;
	}
}

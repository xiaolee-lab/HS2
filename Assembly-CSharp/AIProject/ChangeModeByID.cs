using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C86 RID: 3206
	[TaskCategory("")]
	public class ChangeModeByID : AgentAction
	{
		// Token: 0x060068EB RID: 26859 RVA: 0x002CA334 File Offset: 0x002C8734
		public override TaskStatus OnUpdate()
		{
			base.Agent.ChangeBehavior((Desire.ActionType)this._modeID);
			return TaskStatus.Success;
		}

		// Token: 0x0400597E RID: 22910
		[SerializeField]
		private int _modeID;
	}
}

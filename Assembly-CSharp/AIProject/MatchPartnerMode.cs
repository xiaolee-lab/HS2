using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D74 RID: 3444
	[TaskCategory("")]
	public class MatchPartnerMode : AgentConditional
	{
		// Token: 0x06006C4E RID: 27726 RVA: 0x002E60E5 File Offset: 0x002E44E5
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Partner.Mode == this._mode)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AE8 RID: 23272
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

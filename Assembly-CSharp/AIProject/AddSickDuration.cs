using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1B RID: 3355
	[TaskCategory("")]
	public class AddSickDuration : AgentAction
	{
		// Token: 0x06006B63 RID: 27491 RVA: 0x002E0454 File Offset: 0x002DE854
		public override TaskStatus OnUpdate()
		{
			TimeSpan t = TimeSpan.FromDays((double)this._dailyDuration);
			base.Agent.AgentData.SickState.Duration += t;
			return TaskStatus.Success;
		}

		// Token: 0x04005A81 RID: 23169
		[SerializeField]
		private float _dailyDuration = 1f;
	}
}

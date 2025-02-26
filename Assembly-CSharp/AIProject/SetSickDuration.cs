using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1A RID: 3354
	[TaskCategory("")]
	public class SetSickDuration : AgentAction
	{
		// Token: 0x06006B61 RID: 27489 RVA: 0x002E0410 File Offset: 0x002DE810
		public override TaskStatus OnUpdate()
		{
			TimeSpan duration = TimeSpan.FromDays((double)this._dailyDuration);
			base.Agent.AgentData.SickState.Duration = duration;
			return TaskStatus.Success;
		}

		// Token: 0x04005A80 RID: 23168
		[SerializeField]
		private float _dailyDuration = 1f;
	}
}

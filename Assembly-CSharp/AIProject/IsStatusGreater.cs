using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D6D RID: 3437
	[TaskCategory("")]
	public class IsStatusGreater : AgentConditional
	{
		// Token: 0x06006C40 RID: 27712 RVA: 0x002E5E9C File Offset: 0x002E429C
		public override TaskStatus OnUpdate()
		{
			bool flag = false;
			float num;
			if (!base.Agent.AgentData.StatsTable.TryGetValue((int)this._target, out num))
			{
				return TaskStatus.Failure;
			}
			if (this._compareDowner)
			{
				flag |= (num < this._borderValue);
			}
			else
			{
				flag |= (num > this._borderValue);
			}
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AE0 RID: 23264
		[SerializeField]
		private float _borderValue;

		// Token: 0x04005AE1 RID: 23265
		[SerializeField]
		private bool _compareDowner;

		// Token: 0x04005AE2 RID: 23266
		[SerializeField]
		private Status.Type _target;
	}
}

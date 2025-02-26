using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D6E RID: 3438
	public class IsStatusOver : AgentConditional
	{
		// Token: 0x06006C42 RID: 27714 RVA: 0x002E5F08 File Offset: 0x002E4308
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
				flag |= (num < this._border);
			}
			else
			{
				flag |= (num > this._border);
			}
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AE3 RID: 23267
		[SerializeField]
		private float _border;

		// Token: 0x04005AE4 RID: 23268
		[SerializeField]
		private bool _compareDowner;

		// Token: 0x04005AE5 RID: 23269
		[SerializeField]
		private Status.Type _target;
	}
}

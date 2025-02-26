using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CDD RID: 3293
	[TaskCategory("")]
	public class RandomSelectAction : AgentAction
	{
		// Token: 0x06006A71 RID: 27249 RVA: 0x002D5BCB File Offset: 0x002D3FCB
		public override TaskStatus OnUpdate()
		{
			base.Agent.SelectedActionID = UnityEngine.Random.Range(this._randMin, this._randMax);
			return TaskStatus.Success;
		}

		// Token: 0x04005A02 RID: 23042
		[SerializeField]
		private int _randMin;

		// Token: 0x04005A03 RID: 23043
		[SerializeField]
		private int _randMax = 1;
	}
}

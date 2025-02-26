using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D4E RID: 3406
	[TaskCategory("")]
	public class IsAreaMatch : AgentConditional
	{
		// Token: 0x06006BFF RID: 27647 RVA: 0x002E5354 File Offset: 0x002E3754
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.AreaType == this._area)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC5 RID: 23237
		[SerializeField]
		private MapArea.AreaType _area = MapArea.AreaType.Indoor;
	}
}

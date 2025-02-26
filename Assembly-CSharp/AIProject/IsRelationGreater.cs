using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D69 RID: 3433
	[TaskCategory("")]
	public class IsRelationGreater : AgentConditional
	{
		// Token: 0x06006C37 RID: 27703 RVA: 0x002E5D24 File Offset: 0x002E4124
		public override TaskStatus OnUpdate()
		{
			int id = base.Agent.TargetInSightActor.ID;
			int num;
			if (!base.Agent.AgentData.FriendlyRelationShipTable.TryGetValue(id, out num))
			{
				int num2 = 50;
				base.Agent.AgentData.FriendlyRelationShipTable[id] = num2;
				num = num2;
			}
			if ((float)num > this._borderValue)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ADC RID: 23260
		[SerializeField]
		private float _borderValue = 60f;
	}
}

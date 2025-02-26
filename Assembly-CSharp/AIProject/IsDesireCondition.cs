using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D52 RID: 3410
	[TaskCategory("")]
	public class IsDesireCondition : AgentConditional
	{
		// Token: 0x06006C08 RID: 27656 RVA: 0x002E5518 File Offset: 0x002E3918
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(this._desireType);
			if (desireKey == -1)
			{
				return TaskStatus.Failure;
			}
			if (base.Agent.GetDesire(desireKey) < this._boundingValue.Value)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005AC9 RID: 23241
		[SerializeField]
		private Desire.Type _desireType;

		// Token: 0x04005ACA RID: 23242
		[SerializeField]
		private SharedFloat _boundingValue = 0f;
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D48 RID: 3400
	[TaskCategory("")]
	public class HasPartner : AgentConditional
	{
		// Token: 0x06006BF2 RID: 27634 RVA: 0x002E51B8 File Offset: 0x002E35B8
		public override TaskStatus OnUpdate()
		{
			if (this._condition)
			{
				if (base.Agent.Partner != null)
				{
					return TaskStatus.Success;
				}
			}
			else if (base.Agent.Partner == null)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC2 RID: 23234
		[SerializeField]
		private bool _condition = true;
	}
}

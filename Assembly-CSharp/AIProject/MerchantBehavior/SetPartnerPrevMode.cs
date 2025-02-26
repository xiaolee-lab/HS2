using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC4 RID: 3524
	[TaskCategory("商人")]
	public class SetPartnerPrevMode : MerchantAction
	{
		// Token: 0x06006D65 RID: 28005 RVA: 0x002E9094 File Offset: 0x002E7494
		public override TaskStatus OnUpdate()
		{
			AgentActor agentActor = base.Merchant.Partner as AgentActor;
			if (agentActor == null)
			{
				return TaskStatus.Failure;
			}
			agentActor.PrevMode = this._mode;
			return TaskStatus.Success;
		}

		// Token: 0x04005B3D RID: 23357
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

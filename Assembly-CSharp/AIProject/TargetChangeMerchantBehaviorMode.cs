using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C8C RID: 3212
	[TaskCategory("")]
	public class TargetChangeMerchantBehaviorMode : AgentAction
	{
		// Token: 0x060068F7 RID: 26871 RVA: 0x002CA454 File Offset: 0x002C8854
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent == null)
			{
				return TaskStatus.Failure;
			}
			MerchantActor merchantActor = base.Agent.TargetInSightActor as MerchantActor;
			if (merchantActor == null)
			{
				return TaskStatus.Failure;
			}
			merchantActor.ChangeBehavior(this._mode);
			return TaskStatus.Success;
		}

		// Token: 0x04005982 RID: 22914
		[SerializeField]
		private Merchant.ActionType _mode = Merchant.ActionType.Idle;
	}
}

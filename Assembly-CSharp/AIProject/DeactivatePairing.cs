using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CBD RID: 3261
	[TaskCategory("")]
	public class DeactivatePairing : AgentAction
	{
		// Token: 0x060069C7 RID: 27079 RVA: 0x002D08B0 File Offset: 0x002CECB0
		public override TaskStatus OnUpdate()
		{
			Actor partner = base.Agent.Partner;
			base.Agent.Partner = null;
			if (partner != null)
			{
				partner.Partner = null;
			}
			if (this._changeToPartner)
			{
				if (partner is AgentActor)
				{
					(partner as AgentActor).ChangeBehavior(Desire.ActionType.Normal);
				}
				else if (partner is MerchantActor)
				{
					MerchantActor merchantActor = partner as MerchantActor;
					merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				}
			}
			base.Agent.TargetInSightActor = null;
			return TaskStatus.Success;
		}

		// Token: 0x040059BD RID: 22973
		[SerializeField]
		private bool _changeToPartner;
	}
}

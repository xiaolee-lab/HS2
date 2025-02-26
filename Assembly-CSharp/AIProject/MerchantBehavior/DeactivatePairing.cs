using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB8 RID: 3512
	[TaskCategory("商人")]
	public class DeactivatePairing : MerchantAction
	{
		// Token: 0x06006D3F RID: 27967 RVA: 0x002E8758 File Offset: 0x002E6B58
		public override TaskStatus OnUpdate()
		{
			Actor partner = base.Merchant.Partner;
			base.Merchant.Partner = null;
			if (partner != null && partner.Partner == base.Merchant)
			{
				partner.Partner = null;
			}
			if (this._changeToPartner && partner is AgentActor)
			{
				(partner as AgentActor).BehaviorResources.ChangeMode(Desire.ActionType.Normal);
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005B2B RID: 23339
		[SerializeField]
		private bool _changeToPartner;
	}
}

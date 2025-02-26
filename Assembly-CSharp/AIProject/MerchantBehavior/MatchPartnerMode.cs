using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD8 RID: 3544
	[TaskCategory("商人")]
	public class MatchPartnerMode : MerchantConditional
	{
		// Token: 0x06006DA1 RID: 28065 RVA: 0x002EAE1C File Offset: 0x002E921C
		public override TaskStatus OnUpdate()
		{
			Actor partner = base.Merchant.Partner;
			return (!(partner != null) || partner.Mode != this._mode) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005B48 RID: 23368
		[SerializeField]
		private Desire.ActionType _mode;
	}
}

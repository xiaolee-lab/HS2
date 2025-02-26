using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB1 RID: 3505
	[TaskCategory("商人")]
	public class ChangeMode : MerchantAction
	{
		// Token: 0x06006D31 RID: 27953 RVA: 0x002E8694 File Offset: 0x002E6A94
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ChangeBehavior(this._mode);
			return TaskStatus.Success;
		}

		// Token: 0x04005B2A RID: 23338
		[SerializeField]
		private Merchant.ActionType _mode = AIProject.Definitions.Merchant.ActionType.Wait;
	}
}

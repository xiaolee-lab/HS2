using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA5 RID: 3493
	[TaskCategory("商人")]
	public class IdleWhileFreeLink : MerchantAction
	{
		// Token: 0x06006D04 RID: 27908 RVA: 0x002E7E13 File Offset: 0x002E6213
		public override void OnStart()
		{
			base.OnStart();
			this._prevActiveEncount = base.Merchant.ActiveEncount;
			if (!this._prevActiveEncount)
			{
				base.Merchant.ActiveEncount = true;
			}
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x002E7E43 File Offset: 0x002E6243
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.IsInvalidMoveDestination()) ? TaskStatus.Success : TaskStatus.Running;
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x002E7E5C File Offset: 0x002E625C
		public override void OnEnd()
		{
			if (!this._prevActiveEncount)
			{
				base.Merchant.ActiveEncount = false;
			}
			base.OnEnd();
		}

		// Token: 0x04005B1E RID: 23326
		private bool _prevActiveEncount;
	}
}

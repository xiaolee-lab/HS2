using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB4 RID: 3508
	[TaskCategory("商人")]
	public class ChangePrevMode : MerchantAction
	{
		// Token: 0x06006D37 RID: 27959 RVA: 0x002E86F2 File Offset: 0x002E6AF2
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ChangeBehavior(base.Merchant.PrevMode);
			return TaskStatus.Success;
		}
	}
}

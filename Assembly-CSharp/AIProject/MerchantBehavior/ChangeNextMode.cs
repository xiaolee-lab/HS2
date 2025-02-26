using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB2 RID: 3506
	[TaskCategory("商人")]
	public class ChangeNextMode : MerchantAction
	{
		// Token: 0x06006D33 RID: 27955 RVA: 0x002E86B0 File Offset: 0x002E6AB0
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ChangeBehavior(base.Merchant.NextMode);
			return TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB3 RID: 3507
	[TaskCategory("商人")]
	public class ChangeNextSchedule : MerchantAction
	{
		// Token: 0x06006D35 RID: 27957 RVA: 0x002E86D1 File Offset: 0x002E6AD1
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ChangeNextSchedule();
			base.Merchant.SetCurrentSchedule();
			return TaskStatus.Success;
		}
	}
}

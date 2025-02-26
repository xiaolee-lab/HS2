using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB0 RID: 3504
	[TaskCategory("商人")]
	public class ChangeLastNormalMode : MerchantAction
	{
		// Token: 0x06006D2F RID: 27951 RVA: 0x002E866C File Offset: 0x002E6A6C
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ChangeBehavior(base.Merchant.LastNormalMode);
			return TaskStatus.Success;
		}
	}
}

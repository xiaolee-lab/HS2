using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBF RID: 3519
	[TaskCategory("商人")]
	public class PlayLocomotion : MerchantAction
	{
		// Token: 0x06006D5B RID: 27995 RVA: 0x002E8E32 File Offset: 0x002E7232
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ActivateLocomotionMotion();
			return TaskStatus.Success;
		}
	}
}

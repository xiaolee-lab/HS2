using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCA RID: 3530
	[TaskCategory("商人")]
	public class HasActionPoint : MerchantConditional
	{
		// Token: 0x06006D81 RID: 28033 RVA: 0x002EAA49 File Offset: 0x002E8E49
		public override TaskStatus OnUpdate()
		{
			return (!(base.Merchant.CurrentPoint != null)) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

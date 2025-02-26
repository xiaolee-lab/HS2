using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCC RID: 3532
	[TaskCategory("商人")]
	public class HasTargetMerchantPoint : MerchantConditional
	{
		// Token: 0x06006D85 RID: 28037 RVA: 0x002EAA92 File Offset: 0x002E8E92
		public override TaskStatus OnUpdate()
		{
			return (!(base.TargetInSightMerchantPoint != null)) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

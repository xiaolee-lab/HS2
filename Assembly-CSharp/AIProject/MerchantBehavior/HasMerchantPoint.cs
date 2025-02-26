using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCB RID: 3531
	[TaskCategory("商人")]
	public class HasMerchantPoint : MerchantConditional
	{
		// Token: 0x06006D83 RID: 28035 RVA: 0x002EAA70 File Offset: 0x002E8E70
		public override TaskStatus OnUpdate()
		{
			return (!(base.TargetInSightMerchantPoint != null)) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

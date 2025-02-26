using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD2 RID: 3538
	[TaskCategory("商人")]
	public class IsElapsedDay : MerchantConditional
	{
		// Token: 0x06006D92 RID: 28050 RVA: 0x002EAC06 File Offset: 0x002E9006
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.ElapsedDay) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

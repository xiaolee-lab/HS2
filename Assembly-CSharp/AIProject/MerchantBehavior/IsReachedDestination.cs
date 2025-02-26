using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD5 RID: 3541
	[TaskCategory("商人")]
	public class IsReachedDestination : MerchantConditional
	{
		// Token: 0x06006D9B RID: 28059 RVA: 0x002EADB6 File Offset: 0x002E91B6
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.IsArrived()) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

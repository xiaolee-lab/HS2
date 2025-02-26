using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD7 RID: 3543
	[TaskCategory("商人")]
	public class IsStandByMove : MerchantConditional
	{
		// Token: 0x06006D9F RID: 28063 RVA: 0x002EADF8 File Offset: 0x002E91F8
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.IsInvalidMoveDestination()) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCD RID: 3533
	[TaskCategory("商人")]
	public class IsActiveAgent : MerchantConditional
	{
		// Token: 0x06006D87 RID: 28039 RVA: 0x002EAAB4 File Offset: 0x002E8EB4
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.NavMeshAgent.enabled) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

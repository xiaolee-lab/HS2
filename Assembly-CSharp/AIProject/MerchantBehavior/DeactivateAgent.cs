using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB5 RID: 3509
	[TaskCategory("商人")]
	public class DeactivateAgent : MerchantAction
	{
		// Token: 0x06006D39 RID: 27961 RVA: 0x002E8713 File Offset: 0x002E6B13
		public override TaskStatus OnUpdate()
		{
			base.Merchant.DeactivateNavMeshAgent();
			return TaskStatus.Success;
		}
	}
}

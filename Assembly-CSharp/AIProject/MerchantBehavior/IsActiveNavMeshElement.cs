using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCE RID: 3534
	[TaskCategory("商人")]
	public class IsActiveNavMeshElement : MerchantConditional
	{
		// Token: 0x06006D89 RID: 28041 RVA: 0x002EAADA File Offset: 0x002E8EDA
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.NavMeshAgent.enabled && !base.Merchant.NavMeshObstacle.enabled) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

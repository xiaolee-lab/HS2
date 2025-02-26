using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DCF RID: 3535
	[TaskCategory("商人")]
	public class IsActiveObstacle : MerchantConditional
	{
		// Token: 0x06006D8B RID: 28043 RVA: 0x002EAB15 File Offset: 0x002E8F15
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.NavMeshObstacle.enabled) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB6 RID: 3510
	[TaskCategory("商人")]
	public class DeactivateNavMeshElement : MerchantAction
	{
		// Token: 0x06006D3B RID: 27963 RVA: 0x002E8729 File Offset: 0x002E6B29
		public override TaskStatus OnUpdate()
		{
			base.Merchant.DeactivateNavMeshElement();
			return TaskStatus.Success;
		}
	}
}

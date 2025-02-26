using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB7 RID: 3511
	[TaskCategory("商人")]
	public class DeactivateObstacle : MerchantAction
	{
		// Token: 0x06006D3D RID: 27965 RVA: 0x002E873F File Offset: 0x002E6B3F
		public override TaskStatus OnUpdate()
		{
			base.Merchant.DeactivateNavMeshObjstacle();
			return TaskStatus.Success;
		}
	}
}

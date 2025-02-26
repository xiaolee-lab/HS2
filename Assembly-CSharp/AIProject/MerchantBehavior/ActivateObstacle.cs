using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAD RID: 3501
	[TaskCategory("商人")]
	public class ActivateObstacle : MerchantAction
	{
		// Token: 0x06006D27 RID: 27943 RVA: 0x002E84DB File Offset: 0x002E68DB
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
			return TaskStatus.Success;
		}
	}
}

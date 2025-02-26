using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD6 RID: 3542
	[TaskCategory("商人")]
	public class IsReachedOffMeshLink : MerchantConditional
	{
		// Token: 0x06006D9D RID: 28061 RVA: 0x002EADD7 File Offset: 0x002E91D7
		public override TaskStatus OnUpdate()
		{
			return (!base.Merchant.IsArrivedToOffMesh()) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

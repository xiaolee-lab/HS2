using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAC RID: 3500
	[TaskCategory("商人")]
	public class ActivateAgent : MerchantAction
	{
		// Token: 0x06006D25 RID: 27941 RVA: 0x002E84C5 File Offset: 0x002E68C5
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ActivateNavMeshAgent();
			return TaskStatus.Success;
		}
	}
}

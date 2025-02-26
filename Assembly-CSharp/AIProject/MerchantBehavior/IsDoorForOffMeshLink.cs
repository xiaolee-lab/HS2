using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD1 RID: 3537
	[TaskCategory("商人")]
	public class IsDoorForOffMeshLink : MerchantConditional
	{
		// Token: 0x06006D90 RID: 28048 RVA: 0x002EABC0 File Offset: 0x002E8FC0
		public override TaskStatus OnUpdate()
		{
			DoorPoint component = base.Merchant.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<DoorPoint>();
			return (!(component != null)) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

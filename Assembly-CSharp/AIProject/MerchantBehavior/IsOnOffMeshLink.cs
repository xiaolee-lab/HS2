using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD4 RID: 3540
	[TaskCategory("商人")]
	public class IsOnOffMeshLink : MerchantConditional
	{
		// Token: 0x06006D98 RID: 28056 RVA: 0x002EAD54 File Offset: 0x002E9154
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Merchant.NavMeshAgent;
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x002EAD70 File Offset: 0x002E9170
		public override TaskStatus OnUpdate()
		{
			if (this._agent.isOnOffMeshLink && this._agent.currentOffMeshLinkData.offMeshLink != null)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005B47 RID: 23367
		private NavMeshAgent _agent;
	}
}

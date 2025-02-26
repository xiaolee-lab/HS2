using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA4 RID: 3492
	[TaskCategory("商人")]
	public class HWithPlayer : MerchantAction
	{
		// Token: 0x06006D00 RID: 27904 RVA: 0x002E7D9B File Offset: 0x002E619B
		public override void OnStart()
		{
			base.OnStart();
			base.Merchant.DeactivateNavMeshElement();
			base.Merchant.Partner = Map.GetPlayer();
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x002E7DBE File Offset: 0x002E61BE
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x002E7DC4 File Offset: 0x002E61C4
		public override void OnEnd()
		{
			PlayerActor player = Map.GetPlayer();
			if (player != null && base.Merchant.Partner == player)
			{
				base.Merchant.Partner = null;
			}
			base.OnEnd();
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA8 RID: 3496
	[TaskCategory("商人")]
	public class TalkWithPlayer : MerchantAction
	{
		// Token: 0x06006D0F RID: 27919 RVA: 0x002E80B3 File Offset: 0x002E64B3
		public override void OnStart()
		{
			base.OnStart();
			base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
			base.Merchant.Partner = Map.GetPlayer();
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x002E80E1 File Offset: 0x002E64E1
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x002E80E4 File Offset: 0x002E64E4
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

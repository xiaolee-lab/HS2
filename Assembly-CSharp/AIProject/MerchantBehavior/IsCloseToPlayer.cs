using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD0 RID: 3536
	[TaskCategory("商人")]
	public class IsCloseToPlayer : MerchantConditional
	{
		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06006D8D RID: 28045 RVA: 0x002EAB3B File Offset: 0x002E8F3B
		private PlayerActor Player
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			}
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x002EAB58 File Offset: 0x002E8F58
		public override TaskStatus OnUpdate()
		{
			PlayerActor player = this.Player;
			if (player == null)
			{
				return TaskStatus.Failure;
			}
			float num = Vector3.Distance(player.Position, base.Merchant.Position);
			float arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
			return (num > arrivedDistance) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

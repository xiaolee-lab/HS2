using System;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC2 RID: 3522
	[TaskCategory("商人")]
	public class SetAbsentPoint : MerchantSetPoint
	{
		// Token: 0x06006D61 RID: 28001 RVA: 0x002E8E94 File Offset: 0x002E7294
		protected override IEnumerator NextPointSettingCoroutine()
		{
			base.MainTargetInSightMerchantPoint = null;
			if (base.Merchant.ExitPoint == null)
			{
				yield break;
			}
			if (NavMesh.CalculatePath(base.Merchant.Position, base.Merchant.ExitPoint.Destination, base.Merchant.NavMeshAgent.areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
			{
				base.TargetInSightMerchantPoint = base.Merchant.ExitPoint;
				this.Success = true;
				yield break;
			}
			if (!(base.Merchant.StartPoint != null))
			{
				yield break;
			}
			if (!NavMesh.CalculatePath(base.Merchant.Position, base.Merchant.StartPoint.Destination, base.Merchant.NavMeshAgent.areaMask, this.navMeshPath))
			{
				yield break;
			}
			if (this.navMeshPath.status == NavMeshPathStatus.PathComplete)
			{
				base.TargetInSightMerchantPoint = base.Merchant.StartPoint;
				base.MainTargetInSightMerchantPoint = base.Merchant.ExitPoint;
				this.Success = true;
				yield break;
			}
			yield break;
		}
	}
}

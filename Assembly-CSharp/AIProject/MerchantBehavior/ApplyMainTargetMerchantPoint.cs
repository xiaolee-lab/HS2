using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAF RID: 3503
	[TaskCategory("商人")]
	public class ApplyMainTargetMerchantPoint : MerchantAction
	{
		// Token: 0x06006D2D RID: 27949 RVA: 0x002E85F4 File Offset: 0x002E69F4
		public override TaskStatus OnUpdate()
		{
			if (base.MainTargetInSightMerchantPoint != null)
			{
				base.TargetInSightMerchantPoint = base.MainTargetInSightMerchantPoint;
				base.MainTargetInSightMerchantPoint = null;
				MerchantData merchantData = base.Merchant.MerchantData;
				merchantData.PointPosition = base.TargetInSightMerchantPoint.transform.position;
				merchantData.MainPointPosition = new Vector3(-999f, -999f, -999f);
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC3 RID: 3523
	[TaskCategory("商人")]
	public class SetElapsedDay : MerchantAction
	{
		// Token: 0x06006D63 RID: 28003 RVA: 0x002E9076 File Offset: 0x002E7476
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ElapsedDay = this._setValue;
			return TaskStatus.Success;
		}

		// Token: 0x04005B3C RID: 23356
		[SerializeField]
		private bool _setValue;
	}
}

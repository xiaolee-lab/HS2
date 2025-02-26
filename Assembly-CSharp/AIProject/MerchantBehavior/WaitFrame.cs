using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAA RID: 3498
	[TaskCategory("商人")]
	public class WaitFrame : MerchantAction
	{
		// Token: 0x06006D16 RID: 27926 RVA: 0x002E814C File Offset: 0x002E654C
		public override void OnStart()
		{
			base.OnStart();
			this._updateCount = 0;
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x002E815B File Offset: 0x002E655B
		public override TaskStatus OnUpdate()
		{
			this._updateCount++;
			if (this._waitCount <= this._updateCount)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04005B22 RID: 23330
		[SerializeField]
		private int _waitCount;

		// Token: 0x04005B23 RID: 23331
		private int _updateCount;
	}
}

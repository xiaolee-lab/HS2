using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA9 RID: 3497
	[TaskCategory("商人")]
	public class Wait : MerchantStateAction
	{
		// Token: 0x06006D13 RID: 27923 RVA: 0x002E8133 File Offset: 0x002E6533
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x002E8136 File Offset: 0x002E6536
		public override void OnEnd()
		{
			base.Complete();
			base.OnEnd();
		}
	}
}

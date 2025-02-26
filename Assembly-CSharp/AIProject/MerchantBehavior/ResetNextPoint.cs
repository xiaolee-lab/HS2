using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC1 RID: 3521
	[TaskCategory("商人")]
	public class ResetNextPoint : MerchantAction
	{
		// Token: 0x06006D5F RID: 27999 RVA: 0x002E8E89 File Offset: 0x002E7289
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}

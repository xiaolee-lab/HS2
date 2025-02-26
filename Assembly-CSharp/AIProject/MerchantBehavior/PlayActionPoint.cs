using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBE RID: 3518
	[TaskCategory("商人")]
	public class PlayActionPoint : MerchantAction
	{
		// Token: 0x06006D59 RID: 27993 RVA: 0x002E8E27 File Offset: 0x002E7227
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Failure;
		}
	}
}

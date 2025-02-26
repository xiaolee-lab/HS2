using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC0 RID: 3520
	[TaskCategory("商人")]
	public class RemovePointBooking : MerchantAction
	{
		// Token: 0x06006D5D RID: 27997 RVA: 0x002E8E48 File Offset: 0x002E7248
		public override TaskStatus OnUpdate()
		{
			MerchantActor merchant = base.Merchant;
			ActionPoint bookingActionPoint = merchant.BookingActionPoint;
			if (bookingActionPoint == null)
			{
				return TaskStatus.Failure;
			}
			bookingActionPoint.RemoveBooking(merchant);
			merchant.BookingActionPoint = null;
			return TaskStatus.Success;
		}
	}
}

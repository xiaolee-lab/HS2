using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DAE RID: 3502
	[TaskCategory("商人")]
	public class AddPointBooking : MerchantAction
	{
		// Token: 0x06006D29 RID: 27945 RVA: 0x002E84FC File Offset: 0x002E68FC
		public override void OnStart()
		{
			base.OnStart();
			this._merchant = base.Merchant;
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x002E8510 File Offset: 0x002E6910
		public override TaskStatus OnUpdate()
		{
			NavMeshAgent navMeshAgent = this._merchant.NavMeshAgent;
			ActionPoint bookingPoint;
			if (navMeshAgent == null)
			{
				bookingPoint = null;
			}
			else
			{
				OffMeshLink offMeshLink = navMeshAgent.currentOffMeshLinkData.offMeshLink;
				bookingPoint = ((offMeshLink != null) ? offMeshLink.GetComponent<ActionPoint>() : null);
			}
			this._bookingPoint = bookingPoint;
			if (this._bookingPoint == null)
			{
				return TaskStatus.Failure;
			}
			this._bookingPoint.AddBooking(this._merchant);
			this._merchant.BookingActionPoint = this._bookingPoint;
			return TaskStatus.Success;
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x002E858C File Offset: 0x002E698C
		public override void OnBehaviorComplete()
		{
			if (this._bookingPoint == null)
			{
				return;
			}
			this._bookingPoint.RemoveBooking(this._merchant);
			if (this._merchant.BookingActionPoint == this._bookingPoint)
			{
				this._merchant.BookingActionPoint = null;
			}
			this._bookingPoint = null;
		}

		// Token: 0x04005B28 RID: 23336
		private ActionPoint _bookingPoint;

		// Token: 0x04005B29 RID: 23337
		private MerchantActor _merchant;
	}
}

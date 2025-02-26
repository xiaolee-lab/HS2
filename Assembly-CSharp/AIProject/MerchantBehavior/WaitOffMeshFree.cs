using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC8 RID: 3528
	public class WaitOffMeshFree : MerchantAction
	{
		// Token: 0x06006D79 RID: 28025 RVA: 0x002EA8AC File Offset: 0x002E8CAC
		public override void OnStart()
		{
			base.OnStart();
			this._navMeshStopped = base.Merchant.NavMeshAgent.isStopped;
			if (!this._navMeshStopped)
			{
				base.Merchant.NavMeshAgent.isStopped = true;
			}
			this._bookingPoint = base.Merchant.BookingActionPoint;
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x002EA904 File Offset: 0x002E8D04
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.Update();
			if (taskStatus == TaskStatus.Success)
			{
				this._bookingPoint.ForceUse(base.Merchant);
			}
			return taskStatus;
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x002EA932 File Offset: 0x002E8D32
		private TaskStatus Update()
		{
			if (this._bookingPoint == null)
			{
				return TaskStatus.Failure;
			}
			return (!this._bookingPoint.OffMeshAvailablePoint(base.Merchant)) ? TaskStatus.Running : TaskStatus.Success;
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x002EA964 File Offset: 0x002E8D64
		public override void OnEnd()
		{
			if (!this._navMeshStopped)
			{
				base.Merchant.NavMeshAgent.isStopped = false;
			}
			base.OnEnd();
		}

		// Token: 0x04005B40 RID: 23360
		private bool _navMeshStopped;

		// Token: 0x04005B41 RID: 23361
		private ActionPoint _bookingPoint;
	}
}

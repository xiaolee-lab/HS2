using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D2B RID: 3371
	public class WaitOffMeshFree : AgentAction
	{
		// Token: 0x06006B9D RID: 27549 RVA: 0x002E2078 File Offset: 0x002E0478
		public override void OnStart()
		{
			base.OnStart();
			this._navMeshStopped = base.Agent.NavMeshAgent.isStopped;
			if (!this._navMeshStopped)
			{
				base.Agent.NavMeshAgent.isStopped = true;
			}
			this._bookingPoint = base.Agent.BookingActionPoint;
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x002E20D0 File Offset: 0x002E04D0
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.Update();
			if (taskStatus == TaskStatus.Success)
			{
				this._bookingPoint.ForceUse(base.Agent);
			}
			return taskStatus;
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x002E20FE File Offset: 0x002E04FE
		private TaskStatus Update()
		{
			if (this._bookingPoint == null)
			{
				return TaskStatus.Failure;
			}
			return (!this._bookingPoint.OffMeshAvailablePoint(base.Agent)) ? TaskStatus.Running : TaskStatus.Success;
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x002E2130 File Offset: 0x002E0530
		public override void OnEnd()
		{
			if (!this._navMeshStopped)
			{
				base.Agent.NavMeshAgent.isStopped = false;
			}
			base.OnEnd();
		}

		// Token: 0x04005A9A RID: 23194
		private bool _navMeshStopped;

		// Token: 0x04005A9B RID: 23195
		private ActionPoint _bookingPoint;
	}
}

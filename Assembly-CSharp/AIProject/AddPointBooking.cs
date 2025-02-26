using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C7F RID: 3199
	[TaskCategory("")]
	public class AddPointBooking : AgentAction
	{
		// Token: 0x060068DA RID: 26842 RVA: 0x002CA032 File Offset: 0x002C8432
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x002CA048 File Offset: 0x002C8448
		public override TaskStatus OnUpdate()
		{
			NavMeshAgent navMeshAgent = this._agent.NavMeshAgent;
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
			this._bookingPoint.AddBooking(this._agent);
			this._agent.BookingActionPoint = this._bookingPoint;
			return TaskStatus.Success;
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x002CA0C4 File Offset: 0x002C84C4
		public override void OnBehaviorComplete()
		{
			if (this._bookingPoint == null)
			{
				return;
			}
			this._bookingPoint.RemoveBooking(this._agent);
			if (base.Agent.BookingActionPoint == this._bookingPoint)
			{
				base.Agent.BookingActionPoint = null;
			}
			this._bookingPoint = null;
		}

		// Token: 0x04005971 RID: 22897
		private ActionPoint _bookingPoint;

		// Token: 0x04005972 RID: 22898
		private AgentActor _agent;
	}
}

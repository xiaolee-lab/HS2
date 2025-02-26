using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D09 RID: 3337
	[TaskCategory("")]
	public class RemovePointBooking : AgentAction
	{
		// Token: 0x06006B13 RID: 27411 RVA: 0x002DBFF0 File Offset: 0x002DA3F0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			ActionPoint bookingActionPoint = agent.BookingActionPoint;
			if (bookingActionPoint == null)
			{
				return TaskStatus.Failure;
			}
			bookingActionPoint.RemoveBooking(agent);
			agent.BookingActionPoint = null;
			return TaskStatus.Success;
		}
	}
}

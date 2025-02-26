using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CFD RID: 3325
	[TaskCategory("")]
	public class GyakuYobaiEvent : AgentAction
	{
		// Token: 0x06006AEE RID: 27374 RVA: 0x002DB420 File Offset: 0x002D9820
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			PlayerActor player = Singleton<Map>.Instance.Player;
			if (player == null)
			{
				return TaskStatus.Failure;
			}
			player.CurrentPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
			player.StartGyakuYobaiEvent(agent);
			return TaskStatus.Success;
		}
	}
}

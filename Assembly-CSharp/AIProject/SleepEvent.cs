using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CFF RID: 3327
	[TaskCategory("")]
	public class SleepEvent : AgentAction
	{
		// Token: 0x06006AF2 RID: 27378 RVA: 0x002DB4C0 File Offset: 0x002D98C0
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
			player.StartSleepTogetherEvent(agent);
			return TaskStatus.Success;
		}
	}
}

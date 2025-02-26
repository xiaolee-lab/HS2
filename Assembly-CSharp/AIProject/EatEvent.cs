using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CFC RID: 3324
	[TaskCategory("")]
	public class EatEvent : AgentAction
	{
		// Token: 0x06006AEC RID: 27372 RVA: 0x002DB3D0 File Offset: 0x002D97D0
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
			player.StartEatEvent(agent);
			return TaskStatus.Success;
		}
	}
}

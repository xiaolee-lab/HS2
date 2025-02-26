using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CFE RID: 3326
	[TaskCategory("")]
	public class HizamakuraEvent : AgentAction
	{
		// Token: 0x06006AF0 RID: 27376 RVA: 0x002DB470 File Offset: 0x002D9870
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
			player.StartHizamakuraEvent(agent);
			return TaskStatus.Success;
		}
	}
}

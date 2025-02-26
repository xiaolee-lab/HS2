using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D56 RID: 3414
	[TaskCategory("")]
	public class IsEmptyMotivation : AgentConditional
	{
		// Token: 0x06006C11 RID: 27665 RVA: 0x002E5714 File Offset: 0x002E3B14
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(agent.RequestedDesire);
			float? motivation = agent.GetMotivation(desireKey);
			if (motivation != null && motivation.Value <= 0f)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

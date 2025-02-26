using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D5B RID: 3419
	[TaskCategory("")]
	public class IsGroper : AgentConditional
	{
		// Token: 0x06006C1B RID: 27675 RVA: 0x002E588A File Offset: 0x002E3C8A
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.ChaControl.fileGameInfo.hSkill.ContainsValue(13))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

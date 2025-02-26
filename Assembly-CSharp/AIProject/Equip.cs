using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CBF RID: 3263
	[TaskCategory("")]
	public class Equip : AgentAction
	{
		// Token: 0x060069CE RID: 27086 RVA: 0x002D0E6B File Offset: 0x002CF26B
		public override TaskStatus OnUpdate()
		{
			base.Agent.Animation.Poser.enabled = true;
			base.Agent.Animation.ArmAnimator.enabled = true;
			return TaskStatus.Success;
		}
	}
}

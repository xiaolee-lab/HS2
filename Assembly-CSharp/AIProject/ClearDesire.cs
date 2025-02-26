using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C8E RID: 3214
	[TaskCategory("")]
	public class ClearDesire : AgentAction
	{
		// Token: 0x060068FB RID: 26875 RVA: 0x002CA508 File Offset: 0x002C8908
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(base.Agent.RuntimeDesire);
			if (desireKey != -1)
			{
				base.Agent.SetDesire(desireKey, 0f);
			}
			return TaskStatus.Success;
		}
	}
}

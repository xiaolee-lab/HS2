using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C93 RID: 3219
	[TaskCategory("")]
	public class EmptyRequiredDesire : AgentAction
	{
		// Token: 0x0600690A RID: 26890 RVA: 0x002CA814 File Offset: 0x002C8C14
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(base.Agent.RequestedDesire);
			base.Agent.SetDesire(desireKey, 0f);
			return TaskStatus.Success;
		}
	}
}

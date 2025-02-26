using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D70 RID: 3440
	[TaskCategory("")]
	public class IsTargetNeutralCommand : AgentConditional
	{
		// Token: 0x06006C46 RID: 27718 RVA: 0x002E5F98 File Offset: 0x002E4398
		public override TaskStatus OnUpdate()
		{
			ICommandable commandable = base.Agent.TargetInSightActor as ICommandable;
			if (commandable == null)
			{
				return TaskStatus.Failure;
			}
			return (!commandable.IsNeutralCommand) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

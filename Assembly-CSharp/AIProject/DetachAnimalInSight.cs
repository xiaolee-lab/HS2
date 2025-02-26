using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C92 RID: 3218
	[TaskCategory("")]
	public class DetachAnimalInSight : AgentAction
	{
		// Token: 0x06006908 RID: 26888 RVA: 0x002CA7FD File Offset: 0x002C8BFD
		public override TaskStatus OnUpdate()
		{
			base.Agent.TargetInSightAnimal = null;
			return TaskStatus.Success;
		}
	}
}

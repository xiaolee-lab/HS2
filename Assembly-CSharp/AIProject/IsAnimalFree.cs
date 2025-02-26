using System;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D4C RID: 3404
	[TaskCategory("")]
	public class IsAnimalFree : AgentConditional
	{
		// Token: 0x06006BFA RID: 27642 RVA: 0x002E5278 File Offset: 0x002E3678
		public override TaskStatus OnUpdate()
		{
			AnimalBase targetInSightAnimal = base.Agent.TargetInSightAnimal;
			bool? flag = (targetInSightAnimal != null) ? new bool?(targetInSightAnimal.IsWithAgentFree(base.Agent)) : null;
			return (flag == null || !flag.Value) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}

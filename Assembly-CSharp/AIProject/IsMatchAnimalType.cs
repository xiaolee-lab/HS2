using System;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D5F RID: 3423
	[TaskCategory("")]
	public class IsMatchAnimalType : AgentConditional
	{
		// Token: 0x06006C23 RID: 27683 RVA: 0x002E59A4 File Offset: 0x002E3DA4
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.TargetInSightAnimal == null)
			{
				return TaskStatus.Failure;
			}
			return (base.Agent.TargetInSightAnimal.AnimalType != this.targetAnimalType) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005AD6 RID: 23254
		[SerializeField]
		private AnimalTypes targetAnimalType;
	}
}

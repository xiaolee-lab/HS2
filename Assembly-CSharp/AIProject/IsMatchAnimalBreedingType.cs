using System;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D5E RID: 3422
	[TaskCategory("")]
	public class IsMatchAnimalBreedingType : AgentConditional
	{
		// Token: 0x06006C21 RID: 27681 RVA: 0x002E5960 File Offset: 0x002E3D60
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.TargetInSightAnimal == null)
			{
				return TaskStatus.Failure;
			}
			return (base.Agent.TargetInSightAnimal.BreedingType != this.targetBreedingType) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005AD5 RID: 23253
		[SerializeField]
		private BreedingTypes targetBreedingType;
	}
}

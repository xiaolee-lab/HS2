using System;
using System.Collections.Generic;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D91 RID: 3473
	[TaskCategory("")]
	public class StayAnimalInSight : AgentConditional
	{
		// Token: 0x06006C88 RID: 27784 RVA: 0x002E6693 File Offset: 0x002E4A93
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.TargetInSightAnimal = null;
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x002E66A8 File Offset: 0x002E4AA8
		public override TaskStatus OnUpdate()
		{
			this.CheckAnimal();
			AnimalBase targetInSightAnimal = base.Agent.TargetInSightAnimal;
			if (targetInSightAnimal == null)
			{
				return TaskStatus.Failure;
			}
			if ((targetInSightAnimal.AnimalType & this.targetAnimalType) == (AnimalTypes)0)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x002E66EC File Offset: 0x002E4AEC
		private void CheckAnimal()
		{
			List<AnimalBase> list = ListPool<AnimalBase>.Get();
			foreach (AnimalBase animalBase in base.Agent.TargetAnimals)
			{
				if ((animalBase.AnimalType & this.targetAnimalType) != (AnimalTypes)0)
				{
					CollisionState collisionState;
					if (base.Agent.AnimalCollisionStateTable.TryGetValue(animalBase.InstanceID, out collisionState) && animalBase.IsWithAgentFree(base.Agent) && (collisionState == CollisionState.Enter || collisionState == CollisionState.Stay))
					{
						list.Add(animalBase);
					}
				}
			}
			if (0 < list.Count)
			{
				AnimalBase capturedInSight = this.GetCapturedInSight(base.Agent, list);
				if (capturedInSight == null)
				{
					ListPool<AnimalBase>.Release(list);
					return;
				}
				base.Agent.TargetInSightAnimal = capturedInSight;
			}
			ListPool<AnimalBase>.Release(list);
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x002E67F0 File Offset: 0x002E4BF0
		private AnimalBase GetCapturedInSight(AgentActor _agent, List<AnimalBase> _animals)
		{
			AnimalBase element = _animals.GetElement(UnityEngine.Random.Range(0, _animals.Count));
			if (element == null)
			{
				return null;
			}
			return (!this.IsCaptureInSight(_agent, element)) ? null : element;
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x002E6834 File Offset: 0x002E4C34
		private bool IsCaptureInSight(AgentActor _agent, AnimalBase _target)
		{
			if (_target == null)
			{
				return false;
			}
			Vector3 position = _agent.FovTargetPointTable[Actor.FovBodyPart.Head].position;
			int num = LayerMask.NameToLayer("Map");
			Vector3 position2 = _target.Position;
			Vector3 direction = position - position2;
			Ray ray = new Ray(position, direction);
			return !Physics.Raycast(ray, direction.magnitude, 1 << num);
		}

		// Token: 0x04005AEA RID: 23274
		[SerializeField]
		private AnimalTypes targetAnimalType;
	}
}

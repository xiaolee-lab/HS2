using System;
using System.Collections.Generic;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D34 RID: 3380
	[TaskCategory("")]
	public class EnterAnimalInSight : AgentConditional
	{
		// Token: 0x06006BBC RID: 27580 RVA: 0x002E3AB8 File Offset: 0x002E1EB8
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.TargetInSightAnimal = null;
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x002E3ACC File Offset: 0x002E1ECC
		public override TaskStatus OnUpdate()
		{
			this.CheckAnimal();
			AnimalBase targetInSightAnimal = base.Agent.TargetInSightAnimal;
			return (!(targetInSightAnimal != null)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x002E3B00 File Offset: 0x002E1F00
		private void CheckAnimal()
		{
			List<AnimalBase> list = ListPool<AnimalBase>.Get();
			foreach (AnimalBase animalBase in base.Agent.TargetAnimals)
			{
				if ((animalBase.AnimalType & this.targetAnimalType) != (AnimalTypes)0)
				{
					CollisionState collisionState;
					if (base.Agent.AnimalCollisionStateTable.TryGetValue(animalBase.InstanceID, out collisionState) && animalBase.IsWithAgentFree(base.Agent) && collisionState == CollisionState.Enter)
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

		// Token: 0x06006BBF RID: 27583 RVA: 0x002E3BFC File Offset: 0x002E1FFC
		private AnimalBase GetCapturedInSight(AgentActor _agent, List<AnimalBase> _animals)
		{
			AnimalBase element = _animals.GetElement(UnityEngine.Random.Range(0, _animals.Count));
			if (element == null)
			{
				return null;
			}
			return (!this.IsCaptureInSight(_agent, element)) ? null : element;
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x002E3C40 File Offset: 0x002E2040
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

		// Token: 0x04005AB0 RID: 23216
		[SerializeField]
		private AnimalTypes targetAnimalType;
	}
}

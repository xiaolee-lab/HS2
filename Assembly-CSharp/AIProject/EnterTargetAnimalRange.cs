using System;
using AIProject.Animal;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D35 RID: 3381
	[TaskCategory("")]
	public class EnterTargetAnimalRange : AgentConditional
	{
		// Token: 0x06006BC2 RID: 27586 RVA: 0x002E3CB0 File Offset: 0x002E20B0
		public override void OnStart()
		{
			base.OnStart();
			this.arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x002E3CE0 File Offset: 0x002E20E0
		public override TaskStatus OnUpdate()
		{
			AnimalBase targetInSightAnimal = base.Agent.TargetInSightAnimal;
			return (!(targetInSightAnimal != null) || Vector3.Distance(base.Agent.Position, targetInSightAnimal.Position) > this.arrivedDistance) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005AB1 RID: 23217
		private float arrivedDistance;
	}
}

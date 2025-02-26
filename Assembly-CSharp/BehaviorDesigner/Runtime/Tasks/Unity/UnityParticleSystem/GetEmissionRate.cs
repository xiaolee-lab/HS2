using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CF RID: 463
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the emission rate of the Particle System.")]
	public class GetEmissionRate : Action
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00026DB0 File Offset: 0x000251B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00026DF3 File Offset: 0x000251F3
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00026E09 File Offset: 0x00025209
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x040007A0 RID: 1952
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007A1 RID: 1953
		[Tooltip("The emission rate of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x040007A2 RID: 1954
		private ParticleSystem particleSystem;

		// Token: 0x040007A3 RID: 1955
		private GameObject prevGameObject;
	}
}

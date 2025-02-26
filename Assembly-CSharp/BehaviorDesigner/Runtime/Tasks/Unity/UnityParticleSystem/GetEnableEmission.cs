using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D0 RID: 464
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores if the Particle System is emitting particles.")]
	public class GetEnableEmission : Action
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00026E2C File Offset: 0x0002522C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00026E70 File Offset: 0x00025270
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.emission.enabled;
			return TaskStatus.Success;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00026EAF File Offset: 0x000252AF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x040007A4 RID: 1956
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007A5 RID: 1957
		[Tooltip("Is the Particle System emitting particles?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x040007A6 RID: 1958
		private ParticleSystem particleSystem;

		// Token: 0x040007A7 RID: 1959
		private GameObject prevGameObject;
	}
}

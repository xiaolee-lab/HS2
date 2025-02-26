using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D3 RID: 467
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the particle count of the Particle System.")]
	public class GetParticleCount : Action
	{
		// Token: 0x060008BC RID: 2236 RVA: 0x00027014 File Offset: 0x00025414
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00027057 File Offset: 0x00025457
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = (float)this.particleSystem.particleCount;
			return TaskStatus.Success;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00027084 File Offset: 0x00025484
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x040007B0 RID: 1968
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007B1 RID: 1969
		[Tooltip("The particle count of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x040007B2 RID: 1970
		private ParticleSystem particleSystem;

		// Token: 0x040007B3 RID: 1971
		private GameObject prevGameObject;
	}
}

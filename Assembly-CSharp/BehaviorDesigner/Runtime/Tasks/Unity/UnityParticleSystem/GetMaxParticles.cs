using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D2 RID: 466
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the max particles of the Particle System.")]
	public class GetMaxParticles : Action
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x00026F6C File Offset: 0x0002536C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00026FB0 File Offset: 0x000253B0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = (float)this.particleSystem.main.maxParticles;
			return TaskStatus.Success;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00026FF0 File Offset: 0x000253F0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x040007AC RID: 1964
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007AD RID: 1965
		[Tooltip("The max particles of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x040007AE RID: 1966
		private ParticleSystem particleSystem;

		// Token: 0x040007AF RID: 1967
		private GameObject prevGameObject;
	}
}

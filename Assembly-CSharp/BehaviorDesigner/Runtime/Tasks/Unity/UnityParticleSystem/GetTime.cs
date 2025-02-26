using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D5 RID: 469
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the time of the Particle System.")]
	public class GetTime : Action
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0002714C File Offset: 0x0002554C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002718F File Offset: 0x0002558F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.time;
			return TaskStatus.Success;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000271BB File Offset: 0x000255BB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x040007B8 RID: 1976
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007B9 RID: 1977
		[Tooltip("The time of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x040007BA RID: 1978
		private ParticleSystem particleSystem;

		// Token: 0x040007BB RID: 1979
		private GameObject prevGameObject;
	}
}

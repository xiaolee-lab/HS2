using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E8 RID: 488
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Simulate the Particle System.")]
	public class Simulate : Action
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x00027C74 File Offset: 0x00026074
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00027CB7 File Offset: 0x000260B7
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.Simulate(this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00027CE3 File Offset: 0x000260E3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040007FE RID: 2046
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007FF RID: 2047
		[Tooltip("Time to fastfoward the Particle System to")]
		public SharedFloat time;

		// Token: 0x04000800 RID: 2048
		private ParticleSystem particleSystem;

		// Token: 0x04000801 RID: 2049
		private GameObject prevGameObject;
	}
}

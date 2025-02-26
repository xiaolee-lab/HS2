using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DF RID: 479
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the max particles of the Particle System.")]
	public class SetMaxParticles : Action
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00027688 File Offset: 0x00025A88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000276CC File Offset: 0x00025ACC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.maxParticles = this.maxParticles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002770B File Offset: 0x00025B0B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxParticles = 0;
		}

		// Token: 0x040007DA RID: 2010
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007DB RID: 2011
		[Tooltip("The max particles of the ParticleSystem")]
		public SharedInt maxParticles;

		// Token: 0x040007DC RID: 2012
		private ParticleSystem particleSystem;

		// Token: 0x040007DD RID: 2013
		private GameObject prevGameObject;
	}
}

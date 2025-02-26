using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E3 RID: 483
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start lifetime of the Particle System.")]
	public class SetStartLifetime : Action
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00027934 File Offset: 0x00025D34
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00027978 File Offset: 0x00025D78
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startLifetime = this.startLifetime.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000279BC File Offset: 0x00025DBC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startLifetime = 0f;
		}

		// Token: 0x040007EA RID: 2026
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007EB RID: 2027
		[Tooltip("The start lifetime of the ParticleSystem")]
		public SharedFloat startLifetime;

		// Token: 0x040007EC RID: 2028
		private ParticleSystem particleSystem;

		// Token: 0x040007ED RID: 2029
		private GameObject prevGameObject;
	}
}

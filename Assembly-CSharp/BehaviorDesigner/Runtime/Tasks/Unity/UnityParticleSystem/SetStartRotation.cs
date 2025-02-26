using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E4 RID: 484
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start rotation of the Particle System.")]
	public class SetStartRotation : Action
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x000279E0 File Offset: 0x00025DE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00027A24 File Offset: 0x00025E24
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startRotation = this.startRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00027A68 File Offset: 0x00025E68
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startRotation = 0f;
		}

		// Token: 0x040007EE RID: 2030
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007EF RID: 2031
		[Tooltip("The start rotation of the ParticleSystem")]
		public SharedFloat startRotation;

		// Token: 0x040007F0 RID: 2032
		private ParticleSystem particleSystem;

		// Token: 0x040007F1 RID: 2033
		private GameObject prevGameObject;
	}
}

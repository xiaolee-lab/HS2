using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DC RID: 476
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the emission rate of the Particle System.")]
	public class SetEmissionRate : Action
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x000274CC File Offset: 0x000258CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002750F File Offset: 0x0002590F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00027525 File Offset: 0x00025925
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.emissionRate = 0f;
		}

		// Token: 0x040007CE RID: 1998
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007CF RID: 1999
		[Tooltip("The emission rate of the ParticleSystem")]
		public SharedFloat emissionRate;

		// Token: 0x040007D0 RID: 2000
		private ParticleSystem particleSystem;

		// Token: 0x040007D1 RID: 2001
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DD RID: 477
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Enables or disables the Particle System emission.")]
	public class SetEnableEmission : Action
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x00027548 File Offset: 0x00025948
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002758C File Offset: 0x0002598C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.emission.enabled = this.enable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000275CB File Offset: 0x000259CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.enable = false;
		}

		// Token: 0x040007D2 RID: 2002
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007D3 RID: 2003
		[Tooltip("Enable the ParticleSystem emissions?")]
		public SharedBool enable;

		// Token: 0x040007D4 RID: 2004
		private ParticleSystem particleSystem;

		// Token: 0x040007D5 RID: 2005
		private GameObject prevGameObject;
	}
}

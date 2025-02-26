using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D6 RID: 470
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System alive?")]
	public class IsAlive : Conditional
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x000271DC File Offset: 0x000255DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002721F File Offset: 0x0002561F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.particleSystem.IsAlive()) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002724B File Offset: 0x0002564B
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007BC RID: 1980
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007BD RID: 1981
		private ParticleSystem particleSystem;

		// Token: 0x040007BE RID: 1982
		private GameObject prevGameObject;
	}
}

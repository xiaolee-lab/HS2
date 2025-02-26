using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DA RID: 474
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Pause the Particle System.")]
	public class Pause : Action
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x000273DC File Offset: 0x000257DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002741F File Offset: 0x0002581F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00027440 File Offset: 0x00025840
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007C8 RID: 1992
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C9 RID: 1993
		private ParticleSystem particleSystem;

		// Token: 0x040007CA RID: 1994
		private GameObject prevGameObject;
	}
}

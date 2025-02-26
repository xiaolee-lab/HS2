using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D9 RID: 473
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0002735C File Offset: 0x0002575C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002739F File Offset: 0x0002579F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.particleSystem.isStopped) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000273CB File Offset: 0x000257CB
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007C5 RID: 1989
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C6 RID: 1990
		private ParticleSystem particleSystem;

		// Token: 0x040007C7 RID: 1991
		private GameObject prevGameObject;
	}
}

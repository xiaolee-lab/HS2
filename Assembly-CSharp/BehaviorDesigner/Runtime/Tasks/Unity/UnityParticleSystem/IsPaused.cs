using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D7 RID: 471
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x0002725C File Offset: 0x0002565C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002729F File Offset: 0x0002569F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.particleSystem.isPaused) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000272CB File Offset: 0x000256CB
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007BF RID: 1983
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C0 RID: 1984
		private ParticleSystem particleSystem;

		// Token: 0x040007C1 RID: 1985
		private GameObject prevGameObject;
	}
}

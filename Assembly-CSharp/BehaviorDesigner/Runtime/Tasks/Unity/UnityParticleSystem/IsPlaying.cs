using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D8 RID: 472
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Is the Particle System playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x000272DC File Offset: 0x000256DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002731F File Offset: 0x0002571F
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.particleSystem.isPlaying) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002734B File Offset: 0x0002574B
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007C2 RID: 1986
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C3 RID: 1987
		private ParticleSystem particleSystem;

		// Token: 0x040007C4 RID: 1988
		private GameObject prevGameObject;
	}
}

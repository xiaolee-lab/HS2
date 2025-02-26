using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DB RID: 475
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Play the Particle System.")]
	public class Play : Action
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x00027454 File Offset: 0x00025854
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00027497 File Offset: 0x00025897
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.Play();
			return TaskStatus.Success;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000274B8 File Offset: 0x000258B8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007CB RID: 1995
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007CC RID: 1996
		private ParticleSystem particleSystem;

		// Token: 0x040007CD RID: 1997
		private GameObject prevGameObject;
	}
}

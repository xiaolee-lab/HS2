using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001DE RID: 478
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets if the Particle System should loop.")]
	public class SetLoop : Action
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x000275E8 File Offset: 0x000259E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002762C File Offset: 0x00025A2C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.loop = this.loop.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002766B File Offset: 0x00025A6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x040007D6 RID: 2006
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007D7 RID: 2007
		[Tooltip("Should the ParticleSystem loop?")]
		public SharedBool loop;

		// Token: 0x040007D8 RID: 2008
		private ParticleSystem particleSystem;

		// Token: 0x040007D9 RID: 2009
		private GameObject prevGameObject;
	}
}

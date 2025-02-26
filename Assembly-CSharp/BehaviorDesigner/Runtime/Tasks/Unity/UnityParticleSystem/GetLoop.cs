using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D1 RID: 465
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores if the Particle System should loop.")]
	public class GetLoop : Action
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00026ECC File Offset: 0x000252CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00026F10 File Offset: 0x00025310
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.main.loop;
			return TaskStatus.Success;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00026F4F File Offset: 0x0002534F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = false;
		}

		// Token: 0x040007A8 RID: 1960
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007A9 RID: 1961
		[Tooltip("Should the ParticleSystem loop?")]
		[RequiredField]
		public SharedBool storeResult;

		// Token: 0x040007AA RID: 1962
		private ParticleSystem particleSystem;

		// Token: 0x040007AB RID: 1963
		private GameObject prevGameObject;
	}
}

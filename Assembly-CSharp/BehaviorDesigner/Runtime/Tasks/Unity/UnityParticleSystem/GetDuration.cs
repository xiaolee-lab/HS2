using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CE RID: 462
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the duration of the Particle System.")]
	public class GetDuration : Action
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x00026D0C File Offset: 0x0002510C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00026D50 File Offset: 0x00025150
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.storeResult.Value = this.particleSystem.main.duration;
			return TaskStatus.Success;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00026D8F File Offset: 0x0002518F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x0400079C RID: 1948
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400079D RID: 1949
		[Tooltip("The duration of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x0400079E RID: 1950
		private ParticleSystem particleSystem;

		// Token: 0x0400079F RID: 1951
		private GameObject prevGameObject;
	}
}

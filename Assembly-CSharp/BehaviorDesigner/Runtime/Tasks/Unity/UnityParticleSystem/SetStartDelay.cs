using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E2 RID: 482
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start delay of the Particle System.")]
	public class SetStartDelay : Action
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x00027888 File Offset: 0x00025C88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000278CC File Offset: 0x00025CCC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startDelay = this.startDelay.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00027910 File Offset: 0x00025D10
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startDelay = 0f;
		}

		// Token: 0x040007E6 RID: 2022
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007E7 RID: 2023
		[Tooltip("The start delay of the ParticleSystem")]
		public SharedFloat startDelay;

		// Token: 0x040007E8 RID: 2024
		private ParticleSystem particleSystem;

		// Token: 0x040007E9 RID: 2025
		private GameObject prevGameObject;
	}
}

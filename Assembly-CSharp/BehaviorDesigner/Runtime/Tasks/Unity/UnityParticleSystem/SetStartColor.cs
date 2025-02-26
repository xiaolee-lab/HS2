using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E1 RID: 481
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start color of the Particle System.")]
	public class SetStartColor : Action
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x000277DC File Offset: 0x00025BDC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00027820 File Offset: 0x00025C20
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startColor = this.startColor.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00027864 File Offset: 0x00025C64
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startColor = Color.white;
		}

		// Token: 0x040007E2 RID: 2018
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007E3 RID: 2019
		[Tooltip("The start color of the ParticleSystem")]
		public SharedColor startColor;

		// Token: 0x040007E4 RID: 2020
		private ParticleSystem particleSystem;

		// Token: 0x040007E5 RID: 2021
		private GameObject prevGameObject;
	}
}

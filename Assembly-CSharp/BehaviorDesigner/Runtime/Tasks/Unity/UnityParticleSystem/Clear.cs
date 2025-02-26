using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001CD RID: 461
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Clear the Particle System.")]
	public class Clear : Action
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00026C94 File Offset: 0x00025094
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00026CD7 File Offset: 0x000250D7
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.Clear();
			return TaskStatus.Success;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00026CF8 File Offset: 0x000250F8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000799 RID: 1945
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400079A RID: 1946
		private ParticleSystem particleSystem;

		// Token: 0x0400079B RID: 1947
		private GameObject prevGameObject;
	}
}

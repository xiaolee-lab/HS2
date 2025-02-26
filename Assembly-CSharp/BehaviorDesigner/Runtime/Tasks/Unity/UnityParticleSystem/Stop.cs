using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E9 RID: 489
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stop the Particle System.")]
	public class Stop : Action
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x00027D04 File Offset: 0x00026104
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00027D47 File Offset: 0x00026147
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00027D68 File Offset: 0x00026168
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000802 RID: 2050
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000803 RID: 2051
		private ParticleSystem particleSystem;

		// Token: 0x04000804 RID: 2052
		private GameObject prevGameObject;
	}
}

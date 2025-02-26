using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E7 RID: 487
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the time of the Particle System.")]
	public class SetTime : Action
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00027BE4 File Offset: 0x00025FE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00027C27 File Offset: 0x00026027
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.time = this.time.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00027C53 File Offset: 0x00026053
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040007FA RID: 2042
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007FB RID: 2043
		[Tooltip("The time of the ParticleSystem")]
		public SharedFloat time;

		// Token: 0x040007FC RID: 2044
		private ParticleSystem particleSystem;

		// Token: 0x040007FD RID: 2045
		private GameObject prevGameObject;
	}
}

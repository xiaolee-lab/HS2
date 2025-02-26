using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E6 RID: 486
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start speed of the Particle System.")]
	public class SetStartSpeed : Action
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00027B38 File Offset: 0x00025F38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00027B7C File Offset: 0x00025F7C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startSpeed = this.startSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00027BC0 File Offset: 0x00025FC0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSpeed = 0f;
		}

		// Token: 0x040007F6 RID: 2038
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007F7 RID: 2039
		[Tooltip("The start speed of the ParticleSystem")]
		public SharedFloat startSpeed;

		// Token: 0x040007F8 RID: 2040
		private ParticleSystem particleSystem;

		// Token: 0x040007F9 RID: 2041
		private GameObject prevGameObject;
	}
}

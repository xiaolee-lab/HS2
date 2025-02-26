using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E5 RID: 485
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the start size of the Particle System.")]
	public class SetStartSize : Action
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00027A8C File Offset: 0x00025E8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00027AD0 File Offset: 0x00025ED0
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.startSize = this.startSize.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00027B14 File Offset: 0x00025F14
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.startSize = 0f;
		}

		// Token: 0x040007F2 RID: 2034
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007F3 RID: 2035
		[Tooltip("The start size of the ParticleSystem")]
		public SharedFloat startSize;

		// Token: 0x040007F4 RID: 2036
		private ParticleSystem particleSystem;

		// Token: 0x040007F5 RID: 2037
		private GameObject prevGameObject;
	}
}

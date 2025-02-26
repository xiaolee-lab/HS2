using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001D4 RID: 468
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Stores the playback speed of the Particle System.")]
	public class GetPlaybackSpeed : Action
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x000270A8 File Offset: 0x000254A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000270EC File Offset: 0x000254EC
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			ParticleSystem.MainModule main = this.particleSystem.main;
			this.storeResult.Value = main.simulationSpeed;
			return TaskStatus.Success;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002712B File Offset: 0x0002552B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = 0f;
		}

		// Token: 0x040007B4 RID: 1972
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007B5 RID: 1973
		[Tooltip("The playback speed of the ParticleSystem")]
		[RequiredField]
		public SharedFloat storeResult;

		// Token: 0x040007B6 RID: 1974
		private ParticleSystem particleSystem;

		// Token: 0x040007B7 RID: 1975
		private GameObject prevGameObject;
	}
}

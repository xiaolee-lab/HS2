using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
	// Token: 0x020001E0 RID: 480
	[TaskCategory("Unity/ParticleSystem")]
	[TaskDescription("Sets the playback speed of the Particle System.")]
	public class SetPlaybackSpeed : Action
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x00027738 File Offset: 0x00025B38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002777C File Offset: 0x00025B7C
		public override TaskStatus OnUpdate()
		{
			if (this.particleSystem == null)
			{
				return TaskStatus.Failure;
			}
			this.particleSystem.main.simulationSpeed = this.playbackSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000277BB File Offset: 0x00025BBB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playbackSpeed = 1f;
		}

		// Token: 0x040007DE RID: 2014
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007DF RID: 2015
		[Tooltip("The playback speed of the ParticleSystem")]
		public SharedFloat playbackSpeed = 1f;

		// Token: 0x040007E0 RID: 2016
		private ParticleSystem particleSystem;

		// Token: 0x040007E1 RID: 2017
		private GameObject prevGameObject;
	}
}

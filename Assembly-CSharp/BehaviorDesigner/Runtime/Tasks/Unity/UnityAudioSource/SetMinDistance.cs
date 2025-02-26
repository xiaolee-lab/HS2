using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013E RID: 318
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the min distance value of the AudioSource. Returns Success.")]
	public class SetMinDistance : Action
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0002281C File Offset: 0x00020C1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0002285F File Offset: 0x00020C5F
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.minDistance = this.minDistance.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0002288B File Offset: 0x00020C8B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.minDistance = 1f;
		}

		// Token: 0x040005C2 RID: 1474
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005C3 RID: 1475
		[Tooltip("The min distance value of the AudioSource")]
		public SharedFloat minDistance;

		// Token: 0x040005C4 RID: 1476
		private AudioSource audioSource;

		// Token: 0x040005C5 RID: 1477
		private GameObject prevGameObject;
	}
}

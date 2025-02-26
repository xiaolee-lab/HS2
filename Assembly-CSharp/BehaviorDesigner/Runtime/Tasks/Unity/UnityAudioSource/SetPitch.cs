using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000140 RID: 320
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the pitch value of the AudioSource. Returns Success.")]
	public class SetPitch : Action
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00022938 File Offset: 0x00020D38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0002297B File Offset: 0x00020D7B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.pitch = this.pitch.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000229A7 File Offset: 0x00020DA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.pitch = 1f;
		}

		// Token: 0x040005CA RID: 1482
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005CB RID: 1483
		[Tooltip("The pitch value of the AudioSource")]
		public SharedFloat pitch;

		// Token: 0x040005CC RID: 1484
		private AudioSource audioSource;

		// Token: 0x040005CD RID: 1485
		private GameObject prevGameObject;
	}
}

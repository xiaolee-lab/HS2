using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000135 RID: 309
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x000222F0 File Offset: 0x000206F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00022333 File Offset: 0x00020733
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.Play();
			return TaskStatus.Success;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00022354 File Offset: 0x00020754
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400059E RID: 1438
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400059F RID: 1439
		private AudioSource audioSource;

		// Token: 0x040005A0 RID: 1440
		private GameObject prevGameObject;
	}
}

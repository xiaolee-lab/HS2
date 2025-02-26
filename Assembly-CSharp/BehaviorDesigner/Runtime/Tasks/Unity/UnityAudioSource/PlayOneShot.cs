using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000137 RID: 311
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays an AudioClip, and scales the AudioSource volume by volumeScale. Returns Success.")]
	public class PlayOneShot : Action
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00022418 File Offset: 0x00020818
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002245B File Offset: 0x0002085B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.PlayOneShot((AudioClip)this.clip.Value, this.volumeScale.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00022497 File Offset: 0x00020897
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.clip = null;
			this.volumeScale = 1f;
		}

		// Token: 0x040005A5 RID: 1445
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005A6 RID: 1446
		[Tooltip("The clip being played")]
		public SharedObject clip;

		// Token: 0x040005A7 RID: 1447
		[Tooltip("The scale of the volume (0-1)")]
		public SharedFloat volumeScale = 1f;

		// Token: 0x040005A8 RID: 1448
		private AudioSource audioSource;

		// Token: 0x040005A9 RID: 1449
		private GameObject prevGameObject;
	}
}

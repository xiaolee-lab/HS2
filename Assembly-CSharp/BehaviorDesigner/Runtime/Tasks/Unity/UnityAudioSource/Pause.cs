using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000134 RID: 308
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Pauses the audio clip. Returns Success.")]
	public class Pause : Action
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x00022278 File Offset: 0x00020678
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000222BB File Offset: 0x000206BB
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000222DC File Offset: 0x000206DC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400059B RID: 1435
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400059C RID: 1436
		private AudioSource audioSource;

		// Token: 0x0400059D RID: 1437
		private GameObject prevGameObject;
	}
}

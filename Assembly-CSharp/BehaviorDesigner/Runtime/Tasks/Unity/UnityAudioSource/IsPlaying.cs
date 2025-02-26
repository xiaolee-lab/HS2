using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000133 RID: 307
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Returns Success if the AudioClip is playing, otherwise Failure.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x000221F8 File Offset: 0x000205F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002223B File Offset: 0x0002063B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.audioSource.isPlaying) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00022267 File Offset: 0x00020667
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000598 RID: 1432
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000599 RID: 1433
		private AudioSource audioSource;

		// Token: 0x0400059A RID: 1434
		private GameObject prevGameObject;
	}
}

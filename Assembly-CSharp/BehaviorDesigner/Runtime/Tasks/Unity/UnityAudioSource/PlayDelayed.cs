using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000136 RID: 310
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayDelayed : Action
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00022378 File Offset: 0x00020778
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000223BB File Offset: 0x000207BB
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.PlayDelayed(this.delay.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000223E7 File Offset: 0x000207E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.delay = 0f;
		}

		// Token: 0x040005A1 RID: 1441
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005A2 RID: 1442
		[Tooltip("Delay time specified in seconds")]
		public SharedFloat delay = 0f;

		// Token: 0x040005A3 RID: 1443
		private AudioSource audioSource;

		// Token: 0x040005A4 RID: 1444
		private GameObject prevGameObject;
	}
}

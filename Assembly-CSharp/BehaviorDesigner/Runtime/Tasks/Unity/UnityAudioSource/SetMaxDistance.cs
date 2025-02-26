using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013D RID: 317
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the max distance value of the AudioSource. Returns Success.")]
	public class SetMaxDistance : Action
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0002278C File Offset: 0x00020B8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000227CF File Offset: 0x00020BCF
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.maxDistance = this.maxDistance.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000227FB File Offset: 0x00020BFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxDistance = 1f;
		}

		// Token: 0x040005BE RID: 1470
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005BF RID: 1471
		[Tooltip("The max distance value of the AudioSource")]
		public SharedFloat maxDistance;

		// Token: 0x040005C0 RID: 1472
		private AudioSource audioSource;

		// Token: 0x040005C1 RID: 1473
		private GameObject prevGameObject;
	}
}

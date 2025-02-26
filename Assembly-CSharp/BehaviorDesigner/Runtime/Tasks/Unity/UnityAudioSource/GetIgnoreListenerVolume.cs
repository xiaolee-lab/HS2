using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000128 RID: 296
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the ignore listener volume value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerVolume : Action
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x00021BD4 File Offset: 0x0001FFD4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021C17 File Offset: 0x00020017
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerVolume;
			return TaskStatus.Success;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00021C43 File Offset: 0x00020043
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400056C RID: 1388
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400056D RID: 1389
		[Tooltip("The ignore listener volume value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400056E RID: 1390
		private AudioSource audioSource;

		// Token: 0x0400056F RID: 1391
		private GameObject prevGameObject;
	}
}

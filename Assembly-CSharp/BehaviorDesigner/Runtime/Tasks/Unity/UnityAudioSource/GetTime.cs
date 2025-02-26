using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000130 RID: 304
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the time value of the AudioSource. Returns Success.")]
	public class GetTime : Action
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00022044 File Offset: 0x00020444
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00022087 File Offset: 0x00020487
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.time;
			return TaskStatus.Success;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000220B3 File Offset: 0x000204B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x0400058C RID: 1420
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400058D RID: 1421
		[Tooltip("The time value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400058E RID: 1422
		private AudioSource audioSource;

		// Token: 0x0400058F RID: 1423
		private GameObject prevGameObject;
	}
}

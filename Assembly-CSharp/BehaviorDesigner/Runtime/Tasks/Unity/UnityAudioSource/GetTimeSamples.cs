using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000131 RID: 305
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the time samples value of the AudioSource. Returns Success.")]
	public class GetTimeSamples : Action
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x000220D4 File Offset: 0x000204D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00022117 File Offset: 0x00020517
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = (float)this.audioSource.timeSamples;
			return TaskStatus.Success;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00022144 File Offset: 0x00020544
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000590 RID: 1424
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000591 RID: 1425
		[Tooltip("The time samples value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000592 RID: 1426
		private AudioSource audioSource;

		// Token: 0x04000593 RID: 1427
		private GameObject prevGameObject;
	}
}

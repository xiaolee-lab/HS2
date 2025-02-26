using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012D RID: 301
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the pitch value of the AudioSource. Returns Success.")]
	public class GetPitch : Action
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x00021E98 File Offset: 0x00020298
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00021EDB File Offset: 0x000202DB
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.pitch;
			return TaskStatus.Success;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00021F07 File Offset: 0x00020307
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000580 RID: 1408
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000581 RID: 1409
		[Tooltip("The pitch value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000582 RID: 1410
		private AudioSource audioSource;

		// Token: 0x04000583 RID: 1411
		private GameObject prevGameObject;
	}
}

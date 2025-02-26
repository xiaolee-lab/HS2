using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012E RID: 302
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the priority value of the AudioSource. Returns Success.")]
	public class GetPriority : Action
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x00021F28 File Offset: 0x00020328
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00021F6B File Offset: 0x0002036B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.priority;
			return TaskStatus.Success;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00021F97 File Offset: 0x00020397
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1;
		}

		// Token: 0x04000584 RID: 1412
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000585 RID: 1413
		[Tooltip("The priority value of the AudioSource")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04000586 RID: 1414
		private AudioSource audioSource;

		// Token: 0x04000587 RID: 1415
		private GameObject prevGameObject;
	}
}

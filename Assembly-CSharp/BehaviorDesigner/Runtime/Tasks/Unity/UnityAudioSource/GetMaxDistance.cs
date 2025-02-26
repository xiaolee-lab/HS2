using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012A RID: 298
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the max distance value of the AudioSource. Returns Success.")]
	public class GetMaxDistance : Action
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00021CEC File Offset: 0x000200EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00021D2F File Offset: 0x0002012F
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.maxDistance;
			return TaskStatus.Success;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00021D5B File Offset: 0x0002015B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000574 RID: 1396
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000575 RID: 1397
		[Tooltip("The max distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000576 RID: 1398
		private AudioSource audioSource;

		// Token: 0x04000577 RID: 1399
		private GameObject prevGameObject;
	}
}

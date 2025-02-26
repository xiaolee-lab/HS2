using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000132 RID: 306
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the volume value of the AudioSource. Returns Success.")]
	public class GetVolume : Action
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x00022168 File Offset: 0x00020568
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000221AB File Offset: 0x000205AB
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.volume;
			return TaskStatus.Success;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000221D7 File Offset: 0x000205D7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000594 RID: 1428
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000595 RID: 1429
		[Tooltip("The volume value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000596 RID: 1430
		private AudioSource audioSource;

		// Token: 0x04000597 RID: 1431
		private GameObject prevGameObject;
	}
}

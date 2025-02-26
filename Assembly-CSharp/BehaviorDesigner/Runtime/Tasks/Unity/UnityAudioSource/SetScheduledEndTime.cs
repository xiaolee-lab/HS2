using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000143 RID: 323
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will end. Notice that depending on the timing not all rescheduling requests can be fulfilled. Returns Success.")]
	public class SetScheduledEndTime : Action
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00022AE8 File Offset: 0x00020EE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00022B2B File Offset: 0x00020F2B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.SetScheduledEndTime((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00022B58 File Offset: 0x00020F58
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040005D6 RID: 1494
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D7 RID: 1495
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x040005D8 RID: 1496
		private AudioSource audioSource;

		// Token: 0x040005D9 RID: 1497
		private GameObject prevGameObject;
	}
}

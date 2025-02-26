using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000144 RID: 324
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will start. Returns Success.")]
	public class SetScheduledStartTime : Action
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00022B8C File Offset: 0x00020F8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00022BCF File Offset: 0x00020FCF
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.SetScheduledStartTime((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00022BFC File Offset: 0x00020FFC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040005DA RID: 1498
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005DB RID: 1499
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x040005DC RID: 1500
		private AudioSource audioSource;

		// Token: 0x040005DD RID: 1501
		private GameObject prevGameObject;
	}
}

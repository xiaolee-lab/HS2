using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000146 RID: 326
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the time value of the AudioSource. Returns Success.")]
	public class SetTime : Action
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x00022CB0 File Offset: 0x000210B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00022CF3 File Offset: 0x000210F3
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.time = this.time.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00022D1F File Offset: 0x0002111F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 1f;
		}

		// Token: 0x040005E2 RID: 1506
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005E3 RID: 1507
		[Tooltip("The time value of the AudioSource")]
		public SharedFloat time;

		// Token: 0x040005E4 RID: 1508
		private AudioSource audioSource;

		// Token: 0x040005E5 RID: 1509
		private GameObject prevGameObject;
	}
}

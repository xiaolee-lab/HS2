using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000141 RID: 321
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the priority value of the AudioSource. Returns Success.")]
	public class SetPriority : Action
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x000229C8 File Offset: 0x00020DC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00022A0B File Offset: 0x00020E0B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.priority = this.priority.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00022A37 File Offset: 0x00020E37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.priority = 1;
		}

		// Token: 0x040005CE RID: 1486
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005CF RID: 1487
		[Tooltip("The priority value of the AudioSource")]
		public SharedInt priority;

		// Token: 0x040005D0 RID: 1488
		private AudioSource audioSource;

		// Token: 0x040005D1 RID: 1489
		private GameObject prevGameObject;
	}
}

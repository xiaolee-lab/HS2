using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013F RID: 319
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the mute value of the AudioSource. Returns Success.")]
	public class SetMute : Action
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x000228AC File Offset: 0x00020CAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000228EF File Offset: 0x00020CEF
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.mute = this.mute.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0002291B File Offset: 0x00020D1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mute = false;
		}

		// Token: 0x040005C6 RID: 1478
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005C7 RID: 1479
		[Tooltip("The mute value of the AudioSource")]
		public SharedBool mute;

		// Token: 0x040005C8 RID: 1480
		private AudioSource audioSource;

		// Token: 0x040005C9 RID: 1481
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000142 RID: 322
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetRolloffMode : Action
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00022A54 File Offset: 0x00020E54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00022A97 File Offset: 0x00020E97
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.rolloffMode = this.rolloffMode;
			return TaskStatus.Success;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00022ABE File Offset: 0x00020EBE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rolloffMode = AudioRolloffMode.Logarithmic;
		}

		// Token: 0x040005D2 RID: 1490
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D3 RID: 1491
		[Tooltip("The rolloff mode of the AudioSource")]
		public AudioRolloffMode rolloffMode;

		// Token: 0x040005D4 RID: 1492
		private AudioSource audioSource;

		// Token: 0x040005D5 RID: 1493
		private GameObject prevGameObject;
	}
}

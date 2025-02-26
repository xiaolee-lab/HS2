using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013A RID: 314
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the ignore listener volume value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerVolume : Action
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x000225E8 File Offset: 0x000209E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002262B File Offset: 0x00020A2B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.ignoreListenerVolume = this.ignoreListenerVolume.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00022657 File Offset: 0x00020A57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerVolume = false;
		}

		// Token: 0x040005B2 RID: 1458
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005B3 RID: 1459
		[Tooltip("The ignore listener volume value of the AudioSource")]
		public SharedBool ignoreListenerVolume;

		// Token: 0x040005B4 RID: 1460
		private AudioSource audioSource;

		// Token: 0x040005B5 RID: 1461
		private GameObject prevGameObject;
	}
}

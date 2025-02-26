using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012C RID: 300
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the mute value of the AudioSource. Returns Success.")]
	public class GetMute : Action
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x00021E0C File Offset: 0x0002020C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00021E4F File Offset: 0x0002024F
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.mute;
			return TaskStatus.Success;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00021E7B File Offset: 0x0002027B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400057C RID: 1404
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400057D RID: 1405
		[Tooltip("The mute value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400057E RID: 1406
		private AudioSource audioSource;

		// Token: 0x0400057F RID: 1407
		private GameObject prevGameObject;
	}
}

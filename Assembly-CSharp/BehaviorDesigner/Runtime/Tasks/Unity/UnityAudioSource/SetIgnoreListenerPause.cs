using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013B RID: 315
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the ignore listener pause value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerPause : Action
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00022674 File Offset: 0x00020A74
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000226B7 File Offset: 0x00020AB7
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.ignoreListenerPause = this.ignoreListenerPause.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000226E3 File Offset: 0x00020AE3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerPause = false;
		}

		// Token: 0x040005B6 RID: 1462
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005B7 RID: 1463
		[Tooltip("The ignore listener pause value of the AudioSource")]
		public SharedBool ignoreListenerPause;

		// Token: 0x040005B8 RID: 1464
		private AudioSource audioSource;

		// Token: 0x040005B9 RID: 1465
		private GameObject prevGameObject;
	}
}

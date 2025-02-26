using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000127 RID: 295
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the ignore listener pause value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerPause : Action
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00021B48 File Offset: 0x0001FF48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00021B8B File Offset: 0x0001FF8B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerPause;
			return TaskStatus.Success;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00021BB7 File Offset: 0x0001FFB7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000568 RID: 1384
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000569 RID: 1385
		[Tooltip("The ignore listener pause value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400056A RID: 1386
		private AudioSource audioSource;

		// Token: 0x0400056B RID: 1387
		private GameObject prevGameObject;
	}
}

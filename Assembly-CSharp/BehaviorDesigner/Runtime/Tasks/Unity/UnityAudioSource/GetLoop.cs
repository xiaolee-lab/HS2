using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000129 RID: 297
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the loop value of the AudioSource. Returns Success.")]
	public class GetLoop : Action
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00021C60 File Offset: 0x00020060
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021CA3 File Offset: 0x000200A3
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.loop;
			return TaskStatus.Success;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021CCF File Offset: 0x000200CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000570 RID: 1392
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000571 RID: 1393
		[Tooltip("The loop value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000572 RID: 1394
		private AudioSource audioSource;

		// Token: 0x04000573 RID: 1395
		private GameObject prevGameObject;
	}
}

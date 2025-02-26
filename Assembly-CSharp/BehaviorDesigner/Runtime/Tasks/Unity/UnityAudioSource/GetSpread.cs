using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012F RID: 303
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the spread value of the AudioSource. Returns Success.")]
	public class GetSpread : Action
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x00021FB4 File Offset: 0x000203B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00021FF7 File Offset: 0x000203F7
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.spread;
			return TaskStatus.Success;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022023 File Offset: 0x00020423
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000588 RID: 1416
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000589 RID: 1417
		[Tooltip("The spread value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400058A RID: 1418
		private AudioSource audioSource;

		// Token: 0x0400058B RID: 1419
		private GameObject prevGameObject;
	}
}

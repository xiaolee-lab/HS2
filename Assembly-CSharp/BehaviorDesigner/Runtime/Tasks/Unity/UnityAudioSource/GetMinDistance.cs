using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200012B RID: 299
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stores the min distance value of the AudioSource. Returns Success.")]
	public class GetMinDistance : Action
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x00021D7C File Offset: 0x0002017C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00021DBF File Offset: 0x000201BF
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.audioSource.minDistance;
			return TaskStatus.Success;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00021DEB File Offset: 0x000201EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x04000578 RID: 1400
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000579 RID: 1401
		[Tooltip("The min distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400057A RID: 1402
		private AudioSource audioSource;

		// Token: 0x0400057B RID: 1403
		private GameObject prevGameObject;
	}
}

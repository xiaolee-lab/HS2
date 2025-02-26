using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x0200013C RID: 316
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the loop value of the AudioSource. Returns Success.")]
	public class SetLoop : Action
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00022700 File Offset: 0x00020B00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00022743 File Offset: 0x00020B43
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.loop = this.loop.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0002276F File Offset: 0x00020B6F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x040005BA RID: 1466
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005BB RID: 1467
		[Tooltip("The loop value of the AudioSource")]
		public SharedBool loop;

		// Token: 0x040005BC RID: 1468
		private AudioSource audioSource;

		// Token: 0x040005BD RID: 1469
		private GameObject prevGameObject;
	}
}

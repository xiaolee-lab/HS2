using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000148 RID: 328
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the volume value of the AudioSource. Returns Success.")]
	public class SetVolume : Action
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x00022DC4 File Offset: 0x000211C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00022E07 File Offset: 0x00021207
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.volume = this.volume.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00022E33 File Offset: 0x00021233
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.volume = 1f;
		}

		// Token: 0x040005EA RID: 1514
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005EB RID: 1515
		[Tooltip("The volume value of the AudioSource")]
		public SharedFloat volume;

		// Token: 0x040005EC RID: 1516
		private AudioSource audioSource;

		// Token: 0x040005ED RID: 1517
		private GameObject prevGameObject;
	}
}

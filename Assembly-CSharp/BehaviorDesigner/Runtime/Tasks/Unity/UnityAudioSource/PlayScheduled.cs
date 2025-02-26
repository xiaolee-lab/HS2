using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000138 RID: 312
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayScheduled : Action
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x000224D0 File Offset: 0x000208D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00022513 File Offset: 0x00020913
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.PlayScheduled((double)this.time.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00022540 File Offset: 0x00020940
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040005AA RID: 1450
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005AB RID: 1451
		[Tooltip("Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing")]
		public SharedFloat time = 0f;

		// Token: 0x040005AC RID: 1452
		private AudioSource audioSource;

		// Token: 0x040005AD RID: 1453
		private GameObject prevGameObject;
	}
}

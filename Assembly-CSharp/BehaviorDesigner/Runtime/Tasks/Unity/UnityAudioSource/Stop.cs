using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000149 RID: 329
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Stops playing the audio clip. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x00022E54 File Offset: 0x00021254
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00022E97 File Offset: 0x00021297
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00022EB8 File Offset: 0x000212B8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040005EE RID: 1518
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005EF RID: 1519
		private AudioSource audioSource;

		// Token: 0x040005F0 RID: 1520
		private GameObject prevGameObject;
	}
}

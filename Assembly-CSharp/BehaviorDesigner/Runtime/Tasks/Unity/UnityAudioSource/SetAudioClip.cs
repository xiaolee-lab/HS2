using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000139 RID: 313
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the clip value of the AudioSource. Returns Success.")]
	public class SetAudioClip : Action
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x00022564 File Offset: 0x00020964
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000225A7 File Offset: 0x000209A7
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.clip = this.audioClip;
			return TaskStatus.Success;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000225CE File Offset: 0x000209CE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.audioClip = null;
		}

		// Token: 0x040005AE RID: 1454
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005AF RID: 1455
		[Tooltip("The AudioSource clip")]
		public AudioClip audioClip;

		// Token: 0x040005B0 RID: 1456
		private AudioSource audioSource;

		// Token: 0x040005B1 RID: 1457
		private GameObject prevGameObject;
	}
}

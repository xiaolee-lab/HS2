using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000145 RID: 325
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the spread value of the AudioSource. Returns Success.")]
	public class SetSpread : Action
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00022C20 File Offset: 0x00021020
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00022C63 File Offset: 0x00021063
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.spread = this.spread.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00022C8F File Offset: 0x0002108F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spread = 1f;
		}

		// Token: 0x040005DE RID: 1502
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005DF RID: 1503
		[Tooltip("The spread value of the AudioSource")]
		public SharedFloat spread;

		// Token: 0x040005E0 RID: 1504
		private AudioSource audioSource;

		// Token: 0x040005E1 RID: 1505
		private GameObject prevGameObject;
	}
}

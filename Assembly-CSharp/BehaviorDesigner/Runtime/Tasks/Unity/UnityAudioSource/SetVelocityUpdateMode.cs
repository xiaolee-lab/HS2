using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource
{
	// Token: 0x02000147 RID: 327
	[TaskCategory("Unity/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetVelocityUpdateMode : Action
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00022D40 File Offset: 0x00021140
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00022D83 File Offset: 0x00021183
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				return TaskStatus.Failure;
			}
			this.audioSource.velocityUpdateMode = this.velocityUpdateMode;
			return TaskStatus.Success;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00022DAA File Offset: 0x000211AA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocityUpdateMode = AudioVelocityUpdateMode.Auto;
		}

		// Token: 0x040005E6 RID: 1510
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005E7 RID: 1511
		[Tooltip("The velocity update mode of the AudioSource")]
		public AudioVelocityUpdateMode velocityUpdateMode;

		// Token: 0x040005E8 RID: 1512
		private AudioSource audioSource;

		// Token: 0x040005E9 RID: 1513
		private GameObject prevGameObject;
	}
}

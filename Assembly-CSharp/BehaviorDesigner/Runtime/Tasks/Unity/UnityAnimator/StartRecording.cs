using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000123 RID: 291
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the animator in recording mode. Returns Success.")]
	public class StartRecording : Action
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x000218D0 File Offset: 0x0001FCD0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00021913 File Offset: 0x0001FD13
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.StartRecording(this.frameCount);
			return TaskStatus.Success;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002193A File Offset: 0x0001FD3A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.frameCount = 0;
		}

		// Token: 0x04000558 RID: 1368
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000559 RID: 1369
		[Tooltip("The number of frames (updates) that will be recorded")]
		public int frameCount;

		// Token: 0x0400055A RID: 1370
		private Animator animator;

		// Token: 0x0400055B RID: 1371
		private GameObject prevGameObject;
	}
}

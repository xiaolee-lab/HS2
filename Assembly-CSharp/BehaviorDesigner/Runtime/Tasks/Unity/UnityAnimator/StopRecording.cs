using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000125 RID: 293
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stops animator record mode. Returns Success.")]
	public class StopRecording : Action
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x000219CC File Offset: 0x0001FDCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00021A0F File Offset: 0x0001FE0F
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.StopRecording();
			return TaskStatus.Success;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00021A30 File Offset: 0x0001FE30
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400055F RID: 1375
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000560 RID: 1376
		private Animator animator;

		// Token: 0x04000561 RID: 1377
		private GameObject prevGameObject;
	}
}

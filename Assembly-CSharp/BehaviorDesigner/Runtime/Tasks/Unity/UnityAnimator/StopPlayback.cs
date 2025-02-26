using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000124 RID: 292
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stops the animator playback mode. Returns Success.")]
	public class StopPlayback : Action
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x00021954 File Offset: 0x0001FD54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00021997 File Offset: 0x0001FD97
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.StopPlayback();
			return TaskStatus.Success;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000219B8 File Offset: 0x0001FDB8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400055C RID: 1372
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400055D RID: 1373
		private Animator animator;

		// Token: 0x0400055E RID: 1374
		private GameObject prevGameObject;
	}
}

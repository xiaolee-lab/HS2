using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000120 RID: 288
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the playback speed of the Animator. 1 is normal playback speed. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00021738 File Offset: 0x0001FB38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002177B File Offset: 0x0001FB7B
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.speed = this.speed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000217A7 File Offset: 0x0001FBA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x0400054D RID: 1357
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400054E RID: 1358
		[Tooltip("The playback speed of the Animator")]
		public SharedFloat speed;

		// Token: 0x0400054F RID: 1359
		private Animator animator;

		// Token: 0x04000550 RID: 1360
		private GameObject prevGameObject;
	}
}

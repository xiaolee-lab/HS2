using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011E RID: 286
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the look at position. Returns Success.")]
	public class SetLookAtPosition : Action
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0002153C File Offset: 0x0001F93C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
			this.positionSet = false;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00021586 File Offset: 0x0001F986
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.positionSet) ? TaskStatus.Running : TaskStatus.Success;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000215AD File Offset: 0x0001F9AD
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtPosition(this.position.Value);
			this.positionSet = true;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000215DE File Offset: 0x0001F9DE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x0400053F RID: 1343
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000540 RID: 1344
		[Tooltip("The position to lookAt")]
		public SharedVector3 position;

		// Token: 0x04000541 RID: 1345
		private Animator animator;

		// Token: 0x04000542 RID: 1346
		private GameObject prevGameObject;

		// Token: 0x04000543 RID: 1347
		private bool positionSet;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011F RID: 287
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the look at weight. Returns success immediately after.")]
	public class SetLookAtWeight : Action
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x00021618 File Offset: 0x0001FA18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
			this.weightSet = false;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00021662 File Offset: 0x0001FA62
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.weightSet) ? TaskStatus.Running : TaskStatus.Success;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002168C File Offset: 0x0001FA8C
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtWeight(this.weight.Value, this.bodyWeight, this.headWeight, this.eyesWeight, this.clampWeight);
			this.weightSet = true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000216E0 File Offset: 0x0001FAE0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.weight = 0f;
			this.bodyWeight = 0f;
			this.headWeight = 1f;
			this.eyesWeight = 0f;
			this.clampWeight = 0.5f;
		}

		// Token: 0x04000544 RID: 1348
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000545 RID: 1349
		[Tooltip("(0-1) the global weight of the LookAt, multiplier for other parameters.")]
		public SharedFloat weight;

		// Token: 0x04000546 RID: 1350
		[Tooltip("(0-1) determines how much the body is involved in the LookAt.")]
		public float bodyWeight;

		// Token: 0x04000547 RID: 1351
		[Tooltip("(0-1) determines how much the head is involved in the LookAt.")]
		public float headWeight = 1f;

		// Token: 0x04000548 RID: 1352
		[Tooltip("(0-1) determines how much the eyes are involved in the LookAt.")]
		public float eyesWeight;

		// Token: 0x04000549 RID: 1353
		[Tooltip("(0-1) 0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
		public float clampWeight = 0.5f;

		// Token: 0x0400054A RID: 1354
		private Animator animator;

		// Token: 0x0400054B RID: 1355
		private GameObject prevGameObject;

		// Token: 0x0400054C RID: 1356
		private bool weightSet;
	}
}

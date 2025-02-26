using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000119 RID: 281
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets if root motion is applied. Returns Success.")]
	public class SetApplyRootMotion : Action
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00020EDC File Offset: 0x0001F2DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00020F1F File Offset: 0x0001F31F
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.applyRootMotion = this.rootMotion.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00020F4B File Offset: 0x0001F34B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rootMotion = false;
		}

		// Token: 0x04000521 RID: 1313
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000522 RID: 1314
		[Tooltip("Is root motion applied?")]
		public SharedBool rootMotion;

		// Token: 0x04000523 RID: 1315
		private Animator animator;

		// Token: 0x04000524 RID: 1316
		private GameObject prevGameObject;
	}
}

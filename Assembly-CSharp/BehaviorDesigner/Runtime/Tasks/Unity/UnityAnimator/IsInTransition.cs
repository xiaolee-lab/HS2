using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000114 RID: 276
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified AnimatorController layer in a transition.")]
	public class IsInTransition : Conditional
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x00020AF4 File Offset: 0x0001EEF4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00020B37 File Offset: 0x0001EF37
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.animator.IsInTransition(this.index.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00020B6E File Offset: 0x0001EF6E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
		}

		// Token: 0x04000504 RID: 1284
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000505 RID: 1285
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x04000506 RID: 1286
		private Animator animator;

		// Token: 0x04000507 RID: 1287
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000115 RID: 277
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified name matches the name of the active state.")]
	public class IsName : Conditional
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00020B8C File Offset: 0x0001EF8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00020BD0 File Offset: 0x0001EFD0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.animator.GetCurrentAnimatorStateInfo(this.index.Value).IsName(this.name.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00020C25 File Offset: 0x0001F025
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.name = string.Empty;
		}

		// Token: 0x04000508 RID: 1288
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000509 RID: 1289
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x0400050A RID: 1290
		[Tooltip("The state name to compare")]
		public SharedString name;

		// Token: 0x0400050B RID: 1291
		private Animator animator;

		// Token: 0x0400050C RID: 1292
		private GameObject prevGameObject;
	}
}

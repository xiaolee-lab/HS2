using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000121 RID: 289
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets a trigger parameter to active or inactive. Returns Success.")]
	public class SetTrigger : Action
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x000217C8 File Offset: 0x0001FBC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002180B File Offset: 0x0001FC0B
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.SetTrigger(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00021837 File Offset: 0x0001FC37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName.Value = string.Empty;
		}

		// Token: 0x04000551 RID: 1361
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000552 RID: 1362
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000553 RID: 1363
		private Animator animator;

		// Token: 0x04000554 RID: 1364
		private GameObject prevGameObject;
	}
}

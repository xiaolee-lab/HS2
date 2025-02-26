using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000116 RID: 278
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified parameter is controlled by an additional curve on an animation.")]
	public class IsParameterControlledByCurve : Conditional
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00020C54 File Offset: 0x0001F054
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00020C97 File Offset: 0x0001F097
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.animator.IsParameterControlledByCurve(this.paramaterName.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00020CCE File Offset: 0x0001F0CE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
		}

		// Token: 0x0400050D RID: 1293
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400050E RID: 1294
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400050F RID: 1295
		private Animator animator;

		// Token: 0x04000510 RID: 1296
		private GameObject prevGameObject;
	}
}

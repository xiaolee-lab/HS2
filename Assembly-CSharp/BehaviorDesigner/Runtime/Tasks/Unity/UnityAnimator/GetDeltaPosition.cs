using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010B RID: 267
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Gets the avatar delta position for the last evaluated frame. Returns Success.")]
	public class GetDeltaPosition : Action
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x000205E4 File Offset: 0x0001E9E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00020627 File Offset: 0x0001EA27
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.deltaPosition;
			return TaskStatus.Success;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00020653 File Offset: 0x0001EA53
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040004DF RID: 1247
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004E0 RID: 1248
		[Tooltip("The avatar delta position")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040004E1 RID: 1249
		private Animator animator;

		// Token: 0x040004E2 RID: 1250
		private GameObject prevGameObject;
	}
}

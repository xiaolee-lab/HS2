using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000113 RID: 275
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Interrupts the automatic target matching. Returns Success.")]
	public class InterruptMatchTarget : Action
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00020A70 File Offset: 0x0001EE70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00020AB3 File Offset: 0x0001EEB3
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.InterruptMatchTarget(this.completeMatch);
			return TaskStatus.Success;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00020ADA File Offset: 0x0001EEDA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.completeMatch = true;
		}

		// Token: 0x04000500 RID: 1280
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000501 RID: 1281
		[Tooltip("CompleteMatch will make the gameobject match the target completely at the next frame")]
		public bool completeMatch = true;

		// Token: 0x04000502 RID: 1282
		private Animator animator;

		// Token: 0x04000503 RID: 1283
		private GameObject prevGameObject;
	}
}

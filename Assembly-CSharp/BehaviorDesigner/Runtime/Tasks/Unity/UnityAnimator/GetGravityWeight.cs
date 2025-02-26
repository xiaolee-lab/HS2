using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010E RID: 270
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the current gravity weight based on current animations that are played. Returns Success.")]
	public class GetGravityWeight : Action
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x000207B4 File Offset: 0x0001EBB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000207F7 File Offset: 0x0001EBF7
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.gravityWeight;
			return TaskStatus.Success;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00020823 File Offset: 0x0001EC23
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004EC RID: 1260
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004ED RID: 1261
		[Tooltip("The value of the gravity weight")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004EE RID: 1262
		private Animator animator;

		// Token: 0x040004EF RID: 1263
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000109 RID: 265
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores if root motion is applied. Returns Success.")]
	public class GetApplyRootMotion : Action
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x000204B0 File Offset: 0x0001E8B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000204F3 File Offset: 0x0001E8F3
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.applyRootMotion;
			return TaskStatus.Success;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002051F File Offset: 0x0001E91F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040004D6 RID: 1238
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004D7 RID: 1239
		[Tooltip("Is root motion applied?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040004D8 RID: 1240
		private Animator animator;

		// Token: 0x040004D9 RID: 1241
		private GameObject prevGameObject;
	}
}

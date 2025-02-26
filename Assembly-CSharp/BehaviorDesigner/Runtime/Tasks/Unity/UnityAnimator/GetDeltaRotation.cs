using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010C RID: 268
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Gets the avatar delta rotation for the last evaluated frame. Returns Success.")]
	public class GetDeltaRotation : Action
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00020674 File Offset: 0x0001EA74
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000206B7 File Offset: 0x0001EAB7
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.deltaRotation;
			return TaskStatus.Success;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000206E3 File Offset: 0x0001EAE3
		public override void OnReset()
		{
			if (this.storeValue != null)
			{
				this.storeValue.Value = Quaternion.identity;
			}
		}

		// Token: 0x040004E3 RID: 1251
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004E4 RID: 1252
		[Tooltip("The avatar delta rotation")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x040004E5 RID: 1253
		private Animator animator;

		// Token: 0x040004E6 RID: 1254
		private GameObject prevGameObject;
	}
}

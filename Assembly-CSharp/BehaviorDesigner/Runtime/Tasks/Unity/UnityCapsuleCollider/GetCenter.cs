using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000153 RID: 339
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the center of the CapsuleCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x000233A0 File Offset: 0x000217A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000233E3 File Offset: 0x000217E3
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0002340F File Offset: 0x0002180F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400060E RID: 1550
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400060F RID: 1551
		[Tooltip("The center of the CapsuleCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000610 RID: 1552
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000611 RID: 1553
		private GameObject prevGameObject;
	}
}

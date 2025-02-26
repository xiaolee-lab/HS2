using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000156 RID: 342
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the radius of the CapsuleCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0002354C File Offset: 0x0002194C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002358F File Offset: 0x0002198F
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000235BB File Offset: 0x000219BB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400061A RID: 1562
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400061B RID: 1563
		[Tooltip("The radius of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400061C RID: 1564
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400061D RID: 1565
		private GameObject prevGameObject;
	}
}

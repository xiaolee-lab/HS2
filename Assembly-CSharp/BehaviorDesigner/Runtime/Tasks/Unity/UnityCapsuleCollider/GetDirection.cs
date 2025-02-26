using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000154 RID: 340
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the direction of the CapsuleCollider. Returns Success.")]
	public class GetDirection : Action
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x00023430 File Offset: 0x00021830
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00023473 File Offset: 0x00021873
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.direction;
			return TaskStatus.Success;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0002349F File Offset: 0x0002189F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04000612 RID: 1554
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000613 RID: 1555
		[Tooltip("The direction of the CapsuleCollider")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04000614 RID: 1556
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000615 RID: 1557
		private GameObject prevGameObject;
	}
}

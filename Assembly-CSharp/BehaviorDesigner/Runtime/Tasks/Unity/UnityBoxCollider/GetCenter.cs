using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x0200014D RID: 333
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Stores the center of the BoxCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00023040 File Offset: 0x00021440
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023083 File Offset: 0x00021483
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000230AF File Offset: 0x000214AF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040005F6 RID: 1526
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005F7 RID: 1527
		[Tooltip("The center of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040005F8 RID: 1528
		private BoxCollider boxCollider;

		// Token: 0x040005F9 RID: 1529
		private GameObject prevGameObject;
	}
}

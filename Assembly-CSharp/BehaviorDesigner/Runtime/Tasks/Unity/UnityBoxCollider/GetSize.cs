using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x0200014E RID: 334
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Stores the size of the BoxCollider. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x000230D0 File Offset: 0x000214D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00023113 File Offset: 0x00021513
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider.size;
			return TaskStatus.Success;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0002313F File Offset: 0x0002153F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040005FA RID: 1530
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005FB RID: 1531
		[Tooltip("The size of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040005FC RID: 1532
		private BoxCollider boxCollider;

		// Token: 0x040005FD RID: 1533
		private GameObject prevGameObject;
	}
}

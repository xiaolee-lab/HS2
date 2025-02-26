using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider2D
{
	// Token: 0x02000151 RID: 337
	[TaskCategory("Unity/BoxCollider2D")]
	[TaskDescription("Stores the size of the BoxCollider2D. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00023280 File Offset: 0x00021680
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000232C3 File Offset: 0x000216C3
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider2D.size;
			return TaskStatus.Success;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000232EF File Offset: 0x000216EF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04000606 RID: 1542
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000607 RID: 1543
		[Tooltip("The size of the BoxCollider2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000608 RID: 1544
		private BoxCollider2D boxCollider2D;

		// Token: 0x04000609 RID: 1545
		private GameObject prevGameObject;
	}
}

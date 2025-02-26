using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000230 RID: 560
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the drag of the Rigidbody2D. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A45C File Offset: 0x0002885C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A49F File Offset: 0x0002889F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.drag;
			return TaskStatus.Success;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A4CB File Offset: 0x000288CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000917 RID: 2327
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000918 RID: 2328
		[Tooltip("The drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000919 RID: 2329
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400091A RID: 2330
		private GameObject prevGameObject;
	}
}

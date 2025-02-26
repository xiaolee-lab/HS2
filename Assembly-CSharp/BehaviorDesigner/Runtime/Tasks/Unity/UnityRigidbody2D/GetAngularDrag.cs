using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200022E RID: 558
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the angular drag of the Rigidbody2D. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x0002A33C File Offset: 0x0002873C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002A37F File Offset: 0x0002877F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.angularDrag;
			return TaskStatus.Success;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002A3AB File Offset: 0x000287AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400090F RID: 2319
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000910 RID: 2320
		[Tooltip("The angular drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000911 RID: 2321
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000912 RID: 2322
		private GameObject prevGameObject;
	}
}

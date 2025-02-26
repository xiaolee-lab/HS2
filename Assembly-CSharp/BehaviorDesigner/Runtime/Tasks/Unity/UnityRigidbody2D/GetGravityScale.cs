using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000231 RID: 561
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the gravity scale of the Rigidbody2D. Returns Success.")]
	public class GetGravityScale : Action
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A4EC File Offset: 0x000288EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A52F File Offset: 0x0002892F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.gravityScale;
			return TaskStatus.Success;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A55B File Offset: 0x0002895B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400091B RID: 2331
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400091C RID: 2332
		[Tooltip("The gravity scale of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400091D RID: 2333
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400091E RID: 2334
		private GameObject prevGameObject;
	}
}

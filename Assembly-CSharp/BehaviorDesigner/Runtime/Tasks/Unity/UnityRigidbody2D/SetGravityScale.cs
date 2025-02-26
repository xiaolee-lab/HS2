using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023E RID: 574
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the gravity scale of the Rigidbody2D. Returns Success.")]
	public class SetGravityScale : Action
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0002AC18 File Offset: 0x00029018
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002AC5B File Offset: 0x0002905B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.gravityScale = this.gravityScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002AC87 File Offset: 0x00029087
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.gravityScale = 0f;
		}

		// Token: 0x0400094D RID: 2381
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400094E RID: 2382
		[Tooltip("The gravity scale of the Rigidbody2D")]
		public SharedFloat gravityScale;

		// Token: 0x0400094F RID: 2383
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000950 RID: 2384
		private GameObject prevGameObject;
	}
}

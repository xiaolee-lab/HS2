using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023C RID: 572
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the angular velocity of the Rigidbody2D. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0002AAF8 File Offset: 0x00028EF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002AB3B File Offset: 0x00028F3B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.angularVelocity = this.angularVelocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002AB67 File Offset: 0x00028F67
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = 0f;
		}

		// Token: 0x04000945 RID: 2373
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000946 RID: 2374
		[Tooltip("The angular velocity of the Rigidbody2D")]
		public SharedFloat angularVelocity;

		// Token: 0x04000947 RID: 2375
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000948 RID: 2376
		private GameObject prevGameObject;
	}
}

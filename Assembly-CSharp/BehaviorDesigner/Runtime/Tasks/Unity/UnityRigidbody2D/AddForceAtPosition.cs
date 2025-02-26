using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200022C RID: 556
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a force at the specified position to the Rigidbody2D. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0002A200 File Offset: 0x00028600
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002A243 File Offset: 0x00028643
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddForceAtPosition(this.force.Value, this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002A27A File Offset: 0x0002867A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
			this.position = Vector2.zero;
		}

		// Token: 0x04000906 RID: 2310
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000907 RID: 2311
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04000908 RID: 2312
		[Tooltip("The position of the force")]
		public SharedVector2 position;

		// Token: 0x04000909 RID: 2313
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400090A RID: 2314
		private GameObject prevGameObject;
	}
}

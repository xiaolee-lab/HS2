using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200022B RID: 555
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a force to the Rigidbody2D. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0002A170 File Offset: 0x00028570
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002A1B3 File Offset: 0x000285B3
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddForce(this.force.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A1DF File Offset: 0x000285DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
		}

		// Token: 0x04000902 RID: 2306
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000903 RID: 2307
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04000904 RID: 2308
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000905 RID: 2309
		private GameObject prevGameObject;
	}
}

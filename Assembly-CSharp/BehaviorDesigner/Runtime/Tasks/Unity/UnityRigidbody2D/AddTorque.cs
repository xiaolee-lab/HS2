using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200022D RID: 557
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a torque to the Rigidbody2D. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0002A2AC File Offset: 0x000286AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002A2EF File Offset: 0x000286EF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddTorque(this.torque.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002A31B File Offset: 0x0002871B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = 0f;
		}

		// Token: 0x0400090B RID: 2315
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400090C RID: 2316
		[Tooltip("The amount of torque to apply")]
		public SharedFloat torque;

		// Token: 0x0400090D RID: 2317
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400090E RID: 2318
		private GameObject prevGameObject;
	}
}

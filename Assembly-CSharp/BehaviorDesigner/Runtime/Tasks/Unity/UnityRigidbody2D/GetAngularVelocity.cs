using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200022F RID: 559
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the angular velocity of the Rigidbody2D. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A3CC File Offset: 0x000287CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A40F File Offset: 0x0002880F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.angularVelocity;
			return TaskStatus.Success;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A43B File Offset: 0x0002883B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000913 RID: 2323
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000914 RID: 2324
		[Tooltip("The angular velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000915 RID: 2325
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000916 RID: 2326
		private GameObject prevGameObject;
	}
}

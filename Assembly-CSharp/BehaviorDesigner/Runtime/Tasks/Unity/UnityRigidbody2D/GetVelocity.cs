using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000236 RID: 566
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the velocity of the Rigidbody2D. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x0002A7B8 File Offset: 0x00028BB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002A7FB File Offset: 0x00028BFB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002A827 File Offset: 0x00028C27
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x0400092F RID: 2351
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000930 RID: 2352
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000931 RID: 2353
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000932 RID: 2354
		private GameObject prevGameObject;
	}
}

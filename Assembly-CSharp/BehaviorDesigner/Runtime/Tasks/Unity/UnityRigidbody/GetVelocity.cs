using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000217 RID: 535
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the velocity of the Rigidbody. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x000296A8 File Offset: 0x00027AA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000296EB File Offset: 0x00027AEB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00029717 File Offset: 0x00027B17
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040008B7 RID: 2231
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B8 RID: 2232
		[Tooltip("The velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040008B9 RID: 2233
		private Rigidbody rigidbody;

		// Token: 0x040008BA RID: 2234
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020C RID: 524
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x00029074 File Offset: 0x00027474
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000290B7 File Offset: 0x000274B7
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.AddTorque(this.torque.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000290E9 File Offset: 0x000274E9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x0400088A RID: 2186
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400088B RID: 2187
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x0400088C RID: 2188
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x0400088D RID: 2189
		private Rigidbody rigidbody;

		// Token: 0x0400088E RID: 2190
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020B RID: 523
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeTorque : Action
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x00028FE8 File Offset: 0x000273E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002902B File Offset: 0x0002742B
		public override TaskStatus OnUpdate()
		{
			this.rigidbody.AddRelativeTorque(this.torque.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002904A File Offset: 0x0002744A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000885 RID: 2181
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000886 RID: 2182
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04000887 RID: 2183
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04000888 RID: 2184
		private Rigidbody rigidbody;

		// Token: 0x04000889 RID: 2185
		private GameObject prevGameObject;
	}
}

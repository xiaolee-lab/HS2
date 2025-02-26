using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000208 RID: 520
	[RequiredComponent(typeof(Rigidbody))]
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x00028DE8 File Offset: 0x000271E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00028E2B File Offset: 0x0002722B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.AddForce(this.force.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00028E5D File Offset: 0x0002725D
		public override void OnReset()
		{
			this.targetGameObject = null;
			if (this.force != null)
			{
				this.force.Value = Vector3.zero;
			}
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000875 RID: 2165
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000876 RID: 2166
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04000877 RID: 2167
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000878 RID: 2168
		private Rigidbody rigidbody;

		// Token: 0x04000879 RID: 2169
		private GameObject prevGameObject;
	}
}

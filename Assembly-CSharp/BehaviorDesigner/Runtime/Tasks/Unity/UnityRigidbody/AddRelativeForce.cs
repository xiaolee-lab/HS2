using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020A RID: 522
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeForce : Action
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00028F48 File Offset: 0x00027348
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028F8B File Offset: 0x0002738B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.AddRelativeForce(this.force.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00028FBD File Offset: 0x000273BD
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000880 RID: 2176
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000881 RID: 2177
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04000882 RID: 2178
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000883 RID: 2179
		private Rigidbody rigidbody;

		// Token: 0x04000884 RID: 2180
		private GameObject prevGameObject;
	}
}

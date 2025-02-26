using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021B RID: 539
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Rotates the Rigidbody to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x000298C8 File Offset: 0x00027CC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002990B File Offset: 0x00027D0B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.MoveRotation(this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00029937 File Offset: 0x00027D37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x040008C5 RID: 2245
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008C6 RID: 2246
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x040008C7 RID: 2247
		private Rigidbody rigidbody;

		// Token: 0x040008C8 RID: 2248
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021D RID: 541
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the angular velocity of the Rigidbody. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x000299E8 File Offset: 0x00027DE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00029A2B File Offset: 0x00027E2B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.angularVelocity = this.angularVelocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00029A57 File Offset: 0x00027E57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = Vector3.zero;
		}

		// Token: 0x040008CD RID: 2253
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008CE RID: 2254
		[Tooltip("The angular velocity of the Rigidbody")]
		public SharedVector3 angularVelocity;

		// Token: 0x040008CF RID: 2255
		private Rigidbody rigidbody;

		// Token: 0x040008D0 RID: 2256
		private GameObject prevGameObject;
	}
}

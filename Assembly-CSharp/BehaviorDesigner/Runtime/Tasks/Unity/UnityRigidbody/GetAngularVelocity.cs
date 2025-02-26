using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020E RID: 526
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the angular velocity of the Rigidbody. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x000291A4 File Offset: 0x000275A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000291E7 File Offset: 0x000275E7
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.angularVelocity;
			return TaskStatus.Success;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00029213 File Offset: 0x00027613
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000893 RID: 2195
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000894 RID: 2196
		[Tooltip("The angular velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000895 RID: 2197
		private Rigidbody rigidbody;

		// Token: 0x04000896 RID: 2198
		private GameObject prevGameObject;
	}
}

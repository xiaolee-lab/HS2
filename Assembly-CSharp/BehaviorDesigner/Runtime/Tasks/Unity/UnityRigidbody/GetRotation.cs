using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000215 RID: 533
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002958C File Offset: 0x0002798C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000295CF File Offset: 0x000279CF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000295FB File Offset: 0x000279FB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x040008AF RID: 2223
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B0 RID: 2224
		[Tooltip("The rotation of the Rigidbody")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x040008B1 RID: 2225
		private Rigidbody rigidbody;

		// Token: 0x040008B2 RID: 2226
		private GameObject prevGameObject;
	}
}

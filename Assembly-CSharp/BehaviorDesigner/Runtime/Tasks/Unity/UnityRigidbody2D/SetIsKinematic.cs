using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023F RID: 575
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x0002ACA8 File Offset: 0x000290A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002ACEB File Offset: 0x000290EB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.isKinematic = this.isKinematic.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002AD17 File Offset: 0x00029117
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04000951 RID: 2385
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000952 RID: 2386
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		public SharedBool isKinematic;

		// Token: 0x04000953 RID: 2387
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000954 RID: 2388
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000237 RID: 567
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A848 File Offset: 0x00028C48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002A88B File Offset: 0x00028C8B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.rigidbody2D.isKinematic) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002A8B7 File Offset: 0x00028CB7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000933 RID: 2355
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000934 RID: 2356
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000935 RID: 2357
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000232 RID: 562
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0002A57C File Offset: 0x0002897C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A5BF File Offset: 0x000289BF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.isKinematic;
			return TaskStatus.Success;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A5EB File Offset: 0x000289EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400091F RID: 2335
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000920 RID: 2336
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000921 RID: 2337
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000922 RID: 2338
		private GameObject prevGameObject;
	}
}

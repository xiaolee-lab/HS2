using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000235 RID: 565
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the rotation of the Rigidbody2D. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002A728 File Offset: 0x00028B28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002A76B File Offset: 0x00028B6B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002A797 File Offset: 0x00028B97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400092B RID: 2347
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400092C RID: 2348
		[Tooltip("The rotation of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400092D RID: 2349
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400092E RID: 2350
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023A RID: 570
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Rotates the Rigidbody2D to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0002A9D8 File Offset: 0x00028DD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002AA1B File Offset: 0x00028E1B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.MoveRotation(this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002AA47 File Offset: 0x00028E47
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = 0f;
		}

		// Token: 0x0400093D RID: 2365
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400093E RID: 2366
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedFloat rotation;

		// Token: 0x0400093F RID: 2367
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000940 RID: 2368
		private GameObject prevGameObject;
	}
}

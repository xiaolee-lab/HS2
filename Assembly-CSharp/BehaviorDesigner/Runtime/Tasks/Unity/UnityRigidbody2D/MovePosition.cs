using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000239 RID: 569
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Moves the Rigidbody2D to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0002A948 File Offset: 0x00028D48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002A98B File Offset: 0x00028D8B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.MovePosition(this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002A9B7 File Offset: 0x00028DB7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector2.zero;
		}

		// Token: 0x04000939 RID: 2361
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400093A RID: 2362
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector2 position;

		// Token: 0x0400093B RID: 2363
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400093C RID: 2364
		private GameObject prevGameObject;
	}
}

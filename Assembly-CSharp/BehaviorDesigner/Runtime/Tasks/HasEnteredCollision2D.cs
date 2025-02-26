using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E8 RID: 232
	[TaskDescription("Returns success when a 2D collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredCollision2D : Conditional
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0001EFF7 File Offset: 0x0001D3F7
		public override TaskStatus OnUpdate()
		{
			return (!this.enteredCollision) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001F00B File Offset: 0x0001D40B
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001F014 File Offset: 0x0001D414
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001F06E File Offset: 0x0001D46E
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.collidedGameObject = null;
		}

		// Token: 0x04000460 RID: 1120
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000461 RID: 1121
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04000462 RID: 1122
		private bool enteredCollision;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000EB RID: 235
	[TaskDescription("Returns success when a collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedCollision : Conditional
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001F1EF File Offset: 0x0001D5EF
		public override TaskStatus OnUpdate()
		{
			return (!this.exitedCollision) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001F203 File Offset: 0x0001D603
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001F20C File Offset: 0x0001D60C
		public override void OnCollisionExit(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001F266 File Offset: 0x0001D666
		public override void OnReset()
		{
			this.collidedGameObject = null;
		}

		// Token: 0x04000469 RID: 1129
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = string.Empty;

		// Token: 0x0400046A RID: 1130
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x0400046B RID: 1131
		private bool exitedCollision;
	}
}

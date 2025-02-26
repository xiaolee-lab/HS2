using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000EC RID: 236
	[TaskDescription("Returns success when a 2D collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedCollision2D : Conditional
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0001F287 File Offset: 0x0001D687
		public override TaskStatus OnUpdate()
		{
			return (!this.exitedCollision) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001F29B File Offset: 0x0001D69B
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001F2A4 File Offset: 0x0001D6A4
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001F2FE File Offset: 0x0001D6FE
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.collidedGameObject = null;
		}

		// Token: 0x0400046C RID: 1132
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = string.Empty;

		// Token: 0x0400046D RID: 1133
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x0400046E RID: 1134
		private bool exitedCollision;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E7 RID: 231
	[TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredCollision : Conditional
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0001EF4D File Offset: 0x0001D34D
		public override TaskStatus OnUpdate()
		{
			return (!this.enteredCollision) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001EF61 File Offset: 0x0001D361
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001EF6C File Offset: 0x0001D36C
		public override void OnCollisionEnter(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001EFC6 File Offset: 0x0001D3C6
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.collidedGameObject = null;
		}

		// Token: 0x0400045D RID: 1117
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = string.Empty;

		// Token: 0x0400045E RID: 1118
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x0400045F RID: 1119
		private bool enteredCollision;
	}
}

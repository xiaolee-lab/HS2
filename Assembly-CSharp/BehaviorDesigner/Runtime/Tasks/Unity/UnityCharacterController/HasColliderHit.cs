using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000161 RID: 353
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Returns Success if the collider hit another object, otherwise Failure.")]
	public class HasColliderHit : Conditional
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x00023B88 File Offset: 0x00021F88
		public override TaskStatus OnUpdate()
		{
			return (!this.enteredCollision) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00023B9C File Offset: 0x00021F9C
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00023BA8 File Offset: 0x00021FA8
		public override void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(hit.gameObject.tag))
			{
				this.collidedGameObject.Value = hit.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00023C02 File Offset: 0x00022002
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = string.Empty;
			this.collidedGameObject = null;
		}

		// Token: 0x04000646 RID: 1606
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000647 RID: 1607
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000648 RID: 1608
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04000649 RID: 1609
		private bool enteredCollision;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000162 RID: 354
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Returns Success if the character is grounded, otherwise Failure.")]
	public class IsGrounded : Conditional
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00023C2C File Offset: 0x0002202C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00023C6F File Offset: 0x0002206F
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.characterController.isGrounded) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00023C9B File Offset: 0x0002209B
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400064A RID: 1610
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400064B RID: 1611
		private CharacterController characterController;

		// Token: 0x0400064C RID: 1612
		private GameObject prevGameObject;
	}
}

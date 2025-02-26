using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200015F RID: 351
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the step offset of the CharacterController. Returns Success.")]
	public class GetStepOffset : Action
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x00023A58 File Offset: 0x00021E58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00023A9B File Offset: 0x00021E9B
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.stepOffset;
			return TaskStatus.Success;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00023AC7 File Offset: 0x00021EC7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400063E RID: 1598
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400063F RID: 1599
		[Tooltip("The step offset of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000640 RID: 1600
		private CharacterController characterController;

		// Token: 0x04000641 RID: 1601
		private GameObject prevGameObject;
	}
}

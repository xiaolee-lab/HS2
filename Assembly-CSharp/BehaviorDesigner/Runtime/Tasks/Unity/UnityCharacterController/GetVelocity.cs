using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000160 RID: 352
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the velocity of the CharacterController. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00023AE8 File Offset: 0x00021EE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00023B2B File Offset: 0x00021F2B
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00023B57 File Offset: 0x00021F57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000642 RID: 1602
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000643 RID: 1603
		[Tooltip("The velocity of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000644 RID: 1604
		private CharacterController characterController;

		// Token: 0x04000645 RID: 1605
		private GameObject prevGameObject;
	}
}

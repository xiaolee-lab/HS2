using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200015C RID: 348
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the height of the CharacterController. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x000238A8 File Offset: 0x00021CA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000238EB File Offset: 0x00021CEB
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.height;
			return TaskStatus.Success;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00023917 File Offset: 0x00021D17
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000632 RID: 1586
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000633 RID: 1587
		[Tooltip("The height of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000634 RID: 1588
		private CharacterController characterController;

		// Token: 0x04000635 RID: 1589
		private GameObject prevGameObject;
	}
}

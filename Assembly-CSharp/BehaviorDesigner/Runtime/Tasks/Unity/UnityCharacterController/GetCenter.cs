using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200015B RID: 347
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the center of the CharacterController. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00023818 File Offset: 0x00021C18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0002385B File Offset: 0x00021C5B
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.center;
			return TaskStatus.Success;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00023887 File Offset: 0x00021C87
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400062E RID: 1582
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400062F RID: 1583
		[Tooltip("The center of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000630 RID: 1584
		private CharacterController characterController;

		// Token: 0x04000631 RID: 1585
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200015E RID: 350
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the slope limit of the CharacterController. Returns Success.")]
	public class GetSlopeLimit : Action
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x000239C8 File Offset: 0x00021DC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00023A0B File Offset: 0x00021E0B
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.slopeLimit;
			return TaskStatus.Success;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023A37 File Offset: 0x00021E37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400063A RID: 1594
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400063B RID: 1595
		[Tooltip("The slope limit of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400063C RID: 1596
		private CharacterController characterController;

		// Token: 0x0400063D RID: 1597
		private GameObject prevGameObject;
	}
}

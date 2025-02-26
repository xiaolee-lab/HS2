using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000165 RID: 357
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the height of the CharacterController. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00023DD0 File Offset: 0x000221D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00023E13 File Offset: 0x00022213
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.height = this.height.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00023E3F File Offset: 0x0002223F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.height = 0f;
		}

		// Token: 0x04000655 RID: 1621
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000656 RID: 1622
		[Tooltip("The height of the CharacterController")]
		public SharedFloat height;

		// Token: 0x04000657 RID: 1623
		private CharacterController characterController;

		// Token: 0x04000658 RID: 1624
		private GameObject prevGameObject;
	}
}

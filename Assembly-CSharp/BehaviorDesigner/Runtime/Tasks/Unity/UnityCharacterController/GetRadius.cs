using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200015D RID: 349
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the radius of the CharacterController. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x00023938 File Offset: 0x00021D38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0002397B File Offset: 0x00021D7B
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000239A7 File Offset: 0x00021DA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000636 RID: 1590
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000637 RID: 1591
		[Tooltip("The radius of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000638 RID: 1592
		private CharacterController characterController;

		// Token: 0x04000639 RID: 1593
		private GameObject prevGameObject;
	}
}

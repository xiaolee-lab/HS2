using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000166 RID: 358
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the radius of the CharacterController. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x00023E60 File Offset: 0x00022260
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00023EA3 File Offset: 0x000222A3
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00023ECF File Offset: 0x000222CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04000659 RID: 1625
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400065A RID: 1626
		[Tooltip("The radius of the CharacterController")]
		public SharedFloat radius;

		// Token: 0x0400065B RID: 1627
		private CharacterController characterController;

		// Token: 0x0400065C RID: 1628
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000168 RID: 360
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the step offset of the CharacterController. Returns Success.")]
	public class SetStepOffset : Action
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00023F80 File Offset: 0x00022380
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00023FC3 File Offset: 0x000223C3
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.stepOffset = this.stepOffset.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00023FEF File Offset: 0x000223EF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stepOffset = 0f;
		}

		// Token: 0x04000661 RID: 1633
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000662 RID: 1634
		[Tooltip("The step offset of the CharacterController")]
		public SharedFloat stepOffset;

		// Token: 0x04000663 RID: 1635
		private CharacterController characterController;

		// Token: 0x04000664 RID: 1636
		private GameObject prevGameObject;
	}
}

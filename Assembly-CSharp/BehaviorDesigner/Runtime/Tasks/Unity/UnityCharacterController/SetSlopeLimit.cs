using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000167 RID: 359
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the slope limit of the CharacterController. Returns Success.")]
	public class SetSlopeLimit : Action
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00023EF0 File Offset: 0x000222F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00023F33 File Offset: 0x00022333
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.slopeLimit = this.slopeLimit.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00023F5F File Offset: 0x0002235F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.slopeLimit = 0f;
		}

		// Token: 0x0400065D RID: 1629
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400065E RID: 1630
		[Tooltip("The slope limit of the CharacterController")]
		public SharedFloat slopeLimit;

		// Token: 0x0400065F RID: 1631
		private CharacterController characterController;

		// Token: 0x04000660 RID: 1632
		private GameObject prevGameObject;
	}
}

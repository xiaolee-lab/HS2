using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000164 RID: 356
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the center of the CharacterController. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00023D40 File Offset: 0x00022140
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00023D83 File Offset: 0x00022183
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00023DAF File Offset: 0x000221AF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04000651 RID: 1617
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000652 RID: 1618
		[Tooltip("The center of the CharacterController")]
		public SharedVector3 center;

		// Token: 0x04000653 RID: 1619
		private CharacterController characterController;

		// Token: 0x04000654 RID: 1620
		private GameObject prevGameObject;
	}
}

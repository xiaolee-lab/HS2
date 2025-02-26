using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000169 RID: 361
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Moves the character with speed. Returns Success.")]
	public class SimpleMove : Action
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00024010 File Offset: 0x00022410
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00024053 File Offset: 0x00022453
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.SimpleMove(this.speed.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00024080 File Offset: 0x00022480
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = Vector3.zero;
		}

		// Token: 0x04000665 RID: 1637
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000666 RID: 1638
		[Tooltip("The speed of the movement")]
		public SharedVector3 speed;

		// Token: 0x04000667 RID: 1639
		private CharacterController characterController;

		// Token: 0x04000668 RID: 1640
		private GameObject prevGameObject;
	}
}

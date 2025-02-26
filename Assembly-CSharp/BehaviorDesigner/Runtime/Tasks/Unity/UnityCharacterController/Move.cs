using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000163 RID: 355
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("A more complex move function taking absolute movement deltas. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00023CAC File Offset: 0x000220AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00023CEF File Offset: 0x000220EF
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				return TaskStatus.Failure;
			}
			this.characterController.Move(this.motion.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00023D1C File Offset: 0x0002211C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.motion = Vector3.zero;
		}

		// Token: 0x0400064D RID: 1613
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400064E RID: 1614
		[Tooltip("The amount to move")]
		public SharedVector3 motion;

		// Token: 0x0400064F RID: 1615
		private CharacterController characterController;

		// Token: 0x04000650 RID: 1616
		private GameObject prevGameObject;
	}
}

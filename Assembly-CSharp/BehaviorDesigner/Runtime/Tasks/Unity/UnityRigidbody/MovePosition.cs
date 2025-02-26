using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021A RID: 538
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Moves the Rigidbody to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x00029838 File Offset: 0x00027C38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002987B File Offset: 0x00027C7B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.MovePosition(this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000298A7 File Offset: 0x00027CA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x040008C1 RID: 2241
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008C2 RID: 2242
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x040008C3 RID: 2243
		private Rigidbody rigidbody;

		// Token: 0x040008C4 RID: 2244
		private GameObject prevGameObject;
	}
}

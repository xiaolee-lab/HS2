using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023D RID: 573
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the drag of the Rigidbody2D. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002AB88 File Offset: 0x00028F88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002ABCB File Offset: 0x00028FCB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.drag = this.drag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002ABF7 File Offset: 0x00028FF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04000949 RID: 2377
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400094A RID: 2378
		[Tooltip("The drag of the Rigidbody2D")]
		public SharedFloat drag;

		// Token: 0x0400094B RID: 2379
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400094C RID: 2380
		private GameObject prevGameObject;
	}
}

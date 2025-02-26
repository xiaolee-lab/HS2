using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000242 RID: 578
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x0002AE54 File Offset: 0x00029254
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002AE97 File Offset: 0x00029297
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.Sleep();
			return TaskStatus.Success;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002AEB8 File Offset: 0x000292B8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400095D RID: 2397
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400095E RID: 2398
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400095F RID: 2399
		private GameObject prevGameObject;
	}
}

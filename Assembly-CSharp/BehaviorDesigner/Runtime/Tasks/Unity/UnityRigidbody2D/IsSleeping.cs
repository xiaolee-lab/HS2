using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000238 RID: 568
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0002A8C8 File Offset: 0x00028CC8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002A90B File Offset: 0x00028D0B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.rigidbody2D.IsSleeping()) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002A937 File Offset: 0x00028D37
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000936 RID: 2358
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000937 RID: 2359
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000938 RID: 2360
		private GameObject prevGameObject;
	}
}

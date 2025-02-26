using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000229 RID: 553
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is using gravity, otherwise Failure.")]
	public class UseGravity : Conditional
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x0002A078 File Offset: 0x00028478
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002A0BB File Offset: 0x000284BB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.rigidbody.useGravity) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002A0E7 File Offset: 0x000284E7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008FC RID: 2300
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008FD RID: 2301
		private Rigidbody rigidbody;

		// Token: 0x040008FE RID: 2302
		private GameObject prevGameObject;
	}
}

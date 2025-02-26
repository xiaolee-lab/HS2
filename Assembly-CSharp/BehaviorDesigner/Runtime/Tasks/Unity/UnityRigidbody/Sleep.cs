using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000228 RID: 552
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0002A000 File Offset: 0x00028400
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002A043 File Offset: 0x00028443
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.Sleep();
			return TaskStatus.Success;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002A064 File Offset: 0x00028464
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008F9 RID: 2297
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008FA RID: 2298
		private Rigidbody rigidbody;

		// Token: 0x040008FB RID: 2299
		private GameObject prevGameObject;
	}
}

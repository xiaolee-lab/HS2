using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200022A RID: 554
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x0002A0F8 File Offset: 0x000284F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002A13B File Offset: 0x0002853B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.WakeUp();
			return TaskStatus.Success;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002A15C File Offset: 0x0002855C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008FF RID: 2303
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000900 RID: 2304
		private Rigidbody rigidbody;

		// Token: 0x04000901 RID: 2305
		private GameObject prevGameObject;
	}
}

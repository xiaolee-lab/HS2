using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000243 RID: 579
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0002AECC File Offset: 0x000292CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002AF0F File Offset: 0x0002930F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.WakeUp();
			return TaskStatus.Success;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002AF30 File Offset: 0x00029330
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000960 RID: 2400
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000961 RID: 2401
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000962 RID: 2402
		private GameObject prevGameObject;
	}
}

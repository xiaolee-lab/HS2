using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000227 RID: 551
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the velocity of the Rigidbody. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x00029F70 File Offset: 0x00028370
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00029FB3 File Offset: 0x000283B3
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.velocity = this.velocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00029FDF File Offset: 0x000283DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector3.zero;
		}

		// Token: 0x040008F5 RID: 2293
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008F6 RID: 2294
		[Tooltip("The velocity of the Rigidbody")]
		public SharedVector3 velocity;

		// Token: 0x040008F7 RID: 2295
		private Rigidbody rigidbody;

		// Token: 0x040008F8 RID: 2296
		private GameObject prevGameObject;
	}
}

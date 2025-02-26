using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000241 RID: 577
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0002ADC4 File Offset: 0x000291C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002AE07 File Offset: 0x00029207
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.velocity = this.velocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002AE33 File Offset: 0x00029233
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector2.zero;
		}

		// Token: 0x04000959 RID: 2393
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400095A RID: 2394
		[Tooltip("The velocity of the Rigidbody2D")]
		public SharedVector2 velocity;

		// Token: 0x0400095B RID: 2395
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400095C RID: 2396
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000222 RID: 546
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00029CA8 File Offset: 0x000280A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00029CEB File Offset: 0x000280EB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.isKinematic = this.isKinematic.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00029D17 File Offset: 0x00028117
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x040008E1 RID: 2273
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008E2 RID: 2274
		[Tooltip("The is kinematic value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x040008E3 RID: 2275
		private Rigidbody rigidbody;

		// Token: 0x040008E4 RID: 2276
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021F RID: 543
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the constraints of the Rigidbody. Returns Success.")]
	public class SetConstraints : Action
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00029B08 File Offset: 0x00027F08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029B4B File Offset: 0x00027F4B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.constraints = this.constraints;
			return TaskStatus.Success;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00029B72 File Offset: 0x00027F72
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.constraints = RigidbodyConstraints.None;
		}

		// Token: 0x040008D5 RID: 2261
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008D6 RID: 2262
		[Tooltip("The constraints of the Rigidbody")]
		public RigidbodyConstraints constraints;

		// Token: 0x040008D7 RID: 2263
		private Rigidbody rigidbody;

		// Token: 0x040008D8 RID: 2264
		private GameObject prevGameObject;
	}
}

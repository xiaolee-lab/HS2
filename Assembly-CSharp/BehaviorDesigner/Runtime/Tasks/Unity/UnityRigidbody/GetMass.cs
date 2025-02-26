using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000213 RID: 531
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the mass of the Rigidbody. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0002946C File Offset: 0x0002786C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000294AF File Offset: 0x000278AF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.mass;
			return TaskStatus.Success;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000294DB File Offset: 0x000278DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040008A7 RID: 2215
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A8 RID: 2216
		[Tooltip("The mass of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040008A9 RID: 2217
		private Rigidbody rigidbody;

		// Token: 0x040008AA RID: 2218
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000214 RID: 532
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the position of the Rigidbody. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x000294FC File Offset: 0x000278FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002953F File Offset: 0x0002793F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.position;
			return TaskStatus.Success;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002956B File Offset: 0x0002796B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040008AB RID: 2219
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008AC RID: 2220
		[Tooltip("The position of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040008AD RID: 2221
		private Rigidbody rigidbody;

		// Token: 0x040008AE RID: 2222
		private GameObject prevGameObject;
	}
}

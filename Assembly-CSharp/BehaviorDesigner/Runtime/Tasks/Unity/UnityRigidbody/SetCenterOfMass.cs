using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021E RID: 542
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the center of mass of the Rigidbody. Returns Success.")]
	public class SetCenterOfMass : Action
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00029A78 File Offset: 0x00027E78
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00029ABB File Offset: 0x00027EBB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.centerOfMass = this.centerOfMass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00029AE7 File Offset: 0x00027EE7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.centerOfMass = Vector3.zero;
		}

		// Token: 0x040008D1 RID: 2257
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008D2 RID: 2258
		[Tooltip("The center of mass of the Rigidbody")]
		public SharedVector3 centerOfMass;

		// Token: 0x040008D3 RID: 2259
		private Rigidbody rigidbody;

		// Token: 0x040008D4 RID: 2260
		private GameObject prevGameObject;
	}
}

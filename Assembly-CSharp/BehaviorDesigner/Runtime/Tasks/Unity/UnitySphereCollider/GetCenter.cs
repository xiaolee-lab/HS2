using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x02000268 RID: 616
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Stores the center of the SphereCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002BDB4 File Offset: 0x0002A1B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002BDF7 File Offset: 0x0002A1F7
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.sphereCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002BE23 File Offset: 0x0002A223
		public override void OnReset()
		{
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040009AC RID: 2476
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009AD RID: 2477
		[Tooltip("The center of the SphereCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040009AE RID: 2478
		private SphereCollider sphereCollider;

		// Token: 0x040009AF RID: 2479
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x02000269 RID: 617
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Stores the radius of the SphereCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002BE40 File Offset: 0x0002A240
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002BE83 File Offset: 0x0002A283
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.sphereCollider.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002BEAF File Offset: 0x0002A2AF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040009B0 RID: 2480
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009B1 RID: 2481
		[Tooltip("The radius of the SphereCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040009B2 RID: 2482
		private SphereCollider sphereCollider;

		// Token: 0x040009B3 RID: 2483
		private GameObject prevGameObject;
	}
}

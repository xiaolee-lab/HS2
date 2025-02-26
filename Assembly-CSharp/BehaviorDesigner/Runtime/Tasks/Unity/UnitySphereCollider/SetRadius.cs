using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x0200026B RID: 619
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Sets the radius of the SphereCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002BF60 File Offset: 0x0002A360
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002BFA3 File Offset: 0x0002A3A3
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.sphereCollider.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002BFCF File Offset: 0x0002A3CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x040009B8 RID: 2488
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009B9 RID: 2489
		[Tooltip("The radius of the SphereCollider")]
		public SharedFloat radius;

		// Token: 0x040009BA RID: 2490
		private SphereCollider sphereCollider;

		// Token: 0x040009BB RID: 2491
		private GameObject prevGameObject;
	}
}

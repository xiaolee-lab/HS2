using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x0200026A RID: 618
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Sets the center of the SphereCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002BED0 File Offset: 0x0002A2D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002BF13 File Offset: 0x0002A313
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.sphereCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002BF3F File Offset: 0x0002A33F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x040009B4 RID: 2484
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009B5 RID: 2485
		[Tooltip("The center of the SphereCollider")]
		public SharedVector3 center;

		// Token: 0x040009B6 RID: 2486
		private SphereCollider sphereCollider;

		// Token: 0x040009B7 RID: 2487
		private GameObject prevGameObject;
	}
}

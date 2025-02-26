using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x0200014F RID: 335
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Sets the center of the BoxCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x00023160 File Offset: 0x00021560
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000231A3 File Offset: 0x000215A3
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.boxCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000231CF File Offset: 0x000215CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x040005FE RID: 1534
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005FF RID: 1535
		[Tooltip("The center of the BoxCollider")]
		public SharedVector3 center;

		// Token: 0x04000600 RID: 1536
		private BoxCollider boxCollider;

		// Token: 0x04000601 RID: 1537
		private GameObject prevGameObject;
	}
}

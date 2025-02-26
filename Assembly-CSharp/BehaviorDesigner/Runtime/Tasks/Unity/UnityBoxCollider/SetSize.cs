using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x02000150 RID: 336
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Sets the size of the BoxCollider. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x000231F0 File Offset: 0x000215F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00023233 File Offset: 0x00021633
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.boxCollider.size = this.size.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0002325F File Offset: 0x0002165F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector3.zero;
		}

		// Token: 0x04000602 RID: 1538
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000603 RID: 1539
		[Tooltip("The size of the BoxCollider")]
		public SharedVector3 size;

		// Token: 0x04000604 RID: 1540
		private BoxCollider boxCollider;

		// Token: 0x04000605 RID: 1541
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x0200016A RID: 362
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Stores the radius of the CircleCollider2D. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x000240A4 File Offset: 0x000224A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000240E7 File Offset: 0x000224E7
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.circleCollider2D.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00024113 File Offset: 0x00022513
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000669 RID: 1641
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400066A RID: 1642
		[Tooltip("The radius of the CircleCollider2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400066B RID: 1643
		private CircleCollider2D circleCollider2D;

		// Token: 0x0400066C RID: 1644
		private GameObject prevGameObject;
	}
}

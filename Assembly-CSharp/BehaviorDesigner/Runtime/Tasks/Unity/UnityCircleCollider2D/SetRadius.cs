using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x0200016B RID: 363
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Sets the radius of the CircleCollider2D. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00024134 File Offset: 0x00022534
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024177 File Offset: 0x00022577
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				return TaskStatus.Failure;
			}
			this.circleCollider2D.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000241A3 File Offset: 0x000225A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x0400066D RID: 1645
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400066E RID: 1646
		[Tooltip("The radius of the CircleCollider2D")]
		public SharedFloat radius;

		// Token: 0x0400066F RID: 1647
		private CircleCollider2D circleCollider2D;

		// Token: 0x04000670 RID: 1648
		private GameObject prevGameObject;
	}
}

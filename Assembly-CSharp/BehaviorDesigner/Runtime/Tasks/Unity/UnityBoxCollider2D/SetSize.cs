using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider2D
{
	// Token: 0x02000152 RID: 338
	[TaskCategory("Unity/BoxCollider2D")]
	[TaskDescription("Sets the size of the BoxCollider2D. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x00023310 File Offset: 0x00021710
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00023353 File Offset: 0x00021753
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				return TaskStatus.Failure;
			}
			this.boxCollider2D.size = this.size.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0002337F File Offset: 0x0002177F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector2.zero;
		}

		// Token: 0x0400060A RID: 1546
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400060B RID: 1547
		[Tooltip("The size of the BoxCollider2D")]
		public SharedVector2 size;

		// Token: 0x0400060C RID: 1548
		private BoxCollider2D boxCollider2D;

		// Token: 0x0400060D RID: 1549
		private GameObject prevGameObject;
	}
}

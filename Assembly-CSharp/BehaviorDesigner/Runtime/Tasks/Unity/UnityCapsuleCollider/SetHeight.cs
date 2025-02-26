using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000159 RID: 345
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the height of the CapsuleCollider. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x000236F8 File Offset: 0x00021AF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0002373B File Offset: 0x00021B3B
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.capsuleCollider.height = this.direction.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00023767 File Offset: 0x00021B67
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0f;
		}

		// Token: 0x04000626 RID: 1574
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000627 RID: 1575
		[Tooltip("The height of the CapsuleCollider")]
		public SharedFloat direction;

		// Token: 0x04000628 RID: 1576
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000629 RID: 1577
		private GameObject prevGameObject;
	}
}

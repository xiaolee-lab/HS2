using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000157 RID: 343
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the center of the CapsuleCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x000235DC File Offset: 0x000219DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002361F File Offset: 0x00021A1F
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.capsuleCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0002364B File Offset: 0x00021A4B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x0400061E RID: 1566
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400061F RID: 1567
		[Tooltip("The center of the CapsuleCollider")]
		public SharedVector3 center;

		// Token: 0x04000620 RID: 1568
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000621 RID: 1569
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200015A RID: 346
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the radius of the CapsuleCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x00023788 File Offset: 0x00021B88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000237CB File Offset: 0x00021BCB
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.capsuleCollider.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000237F7 File Offset: 0x00021BF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x0400062A RID: 1578
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400062B RID: 1579
		[Tooltip("The radius of the CapsuleCollider")]
		public SharedFloat radius;

		// Token: 0x0400062C RID: 1580
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400062D RID: 1581
		private GameObject prevGameObject;
	}
}

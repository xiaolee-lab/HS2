using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000158 RID: 344
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the direction of the CapsuleCollider. Returns Success.")]
	public class SetDirection : Action
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x0002366C File Offset: 0x00021A6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000236AF File Offset: 0x00021AAF
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.capsuleCollider.direction = this.direction.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000236DB File Offset: 0x00021ADB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0;
		}

		// Token: 0x04000622 RID: 1570
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000623 RID: 1571
		[Tooltip("The direction of the CapsuleCollider")]
		public SharedInt direction;

		// Token: 0x04000624 RID: 1572
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000625 RID: 1573
		private GameObject prevGameObject;
	}
}

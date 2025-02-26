using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000155 RID: 341
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Gets the height of the CapsuleCollider. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x000234BC File Offset: 0x000218BC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000234FF File Offset: 0x000218FF
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.height;
			return TaskStatus.Success;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002352B File Offset: 0x0002192B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000616 RID: 1558
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000617 RID: 1559
		[Tooltip("The height of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000618 RID: 1560
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000619 RID: 1561
		private GameObject prevGameObject;
	}
}

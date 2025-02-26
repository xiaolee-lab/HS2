using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000211 RID: 529
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the freeze rotation value of the Rigidbody. Returns Success.")]
	public class GetFreezeRotation : Action
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00029354 File Offset: 0x00027754
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00029397 File Offset: 0x00027797
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.freezeRotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000293C3 File Offset: 0x000277C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400089F RID: 2207
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A0 RID: 2208
		[Tooltip("The freeze rotation value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040008A1 RID: 2209
		private Rigidbody rigidbody;

		// Token: 0x040008A2 RID: 2210
		private GameObject prevGameObject;
	}
}

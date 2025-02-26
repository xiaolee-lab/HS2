using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000216 RID: 534
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the use gravity value of the Rigidbody. Returns Success.")]
	public class GetUseGravity : Action
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0002961C File Offset: 0x00027A1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002965F File Offset: 0x00027A5F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.useGravity;
			return TaskStatus.Success;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002968B File Offset: 0x00027A8B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040008B3 RID: 2227
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008B4 RID: 2228
		[Tooltip("The use gravity value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040008B5 RID: 2229
		private Rigidbody rigidbody;

		// Token: 0x040008B6 RID: 2230
		private GameObject prevGameObject;
	}
}

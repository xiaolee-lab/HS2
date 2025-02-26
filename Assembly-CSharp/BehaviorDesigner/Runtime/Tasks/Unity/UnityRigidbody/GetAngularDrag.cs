using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020D RID: 525
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the angular drag of the Rigidbody. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x00029114 File Offset: 0x00027514
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00029157 File Offset: 0x00027557
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.angularDrag;
			return TaskStatus.Success;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00029183 File Offset: 0x00027583
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400088F RID: 2191
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000890 RID: 2192
		[Tooltip("The angular drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000891 RID: 2193
		private Rigidbody rigidbody;

		// Token: 0x04000892 RID: 2194
		private GameObject prevGameObject;
	}
}

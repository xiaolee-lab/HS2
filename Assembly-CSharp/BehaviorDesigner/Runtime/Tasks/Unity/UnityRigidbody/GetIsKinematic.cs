using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000212 RID: 530
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x000293E0 File Offset: 0x000277E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029423 File Offset: 0x00027823
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.isKinematic;
			return TaskStatus.Success;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002944F File Offset: 0x0002784F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040008A3 RID: 2211
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008A4 RID: 2212
		[Tooltip("The is kinematic value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040008A5 RID: 2213
		private Rigidbody rigidbody;

		// Token: 0x040008A6 RID: 2214
		private GameObject prevGameObject;
	}
}

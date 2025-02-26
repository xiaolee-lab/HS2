using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000219 RID: 537
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x000297B8 File Offset: 0x00027BB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000297FB File Offset: 0x00027BFB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.rigidbody.IsSleeping()) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00029827 File Offset: 0x00027C27
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008BE RID: 2238
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008BF RID: 2239
		private Rigidbody rigidbody;

		// Token: 0x040008C0 RID: 2240
		private GameObject prevGameObject;
	}
}

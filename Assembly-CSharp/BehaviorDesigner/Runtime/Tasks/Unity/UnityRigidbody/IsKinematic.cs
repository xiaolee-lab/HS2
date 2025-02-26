using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000218 RID: 536
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00029738 File Offset: 0x00027B38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002977B File Offset: 0x00027B7B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.rigidbody.isKinematic) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000297A7 File Offset: 0x00027BA7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040008BB RID: 2235
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008BC RID: 2236
		private Rigidbody rigidbody;

		// Token: 0x040008BD RID: 2237
		private GameObject prevGameObject;
	}
}

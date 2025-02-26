using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000226 RID: 550
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the use gravity value of the Rigidbody. Returns Success.")]
	public class SetUseGravity : Action
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00029EE4 File Offset: 0x000282E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00029F27 File Offset: 0x00028327
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.useGravity = this.isKinematic.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00029F53 File Offset: 0x00028353
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x040008F1 RID: 2289
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008F2 RID: 2290
		[Tooltip("The use gravity value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x040008F3 RID: 2291
		private Rigidbody rigidbody;

		// Token: 0x040008F4 RID: 2292
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000225 RID: 549
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00029E54 File Offset: 0x00028254
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00029E97 File Offset: 0x00028297
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.rotation = this.rotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00029EC3 File Offset: 0x000282C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x040008ED RID: 2285
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008EE RID: 2286
		[Tooltip("The rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x040008EF RID: 2287
		private Rigidbody rigidbody;

		// Token: 0x040008F0 RID: 2288
		private GameObject prevGameObject;
	}
}

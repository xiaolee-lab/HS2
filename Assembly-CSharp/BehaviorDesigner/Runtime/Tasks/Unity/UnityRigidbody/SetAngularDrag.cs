using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200021C RID: 540
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the angular drag of the Rigidbody. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00029958 File Offset: 0x00027D58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002999B File Offset: 0x00027D9B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.angularDrag = this.angularDrag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000299C7 File Offset: 0x00027DC7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x040008C9 RID: 2249
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008CA RID: 2250
		[Tooltip("The angular drag of the Rigidbody")]
		public SharedFloat angularDrag;

		// Token: 0x040008CB RID: 2251
		private Rigidbody rigidbody;

		// Token: 0x040008CC RID: 2252
		private GameObject prevGameObject;
	}
}

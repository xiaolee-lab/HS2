using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200023B RID: 571
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the angular drag of the Rigidbody2D. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0002AA68 File Offset: 0x00028E68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002AAAB File Offset: 0x00028EAB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.angularDrag = this.angularDrag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002AAD7 File Offset: 0x00028ED7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04000941 RID: 2369
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000942 RID: 2370
		[Tooltip("The angular drag of the Rigidbody2D")]
		public SharedFloat angularDrag;

		// Token: 0x04000943 RID: 2371
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000944 RID: 2372
		private GameObject prevGameObject;
	}
}

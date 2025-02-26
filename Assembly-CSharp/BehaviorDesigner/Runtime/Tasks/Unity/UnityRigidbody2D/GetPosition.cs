using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000234 RID: 564
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the position of the Rigidbody2D. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A698 File Offset: 0x00028A98
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002A6DB File Offset: 0x00028ADB
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.position;
			return TaskStatus.Success;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A707 File Offset: 0x00028B07
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04000927 RID: 2343
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000928 RID: 2344
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000929 RID: 2345
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400092A RID: 2346
		private GameObject prevGameObject;
	}
}

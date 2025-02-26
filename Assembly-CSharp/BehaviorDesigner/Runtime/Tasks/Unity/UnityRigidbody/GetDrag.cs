using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000210 RID: 528
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the drag of the Rigidbody. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x000292C4 File Offset: 0x000276C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00029307 File Offset: 0x00027707
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.drag;
			return TaskStatus.Success;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00029333 File Offset: 0x00027733
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400089B RID: 2203
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400089C RID: 2204
		[Tooltip("The drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400089D RID: 2205
		private Rigidbody rigidbody;

		// Token: 0x0400089E RID: 2206
		private GameObject prevGameObject;
	}
}

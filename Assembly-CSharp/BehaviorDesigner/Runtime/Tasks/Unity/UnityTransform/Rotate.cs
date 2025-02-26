using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000291 RID: 657
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class Rotate : Action
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x0002D370 File Offset: 0x0002B770
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002D3B3 File Offset: 0x0002B7B3
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.Rotate(this.eulerAngles.Value, this.relativeTo);
			return TaskStatus.Success;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002D3E5 File Offset: 0x0002B7E5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
			this.relativeTo = Space.Self;
		}

		// Token: 0x04000A3B RID: 2619
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A3C RID: 2620
		[Tooltip("Amount to rotate")]
		public SharedVector3 eulerAngles;

		// Token: 0x04000A3D RID: 2621
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = Space.Self;

		// Token: 0x04000A3E RID: 2622
		private Transform targetTransform;

		// Token: 0x04000A3F RID: 2623
		private GameObject prevGameObject;
	}
}

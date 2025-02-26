using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028E RID: 654
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the up vector of the Transform. Returns Success.")]
	public class GetUpVector : Action
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x0002D158 File Offset: 0x0002B558
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002D19B File Offset: 0x0002B59B
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.up;
			return TaskStatus.Success;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002D1C7 File Offset: 0x0002B5C7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A2D RID: 2605
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A2E RID: 2606
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A2F RID: 2607
		private Transform targetTransform;

		// Token: 0x04000A30 RID: 2608
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028C RID: 652
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the right vector of the Transform. Returns Success.")]
	public class GetRightVector : Action
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0002D038 File Offset: 0x0002B438
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002D07B File Offset: 0x0002B47B
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.right;
			return TaskStatus.Success;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002D0A7 File Offset: 0x0002B4A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A25 RID: 2597
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A26 RID: 2598
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A27 RID: 2599
		private Transform targetTransform;

		// Token: 0x04000A28 RID: 2600
		private GameObject prevGameObject;
	}
}

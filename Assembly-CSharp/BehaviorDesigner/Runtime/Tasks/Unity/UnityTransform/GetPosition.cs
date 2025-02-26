using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028B RID: 651
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the position of the Transform. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0002CFA8 File Offset: 0x0002B3A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002CFEB File Offset: 0x0002B3EB
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.position;
			return TaskStatus.Success;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002D017 File Offset: 0x0002B417
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A21 RID: 2593
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A22 RID: 2594
		[Tooltip("Can the target GameObject be empty?")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A23 RID: 2595
		private Transform targetTransform;

		// Token: 0x04000A24 RID: 2596
		private GameObject prevGameObject;
	}
}

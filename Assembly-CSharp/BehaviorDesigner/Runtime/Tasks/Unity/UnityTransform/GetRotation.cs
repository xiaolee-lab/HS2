using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028D RID: 653
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the rotation of the Transform. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x0002D0C8 File Offset: 0x0002B4C8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002D10B File Offset: 0x0002B50B
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002D137 File Offset: 0x0002B537
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04000A29 RID: 2601
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A2A RID: 2602
		[Tooltip("The rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04000A2B RID: 2603
		private Transform targetTransform;

		// Token: 0x04000A2C RID: 2604
		private GameObject prevGameObject;
	}
}

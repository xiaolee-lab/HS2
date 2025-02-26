using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000285 RID: 645
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the forward vector of the Transform. Returns Success.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0002CC50 File Offset: 0x0002B050
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002CC93 File Offset: 0x0002B093
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.forward;
			return TaskStatus.Success;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002CCBF File Offset: 0x0002B0BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A09 RID: 2569
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A0A RID: 2570
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A0B RID: 2571
		private Transform targetTransform;

		// Token: 0x04000A0C RID: 2572
		private GameObject prevGameObject;
	}
}

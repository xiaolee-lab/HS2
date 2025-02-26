using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028A RID: 650
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the parent of the Transform. Returns Success.")]
	public class GetParent : Action
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0002CF20 File Offset: 0x0002B320
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CF63 File Offset: 0x0002B363
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.parent;
			return TaskStatus.Success;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002CF8F File Offset: 0x0002B38F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x04000A1D RID: 2589
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A1E RID: 2590
		[Tooltip("The parent of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04000A1F RID: 2591
		private Transform targetTransform;

		// Token: 0x04000A20 RID: 2592
		private GameObject prevGameObject;
	}
}

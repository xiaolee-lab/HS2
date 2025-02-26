using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000295 RID: 661
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local euler angles of the Transform. Returns Success.")]
	public class SetLocalEulerAngles : Action
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0002D604 File Offset: 0x0002BA04
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D647 File Offset: 0x0002BA47
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.localEulerAngles = this.localEulerAngles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D673 File Offset: 0x0002BA73
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localEulerAngles = Vector3.zero;
		}

		// Token: 0x04000A4E RID: 2638
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A4F RID: 2639
		[Tooltip("The local euler angles of the Transform")]
		public SharedVector3 localEulerAngles;

		// Token: 0x04000A50 RID: 2640
		private Transform targetTransform;

		// Token: 0x04000A51 RID: 2641
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000293 RID: 659
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the euler angles of the Transform. Returns Success.")]
	public class SetEulerAngles : Action
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0002D4E4 File Offset: 0x0002B8E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002D527 File Offset: 0x0002B927
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.eulerAngles = this.eulerAngles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002D553 File Offset: 0x0002B953
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
		}

		// Token: 0x04000A46 RID: 2630
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A47 RID: 2631
		[Tooltip("The euler angles of the Transform")]
		public SharedVector3 eulerAngles;

		// Token: 0x04000A48 RID: 2632
		private Transform targetTransform;

		// Token: 0x04000A49 RID: 2633
		private GameObject prevGameObject;
	}
}

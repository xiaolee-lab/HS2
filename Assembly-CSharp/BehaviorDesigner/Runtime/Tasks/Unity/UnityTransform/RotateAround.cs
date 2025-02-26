using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000292 RID: 658
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class RotateAround : Action
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002D410 File Offset: 0x0002B810
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002D454 File Offset: 0x0002B854
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.RotateAround(this.point.Value, this.axis.Value, this.angle.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002D4A1 File Offset: 0x0002B8A1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.point = Vector3.zero;
			this.axis = Vector3.zero;
			this.angle = 0f;
		}

		// Token: 0x04000A40 RID: 2624
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A41 RID: 2625
		[Tooltip("Point to rotate around")]
		public SharedVector3 point;

		// Token: 0x04000A42 RID: 2626
		[Tooltip("Axis to rotate around")]
		public SharedVector3 axis;

		// Token: 0x04000A43 RID: 2627
		[Tooltip("Amount to rotate")]
		public SharedFloat angle;

		// Token: 0x04000A44 RID: 2628
		private Transform targetTransform;

		// Token: 0x04000A45 RID: 2629
		private GameObject prevGameObject;
	}
}

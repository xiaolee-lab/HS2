using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200029E RID: 670
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Moves the transform in the direction and distance of translation. Returns Success.")]
	public class Translate : Action
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0002DB14 File Offset: 0x0002BF14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002DB57 File Offset: 0x0002BF57
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.Translate(this.translation.Value, this.relativeTo);
			return TaskStatus.Success;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002DB89 File Offset: 0x0002BF89
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.translation = Vector3.zero;
			this.relativeTo = Space.Self;
		}

		// Token: 0x04000A72 RID: 2674
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A73 RID: 2675
		[Tooltip("Move direction and distance")]
		public SharedVector3 translation;

		// Token: 0x04000A74 RID: 2676
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = Space.Self;

		// Token: 0x04000A75 RID: 2677
		private Transform targetTransform;

		// Token: 0x04000A76 RID: 2678
		private GameObject prevGameObject;
	}
}

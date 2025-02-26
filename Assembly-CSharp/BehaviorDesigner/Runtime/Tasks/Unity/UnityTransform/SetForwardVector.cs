using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000294 RID: 660
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the forward vector of the Transform. Returns Success.")]
	public class SetForwardVector : Action
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0002D574 File Offset: 0x0002B974
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002D5B7 File Offset: 0x0002B9B7
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.forward = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002D5E3 File Offset: 0x0002B9E3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000A4A RID: 2634
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A4B RID: 2635
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000A4C RID: 2636
		private Transform targetTransform;

		// Token: 0x04000A4D RID: 2637
		private GameObject prevGameObject;
	}
}

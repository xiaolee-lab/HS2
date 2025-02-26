using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000298 RID: 664
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local scale of the Transform. Returns Success.")]
	public class SetLocalScale : Action
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D7B4 File Offset: 0x0002BBB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D7F7 File Offset: 0x0002BBF7
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.localScale = this.localScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D823 File Offset: 0x0002BC23
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localScale = Vector3.zero;
		}

		// Token: 0x04000A5A RID: 2650
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A5B RID: 2651
		[Tooltip("The local scale of the Transform")]
		public SharedVector3 localScale;

		// Token: 0x04000A5C RID: 2652
		private Transform targetTransform;

		// Token: 0x04000A5D RID: 2653
		private GameObject prevGameObject;
	}
}
